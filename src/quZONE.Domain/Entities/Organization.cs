namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization")]
    public partial class Organization : BaseEntity<int>
    {
        //public int Id { get; set; }

        private Organization()
        { }

        public Organization(string notes, 
            bool isActive, 
            string logoImgUrl, 
            string organizationType, 
            string description, 
            string name,
            string tele,
            string code)
        {
            Notes = notes;
            IsActive = isActive;
            LogoImgUrl = logoImgUrl;
            OrganizationType = organizationType;
            Description = description;
            Name = name;
            Telephone = tele;
            MessageCode = code;
        }

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
        public string LogoImgUrlSm { get; set; }

        [StringLength(250)]
        public string LogoImgUrlmd { get; set; }

        public bool IsActive { get; set; }

        public string Telephone { get; set; }

        //public string MesssageCode { get; set; }

        public string MessageCode { get; set; }
        
        [StringLength(450)]
        public string Notes { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

    }
}
