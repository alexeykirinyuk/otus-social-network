using System.Data;

namespace Otus.SocialNetwork.Persistence.Abstranctions;

public sealed class UnitOfWorkAttribute : Attribute
{
    public UnitOfWorkAttribute(IsolationLevel isolationLevel)
    {
        IsolationLevel = isolationLevel;
    }

    public IsolationLevel IsolationLevel { get; }
}