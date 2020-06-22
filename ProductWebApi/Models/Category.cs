using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string categoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }     // one to many relationship
    }
}
