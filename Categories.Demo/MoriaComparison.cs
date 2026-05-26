using System.Collections.Generic;

namespace Categories.Demo
{
    /// <summary>
    /// Συγκρίνει την παλαιά (<see cref="CategoryLegacy"/>) με τη νέα
    /// (<see cref="Category"/>) υλοποίηση για ένα εύρος μηνών.
    /// </summary>
    public static class MoriaComparison
    {
        public static IList<MoriaRow> Build(int fromMonths, int toMonths)
        {
            var rows = new List<MoriaRow>();
            for (int months = fromMonths; months <= toMonths; months++)
            {
                decimal before = CategoryLegacy.GetMoriaForEjoteriko(0, months, 1);
                decimal after = Category.GetMoriaForEjoteriko(months);
                rows.Add(new MoriaRow(months, before, after));
            }
            return rows;
        }
    }
}
