using System.ComponentModel.DataAnnotations;

namespace JoJobsessed.Models
{
    public class Login
    {
        public string LoginUser { get; set; }
        [DataType(DataType.Password)]
        public string PasswordUser { get; set; }

    }
}
