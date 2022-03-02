using System;
using Xunit;
using SoftwarePackageDependencyValidator.Data.Services;
using SoftwarePackageDependencyValidator.Data.Models;
using System.Collections.Generic;

namespace Data.Tests
{
    public class ValidationServiceTests
    {
        [Fact]
        public void ValidValidationResult()
        {
            //Arrange
            List<Package> samplePackageList = new List<Package>();
            samplePackageList.Add(new Package("A", "1"));
            samplePackageList.Add(new Package("B", "1"));

            List<Package> sampleDependencyList = new List<Package>();
            sampleDependencyList.Add(new Package("A", "1")
            {
                Dependency = new Package("B", "1")
            });
            sampleDependencyList.Add(new Package("A", "2")
            {
                Dependency = new Package("B", "2")
            });
            sampleDependencyList.Add(new Package("C", "1")
            {
                Dependency = new Package("B", "1")
            });

            ParsedDataResults validDataFromFile = new ParsedDataResults()
            {
                PackagesToInstall = samplePackageList,
                NeededPackageDependencies = sampleDependencyList
            };

            //Act
            bool pass = ConfigurationValidationService.ValidateDependencies(validDataFromFile);

            //Assert
            Assert.True(pass);
        }
    }
}