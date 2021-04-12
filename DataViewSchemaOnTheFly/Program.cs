using System;
using GeneratedDataModels;

namespace DataViewSchemaOnTheFly
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new InputDataModel
            {
                Temperature = 24.4F,
                Luminosity = 33F,
                Infrared = 0F,
                Distance = 100,
                CreatedAt = "",
                Label = "FlashLight"
            };

            Console.WriteLine($"Temperature: {x.Temperature}");
            Console.WriteLine($"Luminosity: {x.Luminosity}");
            Console.WriteLine($"Infrared: {x.Infrared}");
            Console.WriteLine($"Distance: {x.Distance}");
            Console.WriteLine($"CreatedAt: {x.CreatedAt}");
            Console.WriteLine($"Label: {x.Label}");
        }
    }
}
