namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        public long Id { get; set; }

        public int AccountId { get; set; }

        public decimal PaymentAmount { get; set; }

        public int PaymentTypeId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PaymentDueDate { get; set; }

        public int PaymentMethodId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PaymentDate { get; set; }

        public virtual Account Account { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        public virtual PaymentType PaymentType { get; set; }
    }
}
