using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Server.Web.Extensions;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace Glimpse.Server.Web.Resources
{
    public class RequestHistory : IResource
    {
        private readonly IQueryRequests _requests;

        public RequestHistory(IStorage storage)
        {
            if (!storage.Supports<IQueryRequests>())
                throw new ArgumentException($"IStorage of type '{storage.GetType().AssemblyQualifiedName}' does not support IQueryRequests.", nameof(storage));

            _requests = storage.As<IQueryRequests>();
        }

        // TODO: The parameter parsing in this method is brittle and will require more work
        public async Task Invoke(HttpContext context, IDictionary<string, string> parameters)
        {
            var response = context.Response;
            response.Headers[HeaderNames.ContentType] = "application/json";
            
            var filters = new RequestFilters();
            if (parameters.ContainsKey("dmin"))
                filters.DurationMinimum = float.Parse(parameters["dmin"]);

            if (parameters.ContainsKey("dmax"))
                filters.DurationMaximum = float.Parse(parameters["dmax"]);

            if (parameters.ContainsKey("url"))
                filters.UrlContains = parameters["url"];

            if (parameters.ContainsKey("methods"))
                filters.MethodList = parameters["methods"].Split(',');

            if (parameters.ContainsKey("smin"))
                filters.StatusCodeMinimum = int.Parse(parameters["smin"]);

            if (parameters.ContainsKey("smax"))
                filters.StatusCodeMaximum = int.Parse(parameters["smax"]);

            if (parameters.ContainsKey("tags"))
                filters.TagList = parameters["tags"].Split(',');

            if (parameters.ContainsKey("user"))
                filters.UserId = parameters["user"];

            if (parameters.ContainsKey("before"))
                filters.RequesTimeBefore = DateTime.Parse(parameters["before"]);

            IEnumerable<string> list;
            if (parameters.ContainsKey("types"))
            {
                var types = parameters["types"].Split(',');
                list = await _requests.Query(filters, types);
            }
            else
            {
                list = await _requests.Query(filters);
            }

            await response.WriteAsync(list.ToJsonArray());
        }

        public string Name => "RequestHistory";

        public ResourceParameters Parameters => new ResourceParameters(
                    ResourceParameter.Custom("dmin"),
                    ResourceParameter.Custom("dmax"),
                    ResourceParameter.Custom("url"),
                    ResourceParameter.Custom("methods"),
                    ResourceParameter.Custom("smin"),
                    ResourceParameter.Custom("smax"),
                    ResourceParameter.Custom("tags"),
                    ResourceParameter.Custom("before"),
                    ResourceParameter.Custom("user"),
                    ResourceParameter.Custom("types"));

        public ResourceType Type => ResourceType.Client;
    }
}
