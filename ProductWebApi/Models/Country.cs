using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Models
{
    public class Country
    {
        public int CountryId { get; set; }

        [Required]
        public string countryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }     // one to many relationship
    }
}
