using System;
using System.Collections.Generic;

namespace JoJobsessed.Models
{
    public partial class CategoryProduct
    {
        public CategoryProduct()
        {
            Products = new HashSet<Product>();
        }

        public int IdCategoryProduct { get; set; }
        public string NameCategoryProduct { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
