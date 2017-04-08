using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quZONE.Domain.ViewModels
{
    public class GuestTableViewMode
    {
        public long GuestId { get; set; }
       
        public string GuestFirstName { get; set; }
        
        public string GuestLastName { get; set; }
       
        public string GuestContactTel { get; set; }
       
        public string GuestContactEmail { get; set; }

        public bool IsPreferred { get; set; }


        public string TableNumber { get; set; }

        public int OrganizationId { get; set; }


        public long WaitListId { get; set; }

        public DateTime ServedTime { get; set; }

        public int GuestGroupSize { get; set; }

       
    }
}
