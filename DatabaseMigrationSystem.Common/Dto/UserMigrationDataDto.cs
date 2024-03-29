﻿using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.Common.Dto;

public class UserMigrationDataDto
{
    public int TotalRecordsForMigration { get; set; }
    public int CurrentRecordsCount { get; set; }
    public MigrationStatus MigrationStatus { get; set; }
    public TimeSpan MigrationDuration { get; set; }
    public int MigrationId { get; set; }
    public double MigrationProgressPercentage { get; set; }
}