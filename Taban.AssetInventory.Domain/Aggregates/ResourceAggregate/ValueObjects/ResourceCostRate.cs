using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;

public class ResourceCostRate : ValueObject
{
    public decimal Value { get; set; }

    public ResourceCostRate(decimal value)
    {
        if (value < 0)
            throw new DomainException("نرخ هزینه نمی‌تواند کوچک‌تر از صفر باشد.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();

    public static implicit operator decimal(ResourceCostRate id) => id.Value;

    public static implicit operator ResourceCostRate(decimal value) => new(value);
}
