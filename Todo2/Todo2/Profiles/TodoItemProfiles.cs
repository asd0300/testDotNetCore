using AutoMapper;
using Todo2.Dtos;
using Todo2.Models;

namespace Todo2.Profiles
{
    public class TodoItemProfiles : Profile
    {
        public TodoItemProfiles()
        {
            CreateMap<TodoItem, TodoItemSelectDto>() //Automapper 非一對一對應時，額外設定部分
                .ForMember(
                a=>a.NameId,
                b=>b.MapFrom(c=>c.Name));
        }
    }
}
