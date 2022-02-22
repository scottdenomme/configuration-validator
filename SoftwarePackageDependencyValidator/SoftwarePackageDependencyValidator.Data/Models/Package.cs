using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwarePackageDependencyValidator.Data.Models
{
    public class Package : IEquatable<Package>
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public Package Dependency { get; set; }

        public Package(string name, string version)
        {
            if (string.IsNullOrEmpty((name))) throw new ArgumentNullException("Invalid null input for package name.");
            if (string.IsNullOrEmpty((version))) throw new ArgumentNullException("Invalid null input for version name.");

            // Trim front and back of lines in case any white spaces are accidentally added in config file
            Name = name.Trim();
            Version = version.Trim();
        }

        public bool Equals(Package otherPackage)
        {
            return this.Name == otherPackage.Name &&
                this.Version == otherPackage.Version;
        }
    }
}
