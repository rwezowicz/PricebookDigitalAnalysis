using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PricebookDigitalAnalysis
{
    public class Helper
    {
        public static List<string[]> ReadCsvFile(string file)
        {
            var parser = new TextFieldParser(new StringReader(file));
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");

            var lines = new List<string[]>();

            while (!parser.EndOfData)
            {
                lines.Add(parser.ReadFields());
            }

            parser.Close();

            return lines;
        }

        public static List<string> FormatOutput(List<Dealer> dealers)
        {
            var output = dealers
                        .Select(x => new
                        {
                            Dealer = $"{x.DealerName} ({x.DealerGUID})",
                            Test = x.ModelCounts
                                .Where(mc => mc.LineCount > 1)
                                .Select(x => new { x.ModelNumber, x.LineCount })
                        });

            var outputLines = new List<string>();
            foreach (var o in output)
            {
                outputLines.Add(o.Dealer);
                outputLines.Add("     Models that Appear Multiple Times:");
                foreach (var p in o.Test)
                {
                    outputLines.Add($"     {p.ModelNumber} ({p.LineCount})");
                }
                outputLines.Add(Environment.NewLine);
            }

            return outputLines;
        }
    }
}