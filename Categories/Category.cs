namespace Categories
{
    public static class Category
    {
        #region Υπολογισμός Μορίων Εξωτερικού

        // Αρνητική μοριοδότηση υπηρεσίας στο Εξωτερικό / Κύπρο, ανά κατηγορία:
        //   1)  0-6   μήνες:  1   μόριο/μήνα
        //   2)  6-12  μήνες:  1,5 μόρια/μήνα
        //   3) 12-24  μήνες:  2   μόρια/μήνα
        //   4) 24-36  μήνες:  4   μόρια/μήνα
        //   5) 36+    μήνες:  6   μόρια/μήνα
        // Ο υπολογισμός είναι προοδευτικός: κάθε μήνας μοριοδοτείται με τον
        // συντελεστή της κατηγορίας στην οποία ανήκει.

        // Πλάτος κάθε κατηγορίας σε μήνες (int.MaxValue = ανοιχτή κατηγορία 36+).
        private static readonly int[] CatMonths = { 6, 6, 12, 12, int.MaxValue };

        // Μόρια ανά μήνα για κάθε κατηγορία (αρνητικά).
        private static readonly decimal[] CatRate = { -1m, -1.5m, -2m, -4m, -6m };

        /// <summary>
        /// Επιστρέφει τα (αρνητικά) μόρια για <paramref name="mhnes"/> μήνες
        /// υπηρεσίας στο εξωτερικό, υπολογισμένα προοδευτικά ανά κατηγορία.
        /// </summary>
        public static decimal GetMoriaForEjoteriko(int mhnes)
        {
            decimal result = 0m;
            for (int i = 0; i < CatRate.Length && mhnes > 0; i++)
            {
                int band = mhnes < CatMonths[i] ? mhnes : CatMonths[i];
                result += band * CatRate[i];
                mhnes -= band;
            }
            return result;
        }

        #endregion
    }
}
