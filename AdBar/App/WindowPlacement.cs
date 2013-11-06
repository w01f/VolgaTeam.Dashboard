using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdBAR
{
    /// <summary>
    /// Window Placement object
    /// </summary>
    struct WindowPlacement
    {
        public int length;
        public int flags;
        public int showCmd;
        public System.Drawing.Point ptMinPosition;
        public System.Drawing.Point ptMaxPosition;
        public System.Drawing.Rectangle rcNormalPosition;
    }
}
