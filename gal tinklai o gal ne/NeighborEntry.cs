using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gal_tinklai_o_gal_ne
{
    class NeighborEntry
    {
        public int Handle { get; set; }
        public string Address { get; set; }
        //public string Interface { get; set; }
        public int Hold { get; set; }
        public int Uptime { get; set; }
        public int SmoothRoundTripTime { get; set; }
        public int RetransmissionTimeout { get; set; }
        //public int QueueCount { get; set; }
        public int SequenceNumber { get; set; }

        public NeighborEntry(int handle, string address, int hold, int uptime, int srtt, int rtt, int seq)
        {
            Handle = handle;
            Address = address;
            Hold = hold;
            Uptime = uptime;
            SmoothRoundTripTime = srtt;
            RetransmissionTimeout = rtt;
            SequenceNumber = seq;
        }
    }
}
