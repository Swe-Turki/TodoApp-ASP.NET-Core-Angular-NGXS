namespace TodoApp.Application.Command;

public class CreateTodoCommand
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public int UserId {get; set;}
}