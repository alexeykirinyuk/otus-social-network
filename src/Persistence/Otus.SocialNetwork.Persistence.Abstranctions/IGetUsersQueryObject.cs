using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Persistence.Abstranctions;

public sealed class GetUsersFilters
{
    public string? Username { get; set; }
}

public interface IGetUsersQueryObject
{
    Task<IReadOnlyList<User>> ToListAsync(GetUsersFilters? filters, CancellationToken ct);

    Task<User?> SingleOrDefaultAsync(GetUsersFilters? filters, CancellationToken ct);
}