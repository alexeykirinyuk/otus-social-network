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
    public IReadOnlyList<Friend> Friends { get; private set; }
    public DateTime CreatedAt { get; }
    public string PasswordHash { get; }
    public string PasswordSalt { get; }

    public User(
        string username,
        string? firstName,
        string? lastName,
        DateTime? dateOfBirth,
        Sex? sex,
        IReadOnlyList<string> interests,
        string? city,
        IReadOnlyList<Friend> friends,
        string passwordHash,
        string passwordSalt,
        DateTime createdAt)
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Sex = sex;
        Interests = interests;
        City = city;
        Friends = friends;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
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
        string passwordHash,
        string passwordSalt)
    {
        return new User(
            username,
            firstName,
            lastName,
            dateOfBirth,
            sex,
            interests,
            city,
            Array.Empty<Friend>(),
            passwordHash,
            passwordSalt,
            DateTime.UtcNow);
    }

    public void BeFriends(User user)
    {
        if (Friends.Any(currentFriend => currentFriend.Username == user.Username))
        {
            return;
        }

        Friends = Friends.Append(Friend.Create(user)).ToArray();
    }

    public void StopBeingFriends(User user)
    {
        Friends = Friends
            .Where(friend => friend.Username != user.Username)
            .ToArray();
    }
}