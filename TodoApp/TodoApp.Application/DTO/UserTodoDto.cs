namespace TodoApp.Application.DTO;

public class UserTodoDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }

    public int? TodoId { get; set; }
    public string? TodoTitle { get; set; }
    public string? TodoDescription { get; set; }
    public bool? IsCompleted { get; set; }
}