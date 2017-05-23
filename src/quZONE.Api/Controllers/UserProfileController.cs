using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using quZONE.Api.Infrastructure;
using quZONE.Common.Utilities;
using quZONE.Data.Interfaces;
using quZONE.Domain.Entities;
using quZONE.Domain.Services;
using quZONE.Domain.ViewModels;

namespace quZONE.Api.Controllers
{
    /// <summary>
    /// User Aggregate Root Repository: Entities include UserProfile, Organization, and Position
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("api/profile")]
    public class UserProfileController : BaseApiController
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        #region User Profile

        [Route("allusers")]
        public IHttpActionResult GetAllProfile()
        {
            //var profile = _userProfileRepository.GetAllUserProfiles();
            var profiles = _userProfileService.GetAllUserProfile();
            return Ok(profiles);
        }

        [Route("user/{username}")]
        public IHttpActionResult GetUser(string username)
        {
            var user = _userProfileService.GetUser(username); //AppUserManager.FindById(username);

            //var profile = _userProfileService.GetUserProfile(user.UserProfile.Id);

            //return Ok(profile);
            return Ok(user);

        }

        [Route("users")]
        public IHttpActionResult GetAllUser()
        {
            var users = _userProfileService.GetAllUsers();

            //var profile = _userProfileService.GetUserProfile(user.UserProfile.Id);

            //return Ok(profile);
            return Ok(users);

        }

        [Route("users/{id:int}")]
        public IHttpActionResult GetUsersInOrg(int id) //id: organizaation id
        {
            var usersInOrg = _userProfileService.GetAllUsers().Where(u => u.OrgainzationId == id);

           return Ok(usersInOrg);
        }

        [Route("userProfile/{username}")]
        public IHttpActionResult GetUserProfile(string username)
        {
            //var profileId = _userProfileService.GetUser(username).UserProfile_Id; //AppUserManager.FindById(username);

            var profile = _userProfileService.GetUserProfile(username);

            //return Ok(profile);
            return Ok(profile);

        }

        ////Not in user for now, for future user to update profile related attributes, not AspUser, which is handled with ASP.NET Identity
        [Route("userProfile/update/{username}")]
        public IHttpActionResult UpdateUserProfile(string userName, UserProfileViewModel profile)
        {
            _userProfileService.UpdateUserProfile(userName, profile);

            return Ok();
        }



        [Route("userProfile/upload/{uname}")]
        [HttpPost]
        public async Task<HttpResponseMessage> Upload(string uname) //this upload user avatar images, will be refactored as a generic upload nethod
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            var originalFileName = GetDeserializedFileName(result.FileData.First());




            // uploadedFileInfo object will give you some additional stuff like file length,
            // creation time, directory name, a few filesystem methods etc..
            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            // create a new filename matching the conve

            var originalFileExt = FileProcessor.GetFileExtension(originalFileName);

            var uploadFolder = "~/Content/Images/Avatars"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);



            var newfile = root + "\\" + uname + originalFileExt;

            if (!File.Exists(newfile))
            {
                FileProcessor.RenameFile(result.FileData.First().LocalFileName, newfile);

                //Need to update database for the new url for the image file path




                _userProfileService.UpdateUserAvatarImgUrl(uname, "content/images/avatars/" + uname + originalFileExt);
            }
            else
            {
                File.Delete(newfile);
                FileProcessor.RenameFile(result.FileData.First().LocalFileName, newfile);

                _userProfileService.UpdateUserAvatarImgUrl(uname, "content/images/avatars/" + uname + originalFileExt);
            }


            // Remove this line as well as GetFormData method if you're not 
            // sending any form data with your upload request
            //var fileUploadObj = GetFormData<FileUploadViewModel>(result);

            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            var returnData = "ReturnTest";
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }



        [Route("userProfile/uploadOrgImg/{id:int}")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadOrgImg(int id) //this upload user avatar images, will be refactored as a generic upload nethod
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            var originalFileName = GetDeserializedFileName(result.FileData.First());




            // uploadedFileInfo object will give you some additional stuff like file length,
            // creation time, directory name, a few filesystem methods etc..
            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            // create a new filename matching the conve

            var originalFileExt = FileProcessor.GetFileExtension(originalFileName);

            var uploadFolder = "~/Content/Images/Organizations"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);



            var newfile = root + "\\" + "org-" + id +  originalFileExt;

            if (!File.Exists(newfile))
            {
                FileProcessor.RenameFile(result.FileData.First().LocalFileName, newfile);

                //Need to update database for the new url for the image file path




                _userProfileService.UpdateUserOrgImgUrl(id, "content/images/organizations/" + "org-" + id + originalFileExt);
            }
            else
            {
                File.Delete(newfile);
                FileProcessor.RenameFile(result.FileData.First().LocalFileName, newfile);

                _userProfileService.UpdateUserOrgImgUrl(id, "content/images/organizations/" + "org-" + id + originalFileExt);
            }


            // Remove this line as well as GetFormData method if you're not 
            // sending any form data with your upload request
            //var fileUploadObj = GetFormData<FileUploadViewModel>(result);

            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            var returnData = "ReturnTest";
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }




        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/Content/Images/Organizations"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        //private object GetFormData<T>(MultipartFormDataStreamProvider result)
        //{
        //    if (result.FormData.HasKeys())
        //    {
        //        var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(0).FirstOrDefault() ?? String.Empty);
        //        if (!String.IsNullOrEmpty(unescapedFormData))
        //            return JsonConvert.DeserializeObject<T>(unescapedFormData);
        //    }

        //    return null;
        //}

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
        
        
        
        
        
        
        
        
        #endregion


        #region Trail Request

        [AllowAnonymous]
        [Route("trial")]
        public IHttpActionResult CreateTrialRequest(TrialRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userProfileService.AddRequest(request);

            return Ok();
        }


        [AllowAnonymous] //for testing only
        [Route("requests")]
        public IHttpActionResult GetAllTrialREquests()
        {
            var requests = _userProfileService.GetAllTrialRequests().ToList();

            return Ok(requests);
        }


        [AllowAnonymous] //for testing only
        [Route("request/{id:int}")]
        public IHttpActionResult GetTrialREquestById(int id)
        {
            var requests = _userProfileService.GetTrialRequestById(id);

            return Ok(requests);
        }


        [AllowAnonymous] //for testing only
        [Route("request/create/{id:int}")]
        public IHttpActionResult CreateTrialAccount(int id) //id is the request id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orgData = _userProfileService.GetTrialRequestById(id);

            OrganizationViewModel orgModel = new OrganizationViewModel();

            orgModel.Name = orgData.OrganizationName;
            orgModel.Description = "";
            orgModel.OrganizationType = "Restaurant";
            orgModel.AddressLine1 = orgData.OrgAddressLine1;
            orgModel.City = orgData.OrgAddressCity;
            orgModel.ProvState = orgData.OrgAddressProState;
            orgModel.PostZipCode = orgData.OrgAddressPostZipCodeva;
            orgModel.LogoImgUrl = "content/images/organizations/fs-logo-default.png";
            orgModel.LogoImgUrlMd = "";
            orgModel.LogoImgUrlSm = "";
            orgModel.IsActive = true;
            orgModel.Telephone = orgData.ContactTel;

            _userProfileService.AddOrganization(orgModel);


            //update request status
            //






            return Ok();
        }


        #endregion


        #region Organization


        [Route("organization/create")]
        public IHttpActionResult CreateRognization(OrganizationViewModel organizationViewModel) //Organization organization, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Set pre-defined default parameters
            //
            organizationViewModel.LogoImgUrl = "content/images/organizations/fs-logo-default.png";
            organizationViewModel.LogoImgUrlMd = "";
            organizationViewModel.LogoImgUrlSm = "";
            organizationViewModel.IsActive = true;


            _userProfileService.AddOrganization(organizationViewModel);
            
            return Ok();
        }

        [AllowAnonymous]
        [Route("organization/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetOrganizationDetails(int id)
        {
            var organization = _userProfileService.ShowOrganization(id);

            return Ok(organization);
        }

        [AllowAnonymous]
        [Route("organizations")]
        public IHttpActionResult GetAllOrganizations()
        {
            var orgs = _userProfileService.GetallOrganizations();

            return Ok(orgs);
        }

        
        [Route("organization/update/{id:int}")]
        public IHttpActionResult UpdateOrganization(int id, OrganizationViewModel organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userProfileService.UpdateOrganization(organization);


            return Ok();
        }

        public IHttpActionResult UpdateUserProfile(string username, string imagePath ) //to be reviewed later for profile updates, not only avatar image url
        {
            var userProfile = _userProfileService.GetUserProfile(username);

            _userProfileService.UpdateUserProfile(username, imagePath);

            return Ok();
        }





        #endregion

        
    }
}
