using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Models
{
    [Table("Categories")]
    public class Category
    {
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Name")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }     // one to many relationship
    }
}
