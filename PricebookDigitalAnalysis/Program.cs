using PricebookDigitalAnalysis.Services;
using System;
using System.IO;

namespace PricebookDigitalAnalysis
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Process Pricebook Digital File");

            if (args.Length == 0)
            {
                Console.WriteLine("No path passed for file argument");
            }
            else
            {
                var filePath = args[0];
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Processing:");
                Console.WriteLine($"{filePath}");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine($"* Reading File at {filePath}");
                var file = File.ReadAllText(filePath);

                Console.WriteLine($"* Processing CSV Fields");
                var fileLines = Helper.ReadCsvFile(file);

                var service = new DealerService();

                Console.WriteLine("* Building Dealers");
                var dealers = service.BuildDealers(fileLines);

                Console.WriteLine("* Format Output");
                var outputFormat = Helper.FormatOutput(dealers);

                var resultFilePath = filePath.Replace(Path.GetExtension(filePath), "_Analysis.txt");

                Console.WriteLine("* Saving Output");
                File.WriteAllLinesAsync(resultFilePath, outputFormat);

                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Results: ");
                Console.WriteLine($"{filePath}");
                Console.WriteLine("--------------------------------------");
            }

            Console.WriteLine("Press a Key to Close");
            Console.ReadKey();
        }

    }
}
