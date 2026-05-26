using System.Collections.Generic;
using Xunit;

namespace Categories.Tests
{
    // Characterization tests: lock in the behaviour of the ORIGINAL implementation
    // (4 categories -5/-10/-15/-40, uniform 12-month buckets). These document what
    // the legacy code DOES, including its quirks — not what the spec wants.
    public class CategoryLegacyTests
    {
        // The real call pattern from the app: GetMoriaForEjoteriko(0, months, 1).
        public static IEnumerable<object[]> FreshCallCases => new[]
        {
            new object[] { 0,     0m },
            new object[] { 6,   -30m },  // 6 * -5
            new object[] { 12,  -60m },  // 12 * -5
            new object[] { 13,  -70m },  // 12*-5 + 1*-10
            new object[] { 24, -180m },  // 12*-5 + 12*-10
            new object[] { 36, -360m },  // + 12*-15
            new object[] { 48, -840m },  // + 12*-40
            new object[] { 50, -920m },  // 5th chunk falls back to category 4 (capped)
        };

        [Theory]
        [MemberData(nameof(FreshCallCases))]
        public void GetMoriaForEjoteriko_FreshCall_MatchesLegacyTotals(int months, decimal expected)
        {
            decimal result = CategoryLegacy.GetMoriaForEjoteriko(0, months, 1);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetMoriaForEjoteriko_NonZeroStart_ShiftsStartingCategory()
        {
            // Starting after 24 accumulated months begins in category 2 (-10/mo),
            // documenting the (otherwise unused) DivRem resume path.
            decimal result = CategoryLegacy.GetMoriaForEjoteriko(24, 12, 1);

            Assert.Equal(-120m, result);
        }
    }
}
