using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Amazon.Runtime.Internal.Transform;
using Newtonsoft.Json;

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

    public async Task<HttpResponseMessage> CreateTodoItem(TodoItems todo)
    {
        string data = JsonConvert.SerializeObject(todo);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
       return await  httpClient.PostAsync("api/TodoItems/Post", content);       
    }

    public async Task<HttpResponseMessage> DeleteTodoItem(string Id)
    {
        return await httpClient.DeleteAsync("api/TodoItems/"+Id);
    }

    public async Task<HttpResponseMessage> UpdateTodoItem(string id, TodoItems todo)
    {
        string data = JsonConvert.SerializeObject(todo);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        return await httpClient.PutAsync("api/TodoItems/"+id,content);
    }


}
}