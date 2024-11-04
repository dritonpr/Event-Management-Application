using AutoMapper;
using EventManagement.Common.Dto;
using EventManagement.Core;

namespace EventManagement.Api.Mapper
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            this.CreateMap<User, UserDto>().ReverseMap();
            this.CreateMap<UserDto, User>().ReverseMap();

            this.CreateMap<Event, EventDto>().ReverseMap();
            this.CreateMap<EventDto, Event>().ReverseMap();

            this.CreateMap<UserFeedback, UserFeedbackDto>().ReverseMap();
            this.CreateMap<UserFeedbackDto, UserFeedback>().ReverseMap();
            
        }
    }
}
