using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace ConsumersGenerator
{
    [Generator]
    public class ConsoleConsumerGenerator : ISourceGenerator
    {
        const string GeneratedConsumers = nameof(GeneratedConsumers);
        const string ConsoleConsumer = nameof(ConsoleConsumer);

        public void Execute(GeneratorExecutionContext context)
        {
            StringBuilder source1 = new();
            source1
                .AppendLine("using System;")
                .AppendLine("using Microsoft.ML;")
                .AppendLine("using Microsoft.ML.Data;")
                .AppendLine("namespace GeneratedConsumers")
                .AppendLine("{")
                .AppendLine("public class ConsoleConsumer { public void Run() { Console.WriteLine(1001); } }")
                .AppendLine("}");
            SourceText sourceText1 = SourceText.From(source1.ToString(), Encoding.UTF8);
            context.AddSource($"{ConsoleConsumer}.cs", sourceText1);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
