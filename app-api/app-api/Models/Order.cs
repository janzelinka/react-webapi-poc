using app.Models;

namespace api.Models.Events
{
    public class Order : BaseEntity
    {
        public List<Product> Products { get; set; } = new();
        public required Address Address { get; set; }
        public required Category Category { get; set; }
    }
}