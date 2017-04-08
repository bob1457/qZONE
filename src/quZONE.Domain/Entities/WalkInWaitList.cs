namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WalkInWaitList")]
    public partial class WalkInWaitList : BaseEntity<long>
    {
        private WalkInWaitList()
        {
            
        }

        public WalkInWaitList(int guestGroupSize, int organizationId, string waitingStatus, bool isActive)
        {
            GuestGroupSize = guestGroupSize;
            //ArrivalTime = arrivalTime;
            OrganizationId = organizationId;
            //GuestId = guestId;
            WaitingStatus = waitingStatus;
            IsActive = isActive;
        }

        //public long Id { get; set; }

        public int OrganizationId { get; set; }

        public long GuestId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ArrivalTime { get; set; } //Same as CreateDate

        public int GuestGroupSize { get; set; }

        [Required]
        [StringLength(50)]
        public string WaitingStatus { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StatusChangeTime { get; set; } //Same as UpdateDate

        [Column(TypeName = "datetime2")]
        public DateTime ServedTime { get; set; }

        [StringLength(50)]
        public string ServedTableNumber { get; set; }

        [StringLength(450)]
        public string Notes { get; set; }

        public virtual Guest Guest { get; set; }
    }
}
