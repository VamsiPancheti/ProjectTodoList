using DataModel;
namespace TodoUI.Services
{

public interface ITodoService
{
    Task<List<TodoItems>> GetTodoItems();
     Task<HttpResponseMessage> CreateTodoItem(TodoItems todo);
     Task<HttpResponseMessage> DeleteTodoItem(string id);
     Task<HttpResponseMessage> UpdateTodoItem(string id, TodoItems todoItems);

}
}