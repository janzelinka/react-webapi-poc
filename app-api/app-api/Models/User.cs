using System;

namespace app.Models
{
    public class User : BaseEntity, ICloneable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public object Clone()
        {
            return this;
        }
    }

}