using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace quZONE.Api.Infrastructure
{
    public class ApplicationUser : IdentityUser
    {
        //Custom User Profile Declaration
        public virtual UserProfile UserProfile { get; set; }


        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public byte Level { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

    }

    //Custom user profile signature
    public class UserProfile
    {
        public int Id { get; set; }
        public int OrgainzationId { get; set; }
        public int PositionId { get; set; }
        public string AvatarImgUrl { get; set; }

        public string ContactEmail { get; set; }

        //public string AvatarImgUrlSm { get; set; }

        //public string AvatarImgUrlMd { get; set; }

        //public string ContactTelephone { get; set; }
        //public string ContactOthers { get; set; }
        //public string AddressStreet { get; set; }
        //public string AddressCity { get; set; }
        //public string AddressProvState { get; set; }
        //public string AddressPostZip { get; set; }
        //public string AddressCountry { get; set; }

    }
}