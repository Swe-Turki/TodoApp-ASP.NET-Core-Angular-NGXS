using Microsoft.EntityFrameworkCore;
using TodoApp.Domain;

namespace TodoApp.Application;

public interface IApplicationDbContext
{
    DbSet<Todo> Todos { get; }

    DbSet<User> Users { get; }

   Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}