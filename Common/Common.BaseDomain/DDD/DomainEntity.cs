using System.Diagnostics.CodeAnalysis;

namespace Common.Domain.Generic.DDD;

public enum DomainActivityType: ushort
{
    None = 0,
    Request = 1,
    Manual = 2,
    Automatic = 3,
    Schedule = 4,
    External = 5,
    Trigger = 6,
    CrossDomain = 7,
}

public sealed record DomainActivityObject(DateTimeOffset OccuredTime, string AgentId = "",
                                    DomainActivityType Type = DomainActivityType.None)
{
    public bool OccuredAfter(DateTimeOffset timeStone)
        => OccuredTime >= timeStone;

    public bool OccuredBefore(DateTimeOffset? timeStone)
    {
        if(timeStone.HasValue)
            return OccuredTime <= timeStone.Value;
        return OccuredTime <= DateTimeOffset.UtcNow;
    }
}


public abstract partial class DomainEntity 
{
    public Guid Id { get; protected init; }


    protected DomainEntity(Guid id)
    {
        Id = id;
    }

    public bool IsDeleted { get; private set; } = false;

    public void SoftDelete()
    {
        this.IsDeleted = true;
    }

    public DomainActivityObject CreationActivity { get; private init; } = new DomainActivityObject(DateTimeOffset.UtcNow);

    public DomainActivityObject ModificationActivity { get; private set; } = new DomainActivityObject(DateTimeOffset.UtcNow);


    public virtual void SetModifiedBy(string modifiedBy)
    {
        this.ModificationActivity = new DomainActivityObject(DateTimeOffset.UtcNow,  modifiedBy);
    }

    public bool CreatedAfter(DateTimeOffset timeStone)
        => CreationActivity.OccuredAfter(timeStone);

    public bool CreatedBefore(DateTimeOffset? timeStone)
        => CreationActivity.OccuredBefore(timeStone);



}

// base behaviours 
public abstract partial class DomainEntity : IEquatable<DomainEntity>, IEqualityComparer<DomainEntity>
{
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

    protected static int GenerateHashCode(Guid id)
    {
        var chain = id.ToString();

        int modK = (1 << 16);

        var hashVal = 0;

        foreach(char c in chain)
        {
            if (!Char.IsDigit(c) || !Char.IsLetter(c))
                continue;

            hashVal += c;
        }

        return hashVal % modK;
    }

    public override int GetHashCode()
    {
        return GenerateHashCode(this.Id);
    }

    public bool Equals(DomainEntity? x, DomainEntity? y)
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] DomainEntity obj)
    {
        return obj.GetHashCode();
    }
}
