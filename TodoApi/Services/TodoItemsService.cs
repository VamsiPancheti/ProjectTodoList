using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApi.Models;
using DataModel;

namespace TodoApi.Services
{
    public class TodoItemsService
    {
        private readonly IMongoCollection<TodoItems> _todoItemsCollection;

        public TodoItemsService(IOptions<TodoItemsDatabaseSettings> TodoItemsSettings)
        {
            var mongoClient = new MongoClient(
            TodoItemsSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            TodoItemsSettings.Value.DatabaseName);

        _todoItemsCollection = mongoDatabase.GetCollection<TodoItems>(
            TodoItemsSettings.Value.TodoItemsCollectionName);
        }

     public async Task<List<TodoItems>> GetAsync() =>
        await _todoItemsCollection.Find(_ => true).ToListAsync();

        // objTodo.TaskName = output.Name;
        //objTodo.Author = output.Author;

    public async Task<TodoItems?> GetAsync(string id) =>
        await _todoItemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<TodoItems>> GetAsync(bool isCompleted) =>
        await _todoItemsCollection.Find(x => x.IsCompleted == isCompleted).ToListAsync();

    public async Task CreateAsync(TodoItems newItem) =>
        await _todoItemsCollection.InsertOneAsync(newItem);

    public async Task UpdateAsync(string id, TodoItems newItem) =>
        await _todoItemsCollection.ReplaceOneAsync(x => x.Id == id, newItem);

    public async Task RemoveAsync(string id) =>
        await _todoItemsCollection.DeleteOneAsync(x => x.Id == id);
    }
}