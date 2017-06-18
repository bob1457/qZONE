namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KBVersionHistory")]
    public partial class KBVersionHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Column("KBVersionHistory")]
        [Required]
        [StringLength(50)]
        public string KBVersionHistory1 { get; set; }

        public long KnolwdgeBaseId { get; set; }

        [StringLength(50)]
        public string KBVersionHistoryDesc { get; set; }

        public virtual KnowledgeBase KnowledgeBase { get; set; }
    }
}
