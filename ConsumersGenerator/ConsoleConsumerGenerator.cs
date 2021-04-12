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
        const string Consumer = nameof(Consumer);

        public void Execute(GeneratorExecutionContext context)
        {
            StringBuilder source1 = new();
            source1
                .AppendLine("using System;")
                .AppendLine("namespace GeneratedConsumers")
                .AppendLine("{")
                .AppendLine("public class Consumer { }")
                .AppendLine("}");
            SourceText sourceText1 = SourceText.From(source1.ToString(), Encoding.UTF8);
            context.AddSource($"{Consumer}.cs", sourceText1);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
