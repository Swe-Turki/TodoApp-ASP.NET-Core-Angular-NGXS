export class GetTodos {
  static readonly type = '[Todo] Get Todos';

  
}

export class CreateTodo {
  static readonly type = '[Todo] Create Todo';

  constructor(
    public title: string,
    public description: string
  ) {}
}

export class CompleteTodo {
  static readonly type = '[Todo] Complete Todo';

  constructor(public id: number) {}
}

export class DeleteTodo {
  static readonly type = '[Todo] Delete Todo';

  constructor(public id: number) {}
}