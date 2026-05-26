using System.Collections.Generic;
using Categories.Demo;
using Xunit;

namespace Categories.Tests
{
    public class MoriaComparisonTests
    {
        [Fact]
        public void Build_ProducesOneRowPerMonthInclusive()
        {
            IList<MoriaRow> rows = MoriaComparison.Build(0, 40);

            Assert.Equal(41, rows.Count);
            Assert.Equal(0, rows[0].Months);
            Assert.Equal(40, rows[40].Months);
        }

        [Fact]
        public void Build_PairsLegacyBeforeWithCompliantAfter()
        {
            IList<MoriaRow> rows = MoriaComparison.Build(0, 40);

            // month 12: legacy 12 * -5 = -60 ; spec -6 + 6*-1.5 = -15
            Assert.Equal(-60m, rows[12].Before);
            Assert.Equal(-15m, rows[12].After);

            // month 40: legacy -520 ; spec -111
            Assert.Equal(-520m, rows[40].Before);
            Assert.Equal(-111m, rows[40].After);
        }
    }
}
