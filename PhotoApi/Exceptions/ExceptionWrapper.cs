using System;
using Newtonsoft.Json;

namespace PhotoApi.Exceptions
{
    public class ExceptionWrapper
    {
        public ExceptionWrapper(Exception exception)
        {
            Message = exception.Message;
        }

        public int StatusCode { get; set; } = 500;
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}