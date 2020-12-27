namespace HikePals.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "HikePals";

        public const string AdministratorRoleName = "Administrator";
        public const string UserRoleName = "User";

        public const string AdministratorOrUser = AdministratorRoleName + "," + UserRoleName;

        public const int InternalServerError = 500;
        public const int NotFound = 404;

        public const string ResetPasswordMessage = "<h1>Reset your password</h1><p> You told us you forgot your password. If you really did, click here to choose a new one:</p><p><a href='..URL..' >Click here to reset</a></p><p>If you didn't request this email message, please ignore it!</p>";

        public static readonly string[] AllowedImageExtensions = { ".jpeg", ".jpg", ".png", ".tiff", ".gif" };
    }
}
