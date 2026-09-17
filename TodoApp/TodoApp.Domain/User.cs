namespace TodoApp.Domain;

public class User
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public ICollection<Todo> Todos { get; set; }



   public User(string name, string email, string passwordHash)
{
    Name = name;
    Email = email;
    PasswordHash = passwordHash;
    Todos = new List<Todo>();
}
    public void ChangeName(string newName)
    {
        Name = newName;
    }

    public void ChangeEmail(string newEmail)
    {
        Email = newEmail;
    }
}