using Northwoods.Go;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gal_tinklai_o_gal_ne
{
    public class RouterSource : IListSource
    {
        private GoLayer goLayer;

        public bool ContainsListCollection
        {
            get
            {
                return false;
            }
        }

        public RouterSource(GoLayer layer)
        {
            goLayer = layer;
        }

        public IList GetList()
        {
            return goLayer.Where(o => o is RouterNode).ToList();
        }
    }
}
