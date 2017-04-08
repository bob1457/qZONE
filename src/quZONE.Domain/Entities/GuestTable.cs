namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GuestTable")]
    public partial class GuestTable : BaseEntity<int>
    {
        private GuestTable()
        {
            
        }

        public GuestTable(string tableNumber, int organizationId, string tableSize, int maxSeatsCapacity, DateTime lastUpdateTime, string tableStatus)
        {
            TableNumber = tableNumber;
            OrganizationId = organizationId;
            TableSize = tableSize;
            MaxSeatsCapacity = maxSeatsCapacity;
            LastUpdateTime = lastUpdateTime;
            TableStatus = tableStatus;
        }

        //public int Id { get; set; }

        [Required]
        [StringLength(5)]
        public string TableNumber { get; set; }

        public int OrganizationId { get; set; }

        [Required]
        [StringLength(50)]
        public string TableSize { get; set; }

        public int MaxSeatsCapacity { get; set; }

        [Required]
        [StringLength(50)]
        public string TableStatus { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastUpdateTime { get; set; }

        [StringLength(250)]
        public string TableIconImgUrl { get; set; }

        [StringLength(450)]
        public string Notes { get; set; }
    }
}
