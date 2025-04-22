using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internet_Cafe
{
    public partial class FrmHareketRaporu : Form
    {
        public FrmHareketRaporu()
        {
            InitializeComponent();
        }

        private void FrmHareketRaporu_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'InternetCafeDataSet.TBLHareketler' table. You can move, or remove it, as needed.
            this.TBLHareketlerTableAdapter.Fill(this.InternetCafeDataSet.TBLHareketler);

            this.reportViewer1.RefreshReport();
        }
    }
}
