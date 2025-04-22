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
    public partial class FrmMasaKapat : Form
    {
        public FrmMasaKapat()
        {
            InitializeComponent();
        }

        private void btnMasaKapat_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO TBLSatis (KullaniciID, MasaID, AcilisTuru, BaslangicSaati, BitisSaati, Sure, Tutar, Aciklama, Tarih) " +
               "VALUES (@KullaniciID, @MasaID, @AcilisTuru, @Baslangic, @Bitis, @Sure, @Tutar, 'Yapılmadı', @Tarih)";

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@KullaniciID", Kullanici.KullaniciID);
            cmd.Parameters.AddWithValue("@MasaID", int.Parse(txtMasaID.Text));
            cmd.Parameters.AddWithValue("@AcilisTuru", txtAcilisTuru.Text);
            cmd.Parameters.AddWithValue("@Baslangic", DateTime.Parse(txtBasSaati.Text));
            cmd.Parameters.AddWithValue("@Bitis", DateTime.Parse(txtBitisSaati.Text));
            cmd.Parameters.AddWithValue("@Sure", decimal.Parse(txtSure.Text));
            cmd.Parameters.AddWithValue("@Tutar", decimal.Parse(txtTutar.Text));
            cmd.Parameters.AddWithValue("@Tarih", DateTime.Parse(txtTarih.Text));

            VeriTabani.ESG(cmd, sorgu);

            this.Close();
            FrmSatis frm = (FrmSatis)Application.OpenForms["FrmSatis"];
            frm.yenile();

        }

        private void btniptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
