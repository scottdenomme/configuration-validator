using SoftwarePackageDependencyValidator.Data.Services;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Reflection;
using SoftwarePackageDependencyValidator.Data.Models;

namespace Data.Tests
{
    public class ParsingServiceTests
    {
        [Fact]
        public void ValidFileDataRetrievalTest()
        {
            //Arrange
            string[] validDataFromFile = new string[]{ "2", "A,1", "B,1", "3", "A,1,B,1", "A,2,B,2 ", "C,1,B,1" };
            
            //Act
            string[] testFileDataFromMethod = ConfigurationParsingService.RetrieveFileData("./Resources/testData1.txt");
            
            //Assert
            Assert.Equal(validDataFromFile, testFileDataFromMethod);
        }

        [Fact]
        public void ValidFileParseTest()
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
            ParsedDataResults testDataFromFile = ConfigurationParsingService.ParseFileData("./Resources/testData1.txt");

            Console.WriteLine("PAUSE");

            //Assert
            bool valid = true;

            for (int i = 0; i < testDataFromFile.PackagesToInstall.Count; i++)
            {
                if (!testDataFromFile.PackagesToInstall[i].Equals(validDataFromFile.PackagesToInstall[i]))
                {
                    valid = false;
                }
            }

            for (int i = 0; i < testDataFromFile.NeededPackageDependencies.Count; i++)
            {
                if (!testDataFromFile.NeededPackageDependencies[i].Equals(validDataFromFile.NeededPackageDependencies[i]))
                {
                    valid = false;
                }
            }

            Assert.True(valid);
            Console.WriteLine("PAUSE");
        }
    }
}
