using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quZONE.Domain.Entities;

namespace quZONE.Domain.ViewModels
{
    public class UserProfileViewModel
    {
        //Personal Info
        //
        public int Id { get; set; } // user profile id

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        

        public DateTime JoinDate { get; set; }

        [StringLength(256)]
        public string Email { get; set; }
        

        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public byte Level { get; set; }

        //Orgainzation info
        //
        public string AvatarImgUrl { get; set; }


        public string Name { get; set; }

        [StringLength(350)]
        public string Description { get; set; }

        public string LogoImgUrl { get; set; }

        public string Telephone { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string ProvState { get; set; }
        public string PostCode { get; set; }

        public int OrgainzationId { get; set; }

        public int PositionId { get; set; }

        public string Position { get; set; }
    }
}
