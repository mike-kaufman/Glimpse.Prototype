using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Server.Web.Extensions;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Web
{
    public class MessageHistoryResource : IResource
    {
        private readonly IStorage _store;

        public MessageHistoryResource(IStorage storage)
        {
            _store = storage;
        }

        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "application/json";

            var list = await _store.RetrieveByType(parameters["types"].Split(','));

            await response.WriteAsync(list.ToJsonArray());
        }

        public string Name => "History";
        
        public ResourceParameters Parameters => new ResourceParameters(ResourceParameter.Custom("types"));

        public ResourceType Type => ResourceType.Client;
    }
}