using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExileCore;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using SharpDX;

namespace Wheres_My_Cursor
{
    public class Settings : ISettings
    {

        public Settings()
        {
            WmcLineType = new RangeNode<int>(1, 1, 3);
            WmcLineColor = new Color(255, 255, 255, 255);
            WmcLineLength = new RangeNode<int>(200, 1, 1000);
            WmcLineSize = new RangeNode<int>(1, 1, 10);
            WmcPlayerOffsetXNegitive = new ToggleNode(true);
            WmcPlayerOffsetX = new RangeNode<int>(0, 0, 200);
            WmcPlayerOffsetYNegitive = new ToggleNode(true);
            WmcPlayerOffsetY = new RangeNode<int>(0, 0, 200);
        }

        public ToggleNode Enable { get; set; } = new ToggleNode(true);
        public RangeNode<int> WmcLineType { get; set; }
        public ColorNode WmcLineColor { get; set; }
        public RangeNode<int> WmcLineLength { get; set; }
        public RangeNode<int> WmcLineSize { get; set; }
        public ToggleNode WmcPlayerOffsetXNegitive { get; set; }
        public RangeNode<int> WmcPlayerOffsetX { get; set; }
        public ToggleNode WmcPlayerOffsetYNegitive { get; set; }
        public RangeNode<int> WmcPlayerOffsetY { get; set; }
    }
}
