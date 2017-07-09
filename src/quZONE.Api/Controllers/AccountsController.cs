using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using quZONE.Api.Infrastructure;
using quZONE.Api.Models;
using quZONE.Domain.Services;


namespace quZONE.Api.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        
        [Authorize(Roles = "admin")]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        [Authorize(Roles = "admin")]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        //[Authorize(Roles = "admin")]
        [Authorize]
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [AllowAnonymous]
        //[Authorize]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date,
                EmailConfirmed = true,

                UserProfile = new UserProfile()
                {
                    ContactEmail = createUserModel.Email,
                    OrgainzationId = createUserModel.OrganizationId,
                    //AvatarImgUrlMd = "content/images/avatars/avatar-default-md.png",
                    //AvatarImgUrlSm = "content/images/avatars/avatar-default-sm.png",
                    PositionId = createUserModel.PositionId,
                    AvatarImgUrl = "content/images/avatars/avatar-default.png"

                }
            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            //Add client role - default
            //
            var currentUser = AppUserManager.FindByName(user.UserName);

            AppUserManager.AddToRoles(currentUser.Id, "manager");

            //Enable immediate account activation
            //
            currentUser.EmailConfirmed = true;


            //update user profile
            //
            //currentUser.UserProfile.AvatarImgUrl = "";
            //currentUser.UserProfile.
            //currentUser.UserProfile.OrgainzationId = 1;


            //Send confirmation email
            //

            //string code = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            //var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code = code }));

            //await this.AppUserManager.SendEmailAsync(createUserModel.Email,
            //                                        "Confirm your account",
            //                                        "Your account has been setup. The username and password are:" + createUserModel.Username
            //                                        + "/" + createUserModel.Password + ".");

            EmailNotification emailNotication = new EmailNotification();

            emailNotication.SendEmail(createUserModel.Email,
                                                    "Confirm your account",
                                                    "Your account has been setup. The username and password are:" + createUserModel.Username
                                                    + "/" + createUserModel.Password + ".");

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user)); // Factory pattern is used here to create user account


            


            //return Ok();
        }

/*

        [AllowAnonymous]
        //[Authorize]
        [Route("createtrial")]
        public async Task<IHttpActionResult> CreateTrialUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var req = _profileService.GetTrialRequestById(id);

            var org = _profileService.GetOrganizationByName(req.OrganizationName);

            


            var user = new ApplicationUser()
            {
                UserName = req.ContactFirstName.Substring(0,1) + req.ContactLastName,
                Email = req.ContactEmail,
                FirstName = req.ContactFirstName,
                LastName = req.ContactLastName,
                Level = 0,
                JoinDate = DateTime.Now.Date,
                EmailConfirmed = true,

                UserProfile = new UserProfile()
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


*/


        [Route("update/{username}")]
        public async Task<IHttpActionResult> UpdateUser(string username, CreateUserBindingModel userModel)
        {
            var user = await AppUserManager.FindByNameAsync(username);

            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Email = userModel.Email;

            await AppUserManager.UpdateAsync(user);

            return Ok();
        }



        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                return Ok();
            }

            return GetErrorResult(result);
        }

        [Authorize]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        //[Authorize]
        //[Route("ChangePasswd")]
        //public async Task<IHttpActionResult> ChangePasswd(string uid, ChangePasswordBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await this.AppUserManager.ChangePasswordAsync(uid, model.OldPassword, model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}

        /**/

        public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await AppUserManager.FindByNameAsync(model.Email);
                // If user has to activate his email to confirm his account, then use code listing below
                //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                //{
                //    return Ok();
                //}
                if (user == null)
                {
                    return Ok(); // Don't reveal that the user does not exist or is not confirmed
                    
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await AppUserManager.GeneratePasswordResetTokenAsync(user.Id);
                //await AppUserManager.SendEmailAsync(user.Id, "Reset Password", $"Please reset your password by using this {code}");

                EmailNotification email = new EmailNotification();

                email.SendEmail(model.Email,"Reset Password", "Please reset your password by using this " + code );

                return Ok();
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(ModelState);
        }






        [Authorize(Roles = "admin")]
        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {

            //Only SuperAdmin or Admin can delete users (Later when implement roles)

            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser != null)
            {
                IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();

            }

            return NotFound();

        }

        [Authorize(Roles = "admin")]
        [Route("user/{id:guid}/roles")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        {

            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            var currentRoles = await this.AppUserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = rolesToAssign.Except(this.AppRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Count() > 0)
            {

                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            IdentityResult removeResult = await this.AppUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            IdentityResult addResult = await this.AppUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok();
        }



    }
}