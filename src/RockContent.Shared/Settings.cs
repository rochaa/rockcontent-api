using System;

namespace RockContent.Shared
{
    public static class Settings
    {
        public static string RockContentDatabaseName = Environment.GetEnvironmentVariable("ROCKCONTENT_DATABASE_NAME");
        public static string SecretJWT = "e52316f92a374bbe96cf3f1c0042b093";
    }
}