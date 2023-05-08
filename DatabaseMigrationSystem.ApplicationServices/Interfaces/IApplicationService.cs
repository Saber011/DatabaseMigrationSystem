namespace DatabaseMigrationSystem.ApplicationServices.Interfaces;

public interface IApplicationService<in TRequest, TResult>
{
    /// <summary>
    /// Обработать запрос.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}

public interface IApplicationService<TResult>
{
    /// <summary>
    /// Обработать запрос.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task<TResult> Handle(CancellationToken cancellationToken);
}