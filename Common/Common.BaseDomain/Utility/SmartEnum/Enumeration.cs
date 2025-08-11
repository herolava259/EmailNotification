



using System.Reflection;

namespace Common.ArchitechtureDesign.Generic.Utility.SmartEnum;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<uint, TEnum> Enumerations = CreateEnumerations();
    protected Enumeration(uint value, string name)
    {
        Value = value;
        Name = name;
    }
    public uint Value { get; protected init; }

    public string Name { get; protected init; } = String.Empty;

    public static TEnum? FromValue(uint value)
        => Enumerations.TryGetValue(value, out TEnum? enumeration) ? enumeration : null;

    public static TEnum? FromName(string name)
        => Enumerations
            .Values
            .SingleOrDefault(c => c.Name == name);



    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null) return false;

        return GetType() == other.GetType() &&
                Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
        => this.Name;

    private static Dictionary<uint, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
                .GetFields(
                    BindingFlags.Public |
                    BindingFlags.Static |
                    BindingFlags.FlattenHierarchy)
                .Where(fi => 
                    enumerationType.IsAssignableFrom(fi.FieldType))
                .Select(fi =>
                    (TEnum)fi.GetValue(default)!);
        return fieldsForType.ToDictionary(c => c.Value);
    }
}
