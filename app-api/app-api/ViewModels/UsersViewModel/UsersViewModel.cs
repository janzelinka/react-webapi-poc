using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
    public class UsersViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        public object Clone()
        {
            return this;
        }
    }
}