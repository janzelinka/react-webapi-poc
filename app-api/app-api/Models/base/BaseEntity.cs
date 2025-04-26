using System;

namespace app.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public Guid UpdatedBy { get; set; }
        public Guid CreatedBy { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}