namespace hotelier_core_app.Core.Constants
{
    public class ResponseStatusCode
    {
        public const string DuplicateKeyMessage = "001";
        public const string UpdateSuccessful = "002";
        public const string UpdateFailed = "003";
        public const string InvalidData = "004";
        public const string InvalidFile = "005";
        public const string OperationSuccessful = "006";
        public const string OperationFailed = "007";
        public const string ReasonRequired = "008";
        public const string RequestReasonRequired = "009";
        public const string PartialUpdate = "010";
        public const string UpdateOperationFailed = "011";
        public const string ErrorSendingMessage = "012";

        public const string CachePersisterLogExceptionGotten = "013";
        public const string ErrorPerformingCacheServiceOperation = "014";

        public const string SQlTransactionNotInitialized = "015";
        public const string InvalidElasticTableName = "016";
        public const string NoRecordFound = "017";
        public const string GeneralError = "018";
        public const string SQlException = "019";
        public const string AuditLogObjectEmpty = "020";

        //User Management
        public const string UserExist = "021";
        public const string UserCreated = "022";
        public const string UserDoesNotExist = "023";
        public const string UserActivated = "024";
        public const string UserDeactivated = "025";
        public const string UserRemoved = "026";
        public const string UserInactive = "027";
        public const string LoginSuccessful = "028";
        public const string CantVerifyToken = "029";
        public const string CantVerifyRefreshToken = "030";

        //User Role
        public const string RoleExist = "031";
        public const string RoleNotExist = "032";
        public const string RoleCreated = "033";
        public const string RoleUpdated = "034";

        //Module Service
        public const string ModuleGroupUpdated = "035";
        public const string ModuleGroupNotExist = "036";
        public const string ModuleGroupUpdateValidation = "037";
        public const string ModuleUpdated = "038";
        public const string ModuleNotExist = "039";
        public const string ModuleGroupExist = "040";
        public const string ModuleExist = "041";
        public const string ModuleUpdateValidation = "042";
        public const string NoModuleAccess = "043";
    }
}
