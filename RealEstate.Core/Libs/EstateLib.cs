using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Helpers
{
    public static class EstateLib
    {
        public static string GetDataLocation(string currentDirectory, string dataFile)
        {
            var FilePath = Path.Combine(currentDirectory, "Data");
            // Check if dir must be created
            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);

            // add filename estates.json
            FilePath = Path.Combine(FilePath, dataFile);
            return Path.GetFullPath(FilePath);
        }
    }
}
