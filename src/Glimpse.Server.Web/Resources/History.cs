using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Server.Web.Extensions;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Web
{
    public class History : IResource
    {
        private readonly IStorage _store;

        public History(IStorage storage)
        {
            _store = storage;
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;

            if (!parameters.ContainsKey("types"))
            {
                response.StatusCode = 404;
                await response.WriteAsync("Required parameter 'types' is missing.");
                return;
            }

            var list = await _store.RetrieveByType(parameters["types"].Split(','));

            response.Headers[HeaderNames.ContentType] = "application/json";
            await response.WriteAsync(list.ToJsonArray());
        }

        public string Name => "History";
        
        public ResourceParameters Parameters => new ResourceParameters(+ResourceParameter.Custom("types"));

        public ResourceType Type => ResourceType.Client;
    }
}