using Microsoft.EntityFrameworkCore;
using KanbanBoard.Models;

public class TaskContext : DbContext
{
    public DbSet<KanbanTask> Tasks { get; set; }
    public DbSet<User> Users { get; set; }


    public string DbPath { get; }

    public TaskContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "tasks.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KanbanTask>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<User>()
           .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
