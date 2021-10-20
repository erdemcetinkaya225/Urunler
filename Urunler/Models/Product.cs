using System;
using System.Collections.Generic;

#nullable disable

namespace Urunler.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Category { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Category CategoryNavigation { get; set; }
    }
}
