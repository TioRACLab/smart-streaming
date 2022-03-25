using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TioRACLab.Streaming.Core;

namespace TioRACLab.Streaming.TestPlugins
{
    public class TestDataReceive : IStreamingDataReceive
    {
        public string Name => "Test Receive";

        public string Description => "Example Test Receive";

        public Task<DataReceive> GetDataAsync(DataReceiveEvent dataEvent)
        {
            return Task.FromResult(new DataReceive
            {
                Data = new Dictionary<string, string>()
                {
                    { "intDataExample", "10" },
                    { "stringDataExample", "String Value" },
                    { "dateTimeDataExample", "2022-02-18T20:59:12.111" },
                }
            });
        }
    }
}
