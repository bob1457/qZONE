namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Position")]
    public partial class Position
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string PoistionTitle { get; set; }

        [StringLength(450)]
        public string Description { get; set; }

        [StringLength(350)]
        public string Notes { get; set; }
    }
}
