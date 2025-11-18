using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;

public class ResourceId : ValueObject
{
    public long Value { get; }

    public ResourceId(long value)
    {
        if (value <= 0)
            throw new DomainException("Resource Id must be a positive number");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();

    public static implicit operator long(ResourceId id) => id.Value;

    public static implicit operator ResourceId(long value) => new(value);
}

