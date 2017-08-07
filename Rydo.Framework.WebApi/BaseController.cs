using MediatR;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Rydo.Framework.WebApi
{
    public abstract class BaseController : ApiController
    {
        protected readonly IMediator _mediator;
        protected HttpResponseMessage _responseMessage;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
            _responseMessage = new HttpResponseMessage();
        }

        protected Task<HttpResponseMessage> CreateResponse(dynamic data, HttpStatusCode httpStatusCode)
        {
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            switch (httpStatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                case HttpStatusCode.BadRequest:
                    //_responseMessage = Request.CreateResponse(httpStatusCode, data);
                    break;
            }

            tsc.SetResult(_responseMessage);

            return tsc.Task;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
