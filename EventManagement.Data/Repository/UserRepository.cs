using EventManagement.Core;
using EventManagement.Data.Interface;

namespace EventManagement.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EventManagementDbContext dbContext) : base(dbContext)
        {
        }
    }
}
