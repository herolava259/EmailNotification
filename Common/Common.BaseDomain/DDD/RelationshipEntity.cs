using Common.Domain.Generic.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ArchitechtureDesign.Generic.DDD;

public enum RelationshipType : ushort
{ 
    One2One = 0, 
    One2Many = 1,
    Many2Many = 2,
    SelfRecursive = 3,
}

public abstract class RelationshipEntity<T1Entity, T2Entity>
    where T1Entity: DomainEntity
    where T2Entity: DomainEntity
{
    public RelationshipType Type { get; set; }

    protected Guid FromId { get; set; }

    protected Guid ToId { get; set; }

    protected virtual T1Entity? FromEntity { get; set; }

    protected virtual T2Entity? ToEntity { get; set;}

    protected RelationshipEntity(RelationshipType relationshipType, Guid fromId, Guid toId)
    {
        this.Type = relationshipType;
        this.FromId = fromId;
        this.ToId = toId;
    }


    public string Id => $"{FromId.ToString()}-{ToId.ToString()}";
    //protected abstract void EnsureRestrictRelationship();
}


public abstract class SelfRelationshipEntity<TSelfEntity> : RelationshipEntity<TSelfEntity, TSelfEntity>
    where TSelfEntity: DomainEntity
{
    protected SelfRelationshipEntity(RelationshipType relationshipType, Guid fromId, Guid toId) : base(relationshipType, fromId, toId)
    {
    }
}

public abstract class One2OneRelationshipEntity<TFromEntity, TToEntity>
