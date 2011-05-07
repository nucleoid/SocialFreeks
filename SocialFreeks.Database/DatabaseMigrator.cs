using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using SocialFreeks.Entities;

namespace SocialFreeks.Database
{
    public static class DatabaseMigrator
    {
        public static void MigrateDatabase()
        {
            MigrateDatabase(new FluentMigrator.Runner.Announcers.NullAnnouncer());
        }

        public static void MigrateDatabase(IAnnouncer announcer)
        {
            MigrationRunner
                .MigrateDatabase(Settings.ConnectionString)
                .Using(DatabaseSyntax.SqlServer2005)
                .Using(announcer)
                .Using(new ProcessorOptions { Timeout = 600 })
                .WithMigrationAssembly(typeof(DatabaseMigrator).Assembly)
                .Run();
        }

        public static MigrationState GetDatabaseMigrationState()
        {
            return MigrationRunner
                       .MigrateDatabase(Settings.ConnectionString)
                       .WithMigrationAssembly(typeof(DatabaseMigrator).Assembly)
                       .GetMigrationState();
        }
    }
}
