using TodoApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Application.Query;


public class GetTodosQueryHandler
{
    private readonly IApplicationDbContext _context;

    public GetTodosQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Todo>> Handle(GetTodosQuery query)
    {
        return await _context.Todos.ToListAsync();
    }
}