using SoftwarePackageDependencyValidator.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SoftwarePackageDependencyValidator.Data.Services
{
    public class ConfigurationParsingService
    {
        public ConfigurationParsingService()
        {

        }

        public static ParsedDataResults ParseFileData(string path)
        {
            ParsedDataResults parsedDataResults = new ParsedDataResults();
            int packageCount;
            int dependencyCount;

            string [] configurationFile = RetrieveFileData(path);

            if (configurationFile == null)
                return null;

            try
            {
                //First While Loop Gets ups to the first Line that describes the number of packages to install
                int counter = 0;
                while (counter < configurationFile.Length)
                {
                    if (configurationFile[counter] == "")
                        counter++;

                    if (!int.TryParse(configurationFile[counter], out _)) throw new Exception($"Invalid input on line {counter} in the file: '{path}'");
                    else
                        packageCount = int.Parse(configurationFile[counter]);

                    counter++;
                    
                    //This loop handles collecting all of the packages that need to be installed
                    for (int i = 0; i < packageCount; i++)
                    {
                        string[] packageDetails = configurationFile[counter].Split(',');
                        if (packageDetails.Length > 2) throw new Exception($"Invalid input on line {counter} in the file: '{path}'");
                        parsedDataResults.PackagesToInstall.Add(new Package( packageDetails[0], packageDetails[1]));
                        counter++;
                    }
                    
                    //Check if packages to install have dependencies
                    //Reached end of file there are no dependencies
                    if (counter >= configurationFile.Length)
                    {
                        break; 
                    }
                    else
                    {
                        if (!int.TryParse(configurationFile[counter], out _)) throw new Exception($"Invalid input on line {counter} in the file: '{path}'");
                        else
                            dependencyCount = int.Parse(configurationFile[counter]);
                        counter++;
                    }

                    for (int i = 0; i < dependencyCount; i++)
                    {
                        string[] packageDetails = configurationFile[counter].Split(',');
                        if (packageDetails.Length > 4)
                            return null;
                            //{throw new Exception($"Invalid input on line {counter} in the file: '{path}'");
                        Package dependencyPackage = new Package(packageDetails[0], packageDetails[1]);
                        dependencyPackage.Dependency = new Package(packageDetails[2], packageDetails[3]);
                        parsedDataResults.NeededPackageDependencies.Add(dependencyPackage);
                        counter++;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"File Parser Error: {ex.Message}");
            }
            return parsedDataResults;
        }

        public static string[] RetrieveFileData(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: '{path}' does not exist.");
                return null;
            }

            string [] configurationFile = File.ReadAllLines(path).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

            if (configurationFile.Length == 0)
            {
                Console.WriteLine($"File doesnt contain data: '{path}'.");
                return null;
            }

            return configurationFile;
        }
    }
}
