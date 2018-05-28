using Northwoods.Go;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gal_tinklai_o_gal_ne
{
    public class RouterNode : GoBasicNode
    {
        private List<Guid> GuidHistory = new List<Guid>();

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
        /*
        private void RemoveMissingNeighbors(object o)
        {
            IEnumerable<NeighborEntry> list;
            lock (tablesLock)
            {
                list = NeighborhoodTable.Where(n => n.Hold.CompareTo(TimeSpan.Zero) < 0).ToList();
            }
            foreach (NeighborEntry e in list)
            {
                Guid guid = Guid.NewGuid();
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
                    router.TakeTopologyTable(TopologyTable, Text, link, guid);
                    RouteEntry currentRoute;
                    lock (tablesLock)
                    {
                        currentRoute = RoutingTable.FirstOrDefault(r => r.Destination.Equals(e.Address));
                        if (currentTopology != null && !currentRoute.Neighbor.Equals(currentTopology.Neighbor))
                        {
                            RoutingTable.Remove(currentRoute);
                            RoutingTable.Add(new RouteEntry(e.Address, currentTopology.Neighbor, DateTime.Now));
                        }
                    }
                }
            }
        }
        */

        public void BeginTopologyRefresh()
        {
            //das
        }

        public void EndTopologyRefresh()
        {
            //dfdsafds
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
                Guid guid = Guid.NewGuid();
                lock (tablesLock)
                {
                    NeighborhoodTable.Remove(e);
                    var top = TopologyTable.FirstOrDefault(t => t.Destination.Equals(e.Address) && t.Neighbor.Equals(e.Address));
                    if (top != null)
                        AddTopology(new TopologyEntry(e.Address, e.Address, -1, -1));
                }
                var neighbors = NeighborhoodTable.Where(n => list.FirstOrDefault(l => l.Address.Equals(n.Address)) == null).ToList();
                foreach (var neighbor in neighbors)
                {
                    var node = Nodes.FirstOrDefault(n => (n as RouterNode).Text.Equals(neighbor.Address)) as RouterNode;
                    var link = Links.FirstOrDefault(l => l.ToNode.Equals(node) || l.FromNode.Equals(node)) as GoLabeledLink;
                    if (node == null || link == null)
                        continue;

                    TopologyEntry fastest;
                    lock (tablesLock)
                    {
                        fastest = TopologyTable.Where(t => t.Destination.Equals(e.Address)).OrderBy(t => t.Feasible).FirstOrDefault();
                    }

                    node.AddTopology(new TopologyEntry(e.Address, Text, fastest == null ? -1 : fastest.Feasible + link.UserFlags, fastest == null ? -1 : fastest.Feasible));

                }
            }
        }

        public void AddTopology(TopologyEntry entry)
        {
            /*
            if(entry.Feasible == -1)
            {
                //remove topology
                lock(tablesLock)
                {
                    var existing = TopologyTable.FirstOrDefault(t => t.Destination.Equals(entry.Destination) && t.Neighbor.Equals(entry.Neighbor));
                    if(existing != null)
                    {
                        TopologyTable.Remove(existing);
                        UpdateRoute(entry);
                    }
                    return;
                }
            }
            */
            if (entry.Destination.Equals(Text))
                return;
            if (entry.Feasible != -1)
            {
                lock (tablesLock)
                {
                    var fastest = TopologyTable.Where(t => t.Destination.Equals(entry.Destination))?.OrderBy(t => t.Feasible).FirstOrDefault();

                    if (fastest != null && entry.Advertised >= fastest.Feasible)
                        return;

                    var existing = TopologyTable.Where(t => t.Destination.Equals(entry.Destination) && t.Neighbor.Equals(entry.Neighbor) && t.Feasible == entry.Feasible).FirstOrDefault();
                    if (existing != null)
                        return;

                    TopologyTable.Add(entry);
                }
            }
            else
            {
                lock (tablesLock)
                {
                    var existing = TopologyTable.FirstOrDefault(t => t.Destination.Equals(entry.Destination) && t.Neighbor.Equals(entry.Neighbor));
                    if (existing != null)
                    {
                        TopologyTable.Remove(existing);
                    }
                }
            }
            UpdateRoute(entry);

            var neighbors = NeighborhoodTable.Where(n => !n.Address.Equals(entry.Neighbor)).ToList();
            foreach (var neighbor in neighbors)
            {
                var node = Nodes.FirstOrDefault(n => (n as RouterNode).Text.Equals(neighbor.Address)) as RouterNode;
                var link = Links.FirstOrDefault(l => l.ToNode.Equals(node) || l.FromNode.Equals(node)) as GoLabeledLink;
                if (node == null || link == null)
                    continue;

                TopologyEntry fastest;
                lock (tablesLock)
                {
                    fastest = TopologyTable.Where(t => t.Destination.Equals(entry.Destination)).OrderBy(t => t.Feasible).FirstOrDefault();
                }

                node.AddTopology(new TopologyEntry(entry.Destination, Text, fastest == null ? -1 : fastest.Feasible + link.UserFlags, fastest.Feasible));
            }

        }



        private void UpdateRoute(TopologyEntry newEntry)
        {
            lock (tablesLock)
            {
                var oldEntry = RoutingTable.FirstOrDefault(r => r.Destination.Equals(newEntry.Destination));
                if (oldEntry == null && newEntry.Feasible != -1)
                    RoutingTable.Add(new RouteEntry(newEntry.Destination, newEntry.Neighbor, DateTime.Now));
                else
                {
                    var fastest = TopologyTable.Where(t => t.Destination.Equals(newEntry.Destination)).OrderBy(t => t.Feasible).FirstOrDefault();
                    if(fastest == null)
                    {
                        RoutingTable.Remove(oldEntry);
                    }
                    else if(!oldEntry.Neighbor.Equals(fastest.Neighbor))
                    {
                        RoutingTable.Remove(oldEntry);
                        RoutingTable.Add(new RouteEntry(fastest.Destination, fastest.Neighbor, DateTime.Now));
                    }
                }
            }
        }

        /*
        public void UpdateTopology(TopologyEntry suggested, Guid guid)
        {
            TopologyEntry fastest;
            bool changed = false;
            //remove the entry
            lock (tablesLock)
            {
                var top = TopologyTable.FirstOrDefault(t => t.Destination.Equals(suggested.Destination) && t.Neighbor.Equals(suggested.Neighbor));
                if (top != null)
                    TopologyTable.Remove(top);

                fastest = TopologyTable.Where(t => t.Destination.Equals(suggested.Destination))?.OrderBy(t => t.Feasible).FirstOrDefault();

                var route = RoutingTable.FirstOrDefault(r => r.Destination.Equals(suggested.Destination));
                if (route != null && route.Neighbor.Equals(suggested.Neighbor))
                {
                    //update routing table too
                    RoutingTable.Remove(route);
                    changed = true;
                    if (fastest != null)
                        RoutingTable.Add(new RouteEntry(fastest.Destination, fastest.Neighbor, DateTime.Now));
                }

                // add new topology
                if (suggested.Advertised < 0)
                    //nothing to do here
                    return;
            }
            AddTopology(suggested, guid, false);


            if (changed)
            {
                List<Task> tasks = new List<Task>();
                foreach (var neighbor in NeighborhoodTable.Where(n => !n.Address.Equals(suggested.Neighbor)).ToList())
                {
                    Task t = new Task(() =>
                    {
                        var node = Nodes.FirstOrDefault(n => (n as RouterNode).Text.Equals(neighbor.Address)) as RouterNode;
                        var link = Links.FirstOrDefault(l => l.ToNode.Equals(node)) as GoLabeledLink;
                        if (link == null)
                            return;

                        var newSuggested = new TopologyEntry(suggested.Destination, Text, fastest.Feasible + link.UserFlags, fastest.Feasible);
                        lock (link.UserObject)
                        {
                            link.PenColor = Color.Yellow;
                            Task t1 = Task.Delay(2000);

                            node.UpdateTopology(newSuggested, guid);

                            t1.Wait();
                            link.PenColor = Color.Black;
                        }
                    });
                    t.Start();
                    tasks.Add(t);
                }
                Task.WaitAll(tasks.ToArray());
            }
        }
        */
        
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

        public void SendTopology(RouterNode node, GoLabeledLink link)
        {
            List<TopologyEntry> tempTable;
            lock (tablesLock)
            {
                tempTable = TopologyTable.ToList();
            }

            foreach (var topology in TopologyTable)
            {
                node.AddTopology(new TopologyEntry(topology.Destination, Text, topology.Feasible + link.UserFlags, topology.Feasible));
            }
        }

        private async Task SendAckTo(RouterNode node, GoLabeledLink link)
        {
            try
            {
                lock (link.UserObject)
                {
                    link.PenColor = Color.Green;
                    SendTopology(node, link);
                    node.SendTopology(this, link);

                    //TakeTopologyTable(node.TopologyTable, node.Text, link, Guid.NewGuid());
                    //node.TakeTopologyTable(TopologyTable, Text, link, Guid.NewGuid());


                    Task t = Task.Delay(100);
                    t.Wait();
                    link.PenColor = Color.Black;
                }
            }
            catch (InvalidAsynchronousStateException) { }
        }

        /*
        private void TakeTopologyTable(AsyncBindingList<TopologyEntry> newTable, string neighbor, GoLabeledLink link, Guid guid)
        {
            if (GuidHistory.Contains(guid))
                return;
            lock(tablesLock)
            {
                var existing = TopologyTable.Where(t => t.Neighbor.Equals(neighbor)).ToList();
                foreach(var x in existing)
                {
                    TopologyTable.Remove(x);
                }
            }

            foreach(TopologyEntry newEntry in newTable)
            {
                TopologyEntry entry = new TopologyEntry(newEntry.Destination, neighbor, link.UserFlags + newEntry.Feasible, newEntry.Feasible);
                AddTopology(entry, guid);
            }
        }

        public void AddTopology(TopologyEntry newEntry, Guid guid, bool PassToNeighbors = true)
        {
            if (GuidHistory.Contains(guid))
                return;
            GuidHistory.Add(guid);
            lock (tablesLock)
            {
                if (newEntry.Destination.Equals(Text))
                    return;
                if (PassToNeighbors)
                {
                    foreach (var neighbor in NeighborhoodTable)
                    {
                        if (neighbor.Address.Equals(newEntry.Neighbor))
                            continue;

                        RouterNode node = Nodes.FirstOrDefault(n => n is RouterNode && (n as RouterNode).Text.Equals(neighbor.Address)) as RouterNode;
                        GoLabeledLink link = Links.FirstOrDefault(l => l is GoLabeledLink && ((l as GoLabeledLink).FromNode.Equals(node) || (l as GoLabeledLink).ToNode.Equals(node))) as GoLabeledLink;
                        if (node == null || link == null)
                            continue;
                        node.AddTopology(new TopologyEntry(newEntry.Destination, Text, link.UserFlags + newEntry.Feasible, newEntry.Feasible), guid);
                    }
                }

                var route = RoutingTable.FirstOrDefault(r => r.Destination.Equals(newEntry.Destination));
                if (route == null)
                {
                    //dar neturim route, reiskia nieko nebus ir topology table
                    RoutingTable.Add(new RouteEntry(newEntry.Destination, newEntry.Neighbor, DateTime.Now));
                    TopologyTable.Add(newEntry);
                    return;
                }

                Debug.WriteLine("Origin: {0}, Destination: {1}", Text, newEntry.Destination);

                var existing = TopologyTable.Where(t => t.Destination.Equals(newEntry.Destination));
                var fastest = existing.OrderBy(t => t.Feasible).FirstOrDefault();


                Debug.WriteLine("Fastest feasible: {0}, new advertised:{1}", fastest == null ? 0 : fastest.Feasible, newEntry.Advertised);

                if (fastest != null && newEntry.Advertised >= fastest.Feasible)
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
                fastest = existing.OrderBy(t => t.Feasible).FirstOrDefault();
                if (newEntry.Feasible < fastest.Feasible)
                {
                    RoutingTable.Remove(route);
                    RoutingTable.Add(new RouteEntry(newEntry.Destination, newEntry.Neighbor, DateTime.Now));
                }
            }
        }
        */

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
