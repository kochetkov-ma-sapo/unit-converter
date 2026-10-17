# unit-converter

A tiny command-line unit converter written in C# (.NET 8, no NuGet packages).

Supported units:

- length: `mm cm m km in ft yd mi`
- weight: `mg g kg t oz lb`
- temperature: `c f k`

## Usage

Requires the .NET 8 SDK.

```sh
dotnet run -- 5 km mi     # 5 km = 3.1069 mi
dotnet run -- 70 f c      # 70 f = 21.1111 c
dotnet run -- 2 lb kg     # 2 lb = 0.9072 kg
```
