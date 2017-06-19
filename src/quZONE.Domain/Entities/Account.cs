namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }

        public int OrganizationId { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EffectiveDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateDate { get; set; }

        [StringLength(450)]
        public string Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
