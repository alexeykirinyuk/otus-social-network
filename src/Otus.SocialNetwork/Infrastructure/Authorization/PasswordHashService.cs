using System.Security.Cryptography;

namespace Otus.SocialNetwork.Infrastructure.Authorization;

public sealed class PasswordHashService : IPasswordHashService
{
    private const int SaltSize = 64;
    private const int HashSize = 256;

    public (string Hash, string Salt) CreateHash(string password)
    {
        var saltBytes = new byte[SaltSize];
        var provider = RandomNumberGenerator.Create();
        provider.GetNonZeroBytes(saltBytes);
        var salt = Convert.ToBase64String(saltBytes);

        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
        var hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(HashSize));

        return (hash, salt);
    }

    public bool VerifyPassword(string enteredPassword, string hash, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);

        return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(HashSize)) == hash;
    }
}