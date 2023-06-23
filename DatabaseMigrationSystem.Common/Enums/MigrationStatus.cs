namespace DatabaseMigrationSystem.Common.Enums;

public enum MigrationStatus
{
    /// <summary>
    /// Запущена
    /// </summary>
    Start,
    
    /// <summary>
    /// В работе
    /// </summary>
    Processed,
    
    /// <summary>
    /// Завершина
    /// </summary>
    Finish,
    
    /// <summary>
    /// Отменена
    /// </summary>
    Cancel,
}