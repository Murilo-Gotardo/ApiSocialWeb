using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace apiSocialWeb.Domain.Models.UserAggregate
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Photo { get; set; }

        public string[]? Notifications { get; set; }

        public User(string name, string email, string photo, string[]? notifications)
        {
            Name = name ?? throw new ArgumentException(nameof(name));

            Email = email ?? throw new ArgumentException(nameof(email));

            Photo = photo ?? throw new ArgumentException(nameof(photo));

            Notifications = notifications;
        }
    }
}
