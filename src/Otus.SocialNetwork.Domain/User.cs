namespace Otus.SocialNetwork.Domain;

public sealed class User
{
    public string Username { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public DateTime? DateOfBirth { get; }
    public Sex? Sex { get; }
    public IReadOnlyList<string> Interests { get; }
    public string? City { get; }
    public DateTime CreatedAt { get; }
    public string PasswordHash { get; }

    public User(
        string username,
        string? firstName,
        string? lastName,
        DateTime? dateOfBirth,
        Sex? sex,
        IReadOnlyList<string> interests,
        string? city,
        string passwordHash,
        DateTime createdAt)
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Sex = sex;
        Interests = interests;
        City = city;
        CreatedAt = createdAt;
    }

    public static User RegisterNew(
        string username,
        string? firstName,
        string? lastName,
        DateTime? dateOfBirth,
        Sex? sex,
        IReadOnlyList<string> interests,
        string? city,
        string passwordHash)
    {
        return new User(
            username,
            firstName,
            lastName,
            dateOfBirth,
            sex,
            interests,
            city,
            passwordHash,
            DateTime.UtcNow);
    }
}