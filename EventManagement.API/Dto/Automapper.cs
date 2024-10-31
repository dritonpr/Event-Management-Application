using AutoMapper;
using EventManagement.Core;

namespace EventManagement.API.Dto
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            this.CreateMap<User, UserDto>().ReverseMap();
            this.CreateMap<UserDto, User>().ReverseMap();

            this.CreateMap<Event, EventDto>().ReverseMap();
            this.CreateMap<EventDto, Event>().ReverseMap();

        }
    }
}
