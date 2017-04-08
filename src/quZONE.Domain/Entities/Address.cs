using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quZONE.Domain.Entities
{
    [Table("Address")]
    public partial class Address 
    {
        public Address()
        {
            Organizations = new HashSet<Organization>();
        }

        public Address(string postZipCode, string provState, string city, string addressLine2, string addressLine1)
        {
            PostZipCode = postZipCode;
            ProvState = provState;
            City = city;
            AddressLine2 = addressLine2;
            AddressLine1 = addressLine1;
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string AddressLine1 { get; set; }

        [StringLength(150)]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string ProvState { get; set; }

        [Required]
        [StringLength(50)]
        public string PostZipCode { get; set; }

        //public virtual Organization Organization { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Organization> Organizations { get; set; }
        
    }
}
