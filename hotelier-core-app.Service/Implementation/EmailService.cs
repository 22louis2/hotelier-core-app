using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Service.Interface;

namespace hotelier_core_app.Service.Implementation
{
    public class EmailService : IEmailService
    {
        public Task<bool> SendEmail(SendEmailDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
