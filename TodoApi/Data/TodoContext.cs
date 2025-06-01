namespace TodoApi.Data
{
    using Microsoft.EntityFrameworkCore;
    using TodoApi.Models;

    /// <summary>
    /// EF Core database context for the Todo application.
    /// </summary>
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet for Todo items.
        /// </summary>
        public DbSet<TodoItem> TodoItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the table name
            modelBuilder.Entity<TodoItem>().ToTable("TodoItems");
        }
    }
}
