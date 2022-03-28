using System.ComponentModel.DataAnnotations;

namespace Otus.SocialNetwork.ViewModels;

public sealed class UnfriendRequest
{
    [Required] public string FriendUsername { get; set; } = null!;
}