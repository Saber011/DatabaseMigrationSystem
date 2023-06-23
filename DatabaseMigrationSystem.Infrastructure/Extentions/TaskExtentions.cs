namespace DatabaseMigrationSystem.Infrastructure.Extentions;

/// <summary>
/// Расшериения для класса Task
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    /// Запустить задачу и не ждать ее завершения
    /// </summary>
    public static void FireAndForget(
        this Task task,
        Action<Exception> errorHandler = null)
    {
        task.ContinueWith(t =>
                          {
                              if (t.IsFaulted && (errorHandler != null))
                                  errorHandler(t.Exception);
                          }, TaskContinuationOptions.OnlyOnFaulted);
    }
}