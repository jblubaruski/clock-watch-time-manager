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

        // overwrites the closed test .txt file with the passed in string array
        public static void FillClosedTextFileWithContentStringArray(TextFileManager_Test testManager, string[] content)
        {
            using StreamWriter writer = new(testManager.ExposedFilepath, false);
            
            foreach (string line in content)
            {
                writer.WriteLine(line);
            }
        }

        // reads and returns the content inside a .txt file as a string with each line ending in a new line character
        // returns null if the file is empty
        public static string? ReadClosedTextFile(TextFileManager_Test testManager)
        {
            using StreamReader reader = new(testManager.ExposedFilepath);

            string? line = reader.ReadLine();
            if (line == null)
                return null;

            string content = line + "\n";
            while ((line = reader.ReadLine()) != null)
            {
                content += line + "\n";
            }

            return content[..^1];
        }
    }
}
