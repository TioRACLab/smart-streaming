using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStream.Manager
{
    public record FileContent : Content
    {
        public byte[] DataFile { get; init; }
    }
}
