using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.DDD;

public abstract class DomainEntity : IEquatable<DomainEntity>
{
    public Guid Id { get; protected init; }


    protected DomainEntity(Guid id)
    {
        Id = id;
    }

    protected abstract IEnumerable<object> GetEqualityComponets();

    

    public static bool operator ==(DomainEntity? left, DomainEntity? right)
    {
        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator !=(DomainEntity? left, DomainEntity? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as DomainEntity);
    }

    public bool Equals(DomainEntity? other)
    {
        if (other is null)
            return false;

        if (other.GetType() != GetType())
            return false;

        return this.GetEqualityComponets().SequenceEqual(other.GetEqualityComponets());
    }

}
