using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.TextFileManagerTests
{
    /*
     * Tests for TextFileManager.ReadFile()
     * 
     * Lines are separated with newline characters.
     * If the file is empty, a null value is returned.
     * 
     * Throws an InvalidOperationException if the file is closed
     * Throws an InvalidOperationException if the file does not exist
     * Throws an InvalidOperationException if the file is write-only
     * 
     * Assert that calling ReadFile on a file that doesnt exist throws an InvalidOperationException
     * Assert that calling ReadFile on a write-only file throws an InvalidOperationException
     * Assert that calling ReadFile on an empty file in a valid state returns a null pointer
     * Assert that calling ReadFile on a file in a valid state and with content returns a string equal to that content
     */
    public class ReadFileTest
    {

    }
}
