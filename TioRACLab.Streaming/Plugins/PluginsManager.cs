using System.Reflection;
using System.Runtime.Loader;
using TioRACLab.Streaming.Core;
using System.Collections.ObjectModel;

namespace TioRACLab.Streaming.Plugins
{
    public class PluginsManager
    {
        private static List<PluginsLoaded> plugins = new();

        public ReadOnlyCollection<IStreamingPlugin> Plugins => plugins.Select(p => p.Plugin).ToList().AsReadOnly();

        public static void LoadPluginsFile(string file)
        {
            try
            {
                AssemblyLoadContext assemblyLoader = new(Path.GetFileNameWithoutExtension(file), true);
                Assembly assembly = assemblyLoader.LoadFromAssemblyPath(file);

                IStreamingPlugin? plugin = IsAssemblyPlugin(assembly);

                if (plugin != null)
                {
                    plugins.Add(LoadAllPlugin(assemblyLoader, assembly, plugin));
                }
                else
                {
                    assemblyLoader.Unload();
                    assembly = null;
                    assemblyLoader = null;
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        private static IStreamingPlugin? IsAssemblyPlugin(Assembly assembly)
        {
            var streamingPlugins = GetAllPluginsClass<IStreamingPlugin>(assembly);

            if (streamingPlugins == null)
                return null;

            if (streamingPlugins.Count == 1)
            {
                var pluginObj = Activator.CreateInstance(streamingPlugins.First());

                if (pluginObj != null)
                    return (IStreamingPlugin)pluginObj;
            }

            return null;
        }

        private static List<TypeInfo>? GetAllPluginsClass<TPlugin>()
            where TPlugin : IBasePlugin
        {
            return GetAllPluginsClass<TPlugin>(Assembly.GetEntryAssembly()?
                                                       .GetReferencedAssemblies()
                                                       .Select(Assembly.Load));
        }

        private static List<TypeInfo>? GetAllPluginsClass<TPlugin>(Assembly assembly)
            where TPlugin : IBasePlugin
        {
            return GetAllPluginsClass<TPlugin>(new Assembly[] { assembly });
        }

        private static List<TypeInfo>? GetAllPluginsClass<TPlugin>(IEnumerable<Assembly>? assemblies)
            where TPlugin : IBasePlugin
        {
            return assemblies?
                        .SelectMany(x => x.DefinedTypes)
                        .Where(type => typeof(TPlugin).IsAssignableFrom(type))
                        .ToList();
        }

        private static PluginsLoaded LoadAllPlugin(AssemblyLoadContext assemblyLoader, Assembly assembly, IStreamingPlugin streamingPlugin)
        {
            var allPartsTypes = GetAllPluginsClass<IBasePlugin>(assembly);
            List<IBasePlugin> allParts = new();

            allPartsTypes?.ForEach(type =>
            {
                if (!type.IsAssignableFrom(typeof(IStreamingPlugin)))
                {
                    var pluginPart = Activator.CreateInstance(type);

                    if (pluginPart != null)
                        allParts.Add((IBasePlugin) pluginPart);
                }
            });

            return new PluginsLoaded(assemblyLoader, assembly, streamingPlugin, allParts);
        }
    }
}