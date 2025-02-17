using System.Net;

namespace Vehicle_Assembly.DTOs.Responses
{
    public class BaseResponse
    {
        public int status_code { get; set; }
        public object data { get; set; }

        public void CreateResponse(HttpStatusCode statusCode, Object data)
        {
            status_code = (int)statusCode;
            this.data = data;
        }
    }
}
