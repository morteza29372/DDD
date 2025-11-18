using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;

public class ResourceEfficiency:ValueObject
{
    public decimal Value { get; set; }

    public ResourceEfficiency(decimal value)
    {
        if (value <= 0)
            throw new DomainException("درصد راندمان نمی‌تواند کوچک‌تر برابر با صفر باشد.");

        if (value > 100)
            throw new DomainException("درصد راندمان نمی‌تواند بالای 100 باشد.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();

    public static implicit operator decimal(ResourceEfficiency id) => id.Value;

    public static implicit operator ResourceEfficiency(decimal value) => new(value);
}
