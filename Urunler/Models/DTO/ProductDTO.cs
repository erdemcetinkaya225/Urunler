using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Urunler.Models.DTO
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
       
        public string createdDate { get; set; }
    }
}
