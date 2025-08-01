﻿using Microsoft.Extensions.Configuration;

namespace TRSAP09V2.Logic
{
    public static class Config
    {
        public static IConfiguration ConfigItems { get; private set; }

        static Config()
        {
            ConfigItems = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        }
    }
}
