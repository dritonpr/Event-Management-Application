using EventManagement.Core;
using EventManagement.Data.Interface;

namespace EventManagement.Data.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {

        private EventManagementDbContext _repoContext;
        private IUserRepository _userRepository;
        private IEventsRepository _eventsRepository;
        public RepositoryWrapper(EventManagementDbContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repoContext);
                return _userRepository;
            }
        }
        public IEventsRepository Events
        {
            get
            {
                if (_eventsRepository == null)
                    _eventsRepository = new EventRepository(_repoContext);
                return _eventsRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
