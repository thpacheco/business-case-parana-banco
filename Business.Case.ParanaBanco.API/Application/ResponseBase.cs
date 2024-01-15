
using Business.Case.ParanaBanco.API.Domain.Interfaces;

namespace Business.Case.ParanaBanco.API.Application
{
    public class ResponseBase : IResponseBase
    {
        public ResponseBase() { }

        public static ResponseBase CreateNotfound(string message = "Ops, não encontrado!") => new ResponseBase(false, message, null, 404);
        public static ResponseBase CreateError(string message, int status = 400) => new ResponseBase(false, message, null, status);
        public static ResponseBase CreateSuccess(object data, string message = null) => new ResponseBase(true, message, data, 201);

        public ResponseBase(bool success, string message, object data, int status)
        {
            Success = success;
            Message = message;
            Data = data;
            Status = status;

        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int Status { get; set; }
    }
}
