using System.ComponentModel.DataAnnotations;

namespace Otus.SocialNetwork.Web.API.Models;

public sealed class Register
{
    public sealed class Request
    {
        [Required] public string? Username { get; set; }

        [Required] public string? Password { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Sex? Sex { get; set; }
        public List<string> Interests { get; set; } = new();
        public string? City { get; set; }
    }
}