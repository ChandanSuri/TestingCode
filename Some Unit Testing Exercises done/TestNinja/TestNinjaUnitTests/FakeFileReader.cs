using System;
using NUnit.Framework;
using TestNinja.Mocking;

// Not a part of production code
namespace TestNinjaUnitTests
{
    public class FakeFileReader : IFileReader
    {
        public string Read(string path)
        {
            return "";
        }
    }
}
