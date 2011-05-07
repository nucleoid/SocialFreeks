using System;
using System.Data.SqlClient;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Generators.SqlServer;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SqlServer;

namespace SocialFreeks.Database
{
    public class MigrationRunner
    {
        private readonly string _connectionString;

        private Assembly _assembly;
        private IAnnouncer _announcer;
        private IMigrationProcessorOptions _options;
        private DatabaseSyntax _syntax;

        private MigrationRunner(string connectionString)
        {
            _connectionString = connectionString;

            _assembly = null;
            _announcer = new NullAnnouncer();
            _options = new ProcessorOptions();
            _syntax = DatabaseSyntax.SqlServer2005;
        }

        public static MigrationRunner MigrateDatabase(string connectionString)
        {
            return new MigrationRunner(connectionString);
        }

        public MigrationRunner Using(DatabaseSyntax syntax)
        {
            _syntax = syntax;

            return this;
        }

        public MigrationRunner Using(IMigrationProcessorOptions options)
        {
            _options = options;

            return this;
        }

        public MigrationRunner Using(IAnnouncer announcer)
        {
            _announcer = announcer;

            return this;
        }

        public MigrationRunner WithMigrationAssembly(Assembly assembly)
        {
            _assembly = assembly;

            return this;
        }

        public void Run()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var context = new RunnerContext(_announcer);
                var generator = GetMigrationGenerator(_syntax);
                var processor = new SqlServerProcessor(connection, generator, _announcer, _options);
                var runner = new FluentMigrator.Runner.MigrationRunner(_assembly, context, processor);

                runner.MigrateUp();
            }
        }

        public MigrationState GetMigrationState()
        {
            var state = new MigrationState();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var context = new RunnerContext(_announcer);
                    var generator = GetMigrationGenerator(_syntax);
                    var processor = new SqlServerProcessor(connection, generator, _announcer, _options);
                    var runner = new FluentMigrator.Runner.MigrationRunner(_assembly, context, processor);

                    state.Total = runner.MigrationLoader.Migrations.Keys.Count;

                    foreach (var mig in runner.MigrationLoader.Migrations.Keys)
                    {
                        if (runner.VersionLoader.VersionInfo.HasAppliedMigration(mig))
                        {
                            state.Completed++;
                        }
                        else
                        {
                            state.Pending++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                state.Exception = ex;
            }

            return state;
        }

        private static IMigrationGenerator GetMigrationGenerator(DatabaseSyntax syntax)
        {
            switch (syntax)
            {
                case DatabaseSyntax.SqlServer2000:
                    return new SqlServer2000Generator();
                case DatabaseSyntax.SqlServer2005:
                    return new SqlServer2005Generator();
                case DatabaseSyntax.SqlServer2008:
                    return new SqlServer2008Generator();
                default:
                    throw new ArgumentOutOfRangeException("syntax");
            }
        }
    }
}
