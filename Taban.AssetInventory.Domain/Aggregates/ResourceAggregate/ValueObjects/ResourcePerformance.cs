using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;

public class ResourcePerformance : ValueObject
{
    public decimal Value { get; set; }

    public ResourcePerformance(decimal value)
    {
        if (value <= 0)
            throw new DomainException("ضریب عملکرد نمی‌تواند کوچک‌تر برابر با صفر باشد.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();

    public static implicit operator decimal(ResourcePerformance id) => id.Value;

    public static implicit operator ResourcePerformance(decimal value) => new(value);
}
