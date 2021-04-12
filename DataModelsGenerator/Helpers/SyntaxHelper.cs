using DataModelsGenerator.Mappers;
using DataModelsGenerator.Models;
using System.Collections.Generic;
using System.Text;

namespace DataModelsGenerator.Helpers
{
    public static class SyntaxHelper
    {
        const string Tab = "  ";

        public static StringBuilder UsingAppend(this StringBuilder syntax, string ns)
        {
            return syntax.AppendLine($"using {ns};");
        }

        public static StringBuilder NamespaceWrap(StringBuilder code, string ns)
        {
            StringBuilder sb = new();
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine($"{{");
            sb.Append(code);
            sb.AppendLine($"}}");

            return sb;
        }

        public static StringBuilder InputClassWrap(IEnumerable<Feature> features, string cn)
        {
            StringBuilder sb = new();
            sb.AppendLine($"{Tab}public class {cn}");
            sb.AppendLine($"{Tab}{{");
            foreach (var feature in features)
            {
                sb.AppendLine($"{Tab}{Tab}[LoadColumn({feature.Index})]");
                sb.AppendLine($"{Tab}{Tab}public {feature.Type.Map()} {feature.Name} {{ get; set; }}");
            }
            sb.AppendLine($"{Tab}}}");

            return sb;
        }

        public static StringBuilder OutputClassWrap(string cn)
        {
            StringBuilder sb = new();
            sb.AppendLine($"{Tab}public class {cn}");
            sb.AppendLine($"{Tab}{{");

            sb.AppendLine($@"{Tab}{Tab}[ColumnName(""PredictedLabel"")]");
            sb.AppendLine($"{Tab}{Tab}public string Prediction {{ get; set; }}");
            sb.AppendLine($"{Tab}{Tab}public float[] Score {{ get; set; }}");

            sb.AppendLine($"{Tab}}}");

            return sb;
        }
    }
}
