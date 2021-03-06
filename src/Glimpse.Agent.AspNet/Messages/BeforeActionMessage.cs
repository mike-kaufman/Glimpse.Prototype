﻿
using System.Collections.Generic;

namespace Glimpse.Agent.Messages
{
    public class BeforeActionMessage : IActionRouteMessage
    {
        public string ActionId { get; set; }

        public string ActionDisplayName { get; set; }

        public string ActionName { get; set; }

        public string ActionControllerName { get; set; }

        public string RouteName { get; set; }

        public string RoutePattern { get; set; }

        public IList<KeyValuePair<string, string>> RouteData { get; set; }

        public IList<KeyValuePair<string, RouteConfigurationData>> RouteConfiguration { get; set; }
    }
}