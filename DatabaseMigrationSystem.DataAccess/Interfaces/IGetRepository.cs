namespace DatabaseMigrationSystem.DataAccess.Interfaces;

public interface IGetRepository<in TRequest, TResult>
{
    /// <summary>
    /// Обработать запрос.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task<TResult> Get(TRequest request, CancellationToken cancellationToken);
}

public interface IGetRepository<TResult>
{
    /// <summary>
    /// Обработать запрос.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task<TResult> Get(CancellationToken cancellationToken);
}