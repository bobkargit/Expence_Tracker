using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // Pour les opérations asynchrones sur le contexte
using Application_expenses.Models;  // Remplacez par le namespace où votre modèle "Expense" est défini
using Application_expenses.Contexts;  // Ajoutez cette directive pour utiliser MyDbContext

namespace Application_expenses.Controllers  // Remplacez par le namespace de votre projet
{
    [ApiController]
    [Route("api/expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly MyDbContext _context;  // Utilisation de MyDbContext

        // Injection de dépendance pour MyDbContext
        public ExpensesController(MyDbContext context)
        {
            _context = context;
        }

        // Méthode GET pour récupérer toutes les dépenses
        [HttpGet("getExpense")]
        public async Task<IActionResult> GetExpenses()
        {
            // Récupération de l'identifiant utilisateur depuis la session
    int userId = 1; //HttpContext.Session.GetInt32("UserId");
     Console.WriteLine($"Utilisateur connecté11 : {userId}");
    if (userId == null)
    {
        return Unauthorized(new { message = "Session expired or user not logged in" });
    }

    // Utilisation de ToListAsync pour exécuter la requête de manière asynchrone
    var expenses = await _context.Expenses
        .Where(e => e.UserId == userId)
        .ToListAsync(); // Correct: méthode asynchrone

    // Affichage dans la console

    // Retour des dépenses au client
    return Ok(expenses);
   }

   [HttpGet("getBudget")]
        public async Task<IActionResult> GetBudget()
        {
            // Récupération de l'identifiant utilisateur depuis la session
    int userId = 1; //HttpContext.Session.GetInt32("UserId");
     Console.WriteLine($"Utilisateur connecté11 : {userId}");
    if (userId == null)
    {
        return Unauthorized(new { message = "Session expired or user not logged in" });
    }

    // Utilisation de ToListAsync pour exécuter la requête de manière asynchrone
    var budget = await _context.Budgets
        .Where(e => e.UserId == userId)
        .ToListAsync(); // Correct: méthode asynchrone

    // Affichage dans la console

    // Retour des dépenses au client
    return Ok(budget);
        }


        // Méthode POST pour ajouter une nouvelle dépense
[HttpPost("addExpense")]
public async Task<IActionResult> AddExpense([FromBody] Expense expense)
{
    // Vérification de la validité de l'objet Expense
    if (expense == null)
    {
        Console.WriteLine("Received null expense object.");
        return BadRequest("Expense object is null.");
    }

    // Log des données reçues
    Console.WriteLine($"Received Expense: Category = {expense.Category}, Amount = {expense.Amount}, Date = {expense.Date}, UserId = {expense.UserId}");

    // Récupérer l'utilisateur correspondant
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == expense.UserId);

    if (user == null)
    {
        Console.WriteLine($"User with ID {expense.UserId} not found.");
        return BadRequest($"User with ID {expense.UserId} not found.");
    }

    // Récupérer le budget de l'utilisateur
    var userBudget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == expense.UserId);

    if (userBudget == null)
    {
        Console.WriteLine($"Budget for User with ID {expense.UserId} not found.");
        return BadRequest($"Budget for User with ID {expense.UserId} not found.");
    }

    // Vérifier si le montant de la dépense dépasse le budget
    if (userBudget.Amount < expense.Amount)
    {
        Console.WriteLine($"Insufficient budget. Budget: {userBudget.Amount}, Expense: {expense.Amount}");
        return BadRequest("Insufficient budget for this expense.");
    }

    try
    {
        // Ajouter la dépense à la base de données
        _context.Expenses.Add(expense);

        // Mettre à jour le budget de l'utilisateur
        userBudget.Amount -= expense.Amount;

        // Sauvegarder les changements
        await _context.SaveChangesAsync();

        // Retourner une réponse indiquant que la dépense a été ajoutée avec succès
        return CreatedAtAction(nameof(GetExpenses), new { id = expense.Id }, expense);
    }
    catch (Exception ex)
    {
        // Log d'erreur
        Console.WriteLine($"Error: {ex.Message}");
        return StatusCode(500, "Internal server error.");
    }
}


       [HttpDelete("{id}")]
public async Task<IActionResult> DeleteExpense(int id)
{
    var expense = await _context.Expenses.FindAsync(id);
    var userBudget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == 1);
    
    if (expense == null)
    {
        return NotFound(); // Si l'élément n'est pas trouvé, retourner 404
    }
      userBudget.Amount += expense.Amount;
    _context.Expenses.Remove(expense); // Supprimer l'élément
    await _context.SaveChangesAsync(); // Appliquer les changements dans la base de données

    return NoContent(); // Retourner une réponse sans contenu (204)
}

[HttpPost("update-budget")]
public async Task<IActionResult> UpdateBudget([FromBody] BudgetUpdateDto dto)
{
    var userBudget = await _context.Budgets
        .FirstOrDefaultAsync(b => b.UserId == 1 );

    if (userBudget != null)
    {
        // Mise à jour du budget existant
        userBudget.Amount += dto.Amount; // Ajouter le montant
        _context.Budgets.Update(userBudget);
    }
    /*else
    {
        // Créer une nouvelle entrée de budget
        var newBudget = new Budget
        {
            UserId = dto.UserId,
            Amount = dto.Amount,
            Month = dto.Month
        };
        _context.Budgets.Add(newBudget);
    }*/

    await _context.SaveChangesAsync();
    return Ok(new { message = "Budget updated successfully" });
}

[HttpGet("categories")]
   public async Task<IActionResult> GetExpensesCategories()
    {
        var expenses = await _context.Expenses
            .GroupBy(e => e.Category)
            .Select(g => new
            {
                Category = g.Key,
                Total = g.Sum(e => e.Amount)
            })
            .ToListAsync();

        return Ok(expenses);
    }




    }
}
