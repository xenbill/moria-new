using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Categories.Demo
{
    public static class Program
    {
        private const string Title =
            "Μόρια Εξωτερικού — σύγκριση παλαιάς/νέας υλοποίησης (μήνες 0-40)";

        public static void Main()
        {
            try { Console.OutputEncoding = System.Text.Encoding.UTF8; }
            catch { /* stdout redirected — αγνόησέ το */ }

            string report = BuildReport(0, 40);

            Console.WriteLine(report);
            Console.WriteLine();
            Console.Write("Εξαγωγή σε αρχείο .txt; (y/n): ");

            string answer = Console.ReadLine();
            if (answer != null && answer.Trim().StartsWith("y", StringComparison.OrdinalIgnoreCase))
            {
                string path = Path.GetFullPath("moria-comparison.txt");
                File.WriteAllText(path, report + Environment.NewLine);
                Console.WriteLine("Αποθηκεύτηκε: " + path);
            }
            else
            {
                Console.WriteLine("Δεν έγινε εξαγωγή.");
            }
        }

        // Title + ASCII table for the given month range. Reused for console and file
        // so both look identical.
        public static string BuildReport(int fromMonths, int toMonths)
        {
            var cells = new List<string[]>();
            foreach (MoriaRow row in MoriaComparison.Build(fromMonths, toMonths))
            {
                cells.Add(new[]
                {
                    row.Months.ToString(CultureInfo.InvariantCulture),
                    Format(row.Before),
                    Format(row.After)
                });
            }

            string table = TableFormatter.Format(
                new[] { "Months", "Before (legacy)", "After (spec)" }, cells);

            return Title + Environment.NewLine + Environment.NewLine + table;
        }

        // -7.5 not -7.50, 0 not 0.0
        private static string Format(decimal value)
        {
            return value.ToString("0.##", CultureInfo.InvariantCulture);
        }
    }
}
