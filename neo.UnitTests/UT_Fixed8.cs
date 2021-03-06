using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Neo.UnitTests
{
    [TestClass]
    public class UT_Fixed8
    {
        [TestMethod]
        public void Basic_equality()
        {
            var a = Fixed8.FromDecimal(1.23456789m);
            var b = Fixed8.Parse("1.23456789");
            a.Should().Be(b);
        }

        [TestMethod]
        public void Can_parse_exponent_notation()
        {
            Fixed8 expected = Fixed8.FromDecimal(1.23m);
            Fixed8 actual = Fixed8.Parse("1.23E-0");
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Can_multiply_with_presicion()
        {
            decimal a = 123456789m;
            decimal b = 1.23456789m;
            decimal expected = a * b;
            Fixed8 af = Fixed8.FromDecimal(123456789m);
            Fixed8 bf = Fixed8.FromDecimal(1.23456789m);
            Fixed8 actual = af * bf;
            ((decimal)actual).Should().Be(expected);
        }

        [TestMethod]
        public void Can_multiply_without_overflow()
        {
            decimal a = Math.Round(((decimal)Fixed8.MaxValue - 1m) / 2m, 8);
            decimal b = 2m;
            decimal expected = a * b;
            Fixed8 af = Fixed8.FromDecimal(a);
            Fixed8 bf = Fixed8.FromDecimal(b);
            Fixed8 actual = af * bf;
            ((decimal)actual).Should().Be(expected);
        }

        [TestMethod]
        public void Sort()
        {
            Fixed8[] numbers = new[]
            {
                Fixed8.FromDecimal(5),
                Fixed8.FromDecimal(7),
                Fixed8.FromDecimal(2),
                Fixed8.FromDecimal(4),
                Fixed8.FromDecimal(1),
                Fixed8.FromDecimal(8),
                Fixed8.FromDecimal(9),
                Fixed8.FromDecimal(0),
                Fixed8.FromDecimal(3),
                Fixed8.FromDecimal(6)
            };
            numbers = numbers.OrderBy(p => p).ToArray();
            for (int i = 0; i < numbers.Length; i++)
                numbers[i].Should().Be(Fixed8.FromDecimal(i));
        }
    }
}