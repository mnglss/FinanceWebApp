namespace Domain.Interfaces;
public interface IUnitOfWork
{
    // Task SaveChangesAsync();
    // void Dispose();
    void Commit();
    Task CommitAsync();
}