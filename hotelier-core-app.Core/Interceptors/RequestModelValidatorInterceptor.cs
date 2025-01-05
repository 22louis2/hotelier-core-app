using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using hotelier_core_app.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace hotelier_core_app.Core.Interceptors
{
    public class RequestModelValidatorInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                var validationErrors = new Dictionary<string, string>();
                foreach (var error in result.Errors)
                {
                    validationErrors.Add(error.PropertyName, error.ErrorMessage);
                }
                throw new DataValidationException("One or more request parameters are not valid", validationErrors);
            }
            return result;
        }

        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }
    }
}
