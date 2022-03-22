namespace Otus.SocialNetwork.Infrastructure.Authorization;

public interface IPasswordHashService
{
    (string Hash, string Salt) CreateHash(string password);

    bool VerifyPassword(string enteredPassword, string hash, string salt);
}