using DataModelsGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace DataModelsGenerator.Helpers
{
    public static class StreamHelper
    {
        public static Stream GetZipFileStream(string zipPath)
        {
            var zip = ZipFile.OpenRead(zipPath)
                .Entries.Where(x => x.Name.Equals("Schema", StringComparison.InvariantCulture))
                .FirstOrDefault()
                .Open();

            return zip;
        }

        public static IEnumerable<Feature> ExtractFeatures(BinaryReader reader)
        {
            var features = new List<Feature>();

            //TODO see schema binary file format
            var index = 0;
            reader.ReadBytes(256);
            for (int i = 0; i < 6; i++)
            {
                var name = reader.ReadString();
                var type = reader.ReadString();
                features.Add(new Feature
                {
                    Name = name,
                    Type = type,
                    Index = index++
                });
                reader.ReadBytes(19);
            }

            return features;
        }
    }
}
