namespace Otus.SocialNetwork.Persistence.Abstranctions.Users;

public interface IGetSubscribersQueryObject
{
    Task<IReadOnlyList<string>> GetUsernames(string username, CancellationToken ct);
}