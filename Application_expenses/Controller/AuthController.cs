// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Application_expenses.Models; 
using Microsoft.EntityFrameworkCore;
using Application_expenses.Contexts;  // Ajoutez cette directive pour utiliser MyDbContext

namespace Application_expenses.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AuthController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null || user.Password != request.Password)
            {
                return Unauthorized("Invalid username or password");
            }

            // Retourner un message de succès si l'utilisateur existe et les identifiants sont valides
            // Enregistrer l'ID de l'utilisateur dans la session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            //int? userId = HttpContext.Session.GetInt32("UserId");
            //Console.WriteLine($"Utilisateur connecté : {userId}");
            return Ok(new { message = "Login successful" });
        }

        [HttpGet("check-session")]
public IActionResult CheckSession()
{
     int? userId = HttpContext.Session.GetInt32("UserId");
    if (userId != null)
    {
        return Ok(new { sessionActive = true });
    }

    return Unauthorized(new { message = "Session expired or user not logged in" });
}


    }
}
