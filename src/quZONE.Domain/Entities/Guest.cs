namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Guest")]
    public partial class Guest : BaseEntity<long>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Guest()
        {
            WalkInWaitLists = new HashSet<WalkInWaitList>();
        }

        public Guest(string guestFirstName, string guestLastName, string guestContactTel, string guestContactEmail)
        {
            GuestFirstName = guestFirstName;
            GuestLastName = guestLastName;
            GuestContactTel = guestContactTel;
            GuestContactEmail = guestContactEmail;
        }

        //public long Id { get; set; }

        [Required]
        [StringLength(150)]
        public string GuestFirstName { get; set; }

        [Required]
        [StringLength(150)]
        public string GuestLastName { get; set; }

        [Required]
        [StringLength(20)]
        public string GuestContactTel { get; set; }

        [StringLength(250)]
        public string GuestContactEmail { get; set; }

        public bool IsPreferred { get; set; }

        //[Column(TypeName = "datetime2")]
        //public DateTime CreateDate { get; set; }

        //[Column(TypeName = "datetime2")]
        //public DateTime UpdateDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WalkInWaitList> WalkInWaitLists { get; set; }
    }
}
