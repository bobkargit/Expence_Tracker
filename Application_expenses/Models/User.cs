namespace Application_expenses.Models
{
    public class User
    {
        public  int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        public required ICollection<Expense> Expenses { get; set; } 
        // Relation un-à-plusieurs avec les budgets (un utilisateur a plusieurs budgets)
        public required ICollection<Budget> Budgets { get; set; }
    }
}