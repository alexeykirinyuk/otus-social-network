using System.ComponentModel.DataAnnotations;

namespace Otus.SocialNetwork.ViewModels;

public sealed class FriendRequest
{
    [Required] public string FriendUsername { get; set; } = null!;
}