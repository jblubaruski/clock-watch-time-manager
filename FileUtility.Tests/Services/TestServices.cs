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
    }
}
