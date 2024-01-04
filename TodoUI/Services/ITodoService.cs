using DataModel;
namespace TodoUI.Services
{

public interface ITodoService
{
    Task<List<TodoItems>> GetTodoItems();

}
}