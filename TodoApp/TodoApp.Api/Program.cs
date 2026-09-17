using Microsoft.EntityFrameworkCore;
using TodoApp.Application;
using TodoApp.Application.Command;
using TodoApp.Application.Query;
using TodoApp.Infrastructure;
using TodoApp.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddScoped<IApplicationDbContext, AppDbContext>();
builder.Services.AddScoped<CreateTodoCommandHandler>();
builder.Services.AddScoped<GetTodosQueryHandler>();
builder.Services.AddScoped<CompleteTodoCommandHandler>();
builder.Services.AddScoped<DeleteTodoCommandHandler>();
builder.Services.AddScoped<GetUsersWithTodosQueryHandlerLeft>();
builder.Services.AddScoped<GetUsersWithTodosQueryHandlerRight>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();
app.UseCors("Angular");
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    DataSeeder.Seed(db);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();