namespace ValTech
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class StreetNumberFileParser : IParseFileToInt
    {
        private const char Separator = ' ';

        private readonly IConsoleProxy consoleProxy;

        public StreetNumberFileParser(IConsoleProxy consoleProxy)
        {
            this.consoleProxy = consoleProxy;
        }

        public bool TryParseFileToEnumerableOfInt(string filePath, out List<int> doorNumberEnumerable)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            
            doorNumberEnumerable = new List<int>();

            if (!File.Exists(filePath))
            {
                this.consoleProxy.PrintLine("Unable to find file.");
                return false;
            }

            try
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    doorNumberEnumerable.AddRange(
                        line.Split(Separator).Select(subString => Convert.ToInt32((string)subString)));
                }

                if (doorNumberEnumerable.Any())
                {
                    return true;
                }

                this.consoleProxy.PrintLine("Unable to find any valid routes");
                return false;
            }
            catch (FormatException)
            {
                doorNumberEnumerable = new List<int>();
                this.consoleProxy.PrintLine("Unable to parse file to valid house numbers");
                return false;
            }
            catch (Exception ex)
            {
                doorNumberEnumerable = new List<int>();
                this.consoleProxy.PrintLine($"Unexpected exception:{ex.Message}");
                return false;
            }

        }

    }
}