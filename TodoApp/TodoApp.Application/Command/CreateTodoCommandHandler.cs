using TodoApp.Domain;

namespace TodoApp.Application.Command;

public class CreateTodoCommandHandler
{

    private readonly IApplicationDbContext _context;

      public CreateTodoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
public async Task Handle(CreateTodoCommand command)
{
    var todo = new Todo(
        command.Title,
        command.Description,
        command.UserId



    );

    _context.Todos.Add(todo);

    await _context.SaveChangesAsync();
}
}
