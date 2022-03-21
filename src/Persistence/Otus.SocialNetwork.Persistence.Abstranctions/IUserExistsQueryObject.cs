namespace Otus.SocialNetwork.Persistence.Abstranctions;

public interface IUserExistsQueryObject
{
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct);
}