using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int CountryId { get; set; }
        //public int? Id { get; set; }
        //public int? Id { get; set; }
        //public string CategoryName { get; set; }
        //public string CountryName { get; set; }
    }
}
