using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStream.Manager
{
    public record Content
    {
        public Guid Id { get; set; }

        public string Name { get; init; }

        public string FriendlyName { get; init; }
    }
}
