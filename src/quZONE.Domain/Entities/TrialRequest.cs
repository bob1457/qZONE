namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrialRequest")]
    public partial class TrialRequest
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string OrganizationName { get; set; }

        [Required]
        [StringLength(150)]
        public string OrgAddressLine1 { get; set; }

        [Required]
        [StringLength(50)]
        public string OrgAddressCity { get; set; }

        [Required]
        [StringLength(50)]
        public string OrgAddressProState { get; set; }

        [StringLength(50)]
        public string OrgAddressPostZipCodeva { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactFirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactLastName { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactTel { get; set; }

        [StringLength(450)]
        public string Notes { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }
    }
}
