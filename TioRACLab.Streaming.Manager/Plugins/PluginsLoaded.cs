using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using TioRACLab.Streaming.Core;

namespace TioRACLab.Streaming.Plugins
{
    internal class PluginsLoaded
    {
        internal PluginsLoaded(AssemblyLoadContext assemblyLoader, Assembly assembly, IStreamingPlugin streamingPlugin, List<IBasePlugin> pluginsParts)
        {
            this.AssemblyLoader = assemblyLoader;
            this.PluginAssembly = assembly;
            this.Plugin = streamingPlugin;
            this.PluginParts = pluginsParts;
        }

        internal readonly IStreamingPlugin Plugin;

        private readonly Assembly PluginAssembly;

        private readonly AssemblyLoadContext AssemblyLoader;

        internal readonly List<IBasePlugin> PluginParts;

        internal bool Active { get; set; }

        internal void Unload() => AssemblyLoader.Unload();
    }
}
