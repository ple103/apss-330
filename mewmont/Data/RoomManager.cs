using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using mewmont.Models;

namespace mewmont.Data
{
    public class RoomManager
    {
        IRestService restService;

        public RoomManager(IRestService service)
        {
            restService = service;
        }

        public Task<Room> GetTasksAsync()
        {
            return restService.RefreshDataAsync();
        }
    }
}
