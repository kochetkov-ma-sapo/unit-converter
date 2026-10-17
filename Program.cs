using System.Globalization;

// Factors convert a unit to the base unit of its category (metre, kilogram).
var length = new Dictionary<string, double>
{
    ["mm"] = 0.001, ["cm"] = 0.01, ["m"] = 1, ["km"] = 1000,
    ["in"] = 0.0254, ["ft"] = 0.3048, ["yd"] = 0.9144, ["mi"] = 1609.344,
};
var weight = new Dictionary<string, double>
{
    ["mg"] = 0.000001, ["g"] = 0.001, ["kg"] = 1, ["t"] = 1000,
    ["oz"] = 0.028349523125, ["lb"] = 0.45359237,
};
string[] temperature = ["c", "f", "k"];

if (args.Length != 3 || !double.TryParse(args[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
{
    Console.Error.WriteLine("usage: unit-converter <value> <from> <to>   e.g. 5 km mi, 70 f c, 2 lb kg");
    Console.Error.WriteLine($"length: {string.Join(' ', length.Keys)}");
    Console.Error.WriteLine($"weight: {string.Join(' ', weight.Keys)}");
    Console.Error.WriteLine($"temperature: {string.Join(' ', temperature)}");
    return 1;
}

var from = args[1].ToLowerInvariant();
var to = args[2].ToLowerInvariant();
double? result = null;

if (length.ContainsKey(from) && length.ContainsKey(to))
    result = value * length[from] / length[to];
else if (weight.ContainsKey(from) && weight.ContainsKey(to))
    result = value * weight[from] / weight[to];
else if (temperature.Contains(from) && temperature.Contains(to))
    result = FromCelsius(ToCelsius(value, from), to);

if (result is null)
{
    Console.Error.WriteLine($"cannot convert '{from}' to '{to}'");
    return 1;
}

Console.WriteLine($"{value.ToString(CultureInfo.InvariantCulture)} {from} = {Math.Round(result.Value, 4).ToString(CultureInfo.InvariantCulture)} {to}");
return 0;

static double ToCelsius(double v, string unit) => unit switch
{
    "f" => (v - 32) * 5 / 9,
    "k" => v - 273.15,
    _ => v,
};

static double FromCelsius(double c, string unit) => unit switch
{
    "f" => c * 9 / 5 + 32,
    "k" => c + 273.15,
    _ => c,
};
