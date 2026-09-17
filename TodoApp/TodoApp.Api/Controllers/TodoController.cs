using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Command;
using TodoApp.Application.Query;


namespace TodoApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{

    private readonly CreateTodoCommandHandler _handler;
    private readonly GetTodosQueryHandler _queryHandler;
    private readonly CompleteTodoCommandHandler _completeHandler;
    private readonly DeleteTodoCommandHandler _deleteHandler;
    private readonly GetUsersWithTodosQueryHandlerLeft _getUserTodosLeftJoinHandler;
   private readonly GetUsersWithTodosQueryHandlerRight _getUserTodosRightJoinHandler;
    public TodoController(
    CreateTodoCommandHandler handler,
    GetTodosQueryHandler queryHandler,
    CompleteTodoCommandHandler completeHandler,
    DeleteTodoCommandHandler deleteHandler,
    GetUsersWithTodosQueryHandlerLeft getUserTodosLeftJoinHandler,
    GetUsersWithTodosQueryHandlerRight getUserTodosRightJoinHandler


    )
    {
    _handler = handler;
    _queryHandler = queryHandler;
    _completeHandler = completeHandler;
    _deleteHandler = deleteHandler;
        _getUserTodosLeftJoinHandler = getUserTodosLeftJoinHandler;
        _getUserTodosRightJoinHandler = getUserTodosRightJoinHandler;
    }

    
    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoCommand command)
    {
       await _handler.Handle(command);
        return Ok();
    }

    [HttpGet]
public async Task<IActionResult> GetTodos()
{
    var todos = await _queryHandler.Handle(new GetTodosQuery());

    return Ok(todos);
}

[HttpPut("{id}/complete")]
public async Task<IActionResult> Complete(int id)
{
    await _completeHandler.Handle(
        new CompleteTodoCommand
        {
            TodoId = id
        });

    return Ok();
}

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    await _deleteHandler.Handle(
        new DeleteTodoCommand
        {
            TodoId = id
        });

    return NoContent();
}

    [HttpGet("left-join")]
    public async Task<IActionResult> LeftJoin()
    {
       var result =  await _getUserTodosLeftJoinHandler.Handle(new GetUsersWithTodosQueryLeft());
        return Ok(result);
    }
    
    [HttpGet("right-join")]
public async Task<IActionResult> RightJoin()
{
    var result = await _getUserTodosRightJoinHandler
        .Handle(new GetUsersWithTodosQueryRight());

    return Ok(result);
}
}