using Microsoft.EntityFrameworkCore;
using TodoApp.Application.DTO;
using TodoApp.Domain;

namespace TodoApp.Application.Query;

public class GetUsersWithTodosQueryHandlerLeft
{
    private readonly IApplicationDbContext _context;

    public GetUsersWithTodosQueryHandlerLeft(IApplicationDbContext context)
    {
        _context = context;
    }

   public async Task<object> Handle(GetUsersWithTodosQueryLeft query)
{
    var result = await (
        from user in _context.Users
        join todo in _context.Todos
            on user.Id equals todo.UserId into userTodos
        from todo in userTodos.DefaultIfEmpty()
            select new UserTodoDto
            {
                UserId = user.Id,
                UserName = user.Name,

                TodoId = todo != null ? todo.Id : null,
                TodoTitle = todo != null ? todo.Title : null,
                TodoDescription = todo != null ? todo.Description : null,
                IsCompleted = todo != null ? todo.IsCompleted : null
            }
    ).ToListAsync();

    return result;
}


}