using System;
using System.Collections.Generic;

namespace JoJobsessed.Models
{
    public partial class ContactInformation
    {
        public ContactInformation()
        {
            ClothesShops = new HashSet<ClothesShop>();
        }

        public int IdContactInformation { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string EMail { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<ClothesShop> ClothesShops { get; set; }
    }
}
