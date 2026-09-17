using Bogus;
using TodoApp.Domain;

namespace TodoApp.Infrastructure.Data;

public static class DataSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Users.Any())
            return;

        // Create 20 random users
        var users = new Faker<User>()
            .CustomInstantiator(f =>
                new User(
                    f.Name.FullName(),
                    f.Internet.Email(),
                    "fake-hash"
                ))
            .Generate(20);

        db.Users.AddRange(users);
        db.SaveChanges();

        // Randomly select between 5 and 15 users
        // These users will have todos
        var selectedUsers = users
            .OrderBy(x => Guid.NewGuid())
            .Take(Random.Shared.Next(5, 16))
            .ToList();

        // Create 100 random todos
        var todos = new Faker<Todo>()
            .CustomInstantiator(f =>
            {
                int? userId = f.Random.Bool()
                    ? f.PickRandom(selectedUsers).Id
                    : null;

                return new Todo(
                    f.Lorem.Sentence(),
                    f.Lorem.Sentence(),
                    userId
                );
            })
            .Generate(100);

        db.Todos.AddRange(todos);
        db.SaveChanges();
    }
}