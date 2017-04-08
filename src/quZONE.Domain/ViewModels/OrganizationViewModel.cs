using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quZONE.Domain.ViewModels
{
    public class OrganizationViewModel
    {
        #region Organization

        public int Id { get; set; } //organizational id
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(350)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string OrganizationType { get; set; }

        [StringLength(250)]
        public string LogoImgUrl { get; set; }

        [StringLength(250)]
        public string LogoImgUrlMd { get; set; }

        [StringLength(250)]
        public string LogoImgUrlSm { get; set; }

        public bool IsActive { get; set; }

        public string Telephone { get; set; }

        public string MessageCode { get; set; }

        [StringLength(450)]
        public string Notes { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
        #endregion

        #region Address

        [Required]
        [StringLength(150)]
        public string AddressLine1 { get; set; }

        [StringLength(150)]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string ProvState { get; set; }

        [Required]
        [StringLength(50)]
        public string PostZipCode { get; set; }

        #endregion

    }
}
