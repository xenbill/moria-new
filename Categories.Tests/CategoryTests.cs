using System.Collections.Generic;
using Xunit;

namespace Categories.Tests
{
    public class CategoryTests
    {
        // Expected totals hand-derived from the spec brackets:
        //   0-6 mo: -1/mo, 6-12: -1.5/mo, 12-24: -2/mo, 24-36: -4/mo, 36+: -6/mo
        // applied progressively (each month in its own band).
        public static IEnumerable<object[]> ProgressiveCases => new[]
        {
            new object[] { 0,    0m },
            new object[] { 1,   -1m },
            new object[] { 5,   -5m },
            new object[] { 6,   -6m },   // end of band 1
            new object[] { 7,   -7.5m }, // -6 + 1 * -1.5
            new object[] { 12, -15m },   // -6 + 6 * -1.5  (end of band 2)
            new object[] { 13, -17m },   // -15 + 1 * -2
            new object[] { 18, -27m },   // -15 + 6 * -2
            new object[] { 24, -39m },   // -15 + 12 * -2  (end of band 3)
            new object[] { 25, -43m },   // -39 + 1 * -4
            new object[] { 30, -63m },   // -39 + 6 * -4
            new object[] { 36, -87m },   // -39 + 12 * -4  (end of band 4)
            new object[] { 37, -93m },   // -87 + 1 * -6
            new object[] { 40, -111m },  // -87 + 4 * -6
            new object[] { 48, -159m },  // -87 + 12 * -6
        };

        [Theory]
        [MemberData(nameof(ProgressiveCases))]
        public void GetMoriaForEjoteriko_ReturnsProgressiveTotal(int months, decimal expected)
        {
            decimal result = Category.GetMoriaForEjoteriko(months);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetMoriaForEjoteriko_NegativeMonths_ReturnsZero()
        {
            Assert.Equal(0m, Category.GetMoriaForEjoteriko(-5));
        }
    }
}
