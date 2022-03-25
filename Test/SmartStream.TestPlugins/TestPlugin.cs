using TioRACLab.Streaming.Core;

namespace TioRACLab.Streaming.TestPlugins
{

    public class TestPlugin : IStreamingPlugin
    {
        public Version Version => Version.Parse("1.0");

        public string Name => "Test Plugin";

        public string Description => "Test Plugin";

        public void OnStartPlugin(StartPluginEvent startEvent)
        {
            
        }
    }
}