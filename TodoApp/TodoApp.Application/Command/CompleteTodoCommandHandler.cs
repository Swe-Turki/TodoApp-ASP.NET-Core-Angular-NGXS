using TodoApp.Application;

namespace TodoApp.Application.Command;

public class CompleteTodoCommandHandler
{
    private readonly IApplicationDbContext _context;

    public CompleteTodoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CompleteTodoCommand command)
    {
        var todo = await _context.Todos.FindAsync(command.TodoId);

        if (todo is null)
            throw new Exception("Todo not found");

        todo.Complete();

        await _context.SaveChangesAsync();
    }
}