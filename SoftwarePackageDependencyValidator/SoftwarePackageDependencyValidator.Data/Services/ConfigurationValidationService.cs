using SoftwarePackageDependencyValidator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwarePackageDependencyValidator.Data.Services
{
    public class ConfigurationValidationService
    {
        public static List<Package> RequiredPackages { get; set; } = new List<Package>();

        public ConfigurationValidationService()
        {

        }

        //This method adds all of the packages to install to the required packages group then looks if there are dependencies
        public static bool ValidateDependencies(ParsedDataResults parsedDataResults)
        {
            ConfigurationValidationService configurationValidationService = new ConfigurationValidationService();
            foreach(Package package in parsedDataResults.PackagesToInstall)
            {
                if (!RequiredPackages.Any(p => p.Name == package.Name && p.Version == package.Version))
                {   
                    RequiredPackages.Add(new Package(package.Name, package.Version));
                    AddDependencies(package, parsedDataResults);
                }
            }

            if (RequiredPackages.GroupBy(p => p.Name).Where(g => g.Count() > 1).Select(r => r.Key).Any())
                return false;
            return true;
        }

        //This is a recursive method to retrieve any dependencies that exist on required packages
        public static void AddDependencies(Package package, ParsedDataResults parsedDataResults)
        {
            if (parsedDataResults.NeededPackageDependencies.Where(p => p.Name + p.Version == package.Name + package.Version).Any())
            {
                var newRequiredPackages = parsedDataResults.NeededPackageDependencies
                    .Where(p => p.Name + p.Version == package.Name + package.Version).ToList();
                foreach (Package newPackage in newRequiredPackages)
                {
                    if (!RequiredPackages.Where(p => p.Name + p.Version == newPackage.Dependency.Name + newPackage.Dependency.Version).Any())
                    { 
                        RequiredPackages.Add(new Package(newPackage.Dependency.Name, newPackage.Dependency.Version));
                        AddDependencies(newPackage.Dependency, parsedDataResults);
                    }
                    
                }
            }
        }

    }
}
