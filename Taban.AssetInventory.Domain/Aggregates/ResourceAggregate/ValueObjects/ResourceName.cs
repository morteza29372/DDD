using Taban.AssetInventory.Domain.Common;

namespace Taban.AssetInventory.Domain.Aggregates.ResourceAggregate.ValueObjects;

public class ResourceName : ValueObject
{
    public string Value { get; }

    public ResourceName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("نام منبع اجباری می‌باشد.");

        if (value.Length > 255)
            throw new DomainException("نام نمی‌تواند بیشتر از 255 کاراکتر باشد.");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(ResourceName dec) => dec.Value;
}
