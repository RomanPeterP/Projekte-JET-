using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace CustomLogger
{
    public static class LoggerConfigurator
    {
        public static void ConfigureLogger(this IHostApplicationBuilder builder, string tableName = "Logs", string filePath = "logs/log.txt", string connectionString = "default")
        {
            var sqlServerInstanceName = Environment.GetEnvironmentVariable(
                "SqlServerInstanceName",
                EnvironmentVariableTarget.User
            );

            var conn = builder.Configuration.GetConnectionString(connectionString);

            if (!string.IsNullOrEmpty(sqlServerInstanceName) && !string.IsNullOrEmpty(conn))
            {
                conn = conn.Replace("@SqlServerInstanceName", sqlServerInstanceName);
            }

            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(filePath, rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                    connectionString: conn,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = tableName,
                        AutoCreateSqlTable = true,
                    });

            Log.Logger = logger.CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);
        }

        public static IHostBuilder ConfigureLogger(this IHostBuilder builder, string tableName = "Logs", string filePath = "logs/log.txt", string connectionString = "default")
        {
            return builder.UseSerilog((context, services, lc) =>
            {
                var sqlServerInstanceName = Environment.GetEnvironmentVariable(
                    "SqlServerInstanceName",
                    EnvironmentVariableTarget.User
                );

                var conn = context.Configuration.GetConnectionString(connectionString);

                if (!string.IsNullOrEmpty(sqlServerInstanceName) && !string.IsNullOrEmpty(conn))
                {
                    conn = conn.Replace("@SqlServerInstanceName", sqlServerInstanceName);
                }

                lc.MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File(filePath, rollingInterval: RollingInterval.Day)
                    .WriteTo.MSSqlServer(
                    connectionString: conn,
                    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        TableName = tableName,
                        AutoCreateSqlTable = true,
                    });
            });
        }

    }
}
