using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gal_tinklai_o_gal_ne
{
    public class NeighborEntry
    {
        public string Address { get; set; }
        public TimeSpan Hold
        {
            get
            {
                return HoldUntil.Subtract(DateTime.Now);
            }
        }
        public TimeSpan Uptime
        {
            get
            {
                return DateTime.Now.Subtract(timeCreated);
            }
        }
        private DateTime timeCreated;
        private DateTime HoldUntil;
        private TimeSpan holdFor;

        public NeighborEntry(string address, TimeSpan hold, TimeSpan uptime)
        {
            Address = address;
            holdFor = hold;
            HoldUntil = DateTime.Now.Add(hold);
            timeCreated = DateTime.Now.Subtract(uptime);
        }
        public void ExtendHold()
        {
            HoldUntil = DateTime.Now.Add(holdFor);
        }
    }
}
