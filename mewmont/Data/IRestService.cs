using System.Collections.Generic;
using System.Threading.Tasks;
using mewmont.Models;

namespace mewmont.Data
{
    public interface IRestService
    {
        Task<Room> GetRoomData(int id);

        Task<Room> PutRoomData(Room creatingRoom);

        Task<List<Room>> GetRoomsData();

        //Task SaveTodoItemAsync(TodoItem item);

        //Task UpdateTodoItemAsync(TodoItem item);

        //Task DeleteTodoItemAsync(string id);
    }
}
