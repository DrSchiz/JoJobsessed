using System;
using System.Collections.Generic;

namespace JoJobsessed.Models
{
    public partial class ClothesShop
    {
        public int IdClothesShop { get; set; }
        public string NameClothesShop { get; set; } = null!;
        public int ContactInformationId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public virtual ContactInformation ContactInformation { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
