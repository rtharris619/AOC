using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_Shared
{
    internal class EnvReader
    {
        public static void Load(string filePath)
        {
            if (!Path.IsPathRooted(filePath))
            {
                var projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
                filePath = Path.Combine(projectRoot, filePath);
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue; // skip empty lines and comments
                
                var parts = line.Split('=', 2);

                if (parts.Length != 2)
                    continue; // Skip lines that are not key-value pairs

                var key = parts[0].Trim();
                var value = parts[1].Trim();

                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }
}
