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
    public class InputOutputDataModelsGenerator : ISourceGenerator
    {
        const string InputDataModel = nameof(InputDataModel);
        const string OutputDataModel = nameof(OutputDataModel);
        const string GeneratedDataModels = nameof(GeneratedDataModels);

        public void Execute(GeneratorExecutionContext context)
        {
            var zipPath = context.AdditionalFiles.Single(f => Path.GetExtension(f.Path).Equals(".zip", StringComparison.OrdinalIgnoreCase)).Path;
            var zip = StreamHelper.GetZipFileStream(zipPath);
            
            using var reader = new BinaryReader(zip, Encoding.UTF8);

            var features1 = StreamHelper.ExtractFeatures(reader);
            StringBuilder source1 = new();
            source1
                .UsingAppend("System")
                .UsingAppend("Microsoft.ML.Data")
                .Append(SyntaxHelper.NamespaceWrap(SyntaxHelper.InputClassWrap(features1, InputDataModel), GeneratedDataModels));
            SourceText sourceText1 = SourceText.From(source1.ToString(), Encoding.UTF8);
            context.AddSource($"{InputDataModel}.cs", sourceText1);

            StringBuilder source2 = new();
            source2
                .UsingAppend("System")
                .UsingAppend("Microsoft.ML.Data")
                .Append(SyntaxHelper.NamespaceWrap(SyntaxHelper.OutputClassWrap(OutputDataModel), GeneratedDataModels));
            SourceText sourceText2 = SourceText.From(source2.ToString(), Encoding.UTF8);
            context.AddSource($"{OutputDataModel}.cs", sourceText2);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
