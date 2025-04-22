using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Internet_Cafe
{
    public partial class FrmSatislariListele : Form
    {
        public FrmSatislariListele()
        {
            InitializeComponent();
        }

        private void FrmSatislariListele_Load(object sender, EventArgs e)
        {
            VeriTabani.Listele(dataGridView1, "select * from TBLSatis");
        }

        private void btnGeriAl_Click(object sender, EventArgs e)
        {
            int masaID = int.Parse(dataGridView1.CurrentRow.Cells["MasaID"].Value.ToString());
            int kullaniciID = int.Parse(dataGridView1.CurrentRow.Cells["KullaniciID"].Value.ToString());
            string masa = "MASA-" + masaID;
            string acilisturu = dataGridView1.CurrentRow.Cells["AcilisTuru"].Value.ToString();
            DateTime baslangic = DateTime.Parse(dataGridView1.CurrentRow.Cells["Baslangic"].Value.ToString());
            DateTime tarih = DateTime.Parse(dataGridView1.CurrentRow.Cells["Tarih"].Value.ToString());
            string sql = "insert into TBLSepet (MasaID,Masa,AcilisTuru,Baslangic,Tarih)values ('" + masaID + "','" + masa + "','" + acilisturu + "',@baslangic,@tarih)";
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@baslangic", baslangic);
            cmd.Parameters.AddWithValue("@tarih", tarih);
            VeriTabani.ESG(cmd, sql);
            ///////////////////////
            string sql2 = "delete from TBLSatis where SatisID='" + int.Parse(dataGridView1.CurrentRow.Cells["SatısID"].Value.ToString()) + "'";
            SqlCommand cmd2 = new SqlCommand();
            VeriTabani.ESG(cmd2, sql2);
            this.Close();
            FrmSatis frm = (FrmSatis)Application.OpenForms["FrmSatis"];
            frm.yenile();
        }
    }
}
