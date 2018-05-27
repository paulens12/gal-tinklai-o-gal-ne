using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gal_tinklai_o_gal_ne
{
    public class RouteEntry
    {
        public string Destination { get; set; }
        public string Neighbor { get; set; }
        private DateTime learned;
        public TimeSpan Learned
        {
            get
            {
                return DateTime.Now.Subtract(learned);
            }
        }

        public RouteEntry(string destination, string neighbor, DateTime learned)
        {
            Destination = destination;
            Neighbor = neighbor;
            this.learned = learned;
        }
    }
}
