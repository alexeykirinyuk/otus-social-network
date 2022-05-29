using Otus.SocialNetwork.Domain;
using Otus.SocialNetwork.Persistence.Abstranctions.Posts;

namespace Otus.SocialNetwork.Persistence.QueryObjects.Posts.SavePost;

public sealed class SavePostQueryObject : ISavePostQueryObject
{
    public Task Save(Post post, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}