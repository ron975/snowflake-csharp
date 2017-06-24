﻿using Snowflake.Loader;
using Snowflake.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snowflake.Support.PluginManager
{
    public class PluginManagerComposable : IComposable
    {
        [ImportService(typeof(ILogProvider))]
        [ImportService(typeof(IContentDirectoryProvider))]
        [ImportService(typeof(IServiceRegistrationProvider))]
        public void Compose(IModule composableModule, IServiceContainer serviceContainer)
        {
            var logProvider = serviceContainer.Get<ILogProvider>();
            var appdataProvider = serviceContainer.Get<IContentDirectoryProvider>();
            var registrationProvider = serviceContainer.Get<IServiceRegistrationProvider>();
            var pluginManager = new PluginManager(logProvider, appdataProvider);
            registrationProvider.RegisterService<IPluginManager>(pluginManager);
        }
    }
}
