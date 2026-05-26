namespace Categories.Demo
{
    /// <summary>Μία γραμμή σύγκρισης: μήνες, παλαιό αποτέλεσμα, νέο αποτέλεσμα.</summary>
    public struct MoriaRow
    {
        public int Months { get; }
        public decimal Before { get; }
        public decimal After { get; }

        public MoriaRow(int months, decimal before, decimal after)
        {
            Months = months;
            Before = before;
            After = after;
        }
    }
}
