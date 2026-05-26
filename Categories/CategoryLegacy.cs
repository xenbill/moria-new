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

        // ── Πώς λειτουργεί (και πού διαφέρει από την προδιαγραφή) ────────
        // Η παλιά λογική θεωρεί ΟΜΟΙΟΜΟΡΦΑ 12μηνα και μόνο 4 κατηγορίες. Ο
        // υπολογισμός είναι κι εδώ προοδευτικός, αλλά γίνεται ΜΕ ΑΝΑΔΡΟΜΗ:
        // κόβει ένα 12μηνο στην τρέχουσα κατηγορία και ξανακαλεί τον εαυτό
        // της για τους υπόλοιπους μήνες στην επόμενη κατηγορία.
        //
        // Παράμετροι:
        //   totalmhnes : πόσοι μήνες έχουν ήδη "καταναλωθεί" (για συνέχιση).
        //                Στην κανονική κλήση ξεκινά από 0.
        //   mhnes      : πόσοι μήνες απομένουν να υπολογιστούν.
        //   CatID      : τρέχουσα κατηγορία (ξεκινά από 1).
        //
        // ΣΗΜ.: το μπλοκ με το DivRem ενεργοποιείται μόνο όταν totalmhnes > 0.
        // Στην πραγματική κλήση GetMoriaForEjoteriko(0, μήνες, 1) είναι αδρανές.

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
            // Προσθήκη (εκτός αρχικού κώδικα): αρνητικοί μήνες -> 0, ώστε να μην
            // προκύπτει θετικό αποτέλεσμα (αρνητικό × αρνητικό = θετικό).
            if (mhnes < 0)
                return 0;

            decimal result = 0;
            int restMonths = 0;

            // DivRem: currentCatID = πόσα ΠΛΗΡΗ 12μηνα έχουν περάσει (totalmhnes / 12),
            // restMonths = το υπόλοιπο των μηνών (totalmhnes % 12). Χρησιμεύει για
            // "συνέχιση" όταν ξεκινάμε με ήδη συμπληρωμένους μήνες.
            int currentCatID = Math.DivRem(totalmhnes, 12, out restMonths);

            // Αν έχουν ήδη περάσει πλήρη έτη, μετατοπίζουμε ανάλογα την κατηγορία.
            if (currentCatID > 0)
                CatID = CatID + currentCatID - 1;

            // Πλαφόν: αν προκύψει κατηγορία που δεν υπάρχει (π.χ. 5η), πέφτουμε
            // στην τελευταία διαθέσιμη (την 4η, -40).
            if (!MoriaAnaCatigoria.ContainsKey(CatID))
                CatID = MoriaAnaCatigoria.Last().Key;

            if (mhnes > 12)
            {
                // Χρεώνουμε ό,τι απομένει από το τρέχον 12μηνο (12 - restMonths)
                // με τον συντελεστή της τρέχουσας κατηγορίας...
                result += ((12 - restMonths) * MoriaAnaCatigoria[CatID]);
                // ...και συνεχίζουμε αναδρομικά τους υπόλοιπους μήνες στην
                // επόμενη κατηγορία (CatID + 1).
                result += GetMoriaForEjoteriko(totalmhnes + restMonths, mhnes - (12 - restMonths), CatID + 1);
            }
            else
                // 12 μήνες ή λιγότεροι: όλοι με τον συντελεστή της τρέχουσας κατηγορίας.
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
