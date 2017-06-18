namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupportRequest")]
    public partial class SupportRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupportRequest()
        {
            RequestResponses = new HashSet<RequestResponse>();
            Resolutions = new HashSet<Resolution>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(350)]
        public string RequestSubject { get; set; }

        [Required]
        public string RequestContent { get; set; }

        public int OrganizationId { get; set; }

        [Required]
        [StringLength(50)]
        public string RequestedBy { get; set; }

        public int RequestStatusId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestResponse> RequestResponses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resolution> Resolutions { get; set; }

        public virtual SupportRequestStatu SupportRequestStatu { get; set; }
    }
}
