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
using SendGrid.Resources;

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



        [AllowAnonymous]
        //[Authorize]
        [Route("createtrial/{id:int}")]
        public async Task<IHttpActionResult> CreateTrialUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var req = _userProfileService.GetTrialRequestById(id);

            var org = _userProfileService.GetOrganizationByName(req.OrganizationName);




            var user = new ApplicationUser()
            {
                UserName = req.ContactFirstName.Substring(0, 1) + req.ContactLastName,
                Email = req.ContactEmail,
                FirstName = req.ContactFirstName,
                LastName = req.ContactLastName,
                Level = 0,
                JoinDate = DateTime.Now.Date,
                EmailConfirmed = true,

                UserProfile = new Api.Infrastructure.UserProfile()
                {
                    ContactEmail = req.ContactEmail,
                    OrgainzationId = org.Id,
                    //AvatarImgUrlMd = "content/images/avatars/avatar-default-md.png",
                    //AvatarImgUrlSm = "content/images/avatars/avatar-default-sm.png",
                    PositionId = 1,
                    AvatarImgUrl = "content/images/avatars/avatar-default.png"

                }
            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, "ChangeIt!");

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            //Add client role - default
            //
            var currentUser = AppUserManager.FindByName(user.UserName);

            AppUserManager.AddToRoles(currentUser.Id, "manager");





            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));


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


        //[AllowAnonymous] //for testing only
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

            var request = _userProfileService.GetTrialRequestById(id);

            OrganizationViewModel orgModel = new OrganizationViewModel
            {
                Name = orgData.OrganizationName,
                Description = "",
                OrganizationType = "Restaurant",
                AddressLine1 = orgData.OrgAddressLine1,
                City = orgData.OrgAddressCity,
                ProvState = orgData.OrgAddressProState,
                PostZipCode = orgData.OrgAddressPostZipCodeva,
                LogoImgUrl = "content/images/organizations/fs-logo-default.png",
                LogoImgUrlMd = "",
                LogoImgUrlSm = "",
                IsActive = true,
                Telephone = orgData.ContactTel
            };


            _userProfileService.AddOrganization(orgModel);


            //update request status
            //

            _userProfileService.UpdateRequest(id);




            return Ok();
        }

        [AllowAnonymous] //for testing only
        [Route("org/{name}")]
        public IHttpActionResult GetOrgnizationByName(string name)
        {
            var org = _userProfileService.GetOrganizationByName(name);

            return Ok(org);
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

        //[AllowAnonymous] //testing only
        [Route("organization/account/{id:int}/{pymnt:int}")]
        public IHttpActionResult ActivateOrgAccount(int id, int pymnt) //id is org id, to create an account connected to the organization for billing and management
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //try
            //{
            //    _userProfileService.ActivateAccount(id, pymnt);
            //}
            //catch (Exception)
            //{
                
            //    throw;
            //}

            _userProfileService.ActivateAccount(id, pymnt);

            return Ok();
        }

        [AllowAnonymous] //testing only
        [Route("organization/user/update/{uname}")]
        public IHttpActionResult UpdateUserLevel(string uname)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = AppUserManager.FindByName(uname);

            user.Level = 1;

            AppUserManager.Update(user);

            return Ok();
        }


        #endregion


        #region Organization Account

        //[AllowAnonymous] //testing mathod
        [Route("request/update/{id:int}")]
        public IHttpActionResult UpdateRequest(int id)
        {
            _userProfileService.UpdateRequest(id);

            return Ok();
        }

        [AllowAnonymous] //testing mathod
        [Route("account/{id:int}")]
        public IHttpActionResult GetOrgAccountDetails(int id) //id: organziation id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = _userProfileService.GetOrgAccount(id);

            return Ok(account);
        }

        #endregion
    }
}
