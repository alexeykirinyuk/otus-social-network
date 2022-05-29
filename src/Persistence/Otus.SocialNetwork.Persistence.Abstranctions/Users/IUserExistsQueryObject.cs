namespace Otus.SocialNetwork.Persistence.Abstranctions.Users;

public interface IUserExistsQueryObject
{
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct);
}