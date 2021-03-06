﻿using DictionariesSystem.Data.Logs.Migrations;
using DictionariesSystem.Models.Logs;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace DictionariesSystem.Data.Logs
{
    public class LogsDbContext : DbContext
    {
        private const string ConnectionStringName = "LogsDb";

        public LogsDbContext()
            : base(ConnectionStringName)
        {
        }

        public virtual IDbSet<UserLog> UserLogs { get; set; }

        public virtual IDbSet<ExceptionLog> ExceptionLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<LogsDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
