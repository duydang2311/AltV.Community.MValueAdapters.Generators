﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using AltV.Community.MValueAdapters.Generators.Abstractions;
using AltV.Community.MValueAdapters.Generators.Constants;
using AltV.Community.MValueAdapters.Generators.Converters;
using AltV.Community.MValueAdapters.Generators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace AltV.Community.MValueAdapters.Generators;

[Generator]
public class MValueAdapterGenerator : IIncrementalGenerator
{
    private readonly Dictionary<string, ITypeConverter> _typeConverters;

    public MValueAdapterGenerator()
    {
        _typeConverters = new Dictionary<string, ITypeConverter>()
        {
            // Bool
            { "bool", new BoolConverter() },
            // Byte
            { "byte", new ByteConverter() },
            { "sbyte", new SByteConverter() },
            // Short
            { "short", new ShortConverter() },
            { "ushort", new UShortConverter() },
            // Int
            { "int", new IntConverter() },
            { "uint", new UIntConverter() },
            // Long
            { "long", new LongConverter() },
            { "ulong", new ULongConverter() },
            // Float
            { "float", new FloatConverter() },
            // Double
            { "double", new DoubleConverter() },
            // Decimal
            { "decimal", new DecimalConverter() },
            // Char
            { "char", new CharConverter() },
            // String
            { "string", new StringConverter() }
        };
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var mValues = context
            .SyntaxProvider
            .CreateSyntaxProvider(
                predicate: (node, _) => node is ClassDeclarationSyntax { AttributeLists.Count: > 0 },
                transform: (ctx, _) => GetMValueClasses(ctx)
            )
            .Where(c => c != null);

        var compilationModel = context.CompilationProvider.Combine(mValues.Collect());
        context.RegisterSourceOutput(compilationModel, (sourceContext, source) =>
        {
            foreach (var diagnostic in diagnostics)
            {
                sourceContext.ReportDiagnostic(diagnostic);
            }
            Execute(source.Right, sourceContext);
        });
    }

    private List<Diagnostic> diagnostics = [];
    private int inc = 0;

    private MValueClassInfo? GetMValueClasses(GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
        foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                    continue;

                var fqName = attributeSymbol.ContainingType.ToString();
                if (!fqName.Equals("AltV.Community.MValueAdapters.Generators.MValueAdapterAttribute", StringComparison.Ordinal)) continue;

                return new(classDeclarationSyntax.Identifier.ValueText, GetNamespace(classDeclarationSyntax), GetClassProperties(context.SemanticModel, classDeclarationSyntax, GetClassNamingConvention(context.SemanticModel, attributeSyntax)));
            }
        }

        return null;
    }

    private NamingConvention GetClassNamingConvention(SemanticModel semanticModel, AttributeSyntax? attributeSyntax)
    {
        if (attributeSyntax?.ArgumentList is null) return NamingConvention.UsePropertyName;

        var attributeArgumentSyntax = attributeSyntax.ArgumentList.Arguments
            .FirstOrDefault(x => x.NameEquals is not null && x.Expression is not null && x.NameEquals.Name.Identifier.ValueText.Equals(nameof(NamingConvention), StringComparison.Ordinal));
        if (attributeArgumentSyntax is null) return NamingConvention.UsePropertyName;

        var operation = semanticModel.GetOperation(attributeArgumentSyntax.Expression);
        if (operation is null) return NamingConvention.UsePropertyName;

        if (!operation.ConstantValue.HasValue) return NamingConvention.UsePropertyName;
        return (NamingConvention)operation.ConstantValue.Value!;
    }

    private string GetNamespace(BaseTypeDeclarationSyntax syntax)
    {
        var @namespace = string.Empty;

        var potentialNamespaceParent = syntax.Parent;

        while (potentialNamespaceParent is not null and not NamespaceDeclarationSyntax and not FileScopedNamespaceDeclarationSyntax)
            potentialNamespaceParent = potentialNamespaceParent.Parent;

        if (potentialNamespaceParent is not BaseNamespaceDeclarationSyntax namespaceParent) return @namespace;

        @namespace = namespaceParent.Name.ToString();

        while (true)
        {
            if (namespaceParent.Parent is not NamespaceDeclarationSyntax parent)
                break;

            @namespace = $"{namespaceParent.Name}.{@namespace}";
            namespaceParent = parent;
        }

        return @namespace;
    }

    private MValuePropertyInfo[] GetClassProperties(SemanticModel semanticModel, ClassDeclarationSyntax classDeclarationSyntax, NamingConvention namingConvention)
    {
        var handledProperties = new List<MValuePropertyInfo>();
        var classProperties = classDeclarationSyntax.Members.OfType<PropertyDeclarationSyntax>();

        // ReSharper disable once TooWideLocalVariableScope
        bool skipProperty;
        string? customName;

        foreach (var propertyDeclarationSyntax in classProperties)
        {
            skipProperty = false;
            customName = null;

            foreach (var attributeListSyntax in propertyDeclarationSyntax.AttributeLists)
            {
                foreach (var attributeSyntax in attributeListSyntax.Attributes)
                {
                    if (semanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                        continue;

                    var fqName = attributeSymbol.ContainingType.ToString();
                    switch (fqName)
                    {
                        case "AltV.Community.MValueAdapters.Generators.MValueIgnoreAttribute":
                            skipProperty = true;
                            break;
                        case "AltV.Community.MValueAdapters.Generators.MValuePropertyNameAttribute":
                            customName = attributeSyntax.ArgumentList!.Arguments[0].ToString();
                            break;
                    }
                }

                if (skipProperty) break;
            }

            if (skipProperty) continue;

            if (customName == null && namingConvention != NamingConvention.UsePropertyName)
                customName = GetPropertyName(propertyDeclarationSyntax.Identifier.ValueText, namingConvention);

            var propertyData = GetPropertyData(propertyDeclarationSyntax.Type.ToString());
            handledProperties.Add(new MValuePropertyInfo(propertyData, propertyDeclarationSyntax.Identifier.ValueText, customName));
        }

        return handledProperties.ToArray();
    }

    private string GetPropertyName(string name, NamingConvention namingConvention)
    {
        return namingConvention switch
        {
            NamingConvention.UsePropertyName => name,
            NamingConvention.UpperCase => name.ToUpper(),
            NamingConvention.LowerCase => name.ToLower(),
            NamingConvention.PascalCase => NamingConventionHelpers.ToPascalCase(name),
            NamingConvention.CamelCase => NamingConventionHelpers.ToCamelCase(name),
            _ => throw new InvalidOperationException("Used naming convention is not valid")
        };
    }

    private PropertyData GetPropertyData(string type)
    {
        var propertyType = PropertyType.Default;

        // Nullable BaseType
        var nullable = IsTypeNullable(ref type);

        // Collections
        if (type.EndsWith("[]"))
        {
            propertyType = PropertyType.Array;
            type = type.Substring(0, type.Length - 2);
        }
        else if (type.StartsWith("List<") && type.EndsWith(">"))
        {
            propertyType = PropertyType.List;
            type = type.Substring(5, type.Length - 6);
        }
        else if (type.StartsWith("IEnumerable<") && type.EndsWith(">"))
        {
            propertyType = PropertyType.Array;
            type = type.Substring(12, type.Length - 13);
        }
        else if (type.StartsWith("ICollection<") && type.EndsWith(">"))
        {
            propertyType = PropertyType.Array;
            type = type.Substring(12, type.Length - 13);
        }

        if (propertyType == PropertyType.Default) return new PropertyData(propertyType, type, nullable);

        // Nullable collections
        var nullableCollection = IsTypeNullable(ref type);
        return new PropertyData(propertyType, type, nullable, nullableCollection);
    }

    private bool IsTypeNullable(ref string type)
    {
        var nullable = false;
        if (type.EndsWith("?"))
        {
            nullable = true;
            type = type.Substring(0, type.Length - 1);
        }
        else if (type.StartsWith("Nullable<") && type.EndsWith(">"))
        {
            nullable = true;
            type = type.Substring(9, type.Length - 10);
        }

        return nullable;
    }

    private void Execute(ImmutableArray<MValueClassInfo?> classes, SourceProductionContext context)
    {
        var adapterList = new StringBuilder();
        foreach (var classInfo in classes)
        {
            if (classInfo == null) continue;

            var readerIndentation = 4;
            var readerCode = new StringBuilder();
            var writerIndentation = 2;
            var writerCode = new StringBuilder();

            foreach (var propertyInfo in classInfo.PropertyInfos)
            {
                if (!_typeConverters.TryGetValue(propertyInfo.TypeName, out var converter))
                {
                    if (classes.Any(x => x is not null && x.Name == propertyInfo.TypeName))
                    {
                        converter = new ByAdapterConverter(propertyInfo.TypeName);
                    }
                    else
                    {
                        var diagnostic = Diagnostic.Create(
                            "MVC0001",
                            "Source Generator",
                            $"Unsupported type {propertyInfo.TypeName} at property {propertyInfo.Name}",
                            DiagnosticSeverity.Error,
                            DiagnosticSeverity.Error,
                            true,
                            0
                        );
                        context.ReportDiagnostic(diagnostic);
                        continue;
                    }
                }

                if (propertyInfo.PropertyType == PropertyType.Default)
                {
                    converter.ReadItem(readerCode, ref readerIndentation, propertyInfo);
                    converter.WriteItem(writerCode, ref writerIndentation, propertyInfo);
                }
                else
                {
                    converter.ReadCollection(readerCode, ref readerIndentation, propertyInfo);
                    converter.WriteCollection(writerCode, ref writerIndentation, propertyInfo);
                }
            }

            context.AddSource(
                $"MValueAdapter.{classInfo.Name}.g.cs",
                SourceText.From(
                    string.Format(
                        Templates.ConverterTemplate,
                        classInfo.Namespace,
                        classInfo.Name,
                        readerCode,
                        writerCode
                    ),
                    Encoding.UTF8
                )
            );
            adapterList.AppendLine(string.Format(Templates.AdapterTemplate, classInfo.Name));
        }

        context.AddSource("MValueAdapters.Generators.AltExtensions.g.cs", SourceText.From(string.Format(Templates.ExtensionTemplate, adapterList), Encoding.UTF8));
    }
}
