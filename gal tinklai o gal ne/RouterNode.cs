using Northwoods.Go;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gal_tinklai_o_gal_ne
{
    public class RouterNode : GoBasicNode
    {
        public event EventHandler<GoSelectionEventArgs> Selected;
        public event EventHandler<GoSelectionEventArgs> Deselected;

        public RouterNode() : base() { }

        public override void OnGotSelection(GoSelection sel)
        {
            base.OnGotSelection(sel);
            Selected?.Invoke(this, new GoSelectionEventArgs(this));
        }

        public override void OnLostSelection(GoSelection sel)
        {
            base.OnLostSelection(sel);
            Deselected?.Invoke(this, new GoSelectionEventArgs(this));
        }
    }
}
