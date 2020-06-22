using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Models
{
    public class Product
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string productName { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal price { get; set; }

        public int? CategoryId { get; set; }


        public int? CountryId { get; set; }


        public virtual Category Category { get; set; }      // each product has only one category

        public virtual Country Country { get; set; }     // each product has only one country
    }
}
