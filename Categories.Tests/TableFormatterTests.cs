using System.Collections.Generic;
using Categories.Demo;
using Xunit;

namespace Categories.Tests
{
    public class TableFormatterTests
    {
        [Fact]
        public void Format_RendersBorderedTableWithRightAlignedCells()
        {
            var headers = new[] { "Months", "Before", "After" };
            var rows = new List<string[]>
            {
                new[] { "0", "0", "0" },
                new[] { "1", "-5", "-1" },
            };

            string table = TableFormatter.Format(headers, rows);

            string expected = string.Join("\n", new[]
            {
                "+--------+--------+-------+",
                "| Months | Before | After |",
                "+--------+--------+-------+",
                "|      0 |      0 |     0 |",
                "|      1 |     -5 |    -1 |",
                "+--------+--------+-------+",
            });
            Assert.Equal(expected, table);
        }
    }
}
