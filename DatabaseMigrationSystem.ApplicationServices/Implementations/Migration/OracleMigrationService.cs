﻿using DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;

namespace DatabaseMigrationSystem.ApplicationServices.Implementations.Migration;

public class OracleMigrationService : IMigrationService
{
    public Task<object> Handle(string request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}