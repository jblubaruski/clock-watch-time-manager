using FileUtility.Tests.Derivations;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.Services
{
    internal class TestServices
    {
        // removes a test directory
        public static void RemoveTestDirectory(string directoryPath) => Directory.Delete(directoryPath, true);

        // creates a closed empty file with the passed in base filename and extension in the passed in directory
        // doesnt do anything if the file already exist
        public static void CreateClosedEmptyTestFile(string baseFilename, string extension, string directoryPath)
        {
            string filepath = Path.Combine(directoryPath, baseFilename + extension);

            if (!File.Exists(filepath))
            {
                using FileStream fs = File.Create(filepath);
            }       
        }

        // overwrites the closed test .txt file with the passed in string
        public static void FillClosedTestFileWithContentString(TextFileManager_Test testManager, string content)
        {
            using StreamWriter writer = new(testManager.ExposedFilepath, false);
            writer.WriteLine(content);
        }

        // overwrites the closed test .txt file with the passed in string array
        public static void FillClosedTestFileWithContentStringArray(TextFileManager_Test testManager, string[] content)
        {
            using StreamWriter writer = new(testManager.ExposedFilepath, false);
            
            foreach (string line in content)
            {
                writer.WriteLine(line);
            }
        }
    }
}
