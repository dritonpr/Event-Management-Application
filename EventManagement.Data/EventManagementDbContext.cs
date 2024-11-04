using EventManagement.Core;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Data
{
    public class EventManagementDbContext :DbContext
    {
        public EventManagementDbContext(DbContextOptions<EventManagementDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserFeedback> UserFeedback { get; set; }

    }
}
