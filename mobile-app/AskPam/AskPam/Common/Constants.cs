namespace AskPam.Common
{
    public static class ServiceUri
    {
        public const string TestEnvironmentHost = "{TEST ENVIRONMENT URI}";
        public const string ReleaseEnvironmentHost = "{RELEASE ENVIRONMENT URI}";
        
    }

    public static class AccountStoreKeys
    {
        public const string UserIdKey = "UserId";
        public const string UsernameKey = "Username";
        public const string TokenKey = "Token";
        public const string OrgIdKey = "OrgId";
    }

    public static class NotificationHubKeys
    {
        public const string SenderID = "{GOOGLE API PROJECT NUMBER}"; 
        public const string ListenConnectionString = "{NOTIFICATION HUB CONNECTION STRING}";
        public const string NotificationHubName = "{HUB NAME}";
        public const string NotificationTitle = "{NOTIFICATION TITLE}";
    }

}
