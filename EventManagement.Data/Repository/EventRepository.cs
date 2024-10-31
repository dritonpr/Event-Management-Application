using EventManagement.Core;
using EventManagement.Data.Interface;

namespace EventManagement.Data.Repository
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(EventManagementDbContext dbContext) : base(dbContext)
        {
        }
    }
}
