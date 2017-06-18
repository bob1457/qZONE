namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequestResponse")]
    public partial class RequestResponse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("RequestResponse")]
        [Required]
        public string RequestResponse1 { get; set; }

        public long SupportRequestId { get; set; }

        public string SupportNotes { get; set; }

        [Required]
        [StringLength(50)]
        public string ResponseBy { get; set; }

        public bool ResponseFromSupport { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ResponseDate { get; set; }

        public virtual SupportRequest SupportRequest { get; set; }
    }
}
