namespace MIOTO.DOMAIN;

public interface IUnitOfWork : IAsyncDisposable
{
    /// <summary>
    /// Call save change
    /// </summary>
    /// <returns></returns>
    Task CommitAsync(CancellationToken cancellationToken = default);

}