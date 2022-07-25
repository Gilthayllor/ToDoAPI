using Microsoft.EntityFrameworkCore;
using ToDoAPI.Entities;

namespace ToDoAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<ToDoEntity> ToDos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=ToDoDB;Cache=Shared");
        }
    }
}
