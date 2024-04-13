using System.Net;

namespace Gallery.DTO.BaseInterfaceAndClass
{
    public class BaseDTO
    {
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public HttpStatusCode StatusCode { get; set; }
        public List<string> SuccessfulMessages { get; set; } = new List<string>();
    }

    public class BaseDTO<KeyTypeId> : BaseDTO, IBaseDTO where KeyTypeId : struct
    {
        /// <summary>
        /// the type of the Id
        /// </summary>
        public KeyTypeId Id { get; set; }

        public virtual async Task<List<string>> SetError(List<string> errors)
        {
            ErrorMessages.AddRange(errors);
            StatusCode = HttpStatusCode.BadRequest;
            return await Task.Run(() => ErrorMessages);
        }
        public virtual async Task<List<string>> SetError(string error)
        {
            ErrorMessages.Add(error);
            StatusCode = HttpStatusCode.BadRequest;
            return await Task.Run(() => ErrorMessages);
        }
        public virtual async Task<List<string>> SetError(string error, HttpStatusCode statusCode)
        {
            ErrorMessages.Add(error);
            StatusCode = statusCode;
            return await Task.Run(() => ErrorMessages);
        }
        public virtual async Task<List<string>> SetError(List<string> errors, HttpStatusCode statusCode)
        {
            ErrorMessages.AddRange(errors);
            StatusCode = statusCode;
            return await Task.Run(() => ErrorMessages);
        }

        public virtual async Task<List<string>> SetSuccessfulMessage(List<string> messages, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            SuccessfulMessages.AddRange(messages);
            StatusCode = statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created || statusCode == HttpStatusCode.Accepted ||
                statusCode == HttpStatusCode.NonAuthoritativeInformation || statusCode == HttpStatusCode.NoContent ? statusCode : HttpStatusCode.OK;

            return await Task.Run(() => ErrorMessages);
        }

        public virtual async Task<List<string>> SetSuccessfulMessage(string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            SuccessfulMessages.Add(message);
            StatusCode = statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created || statusCode == HttpStatusCode.Accepted ||
                statusCode == HttpStatusCode.NonAuthoritativeInformation || statusCode == HttpStatusCode.NoContent ? statusCode : HttpStatusCode.OK;

            return await Task.Run(() => ErrorMessages);
        }

        public virtual async Task SetStatusCode(HttpStatusCode httpStatus) => await Task.Run(() => StatusCode = httpStatus);
        public virtual async Task<bool> HasError() => await Task.Run(() => ErrorMessages.Count > 0);
    }
}