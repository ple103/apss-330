﻿using System.Collections.Generic;
using System.Threading.Tasks;
using mewmont.Models;

namespace mewmont.Data
{
    public interface IRestService
    {
        Task<Room> GetRoomData();

        //Task SaveTodoItemAsync(TodoItem item);

        //Task UpdateTodoItemAsync(TodoItem item);

        //Task DeleteTodoItemAsync(string id);
    }
}