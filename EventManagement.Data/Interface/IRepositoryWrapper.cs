namespace EventManagement.Data.Interface
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IEventsRepository Events { get; }
        Task SaveChangesAsync();
    }
}
