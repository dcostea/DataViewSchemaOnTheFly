namespace DataModelsGenerator.Mappers
{
    public static class TypeMapper
    {
        public static string Map(this string modelType)
        {
            return modelType switch
            {
                //TODO add more mlnet data types
                "TextSpan" => "string",
                "Single" => "float",
                _ => modelType,
            };
        }
    }
}
