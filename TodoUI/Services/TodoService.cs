using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using DataModel;

namespace TodoUI.Services
{
public class TodoService : ITodoService
{
    private readonly HttpClient httpClient;

    public TodoService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<TodoItems>> GetTodoItems()
    {
        return await httpClient.GetFromJsonAsync<List<TodoItems>>("api/TodoItems");
    }

}
}