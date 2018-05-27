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
        private BindingSource neighborSource = new BindingSource();

        public Form1()
        {
            Text = "EIGRP simuliacija";
            InitializeComponent();
            neighborSource.Add(new NeighborEntry(1, "fdasf", 1, 12, 1, 1, 1));
            neighborTable.DataSource = neighborSource;
            goView1.BackgroundDoubleClicked += new GoInputEventHandler(backgroundDoubleClicked);
            goView1.Document.UndoManager = new GoUndoManager();
            goView1.ObjectEdited += event1;
            goView1.Document.Changed += event2;
            goView1.link
            //goView1.ObjectGotSelection += selected;
            //goView1.ObjectLostSelection += deselected;
            //goView1.SelectionFinished += event4;
        }

        private void backgroundDoubleClicked(object sender, GoInputEventArgs e)
        {
            goView1.Document.StartTransaction();
            RouterNode n = new RouterNode();
            n.Selected += selected;
            n.Deselected += deselected;
            n.LabelSpot = GoObject.MiddleTop;
            Guid guid = Guid.NewGuid();
            n.Text = guid.ToString();
            n.Location = e.DocPoint;
            n.Editable = true;
            goView1.Document.Add(n);
            goView1.Document.FinishTransaction("new node");
        }

        private void event1(object sender, GoSelectionEventArgs e)
        {

        }
        private void event2(object sender, GoChangedEventArgs e)
        {

        }
        private void selected(object sender, GoSelectionEventArgs e)
        {

        }
        private void deselected(object sender, GoSelectionEventArgs e)
        {

        }
        private void event4(object sender, EventArgs e)
        {
            //var x = (goView1.Selection.Primary as GoBasicNode).Text;
            //var y = x.Text;
        }
    }
}
