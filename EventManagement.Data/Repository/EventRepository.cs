using EventManagement.Core;
using EventManagement.Data.Interface;

namespace EventManagement.Data.Repository
{
    public class EventRepository : GenericRepository<Event>, IEventsRepository
    {
        public EventRepository(EventManagementDbContext dbContext) : base(dbContext)
        {
        }
    }
}
