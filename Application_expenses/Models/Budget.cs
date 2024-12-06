namespace Application_expenses.Models

{
public class Budget
{
    public int Id { get; set; }
    public required decimal Amount { get; set; }  // Le budget pour le mois
    
    public required DateTime Month { get; set; }  // Le mois du budget (par exemple, janvier 2024)
     // Ajout de UserId pour lier le budget à un utilisateur
    public required int UserId { get; set; }  // Identifiant de l'utilisateur (clé étrangère)
        
     // Navigation vers l'utilisateur associé au budget
    //public required User User { get; set; }   // Représente la relation avec la classe User
}
}
