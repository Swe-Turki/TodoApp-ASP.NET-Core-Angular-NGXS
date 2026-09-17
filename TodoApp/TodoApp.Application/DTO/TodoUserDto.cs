namespace TodoApp.Application.DTO;

public class TodoUserDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? userId { get; set; }
    public string? UserName { get; set; }
}
