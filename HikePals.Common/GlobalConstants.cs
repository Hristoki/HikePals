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

        public const int MaxLocationNameLenght = 300;
        public const int MinLocationNameLenght = 5;
        public const string TripLocationNameErrorMessage = "Location name should be between 5 and 300 symbols long";

        public const int MaxTripDescriptionLenght = 10000;
        public const int MinTripDescriptionLenght = 25;
        public const string TripDescriptionErrorMessage = "Description should be between 25 and 10000 symbols long";

        public const int MaxTripTitleLenght = 100;
        public const int MinTripTitleLenght = 5;
        public const string TripTitleErrorMessage = "Trip title should be between 5 and 100 symbols long";

        public const int MaxTripDistance = 1000;
        public const int MinTripDistance = 1;
        public const string TripDistanceErrorMessage = "Trip distance should be between 1 and 1000 km long";

        public const int MaxGroupSize = 25;
        public const int MinGorupSize = 2;
        public const string GroupsSizeErrorMessage = "Group size should be between 2 and 25 members";

        public const int MaxContactsSubjectLenght = 200;
        public const int MinContactsSubjectLenght = 5;
        public const string SubjectLengthErrorMessage = "Subject should be between 5 and 200 symbols long";

        public const int MaxContactsContentLenght = 1000;
        public const int MinContactsContentLenght = 10;
        public const string ContactsContentLengthErrorMessage = "Text Message should be between 10 and 1000 symbols long";
       
        public const string ConfirmPassErrorMessage = "Password and Confirm Password must match";

        public const string ResetPasswordMessage = "<h1>Reset your password</h1><p> You told us you forgot your password. If you really did, click here to choose a new one:</p><p><a href='..URL..' >Click here to reset</a></p><p>If you didn't request this email message, please ignore it!</p>";

        public static readonly string[] AllowedImageExtensions = { ".jpeg", ".jpg", ".png", ".tiff", ".gif" };
    }
}
