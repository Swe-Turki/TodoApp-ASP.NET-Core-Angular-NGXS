import { State, Action, StateContext, Selector } from '@ngxs/store';
import { Todo } from '../models/todo';
import { TodoService } from '../services/todo.service';
import { GetTodos, CreateTodo , CompleteTodo , DeleteTodo} from './actions/todo.actions';
import { Injectable } from '@angular/core';

export interface TodoStateModel {
  todos: Todo[];
}

@Injectable()
@State<TodoStateModel>({
  name: 'todos',
  defaults: {
    todos: []
  }
})

export class TodoState {

@Selector()
static todos(state: TodoStateModel) {
  return state.todos;
}

@Selector()
static totalTodos(state: TodoStateModel) {
  return state.todos.length;
}

@Selector()
static completedTodos(state: TodoStateModel) {
  return state.todos.filter(todo => todo.isCompleted).length;
}

@Selector()
static activeTodos(state: TodoStateModel) {
  return state.todos.filter(todo => !todo.isCompleted).length;
}
  constructor(private todoService: TodoService) {}

  @Action(GetTodos)
  getTodos(ctx: StateContext<TodoStateModel>) {

    return this.todoService.getTodos().subscribe({
      next: (todos) => {
        ctx.patchState({
          todos: todos
        });
      }
    });

  }

  @Action(CreateTodo)
createTodo(
  ctx: StateContext<TodoStateModel>,
  action: CreateTodo
) {
  return this.todoService.createTodo(
    action.title,
    action.description
  ).subscribe({
    next: () => {
      this.todoService.getTodos().subscribe({
        next: (todos) => {
          ctx.patchState({
            todos: todos
          });
        }
      });
    }
  });
}

@Action(CompleteTodo)
completeTodo(
  ctx: StateContext<TodoStateModel>,
  action: CompleteTodo
) {
  return this.todoService.completeTodo(action.id).subscribe({
    next: () => {
      const state = ctx.getState();

      ctx.patchState({
        todos: state.todos.map(todo =>
          todo.id === action.id
            ? { ...todo, isCompleted: true }
            : todo
        )
      });
    }
  });
}

@Action(DeleteTodo)
deleteTodo(
  ctx: StateContext<TodoStateModel>,
  action: DeleteTodo
) {
  return this.todoService.deleteTodo(action.id).subscribe({
    next: () => {
      const state = ctx.getState();

      ctx.patchState({
        todos: state.todos.filter(todo => todo.id !== action.id)
      });
    }
  });
}
}