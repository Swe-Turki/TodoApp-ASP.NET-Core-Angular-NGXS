using Microsoft.EntityFrameworkCore;
using TodoApp.Application;
using TodoApp.Domain;

namespace TodoApp.Infrastructure;

public class AppDbContext : DbContext, IApplicationDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Todo> Todos { get; set; }
}