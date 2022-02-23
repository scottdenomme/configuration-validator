using System;
using SoftwarePackageDependencyValidator.Data;
using SoftwarePackageDependencyValidator.Data.Models;
using SoftwarePackageDependencyValidator.Data.Services;

namespace SoftwarePackageDependencyValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a Configuration File location: ");
            string filePath = Console.ReadLine();

            ParsedDataResults parsedDataResults = ConfigurationParsingService.ParseFileData(filePath);

            if (parsedDataResults == null)
                Console.WriteLine('\n' + "FAIL");
            else
            {
                bool result = ConfigurationValidationService.ValidateDependencies(parsedDataResults);
                if (result)
                    Console.WriteLine('\n' + "PASS");
                else
                    Console.WriteLine('\n' + "FAIL");
            }

            Console.WriteLine('\n' + "Press Enter key to continue: ");
            Console.ReadLine();
            return;
        }
    }
}
