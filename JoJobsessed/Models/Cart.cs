namespace JoJobsessed.Models
{
    public class Cart
    {
        public Cart()
        {
            CartLines = new List<Product>();
        }

        public List<Product> CartLines { get; set; } = new List<Product>();
    }
}
