using System;
using Glimpse.Common;
using Glimpse.Initialization;
using Glimpse.Internal;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace Glimpse.Agent.Razor
{
    [HtmlTargetElement("body")]
    public class ScriptInjector : TagHelper
    {
        private readonly Guid _requestId;
        private readonly ScriptOptions _scriptOptions;

        public ScriptInjector(IGlimpseContextAccessor context, IScriptOptionsProvider scriptOptionsProvider)
        {
            _requestId = context.RequestId;
            _scriptOptions = scriptOptionsProvider.BuildInstance();
        }

        public override int Order => int.MaxValue;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.PostContent.SetContentEncoded(
                $@"<script src=""{_scriptOptions.HudClientScriptUri}"" data-request-id=""{_requestId}"" data-client-url=""{_scriptOptions.SpaClientScriptUri}"" async></script>
                   <script src=""{_scriptOptions.BrowserAgentScriptUri}"" data-request-id=""{_requestId}"" data-action-url=""{_scriptOptions.HttpMessageUri}"" async></script>");
        }
    }
}
