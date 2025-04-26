using app.Models;

namespace api.Models.Events
{
    public class Product : BaseEntity
    {
        public required Category Category { get; set; }
    }
}