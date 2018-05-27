using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gal_tinklai_o_gal_ne
{
    public class AsyncBindingList<T> : BindingList<T>
    {
        private ISynchronizeInvoke invoke;
        public AsyncBindingList(ISynchronizeInvoke invoke)
        {
            this.invoke = invoke;
        }

        delegate void ListChangedDelegate(ListChangedEventArgs e);

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (invoke.InvokeRequired)
            {
                IAsyncResult ar = invoke.BeginInvoke(new ListChangedDelegate(base.OnListChanged), new object[] { e });
            }
            else
            {
                base.OnListChanged(e);
            }
        }
    }
}
