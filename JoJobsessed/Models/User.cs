using System;
using System.Collections.Generic;

namespace JoJobsessed.Models
{
    public partial class User
    {
        public User()
        {
            ClothesShops = new HashSet<ClothesShop>();
        }

        public int IdUser { get; set; }
        public string LoginUser { get; set; } = null!;
        public string PasswordUser { get; set; } = null!;
        public string? EMailUser { get; set; }
        public string? PhoneNumberUser { get; set; }

        public virtual ICollection<ClothesShop> ClothesShops { get; set; }
    }
}
