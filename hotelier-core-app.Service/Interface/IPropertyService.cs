using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Model.DTOs.Response;
using hotelier_core_app.Model.Entities;

namespace hotelier_core_app.Service.Interface
{
    public interface IPropertyService
    {
        // add
        // edit
        // delete
        // get
        // get list

        Task<BaseResponse> AddProperty(AddPropertyRequestDTO request, AuditLog auditLog);
        Task<BaseResponse> UpdateProperty(UpdatePropertyRequestDTO request, AuditLog auditLog);
    }
}
