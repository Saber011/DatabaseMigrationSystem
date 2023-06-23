﻿using DatabaseMigrationSystem.Common.Dto;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Migration.Queries;

public class GetMigrationJournalDataQuery : IRequest<List<UserMigrationData>>
{
    
}