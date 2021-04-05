using GeneratedDataModels;
using System;

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
                Label = "FlashLight"
            };

            Console.WriteLine($"Distance: {x.Distance}");
        }
    }
}
