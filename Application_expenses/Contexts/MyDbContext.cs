// MyDbContext.cs
using Microsoft.EntityFrameworkCore;
using Application_expenses.Models;

namespace Application_expenses.Contexts  // Assurez-vous que ce namespace est le bon
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Budget> Budgets { get; set; } 
    }
}
