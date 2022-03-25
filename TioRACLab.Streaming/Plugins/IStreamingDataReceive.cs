using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TioRACLab.Streaming.Plugins
{
    public interface IStreamingDataReceive : IBasePlugin
    {
        Task<DataReceive> GetDataAsync(DataReceiveEvent dataEvent);
    }
}
