using Northwoods.Go;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gal_tinklai_o_gal_ne
{
    public class RouterNode : GoBasicNode
    {
        private Timer helloTimer;
        private Timer holdTimer;

        public event EventHandler<GoSelectionEventArgs> Selected;
        public event EventHandler<GoSelectionEventArgs> Deselected;

        private object tablesLock = new object();

        public AsyncBindingList<NeighborEntry> NeighborhoodTable { get; set; }
        public AsyncBindingList<RouteEntry> RoutingTable { get; set; }
        public AsyncBindingList<TopologyEntry> TopologyTable { get; set; }

        public RouterNode(string text, ISynchronizeInvoke invoke) : base()
        {
            NeighborhoodTable = new AsyncBindingList<NeighborEntry>(invoke);
            RoutingTable = new AsyncBindingList<RouteEntry>(invoke);
            TopologyTable = new AsyncBindingList<TopologyEntry>(invoke);
            //NeighborhoodTable.Add(new NeighborEntry("dummy" + text, TimeSpan.FromSeconds(15), TimeSpan.Zero));
            helloTimer = new Timer(SendHello, null, 0, 5000);
            holdTimer = new Timer(RemoveMissingNeighbors, null, 0, 100);
            Text = text;
            // disable: timer.Change(Timeout.Infinite, Timeout.Infinite);

        }

        private void RemoveMissingNeighbors(object o)
        {
            IEnumerable<NeighborEntry> list;
            lock (tablesLock)
            {
                list = NeighborhoodTable.Where(n => n.Hold.CompareTo(TimeSpan.Zero) < 0).ToList();
            }
            foreach (NeighborEntry e in list)
            {
                lock (tablesLock)
                {
                    NeighborhoodTable.Remove(e);
                    var top = TopologyTable.FirstOrDefault(t => t.Destination.Equals(e.Address) && t.Neighbor.Equals(e.Address));
                    if (top != null)
                        TopologyTable.Remove(top);
                }
                foreach(var node in Nodes)
                {
                    RouterNode router = node as RouterNode;
                    if (router == null)
                        continue;
                    GoLabeledLink link = Links.FirstOrDefault(l => l is GoLabeledLink && ((l as GoLabeledLink).FromNode.Equals(router) || (l as GoLabeledLink).ToNode.Equals(router))) as GoLabeledLink;
                    TopologyEntry currentTopology;
                    lock (tablesLock)
                    {
                        currentTopology = TopologyTable.Where(t => t.Destination.Equals(e.Address))?.OrderBy(t => t.Feasible).FirstOrDefault();
                    }
                    int newAdvertised = currentTopology == null ? -1 : currentTopology.Feasible;
                    router.UpdateTopology(new TopologyEntry(e.Address, Text, newAdvertised + link.UserFlags, newAdvertised));
                    RouteEntry currentRoute;
                    lock (tablesLock)
                    {
                        currentRoute = RoutingTable.FirstOrDefault(r => r.Destination.Equals(e.Address));
                        if (!currentRoute.Neighbor.Equals(currentTopology.Neighbor))
                        {
                            RoutingTable.Remove(currentRoute);
                            RoutingTable.Add(new RouteEntry(e.Address, currentTopology.Neighbor, DateTime.Now));
                        }
                    }
                }
            }
        }

        public void UpdateTopology(TopologyEntry suggested)
        {
            //remove the entry
            var top = TopologyTable.FirstOrDefault(t => t.Destination.Equals(suggested.Destination) && t.Neighbor.Equals(suggested.Neighbor));
            if (top != null)
                lock (tablesLock)
                {
                    TopologyTable.Remove(top);
                }
            if(suggested.Advertised >= 0)
            {
                //add a new one
                lock(tablesLock)
                {
                    AddTopology(suggested);
                }
            }

        }

        public void AddNeighbor(RouterNode neighbor, int metric)
        {
            lock(tablesLock)
            {
                NeighborhoodTable.Add(new NeighborEntry(neighbor.Text, TimeSpan.FromSeconds(15), TimeSpan.Zero));
            }
            AddTopology(new TopologyEntry(neighbor.Text, neighbor.Text, metric, 0));
        }

        public async Task ReceiveHello(RouterNode sender, GoLabeledLink link)
        {
            var existing = NeighborhoodTable.FirstOrDefault(n => n.Address.Equals(sender.Text));
            if(existing != null)
            {
                existing.ExtendHold();
            }
            else
            {
                AddNeighbor(sender, link.UserFlags);
                sender.AddNeighbor(this, link.UserFlags);
                await Task.Delay(200);
                await SendAckTo(sender, link);
            }
        }

        private async Task SendAckTo(RouterNode node, GoLabeledLink link)
        {
            try
            {
                while (link.PenColor != Color.Black)
                    await Task.Delay(50);
                link.PenColor = Color.Green;
                TakeTopologyTable(node.TopologyTable, node.Text, link);
                node.TakeTopologyTable(TopologyTable, Text, link);
                await Task.Delay(100);
                link.PenColor = Color.Black;
            }
            catch (InvalidAsynchronousStateException) { }
        }

        private void TakeTopologyTable(AsyncBindingList<TopologyEntry> newTable, string neighbor, GoLabeledLink link)
        {
            foreach(TopologyEntry newEntry in newTable)
            {
                TopologyEntry entry = new TopologyEntry(newEntry.Destination, neighbor, link.UserFlags + newEntry.Feasible, newEntry.Feasible);
                AddTopology(entry);
            }
        }

        public void AddTopology(TopologyEntry newEntry)
        {
            lock (tablesLock)
            {
                if (newEntry.Destination.Equals(Text))
                    return;

                foreach (var neighbor in NeighborhoodTable)
                {
                    if (neighbor.Address.Equals(newEntry.Neighbor))
                        continue;

                    RouterNode node = Nodes.FirstOrDefault(n => n is RouterNode && (n as RouterNode).Text.Equals(neighbor.Address)) as RouterNode;
                    GoLabeledLink link = Links.FirstOrDefault(l => l is GoLabeledLink && ((l as GoLabeledLink).FromNode.Equals(node) || (l as GoLabeledLink).ToNode.Equals(node))) as GoLabeledLink;
                    node.AddTopology(new TopologyEntry(newEntry.Destination, Text, link.UserFlags + newEntry.Feasible, newEntry.Feasible));
                }

                var route = RoutingTable.FirstOrDefault(r => r.Destination.Equals(newEntry.Destination));
                if (route == null)
                {
                    //dar neturim route, reiskia nieko nebus ir topology table
                    RoutingTable.Add(new RouteEntry(newEntry.Destination, newEntry.Neighbor, DateTime.Now));
                    TopologyTable.Add(newEntry);
                    return;
                }

                var existing = TopologyTable.Where(t => t.Destination.Equals(newEntry.Destination));
                var fastest = existing.OrderBy(t => t.Feasible).FirstOrDefault();

                if (newEntry.Advertised >= fastest.Feasible)
                    return;

                if (existing.Count() >= 6)
                {
                    //overflow
                    var slowest = existing.OrderByDescending(t => t.Feasible).FirstOrDefault();
                    if (slowest.Feasible < newEntry.Feasible)
                        return;
                    TopologyTable.Remove(slowest);
                }
                TopologyTable.Add(newEntry);

                if (newEntry.Feasible < fastest.Feasible)
                {
                    RoutingTable.Remove(route);
                    RoutingTable.Add(new RouteEntry(newEntry.Destination, newEntry.Neighbor, DateTime.Now));
                }
            }
        }

        private void SendHello(object state)
        {
            helloTimer.Change(Timeout.Infinite, Timeout.Infinite);
            List<Task> tasks = new List<Task>();
            foreach (var node in Nodes)
            {
                RouterNode router = node as RouterNode;
                if (router != null)
                    tasks.Add(SendHelloTo(router));
            }

            Task.WaitAll(tasks.ToArray());
            helloTimer.Change(5000, 5000);
        }

        private async Task SendHelloTo(RouterNode destination)
        {
            try
            {
                GoLabeledLink link = Links.FirstOrDefault(l => l.ToNode.Equals(destination) || l.FromNode.Equals(destination)) as GoLabeledLink;
                if (link.UserFlags < 1)
                    return;
                await destination.ReceiveHello(this, link);
                while (link.PenColor != Color.Black)
                    await Task.Delay(50);
                link.PenColor = Color.Red;
                await Task.Delay(100);
                link.PenColor = Color.Black;
            }
            catch (InvalidAsynchronousStateException) { }
        }

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
