using FileUtility.Tests.Derivations;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.Services
{
    internal class TestServices
    {
        // closes any potentially open files and deletes any created directories
        public static void RunTestCleanup(DerivedTextFileManager testManager, string directoryPath)
        {
            testManager.ExposedReader?.Close();
            testManager.ExposedWriter?.Close();
            Directory.Delete(directoryPath, true);
        }
    }
}
