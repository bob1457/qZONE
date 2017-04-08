using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quZONE.Domain.Entities
{
    public abstract class Booking : BaseEntity<long>
    {
        public int OrganizationId { get; private set; }
        public int GuestId { get; private set; }
        public int GuestGroupSize { get; set; }
        public string Status { get; private set; }
        public DateTime ServedTime { get; set; }
        public string Notes { get; set; }
    }
}
