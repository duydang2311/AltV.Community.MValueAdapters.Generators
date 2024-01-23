# AltV.Community.MValueAdapters.Generators

[![NuGet badge](https://img.shields.io/nuget/v/AltV.Community.MValueAdapters.Generators?color=blue&cacheSeconds=3600)](https://www.nuget.org/packages/AltV.Community.MValueAdapters.Generators/)

## Quickstart

### Installation

1. Add the NuGet package to your project.

```bash
dotnet add package AltV.Community.MValueAdapters.Generators
```

2. In case there are [CS0436](https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs0436) warnings during compilation, you must add these attributes to your `<PackageReference>`.

```xml
<PackageReference Include="AltV.Community.MValueAdapters.Generators" PrivateAssets="all" ExcludeAssets="runtime" />
```

### Generate your first MValue adapter

1. Add `MValueAdapter` attribute to your class.

```csharp
using AltV.Community.MValueAdapters.Generators;

[MValueAdapter]
public class ParentDto
{
    public string First { get; set; } = string.Empty;
    public string Second { get; set; } = string.Empty;
    public ChildDto Dto { get; set; } = null!;
}

[MValueAdapter]
public class ChildDto
{
    public string First { get; set; } = string.Empty;
    public string Second { get; set; } = string.Empty;
}
```

2. Register the MValue adapters generated when the resource start.

```csharp
public override void OnStart()
{
    AltExtensions.RegisterAdapters();
}
```

Huge thanks to deluvas1911 for sharing his great work and allowing me to open source this.
