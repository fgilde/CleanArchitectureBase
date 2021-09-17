namespace CleanArchitectureBase.Application
{
    public class Constants
    {
        public const string ApplicationName = "CleanArchitectureBase";
        public const string SessionIdKey = nameof(SessionIdKey);
        public const string ApiBasePath = "api";

        public static class Environment
        {
            public const string Testing = nameof(Testing);
            public const string Development = nameof(Development);
            public const string Production = nameof(Production);
        }

        public static class EventNames
        {
            public const string ClientEventName = "EventRecieved";
        }

        public static class ServiceBusQueues
        {
            public const string TestQueue = nameof(TestQueue);
            public const string ClientEventQueue = nameof(ClientEventQueue);
            public const string MainQueue = nameof(MainQueue);
        }

        public static class Roles
        {
            public const string Administrator = nameof(Administrator);
        }     
        public static class Policies
        {
            public const string CanPurge = nameof(CanPurge);
        }
    }
}
