using quZONE.Domain.Entities;

namespace quZONE.Domain.Temp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public int Id { get; set; }

        public int OrganizationId { get; set; }

        public bool IsActive { get; set; }

        public int PaymentHistoryId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EffectiveDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateDate { get; set; }

        [StringLength(450)]
        public string Notes { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
