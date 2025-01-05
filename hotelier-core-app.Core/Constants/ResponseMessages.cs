namespace hotelier_core_app.Core.Constants
{
    public class ResponseMessages
    {
        public const string DuplicateKeyMessage = "duplicate key";
        public const string UpdateSuccessful = "Record successfully updated";
        public const string UpdateFailed = "Update operation failed. Kindly refer to the StatusCode.";
        public const string InvalidData = "Supplied data is invalid";
        public const string InvalidFile = "Invalid File. Please check file extension";
        public const string OperationSuccessful = "Operation successful!";
        public const string OperationFailed = "Sorry, there was an error processing your request.";
        public const string ReasonRequired = "Reason for declining request is required";
        public const string RequestReasonRequired = "Reason for request is required";
        public const string PartialUpdate = "Update operation failed for some record.";
        public const string UpdateOperationFailed = "Update operation failed. Please update the card status to a valid state.";
        public const string ErrorSendingMessage = "Could not send message. Please try again or contact admin for assistance";

        public const string CachePersisterLogExceptionGotten = "Oops! A error occurred while attempting to log failed cache object to temporal database";
        public const string ErrorPerformingCacheServiceOperation = "Oops! An error occurred while performing cache operation.";

        public const string SQlTransactionNotInitialized = "Oops! An error occurred while processing your request. If this persists after three(3) trials, kindly contact your administrator.";
        public const string InvalidElasticTableName = "Could not locate entity for search. Kindly contact your administrator";
        public const string NoRecordFound = "No record was found";
        public const string GeneralError = "Oops! An error occurred while processing your request. If this persists after three(3) trials, kindly contact your administrator.";
        public const string SQlException = "Oops! A database error occurred while processing your request. If this persists after three(3) trials, kindly contact your administrator.";
        public const string AuditLogObjectEmpty = "Oops! A error occurred while preparing a trail for your request. If this persists after three(3) trials, kindly contact your administrator.";

        //User Management
        public const string UserExist = "User already exist";
        public const string UserCreated = "User created successfully";
        public const string UserDoesNotExist = "User does not exist";
        public const string UserActivated = "User activated";
        public const string UserDeactivated = "User deactivated";
        public const string UserRemoved = "User removed successfully";
        public const string UserInactive = "This account is currently not active, contact Admin for help";
        public const string LoginSuccessful = "Login successful";
        public const string CantVerifyToken = "Can't Verify Token";
        public const string CantVerifyRefreshToken = "Can't Verify Refresh Token";

        //User Role
        public const string RoleExist = "Role with this name already exist";
        public const string RoleNotExist = "This role does not already exist";
        public const string RoleCreated = "Role created successfully";
        public const string RoleUpdated = "Role updated successfully";

        //Module Service
        public const string ModuleGroupUpdated = "Dashboard detail is updated";
        public const string ModuleGroupNotExist = "Module Group does not exist";
        public const string ModuleGroupUpdateValidation = "Any of the module group field is required";
        public const string ModuleUpdated = "Module detail is updated";
        public const string ModuleNotExist = "Module does not exist";
        public const string ModuleGroupExist = "Module group already exist";
        public const string ModuleExist = "Module already exist";
        public const string ModuleUpdateValidation = "Either of the module field is required";
        public const string NoModuleAccess = "No module access found for user";
    }
}
