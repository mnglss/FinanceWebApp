namespace Application.Models
{
    public static class Category
    {
        public static Dictionary<string, string> Index { get; } = new Dictionary<string, string>() {
            { "Stipendio", "rgba(17, 176, 5, 0.5)" },
            { "Mutuo", "rgba(176, 115, 5, 0.5)" },
            { "Bollette", "rgba(176, 5, 159, 0.5)" },
            { "Veicoli", "rgba(13, 5, 176, 0.5)" },
            { "Alimentari", "rgba(5, 176, 163, 0.5)" },
            { "Telefono", "rgba(171, 176, 5, 0.5)" },
            { "Internet", "rgba(148, 225, 219, 0.5)" },
            { "Abbonamenti", "rgba(211, 148, 225, 0.5)" },
            { "Finanziamenti", "rgba(211, 148, 225, 0.5)" }

        };
    }
}
