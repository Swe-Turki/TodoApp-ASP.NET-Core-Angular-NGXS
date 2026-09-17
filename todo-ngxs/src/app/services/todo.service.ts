import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Todo } from '../models/todo';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  constructor(private http: HttpClient) {}

  getTodos() {
  return this.http.get<Todo[]>('http://localhost:5001/api/Todo');
}

createTodo(title: string, description: string) {
  return this.http.post<Todo>(
    'http://localhost:5001/api/Todo',
    {
      title: title,
      description: description
    }
  );
}

completeTodo(id: number) {
  return this.http.put(
    `http://localhost:5001/api/Todo/${id}/complete`,
    {}
  );
}

deleteTodo(id: number) {
  return this.http.delete(
    `http://localhost:5001/api/Todo/${id}`
  );
}


}

