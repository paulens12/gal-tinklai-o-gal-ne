using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gal_tinklai_o_gal_ne
{
    public class TopologyEntry
    {
        public string Destination { get; set; }
        public string Neighbor { get; set; }
        public int Feasible { get; set; }
        public int Advertised { get; set; }

        public TopologyEntry(string destination, string neighbor, int feasible, int advertised)
        {
            Destination = destination;
            Neighbor = neighbor;
            Feasible = feasible;
            Advertised = advertised;
        }
    }
}
