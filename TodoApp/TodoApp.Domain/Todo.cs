

namespace TodoApp.Domain;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public User User { get; set; }
    public int? UserId { get; set; }




    public Todo(string title, string? description , int? userId)
    {
        Title = title;
        Description = description;
        UserId = userId;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }
    public void Complete()
    {
        IsCompleted = true ;
    
    }




};



