namespace Application.Models
{
    public class DashBoard
    {
        public double TotalIncome { get; set; } = 0;
        public double TotalOutcome { get; set; } = 0;
        public double Balance { get; set; }
        public double BalancePercentage { get; set; }
        public List<TotalCategory> TotalForCategory { get; set; } = [];

        public List<double> PieDataSource { get; set; } = [];
        public List<string> PieDataLabels { get; set; } = [];
        public List<string> PieDataColors { get; set; } = [];

    }

    public class TotalCategory()
    {
        public string Category { get; set; } = string.Empty;
        public double Amount { get; set; } = 0;
        public double CategoryPercentage { get; set; } = 0;
    }
}
