using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Models
{
    [Table("Products")]
    public class Product
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [Column("Price",TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        
        public int? CategoryId { get; set; }

        
        public int? CountryId { get; set; }

        
        [NotMapped]
        //[Column("CategoryName")]
        public string? CategoryName { get; set; }
        [NotMapped]
        //[Column("CountryName")]
        public string? CountryName { get; set; }
        

        public virtual Category Category { get; set; }      // each product has only one category

        public virtual Country Country { get; set; }     // each product has only one country
    }
}
