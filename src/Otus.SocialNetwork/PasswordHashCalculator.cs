using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Otus.SocialNetwork.Application.Features.Users.RegisterUser;

namespace Otus.SocialNetwork;

public sealed class PasswordHashCalculator : IPasswordHashCalculator
{
    public string CalculateHash(string password)
    {
        var salt = new byte[128 / 8];
        using (var rngCsp = RandomNumberGenerator.Create())
        {
            rngCsp.GetNonZeroBytes(salt);
        }

        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }
}