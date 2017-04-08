using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quZONE.Domain.ViewModels
{
    public class WaitListViewModel
    {
        //Guest
        //
        public long GuestId { get; set; }

        [Required]
        [StringLength(150)]
        public string GuestFirstName { get; set; }

        [Required]
        [StringLength(150)]
        public string GuestLastName { get; set; }

        [Required]
        [StringLength(20)]
        public string GuestContactTel { get; set; }

        [StringLength(250)]
        public string GuestContactEmail { get; set; }

        public bool IsPreferred { get; set; }


        //Wait List
        //
        public int OrganizationId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ArrivalTime { get; set; }

        public int GuestGroupSize { get; set; }

        [Required]
        [StringLength(50)]
        public string WaitingStatus { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StatusChangeTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ServedTime { get; set; }

        [StringLength(50)]
        public string ServedTableNumber { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [StringLength(450)]
        public string Notes { get; set; }
    }
}
