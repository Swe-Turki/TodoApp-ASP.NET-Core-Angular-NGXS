import { RouterOutlet } from '@angular/router';
import { Store } from '@ngxs/store';
import { GetTodos, CreateTodo, CompleteTodo, DeleteTodo} from './store/actions/todo.actions';
import { TodoState } from './store/todo.state';
import { AsyncPipe } from '@angular/common';
import { Component, Signal } from '@angular/core';
import { Todo } from './models/todo';


@Component({
  selector: 'app-root',
imports: [RouterOutlet, AsyncPipe],
templateUrl: './app.html',
styleUrl: './app.css'
})

export class App {

  isCreateModalOpen = false;
  title = '';
  description = '';


totalTodos!: Signal<number>;
completedTodos!: Signal<number>;
activeTodos!: Signal<number>;
todos!: Signal<Todo[]>;
constructor(private store: Store) {
  this.store.select(TodoState.todos).subscribe(todos => {
    console.log('TODOS FROM STORE:', todos);
    this.totalTodos = this.store.selectSignal(TodoState.totalTodos);
this.completedTodos = this.store.selectSignal(TodoState.completedTodos);
this.activeTodos = this.store.selectSignal(TodoState.activeTodos);
this.todos = this.store.selectSignal(TodoState.todos);
  });

  this.store.dispatch(new GetTodos());
}

createTodo() {
  this.isCreateModalOpen = false;

  this.store.dispatch(
    new CreateTodo(this.title, this.description)
  );

  this.title = '';
  this.description = '';
}

  cancelCreate() {
    this.isCreateModalOpen = false;

    this.title = '';
    this.description = '';
  }



completeTodo(id: number) {
  this.store.dispatch(new CompleteTodo(id));
}

deleteTodo(id: number) {
  this.store.dispatch(new DeleteTodo(id));
}

}