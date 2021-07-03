using System;

namespace RockContent.Shared
{
    public static class Settings
    {
        public static string RockContentDatabaseName = Environment.GetEnvironmentVariable("ROCKCONTENT_DATABASE_NAME");
    }
}