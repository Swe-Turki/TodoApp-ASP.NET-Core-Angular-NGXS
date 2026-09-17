using Microsoft.EntityFrameworkCore;
using TodoApp.Application.DTO;
using TodoApp.Domain;

namespace TodoApp.Application.Query;

public class GetUsersWithTodosQueryHandlerRight
{
        private readonly IApplicationDbContext _context;
    public GetUsersWithTodosQueryHandlerRight(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> Handle(GetUsersWithTodosQueryRight Query)
    {
        var result = await (
         from todo in _context.Todos
         join user in _context.Users
         on todo.UserId equals user.Id into todoUser
         from user in todoUser.DefaultIfEmpty()
         select new TodoUserDto
         {
             Title = todo.Title,
             Description = todo.Description,
             IsCompleted = todo.IsCompleted,
             CreatedAt = todo.CreatedAt,
             userId = user != null ? user.Id : null,
             UserName = user != null ? user.Name : null
         }
        ).ToListAsync();

        return result ;

    }
}
