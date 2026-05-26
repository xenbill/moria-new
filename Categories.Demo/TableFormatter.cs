using System.Collections.Generic;
using System.Text;

namespace Categories.Demo
{
    /// <summary>Renders a simple bordered ASCII table with right-aligned cells.</summary>
    public static class TableFormatter
    {
        public static string Format(string[] headers, IEnumerable<string[]> rows)
        {
            var allRows = new List<string[]>();
            allRows.Add(headers);
            foreach (var row in rows)
                allRows.Add(row);

            int columns = headers.Length;
            int[] widths = new int[columns];
            foreach (var row in allRows)
                for (int c = 0; c < columns; c++)
                    if (row[c].Length > widths[c])
                        widths[c] = row[c].Length;

            string border = Border(widths);
            var sb = new StringBuilder();
            sb.Append(border).Append('\n');
            sb.Append(Row(headers, widths)).Append('\n');
            sb.Append(border).Append('\n');
            foreach (var row in rows)
                sb.Append(Row(row, widths)).Append('\n');
            sb.Append(border);
            return sb.ToString();
        }

        private static string Border(int[] widths)
        {
            var sb = new StringBuilder("+");
            foreach (int w in widths)
                sb.Append(new string('-', w + 2)).Append('+');
            return sb.ToString();
        }

        private static string Row(string[] cells, int[] widths)
        {
            var sb = new StringBuilder("|");
            for (int c = 0; c < widths.Length; c++)
                sb.Append(' ').Append(cells[c].PadLeft(widths[c])).Append(" |");
            return sb.ToString();
        }
    }
}
