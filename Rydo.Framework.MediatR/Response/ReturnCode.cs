namespace Rydo.Framework.MediatR.Response
{
    public class ReturnCode
    {
        public ReturnCode(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; private set; }
        public string Message { get; private set; }
    }
}