using NUnit.Framework;

namespace Algorithms.Tests;

public class IntegerTests
{
    [TestCase(0, 1)]
    [TestCase(1, 1)]
    [TestCase(100, 3)]
    [TestCase(10000, 5)]
    [TestCase(9223372036854775807, 19)]
    public void NumberOfDigitsTest(long num, int expected)
    {
        Assert.AreEqual(expected, Integer.NumberOfDigits(num));
    }

    [TestCase(-1, 1)]
    [TestCase(-100, 3)]
    [TestCase(-10000, 5)]
    [TestCase(-1000000, 7)]
    [TestCase(-9223372036854775808, 19)]
    public void NumberOfDigitsOfNegativeNumberTest(long num, int expected)
    {
        Assert.AreEqual(expected, Integer.NumberOfDigits(num));
    }
}