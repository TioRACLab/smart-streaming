using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStream.Plugins
{
    public interface IStreamingPlugin : IBasePlugin
    {
        Version Version { get; }

        void OnStartPlugin(StartPluginEvent startEvent);
    }
}
