namespace Application_expenses.Models

{
    public class Expense
    {
        public int Id { get; set; }         // Identifiant unique de l'expense (clé primaire)
        public string? Category { get; set; }  // Déclaration nullable // Catégorie de la dépense (par exemple : nourriture, transport, etc.)
        public required decimal Amount { get; set; }  // Montant de la dépense
        public required DateTime Date { get; set; }   // Date de la dépense

         // Ajout de UserId pour lier l'expense à un utilisateur
        public required int UserId { get; set; }  // Identifiant de l'utilisateur (clé étrangère)
        
        // Navigation vers l'utilisateur associé à la dépense
       // public required User User { get; set; }   // Représente la relation avec la classe User

         public Expense() { }
    }
}