namespace Otus.SocialNetwork.Application.Features.Users.RegisterUser;

public interface IPasswordHashCalculator
{
    string CalculateHash(string password);
}