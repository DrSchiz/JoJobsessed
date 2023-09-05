using System;
using System.Collections.Generic;

namespace JoJobsessed.Models
{
    public partial class Product
    {
        public Product()
        {
            ClothesShops = new HashSet<ClothesShop>();
        }

        public int IdProduct { get; set; }
        public string NameProduct { get; set; } = null!;
        public int PriceProduct { get; set; }
        public int CategoryProductId { get; set; }

        public virtual CategoryProduct CategoryProduct { get; set; } = null!;
        public virtual ICollection<ClothesShop> ClothesShops { get; set; }
    }
}
