﻿using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.User;

public interface IGetUserRolesRepository : IGetRepository<int, UserRoles[]>
{
    
}