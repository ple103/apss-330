using System.Collections.Generic;
using System.Threading.Tasks;
using mewmont.Models;
using mewmont.Models.SocketSenders;

namespace mewmont.Data
{
    public interface IRestService
    {
        Task<Room> GetRoomData(int id);

        Task<Room> PutRoomData(Room creatingRoom);

        Task<List<Room>> GetRoomsData();

        Task<User> Login(Login loginData);

        Task<RegistrationResponse> Register(Login registerData);
    }
}
