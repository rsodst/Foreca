namespace Foreca.App.ValueObjects;

public sealed class CityName
{
    public CityName(string name)
    {
        RawValue = name;

        _name = name.ToLower()
            .Replace(" ", "")
            .Replace("\t", "")
            .Replace("  ", " ");

        ArgumentException.ThrowIfNullOrEmpty(_name);
    }

    private string _name { get; }

    public string RawValue { get; set; }

    public static implicit operator CityName(string name)
    {
        return new CityName(name);
    }

    public static implicit operator string(CityName cityName)
    {
        return cityName._name;
    }

    public override string ToString()
    {
        return _name;
    }
}