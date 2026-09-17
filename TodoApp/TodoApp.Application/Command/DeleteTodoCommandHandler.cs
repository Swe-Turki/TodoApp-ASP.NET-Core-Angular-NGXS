using TodoApp.Application;

namespace TodoApp.Application.Command;

public class DeleteTodoCommandHandler
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTodoCommand command)
    {
        var todo = await _context.Todos.FindAsync(command.TodoId);

        if (todo is null)
            throw new Exception("Todo not found");

        _context.Todos.Remove(todo);

        await _context.SaveChangesAsync();
    }
}