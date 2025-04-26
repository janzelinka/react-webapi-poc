using app.Models;

namespace api.Models.Events
{
    public class Category : BaseEntity
    {
        public virtual List<Product> Products { get; set; } = new List<Product>();
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}