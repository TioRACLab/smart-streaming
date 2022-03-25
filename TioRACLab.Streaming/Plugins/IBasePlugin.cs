using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TioRACLab.Streaming.Plugins
{
    public interface IBasePlugin
    {
        string Name { get; }

        string Description { get; }
    }
}
