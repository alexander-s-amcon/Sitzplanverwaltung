using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aufgabe_1.View
{
    public partial class frmPlatzsuche : Form
    {
        public frmPlatzsuche()
        {
            InitializeComponent();
            frmMain frmMain = new frmMain();
            this.FormBorderStyle= FormBorderStyle.FixedSingle;
            this.MaximizeBox= false;
        }

    }
}
