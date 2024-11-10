## AltV.Community.MValueAdapters.Generators

[![NuGet badge](https://img.shields.io/nuget/v/AltV.Community.MValueAdapters.Generators?color=blue&cacheSeconds=3600)](https://www.nuget.org/packages/AltV.Community.MValueAdapters.Generators/)

### Quickstart

This library helps you generate the needed code for implementing any MValue adapter.

#### Installation

1. Add the NuGet package to your project.

```bash
dotnet add package AltV.Community.MValueAdapters.Generators
```

2. *(Optional)* Enable `EmitCompilerGeneratedFiles` flag in your `.csproj` by adding this to your `<PropertyGroup>`.

This helps preventing generated files not found issue if you happen to use copy build output task.

```xml
<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
```

3. *(Optional)* In case there are [CS0436](https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs0436) warnings during compilation, you must add these attributes to your `<PackageReference>`.

```xml
<PackageReference Include="AltV.Community.MValueAdapters.Generators" PrivateAssets="all" ExcludeAssets="runtime" />
```

**Note:** If you use a _shared_ project between client and server, only add the NuGet to the _shared_ project and neither to the client nor server project to avoid ambigious references.

#### Generate your first MValue adapter

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

2. Register the MValue adapters generated when the resource (client / server) starts.

```csharp
public override void OnStart()
{
    AltExtensions.RegisterAdapters();
}
```

Huge thanks to deluvas1911 for sharing his great work and allowing me to open source this.

### How to contribute

If you'd like to contribute, some adjustments need to be made to the project setup.

1. Fork the project, clone and build it `dotnet build`.
2. Remove any existing `<PackageReference>` to this project from your `Server`/`Client` project.
3. Add the following lines to an `<ItemGroup>` and replace the `Include=` value with the relative paths:

```xml
<ProjectReference Include="..\AltV.Community.MValueAdapters.Generators\AltV.Community.MValueAdapters.Generators.csproj" OutputItemType="Analyzer" ReferenceOutputAssembly="false" PrivateAssets="all" />
<ProjectReference Include="..\AltV.Community.MValueAdapters.Generators.Abstractions\AltV.Community.MValueAdapters.Generators.Abstractions.csproj" OutputItemType="Analyzer" />
```
4. Add the following line to an `<ItemGroup>` in your `Server`/`Client` project to view generated files:

```xml
<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
```

5. In your `Server`/`Client` project, make sure you clear the generator cache before building to keep generated files always latest, or you can use the command below which already does that.

```bash
dotnet build-server shutdown && dotnet clean && dotnet build
```

---

### Other community libraries

- [AltV.Community.Events](https://github.com/duydang2311/AltV.Community.Events): create events as classes with both sync and async support.
