using Northwoods.Go;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gal_tinklai_o_gal_ne
{
    public partial class Form1 : Form
    {
        private Timer t = new Timer();
        //private BindingSource neighborSource = new BindingSource();

        public Form1()
        {
            Text = "EIGRP simuliacija";
            InitializeComponent();

            destBox.DisplayMember = "Text";

            destBox.DataSource = goView1.Document.DefaultLayer.Where(o => o is RouterNode).Select(o=>(o as RouterNode).Text).ToList();
            //neighborSource.Add(new NeighborEntry("fdasf", TimeSpan.FromSeconds(15), TimeSpan.Zero));
            //neighborSource.Add(new NeighborEntry("afdasf", TimeSpan.FromSeconds(15), TimeSpan.Zero));
            //neighborTable.DataSource = neighborSource; //default: null

            t.Interval = 300;
            t.Tick += RefreshSources;
            t.Start();

            goView1.BackgroundDoubleClicked += new GoInputEventHandler(backgroundDoubleClicked);
            goView1.Document.UndoManager = new GoUndoManager();
            goView1.NewLinkClass = typeof(GoLabeledLink);
            
            goView1.ObjectEdited += event1;
            goView1.Document.Changed += event2;
            goView1.DocumentChanged += DocumentChanged;
            goView1.LinkCreated += LinkCreated;
        }
        
        private void RefreshSources(object sender, EventArgs e)
        {
            //neighborSource.ResetBindings(false);
            neighborTable.ClearSelection();
            if((neighborTable.DataSource as AsyncBindingList<NeighborEntry>)?.Count > 0)
                neighborTable.Refresh();
            routingTable.ClearSelection();
            if ((routingTable.DataSource as AsyncBindingList<RouteEntry>)?.Count > 0)
                routingTable.Refresh();
            topologyTable.ClearSelection();
            if ((topologyTable.DataSource as AsyncBindingList<TopologyEntry>)?.Count > 0)
                topologyTable.Refresh();
        }

        private async void LinkCreated(object sender, GoSelectionEventArgs e)
        {
            var link = e.GoObject as GoLabeledLink;
            if(link != null)
            {
                InputForm form = new InputForm();
                Enabled = false;
                form.Show();
                bool cont = false;
                Action<object, EventArgs> act = (o, ea) =>
                {
                    link.MidLabel = new GoText { Text = form.inputBox.Text };
                    link.UserFlags = int.Parse(form.inputBox.Text);
                    link.UserObject = new object();
                    cont = true;
                    Enabled = true;
                    form.Close();
                };

                form.OKbutton.Click += new EventHandler(act);
                while (!cont)
                    await Task.Delay(10);
            }
        }

        private void DocumentChanged(object sender, GoChangedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void backgroundDoubleClicked(object sender, GoInputEventArgs e)
        {
            Guid guid = Guid.NewGuid();
            goView1.Document.StartTransaction();
            RouterNode n = new RouterNode(guid.ToString(), this);
            n.Selected += selected;
            n.Deselected += deselected;
            n.LabelSpot = GoObject.MiddleTop;
            n.Location = e.DocPoint;
            n.Editable = true;
            goView1.Document.Add(n);
            goView1.Document.FinishTransaction("new node " + guid);
        }

        private void event1(object sender, GoSelectionEventArgs e)
        {

        }
        private void event2(object sender, GoChangedEventArgs e)
        {

        }
        private void selected(object sender, GoSelectionEventArgs e)
        {
            var node = e.GoObject as RouterNode;
            if (node == null)
                return;
            neighborTable.DataSource = node.NeighborhoodTable;
            topologyTable.DataSource = node.TopologyTable;
            routingTable.DataSource = node.RoutingTable;
        }
        private void deselected(object sender, GoSelectionEventArgs e)
        {

        }
        private void event4(object sender, EventArgs e)
        {
            var x = (goView1.Selection.Primary as GoBasicNode);
            var y = x.Text;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            destBox.DataSource = goView1.Document.DefaultLayer.Where(o => o is RouterNode).Select(o => (o as RouterNode).Text).ToList();
            destBox.Refresh();
            originBox.DataSource = goView1.Document.DefaultLayer.Where(o => o is RouterNode).Select(o => (o as RouterNode).Text).ToList();
            originBox.Refresh();
        }
    }
}
