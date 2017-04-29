using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using quZONE.Common.Messaging;
using quZONE.Domain.Entities;
using quZONE.Domain.Services;
using quZONE.Domain.ViewModels;
using Twilio.Mvc;
using Twilio.TwiML;
using quZONE.Common.Logging;


namespace quZONE.Api.Controllers
{
    /// <summary>
    /// Aggregate Root: Wait List and Guest
    /// </summary>


    [Authorize]
    [RoutePrefix("api/waitlist")]
    public class WaitListController : BaseApiController
    {

        private readonly IWaitListService _waitListService;
        private readonly IUserProfileService _userProfileService;
        private readonly IWaitTimeService _waitTimeService;

        readonly Logger _logger = new Logger();

        public WaitListController(IWaitListService waitListService, IUserProfileService userProfileService,
            IWaitTimeService waitTimeService)
        {
            _waitListService = waitListService;
            _userProfileService = userProfileService;
            _waitTimeService = waitTimeService;
        }

        [Route("list/{id:int}")]
        public IHttpActionResult GetWaitListByOrgId(int id) //id: organization id
        {
            var list = _waitListService.GetWaitListByOrgId(id);

            return Ok(list);
        }


        //[Route("guest/{id:int}")]
        [Route("list/{id:int}/guest/{gid:int}")]
        [HttpGet]
        public IHttpActionResult GetWaitListGuestById(int id, int gid) //id: guest waitlist id
        {
            var list = _waitListService.GetWaitGuest(id, gid);

            return Ok(list);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("list/sms/{smsMessageId}")]
        public IHttpActionResult GetReceivedSms(string smsMessageId)
        {
            var smsClient = new SmsClient();

            var msg = smsClient.GetReceivedMessage(smsMessageId);

            return Ok(msg);
        }

        [AllowAnonymous]
        [Route("list/msg/{smsCode}")] //During development
        public IHttpActionResult ReceiveSmsMessage(SmsMessageViewModel smsMessage)
            //Extract meta data and body of the sms message sent to the application
        {

            return Ok();
        }

        [AllowAnonymous]
        [Route("list/msg/list")] //During development
        public IHttpActionResult GetAllMessageList()
        {
            var smsClient = new SmsClient();



            return Ok(smsClient.GetAllMessagesList());
        }

        [AllowAnonymous]
        [Route("list/add")]
        public IHttpActionResult AddToWaitList(WaitListViewModel waitListInfo)
        {

            var hourOfDay = Int32.Parse(DateTime.Now.Hour.ToString());
            var weekDay = DateTime.Now.DayOfWeek;

            var waitTime = "";

            if (waitListInfo.WaitTime == null)
            {
                waitTime = _waitTimeService.GetWaitTime(hourOfDay, weekDay);
            }
            else
            {
                waitTime = waitListInfo.WaitTime;
            }
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //set default values
                //
                waitListInfo.IsActive = true;
                waitListInfo.IsPreferred = false;
                waitListInfo.WaitingStatus = "Waiting";
                waitListInfo.ServedTableNumber = "0";
                waitListInfo.ArrivalTime = DateTime.Now;
                waitListInfo.StatusChangeTime = DateTime.Now;
                waitListInfo.CreateDate = DateTime.Now;
                waitListInfo.UpdateDate = DateTime.Now;

                //Add to the wait list

                if (waitTime != "0")
                {
                    AddToList(waitListInfo);
                }
                

            }
            catch (Exception)
            {

                throw;
            }
            //set default values
            //
            //waitListInfo.IsActive = true;
            //waitListInfo.IsPreferred = false;
            //waitListInfo.WaitingStatus = "Waiting";
            //waitListInfo.ServedTableNumber = "0";
            //waitListInfo.ArrivalTime = DateTime.Now;
            //waitListInfo.StatusChangeTime = DateTime.Now;
            //waitListInfo.CreateDate = DateTime.Now;
            //waitListInfo.UpdateDate = DateTime.Now;

            var org = _userProfileService.GetOrganizationById(waitListInfo.OrganizationId);

            ////Persist waitlist in database
            ////
            //_waitListService.AddToWaitList(waitListInfo);



            //Get wait time depending on the hour of day
            //
            //var hourOfDay = Int32.Parse(DateTime.Now.Hour.ToString());
            //var weekDay = DateTime.Now.DayOfWeek;

            //var waitTime = _waitTimeService.GetWaitTime(hourOfDay, weekDay);


            //Send sms to guest
            //
            var to = waitListInfo.GuestContactTel;

            var msg = "";

            if (waitTime == "0")
            {
                msg = "Dear " + waitListInfo.GuestFirstName + ", The restaurant is currently closed. Please call tomorrow. " +
                      org.Name + ".";
            }
            else
            {
                msg = "Dear " + waitListInfo.GuestFirstName + ", you have been put in our waitlist. " +
                      "The approximate wait time is " + waitTime +
                      " min. You will be notified when a table is available. Thanks." + "  " + org.Name + ".";
            }
            

            //SendSmsMessage();

            SendSmsMessageToGuest(to, msg); //For full testing and production

            return Ok();
        }


        [Route("list/update/{id:int}")]
        public IHttpActionResult UpdateWaitList(int id, WalkInWaitList list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _waitListService.UpdateWaitList(id, list);

            //Get guest info and send sms message - for production
            //

            var guest = _waitListService.GetGuestFromWaitList(list.GuestId);

            //Get organization name
            var org = _userProfileService.GetOrganizationById(list.OrganizationId);

            var to = guest.GuestContactTel;

            var msg = "";

            if (list.WaitingStatus == "Available")
            {
                msg = "Dear " + guest.GuestFirstName +
                      ", a table is available for you now. Please come to report to the front desk. Thanks.";
            }
            else
            {
                msg = "Dear " + guest.GuestFirstName + ", Welcome to " + org.Name + ". Thank you.";
            }

            //SendSmsMessage();

            SendSmsMessageToGuest(to, msg); //For full testing and production

            return Ok();
        }

        [Route("list/msg")]
        public IHttpActionResult SendSmsMessage()
        {
            var smsClient = new SmsClient();

            smsClient.SendSmsMessage();


            return Ok();
        }


        [AllowAnonymous]
        [Route("list/sms")] //Add to wait list via SMS message
        public IHttpActionResult SmsAddToList(SmsRequest message) // change the SmsMessageViewModel to SmsRequest, a Twilio message request object 
        { 
            // for real Twilio SMS message 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //set organizationId to the target code from the argument passed in, then find out the organizaitonId

            var orgId = _userProfileService.GetOrganizationByCode(message.To).Id;

            string[] info = GetRequest(message.Body);

            var customerName = "";
            var guest = "";

            var waitListInfo = new WaitListViewModel();

            try
            {
                customerName = info[0];
                guest = info[1];
                

                waitListInfo.GuestFirstName = customerName;
                waitListInfo.GuestLastName = "Guest";
                waitListInfo.GuestGroupSize = int.Parse(guest);
                waitListInfo.GuestContactTel = message.From.Substring(2);
                waitListInfo.OrganizationId = orgId;

                waitListInfo.IsActive = true;
                waitListInfo.IsPreferred = false;
                waitListInfo.WaitingStatus = "Waiting";
                waitListInfo.ServedTableNumber = "0";
                waitListInfo.ArrivalTime = DateTime.Now;
                waitListInfo.StatusChangeTime = DateTime.Now;
                waitListInfo.CreateDate = DateTime.Now;
                waitListInfo.UpdateDate = DateTime.Now;


            }
            catch (Exception)
            {
                var error_msg = "Invalid format. Please resend in the form of 'name number, e.g. John 5. Thanks.";
                var outTo = message.From;

                SendSmsMessageToGuest(outTo, error_msg);
   

                throw;
            }

            

            



            #region implementation

            /* */
            try
            {
                //Add to the wait list
                AddToList(waitListInfo);

            }
            catch (Exception)
            {

                throw;
            }

            var org = _userProfileService.GetOrganizationById(waitListInfo.OrganizationId);

            ////Persist waitlist in database
            ////
            //_waitListService.AddToWaitList(waitListInfo);



            //Get wait time depending on the hour of day and day of week
            //

            var weekDay = DateTime.Now.DayOfWeek; //Int32.Parse(DateTime.Now.DayOfWeek.ToString());

            var hourOfDay = Int32.Parse(DateTime.Now.Hour.ToString());

            var waitTime = _waitTimeService.GetWaitTime(hourOfDay, weekDay);

            var to = waitListInfo.GuestContactTel;

            var msg = "";

            if (waitTime == "0")
            {
                msg = "Dear " + waitListInfo.GuestFirstName + ", The restaurant is currently closed. Please call tomorrow. " +
                      org.Name + ".";
            }
            else
            {
                msg = "Dear " + waitListInfo.GuestFirstName + ", you have been put in our waitlist. " +
                      "The approximate wait time is " + waitTime +
                      " min. You will be notified when a table is available. Thanks." + "  " + org.Name + ".";
            }
            //Send sms to guest
            //
            
           

            //SendSmsMessage();

            SendSmsMessageToGuest(to, msg); //For full testing and production

            #endregion

            return Ok();
        }



        [Route("list/call")]
        public IHttpActionResult SendSmsMessage(Guest guest)
        {
            var smsClient = new SmsClient();

            var to = guest.GuestContactTel;
            var name = guest.GuestFirstName;

            var msg = "Dear " + name +
                      ", a table is available to you, please come to report to the reception as soon as you can. Thanks.";

            smsClient.SendSmsMessageLive(to, msg);


            return Ok();
        }


        //Production Live Implementation

        //[Route("list/msg")]
        private void SendSmsMessageToGuest(string to, string msg)
        {
            var smsClient = new SmsClient();

            smsClient.SendSmsMessageLive(to, msg);
        }


        private void AddToList(WaitListViewModel waitListInfo)
        {
            //set default values
            //
            //waitListInfo.IsActive = true;
            //waitListInfo.IsPreferred = false;
            //waitListInfo.WaitingStatus = "Waiting";
            //waitListInfo.ServedTableNumber = "0";
            //waitListInfo.ArrivalTime = DateTime.Now;
            //waitListInfo.StatusChangeTime = DateTime.Now;
            //waitListInfo.CreateDate = DateTime.Now;
            //waitListInfo.UpdateDate = DateTime.Now;

            //var org = _userProfileService.GetOrganizationById(waitListInfo.OrganizationId);

            //Persist waitlist in database
            //
            _waitListService.AddToWaitList(waitListInfo);


        }


        //[AllowAnonymous]
        //[Route("list/test")]

        //public IHttpActionResult textSplit()
        //{
        //    var result = GetRequest("bob 5");

        //    return Ok(result);
        //}

        private string[] GetRequest(string text)
        {
            char[] delimiterChars = {' ', ',', '.', ':', '\t'};

            string[] content = text.Split(delimiterChars);

            //var list = content.ToList();

            return content;
        }




        //Test code for receiving SMS message from Twilio
        //To be tested with Twilio number

        // ***************** This method may not be required at all ***********************

        public HttpResponseMessage GetMessage(SmsRequest request)
        {
            TwilioResponse response = new TwilioResponse();

            var msg = request.Body;


            return Request.CreateResponse(HttpStatusCode.OK, response.Element);




        }



        //[AllowAnonymous]
        //[Route("guest")]
        //[HttpGet]
        //public IHttpActionResult GetGuuestByTel(string tele)
        //{
        //    var guest = _waitListService.GetGuestByTel(tele);

        //    return Ok(guest);
        //}
    }
}
