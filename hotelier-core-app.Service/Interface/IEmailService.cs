using hotelier_core_app.Model.DTOs.Request;

namespace hotelier_core_app.Service.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmail(SendEmailDTO model);
    }
}
