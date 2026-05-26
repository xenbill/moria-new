using System;
using System.IO;
using System.Text;

namespace MoriaConsole
{
    class Program
    {
        static readonly int[] CatMonths = { 6, 6, 12, 12, int.MaxValue };
        static readonly decimal[] CatRate = { -1m, -1.5m, -2m, -4m, -6m };

        static decimal GetMoriaForEjoteriko(int mhnes)
        {
            decimal result = 0m;
            for (int i = 0; i < CatRate.Length && mhnes > 0; i++)
            {
                // όσοι μήνες απομένουν, το πολύ όσο το πλάτος της ζώνης
                int band = mhnes < CatMonths[i] ? mhnes : CatMonths[i];
                result += band * CatRate[i];
                mhnes -= band;
            }
            return result;
        }

        static void Main(string[] args)
        {
            // Πίνακας Μήνες/Μόρια για 0..40.
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0,6}{1,8}", "Months", "Moria"));
            for (int m = 0; m <= 40; m++)
                sb.AppendLine(string.Format("{0,6}{1,8}", m, GetMoriaForEjoteriko(m).ToString("0.##")));

            string text = sb.ToString();
            Console.Write(text);                    // εμφάνιση στην κονσόλα
            File.WriteAllText("moria.txt", text);   // απλή εξαγωγή σε αρχείο
            Console.WriteLine("Saved: " + Path.GetFullPath("moria.txt"));
        }
    }
}
