using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    // I wonder if filesizes will ever be big enough to use a stream with a delegate in the while or something
    public class FileHandler
    {
        public static List<int> GetFileContentsAsNumbers(string fileLocation)
        {
            var values = new List<int>();
            using var fileStream = File.OpenRead(fileLocation);
            using var sr = new StreamReader(fileStream);
            var line = "";
            while ((line = sr.ReadLine()) != null)
            {
                values.Add(int.Parse(line));
            }

            return values;
        }

        public static IEnumerable<int> GetNumbersFromCommaSeparatedSingleLine(string fileLocation)
        {
            var file = GetFileContentsAsStrings(fileLocation);
            var values = file.First().Split(',');

            foreach (var value in values)
            {
                yield return int.Parse(value);
            }
        }

        public static List<string> GetFileContentsAsStrings(string fileLocation)
        {
            var values = new List<string>();
            using var fileStream = File.OpenRead(fileLocation);
            using var sr = new StreamReader(fileStream);
            var line = "";
            while ((line = sr.ReadLine()) != null)
            {
                values.Add(line);
            }

            return values;
        }

        public static List<string> GetFileContentsWithBlankLineSeparator(string fileLocation)
        {
            var values = new List<string>();
            using var fileStream = File.OpenRead(fileLocation);
            using var sr = new StreamReader(fileStream);
            var fullLine = new StringBuilder();
            var line = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                {
                    values.Add(fullLine.ToString());
                    fullLine.Clear();
                }
                else
                {
                    fullLine.Append($" {line}");
                }
            }

            return values;
        }

        public static IEnumerable<string> GetFileContentsWithDelimiter(string fileLocation, char delimiter)
        {
            var file = GetFileContentsAsStrings(fileLocation);
            var values = file.First().Split(delimiter);

            foreach (var value in values)
            {
                yield return value;
            }
        }
    }
}