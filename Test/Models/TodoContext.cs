using Microsoft.EntityFrameworkCore;
namespace Test.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }
        public DbSet<Todoitem> TodoItems { get; set; } = null!;
    }
}
