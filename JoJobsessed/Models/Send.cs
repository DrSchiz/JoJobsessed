using System.ComponentModel.DataAnnotations;

namespace JoJobsessed.Models
{
    public class Send
    {

        [RegularExpression(@"[A-Za-z0-9._%+-]+.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string EMail { get; set; }

        public string Message { get; set; }
    }
}
