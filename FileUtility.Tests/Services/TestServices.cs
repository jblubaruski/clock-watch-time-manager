using FileUtility.Tests.Derivations;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.Services
{
    internal class TestServices
    {
        // closes any potentially open files and deletes any created directories
        public static void RunTestCleanup(FileManager_Test testManager, string directoryPath)
        {
            try
            {
                testManager.ExposedReader?.Close();
            }
            catch (Exception) { }

            try
            {
                testManager.ExposedWriter?.Close();
            }
            catch (Exception) { }

            Directory.Delete(directoryPath, true);
        }

        // creates three closed test files names "a{extension}", "b{extension}", and "c{extension" in the passed in directory
        public static void CreateThreeClosedTestFiles(string directoryPath, string extension)
        {
            using (FileStream fs = File.Create(Path.Combine(directoryPath, "a" + extension))) { }
            using (FileStream fs = File.Create(Path.Combine(directoryPath, "b" + extension))) { }
            using (FileStream fs = File.Create(Path.Combine(directoryPath, "c" + extension))) { }
        }
    }
}
