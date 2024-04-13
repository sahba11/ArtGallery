using System.Net;

namespace Gallery.DTO.BaseInterfaceAndClass
{
    public interface IBaseDTO
    {
        Task<List<string>> SetError(List<string> errors);
        Task<List<string>> SetError(string error);
        Task<List<string>> SetSuccessfulMessage(List<string> messages, HttpStatusCode statusCode = HttpStatusCode.OK);
        Task<List<string>> SetSuccessfulMessage(string message, HttpStatusCode statusCode = HttpStatusCode.OK);
        Task SetStatusCode(HttpStatusCode httpStatus);
        Task<bool> HasError();
    }
}
