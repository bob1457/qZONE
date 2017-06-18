namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resolution")]
    public partial class Resolution
    {
        public int Id { get; set; }

        [Required]
        public string ResolutionContent { get; set; }

        public string OtherNotes { get; set; }

        public long SupportRequestId { get; set; }

        [Required]
        [StringLength(50)]
        public string ResolvedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ResolveDate { get; set; }

        public virtual SupportRequest SupportRequest { get; set; }
    }
}
