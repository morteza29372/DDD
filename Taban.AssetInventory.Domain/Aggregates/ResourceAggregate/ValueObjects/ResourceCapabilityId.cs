using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;

public class ResourceCapabilityId : ValueObject
{
    public long Value { get; }

    public ResourceCapabilityId(long value)
    {
        if (value <= 0)
            throw new DomainException("Resource Capability Id must be a positive number");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();

    public static implicit operator long(ResourceCapabilityId id) => id.Value;

    public static implicit operator ResourceCapabilityId(long value) => new(value);
}

