using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using hotelier_core_app.Model.DTOs.Response;

namespace hotelier_core_app.API.Helpers
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : string.Empty;
            Message = message;
        }
    }

    public class ValidationResultModel : BaseResponse
    {
        public List<ValidationError> Data { get; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Message = "Validation Failed";
            Data = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }

    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
