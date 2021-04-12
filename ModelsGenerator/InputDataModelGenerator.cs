using DataModelsGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace DataModelsGenerator
{
    [Generator]
    public class InputDataModelGenerator : ISourceGenerator
    {
        const string InputDataModel = nameof(InputDataModel);
        const string GeneratedDataModels = nameof(GeneratedDataModels);

        public void Execute(GeneratorExecutionContext context)
        {
            var zipPath = context.AdditionalFiles.Single(f => Path.GetExtension(f.Path).Equals(".zip", StringComparison.OrdinalIgnoreCase)).Path;
            var zip = StreamHelper.GetZipFileStream(zipPath);
            
            using var reader = new BinaryReader(zip, Encoding.UTF8);
            var features = StreamHelper.ExtractFeatures(reader);

            StringBuilder source = new();
            source
                .UsingAppend("System")
                .UsingAppend("Microsoft.ML")
                .UsingAppend("Microsoft.ML.Data")
                .Append(SyntaxHelper.NamespaceWrap(SyntaxHelper.ClassWrap(features, InputDataModel), GeneratedDataModels));

            SourceText sourceText = SourceText.From(source.ToString(), Encoding.UTF8);
            context.AddSource($"{InputDataModel}.cs", sourceText);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
