using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataModelsGenerator
{
    [Generator]
    public class InputDataModelGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var csvSettings = context.AdditionalFiles.Single(f => Path.GetExtension(f.Path).Equals(".csv", StringComparison.OrdinalIgnoreCase));
            var modelSettings = context.AdditionalFiles.Single(f => Path.GetExtension(f.Path).Equals(".zip", StringComparison.OrdinalIgnoreCase));

            var csvText = csvSettings.GetText().ToString();
            var csvData = csvText.Split('\n');
            var csvHeader = csvData[0];

            //MLContext mlContext = new MLContext(seed: 1);
            //var _ = mlContext.Model.Load(modelSettings.Path, out var modelSchema);
            //TODO get data from modelSchema instead of csv

            StringBuilder source = new();
            source.Append(@"using System;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace GeneratedDataModels
{
    public class InputDataModel
    {"
);
            for (int i = 1; i < csvData.Length - 1; i++)
            {
                var rows = csvData[i].Split('\t');

                if (rows.Count() > 4)
                {
                    source.Append($"\n        [LoadColumn({i - 1})]");
                    source.Append($"\n        public {rows[3]} {rows[0]} {{ get; set; }}\n");
                }
            }
            source.Append("\n    }\n}");

            const string desiredFileName = "InputModel.cs";
            SourceText sourceText = SourceText.From(source.ToString(), Encoding.UTF8);
            context.AddSource(desiredFileName, sourceText);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
