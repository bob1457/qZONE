using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quZONE.Domain.Entities;

namespace quZONE.Domain.ViewModels
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }

        public int OrganizationId { get; set; }

        public bool IsActive { get; set; }

        public int PaymentOption { get; set; }

       
        public DateTime CreateDate { get; set; }

      
        public DateTime EffectiveDate { get; set; }

     
        public DateTime UpdateDate { get; set; }

        
        public string Notes { get; set; }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        
        public string Description { get; set; }

        public string OrganizationType { get; set; }

        
        public string LogoImgUrl { get; set; }

       
        public string LogoImgUrlSm { get; set; }

       
        public string LogoImgUrlmd { get; set; }

        public bool OrgIsActive { get; set; }

        public string Telephone { get; set; }

        //public string MesssageCode { get; set; }

        public string MessageCode { get; set; }

        

        public int AddressId { get; set; }



        public string AddressLine1 { get; set; }

        
        public string AddressLine2 { get; set; }

       
        public string City { get; set; }

        
        public string ProvState { get; set; }

        
        public string PostZipCode { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
