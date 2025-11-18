using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;

public class ResourceCapacity : ValueObject
{
    public decimal Value { get; set; }

    public ResourceCapacity(decimal value)
    {
        if (value < 1)
            throw new DomainException("ظرفیت نمی‌تواند کوچک‌تر از یک باشد.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();

    public static implicit operator decimal(ResourceCapacity id) => id.Value;

    public static implicit operator ResourceCapacity(decimal value) => new(value);
}
