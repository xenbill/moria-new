using System;
using System.Collections.Generic;
using System.Linq;

namespace Categories
{
    /// <summary>
    /// Παλαιά (αρχική) υλοποίηση του υπολογισμού μορίων εξωτερικού.
    /// Διατηρείται ως αναφορά — ΔΕΝ είναι σύμφωνη με την ισχύουσα προδιαγραφή
    /// (4 κατηγορίες, ομοιόμορφα 12μηνα, συντελεστές -5/-10/-15/-40).
    /// Η ισχύουσα υλοποίηση βρίσκεται στην <see cref="Category"/>.
    /// </summary>
    public static class CategoryLegacy
    {
        #region Υπολογισμός Μορίων Εξωτερικού

        //Κατηγορίες αφαίρεσης μορίων για υπηρέτηση στο Εξωτερικό
        private static Dictionary<int, int> MoriaAnaCatigoria = new Dictionary<int, int>()
        {
            {1, -5},
            {2, -10},
            {3, -15},
            {4, -40}
        };

        public static decimal GetMoriaForEjoteriko(int totalmhnes, int mhnes, int CatID)
        {
            decimal result = 0;
            int restMonths = 0;
            int currentCatID = Math.DivRem(totalmhnes, 12, out restMonths);
            if (currentCatID > 0)
                CatID = CatID + currentCatID - 1;
            if (!MoriaAnaCatigoria.ContainsKey(CatID))
                CatID = MoriaAnaCatigoria.Last().Key;
            if (mhnes > 12)
            {
                result += ((12 - restMonths) * MoriaAnaCatigoria[CatID]);
                result += GetMoriaForEjoteriko(totalmhnes + restMonths, mhnes - (12 - restMonths), CatID + 1);
            }
            else
                result += mhnes * MoriaAnaCatigoria[CatID];

            //----------------------------------------------------------------
            ////Παλαιός τρόπος υπολογισμού. Κρατιέται για τυχόν διευκρινήσεις
            //if (mhnes <= 12)
            //{
            //    result = mhnes * -5;
            //}
            //else if (mhnes > 12 && mhnes <= 24)
            //{
            //    result = mhnes * -10;
            //}
            //else if (mhnes > 24 && mhnes <= 36)
            //{
            //    result = mhnes * -15;
            //}
            //else if (mhnes > 36)
            //{
            //    result = mhnes * -40;
            //}
            //----------------------------------------------------------------

            return result;
        }

        #endregion
    }
}
