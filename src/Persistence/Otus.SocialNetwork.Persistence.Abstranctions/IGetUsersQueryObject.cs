using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Persistence.Abstranctions;

public sealed class GetUsersParams
{
    public string? Username { get; set; }
    
    public string? FirstNamePrefix { get; set; }
    
    public string? LastNamePrefix { get; set; }
    
    public long? Offset { get; set; }
    
    public int? Limit { get; set; }
}

public interface IGetUsersQueryObject
{
    Task<IReadOnlyList<User>> ToListAsync(GetUsersParams? filters, CancellationToken ct);

    Task<User?> SingleOrDefaultAsync(GetUsersParams? filters, CancellationToken ct);

    Task<long> CountAsync(CancellationToken ct);
}