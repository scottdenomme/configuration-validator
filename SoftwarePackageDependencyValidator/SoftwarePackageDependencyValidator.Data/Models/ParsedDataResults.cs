using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwarePackageDependencyValidator.Data.Models
{
    public class ParsedDataResults
    {
        public List<Package> PackagesToInstall { get; set; }
        public List<Package> NeededPackageDependencies { get; set; }

        public ParsedDataResults()
        {
            PackagesToInstall = new List<Package>();
            NeededPackageDependencies = new List<Package>();
        }
    }
}
