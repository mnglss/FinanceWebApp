namespace Domain.Enum
{
    public static class MovementType
    {
        public const string Income = "Income";
        public const string Expense = "Expense";

        public static string[] GetAllTypes()
        {
            return new[] { Income, Expense };
        }
    }
}