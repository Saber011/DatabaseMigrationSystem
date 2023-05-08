namespace DatabaseMigrationSystem.DataAccess.Interfaces;

public interface IMutateRepository<in TRequest, TResult>
{
    /// <summary>
    /// Обработать запрос.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task<TResult> Mutate(TRequest request, CancellationToken cancellationToken);
}

public interface IMutateRepository<in TRequest>
{
    /// <summary>
    /// Обработать запрос.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task Mutate(TRequest request, CancellationToken cancellationToken);
}