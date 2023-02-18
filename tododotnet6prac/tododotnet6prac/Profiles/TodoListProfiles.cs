using AutoMapper;
using tododotnet6prac.Dtos;
using tododotnet6prac.Models;

namespace tododotnet6prac.Profiles
{
    public class TodoListProfiles:Profile
    {
        public TodoListProfiles() 
        {
            CreateMap<TodoList, TodoListSelectDto>();
        }

    }
}
