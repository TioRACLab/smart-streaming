using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStream.Plugins
{
    public class StartPluginEvent
    {
        public StartPluginEvent(bool isEditMode)
        {
            IsEditMode = isEditMode;
        }


        public readonly bool IsEditMode;
    }
}
