﻿using Microsoft.Extensions.OptionsModel;

namespace Glimpse.Initialization
{
    public class OptionsScriptOptionsProvider : IScriptOptionsProvider
    {
        private readonly ScriptOptions _scriptOptions;

        public OptionsScriptOptionsProvider(IOptions<ScriptOptions> optionsAccessor)
        {
            _scriptOptions = optionsAccessor.Value;
        }

        public ScriptOptions BuildInstance()
        {
            return _scriptOptions;
        }
    }
}
