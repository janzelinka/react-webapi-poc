using app.Models;

namespace api.Models.Events
{
    public class Event : BaseEntity
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Description { get; set; } = string.Empty;
        public Order? Order { get; set; }
    }



}