namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KnowledgeBase")]
    public partial class KnowledgeBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KnowledgeBase()
        {
            KBVersionHistories = new HashSet<KBVersionHistory>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(350)]
        public string KBSubject { get; set; }

        [Required]
        public string KBContent { get; set; }

        [Required]
        [StringLength(50)]
        public string KBContentVersion { get; set; }

        public int KBversionTrackiingId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdateDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KBVersionHistory> KBVersionHistories { get; set; }
    }
}
