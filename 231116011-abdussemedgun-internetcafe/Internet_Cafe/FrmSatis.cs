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
    public partial class FrmSatis : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=ABDUSSAMED\\sqlexpress;Initial Catalog=InternetCafe;Integrated Security=True;Pooling=False");
        public FrmSatis()
        {
            InitializeComponent();
        }
        Button btn;
        private void SecileneGore(object sender, MouseEventArgs e)
        {
            btn = sender as Button;
            if (btn.BackColor == Color.OrangeRed)
            {
                süreliMaçaAçmaİsteğiToolStripMenuItem.Visible = false;
                süresizMasaAçmaİsteğiToolStripMenuItem.Visible = false;
            }
            if (btn.BackColor == Color.LightGreen)
            {
                süreliMaçaAçmaİsteğiToolStripMenuItem.Visible = true;
                süresizMasaAçmaİsteğiToolStripMenuItem.Visible = true;
            }

        }
        RadioButton radio;
        private void RadioButtonSeciliyeGore(object sender, EventArgs e)
        {
            radio = sender as RadioButton;
        }

        private void FrmSatis_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'internetCafeDataSet.TBLSaatUcreti' table. You can move, or remove it, as needed.
            this.tBLSaatUcretiTableAdapter.Fill(this.internetCafeDataSet.TBLSaatUcreti);
            rdrbSuresiz.Checked = true;
            yenile();
            cmbBosMasalar.Text = "";
            timer1.Start();
        }

        public void yenile()
        {
            VeriTabani.SepetListele(dataGridView1);
            VeriTabani.ListViewdeKayitlariGoster(listView1);
            VeriTabani.CombayaBosmasaGetir(cmbBosMasalar);
            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    if (item.Name != btnMasaAc.Name)
                    {
                        VeriTabani.baglanti.Open();
                        SqlCommand komut = new SqlCommand("select * from TBLMasalar", VeriTabani.baglanti);
                        SqlDataReader dr = komut.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["Durumu"].ToString() == "BOŞ" && dr["Masalar"].ToString() == item.Text)
                            {
                                item.BackColor = Color.LightGreen;
                            }
                            if (dr["Durumu"].ToString() == "DOLU" && dr["Masalar"].ToString() == item.Text)
                            {
                                item.BackColor = Color.OrangeRed;
                            }

                        }
                        VeriTabani.baglanti.Close();
                    }
                }
            }
        }

        private void btnMasaAc_Click(object sender, EventArgs e)
        {
            if (Kullanici.KullaniciID == 1)
            {
                string sqlsorgu = "INSERT INTO TBLSepet (MasaID, Masa, AcilisTuru, Baslangic, Tarih) VALUES ('"
                + cmbBosMasalar.Text.Substring(5) + "','"
                + cmbBosMasalar.Text + "','"
                + radio.Text + "', CONVERT(DATETIME, '"
                + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "', 104), CONVERT(DATETIME, '"
                + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "', 104))";
                SqlCommand komut = new SqlCommand();
                VeriTabani.ESG(komut, sqlsorgu);
                MessageBox.Show(cmbBosMasalar.Text.Substring(5) + " Nolu Masa Açılmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                yenile();
                rdrbSuresiz.Checked = true;

            }
            else
            {
                MessageBox.Show("Böyle Bir Yetkiniz Bulunmuyor", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FrmSatis_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Hesapla"].Index)
            {
                if (cmbSaatUcret.Text != "")
                {
                    DateTime BitisTarihi = DateTime.Now;
                    DateTime BaslangicTarihi = DateTime.Parse(dataGridView1.CurrentRow.Cells["BaslamaSaati"].Value.ToString());
                    TimeSpan saatfarki = BitisTarihi - BaslangicTarihi;
                    double saatfarki2 = saatfarki.TotalHours;
                    dataGridView1.CurrentRow.Cells["Sure"].Value = saatfarki2.ToString("0.00");
                    double toplamtutar = saatfarki2 * double.Parse(cmbSaatUcret.Text);
                    dataGridView1.CurrentRow.Cells["Tutar"].Value = toplamtutar.ToString("0.00");
                    dataGridView1.CurrentRow.Cells["BitisSaati"].Value = BitisTarihi;


                }
                if (cmbSaatUcret.Text == "")
                {
                    MessageBox.Show("Önce Saat Ücreti Seçiniz", "Uyarı");
                }

            }
            if (e.ColumnIndex == dataGridView1.Columns["MasaKapat"].Index)
            {
                if (dataGridView1.CurrentRow.Cells["BitisSaati"].Value != null)
                {
                    FrmMasaKapat frmMasaKapat = new FrmMasaKapat();
                    frmMasaKapat.txtID.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                    frmMasaKapat.txtMasaID.Text = dataGridView1.CurrentRow.Cells["Masa_ID"].Value.ToString();
                    frmMasaKapat.txtMasa.Text = dataGridView1.CurrentRow.Cells["_Masa"].Value.ToString();
                    frmMasaKapat.txtAcilisTuru.Text = dataGridView1.CurrentRow.Cells["AcilisTuru"].Value.ToString();
                    frmMasaKapat.txtBasSaati.Text = dataGridView1.CurrentRow.Cells["BaslamaSaati"].Value.ToString();
                    frmMasaKapat.txtBitisSaati.Text = dataGridView1.CurrentRow.Cells["BitisSaati"].Value.ToString();
                    frmMasaKapat.txtSaatUcret.Text = cmbSaatUcret.Text;
                    frmMasaKapat.txtSure.Text = dataGridView1.CurrentRow.Cells["Sure"].Value.ToString();
                    frmMasaKapat.txtTutar.Text = dataGridView1.CurrentRow.Cells["Tutar"].Value.ToString();
                    frmMasaKapat.txtTarih.Text = dataGridView1.CurrentRow.Cells["_Tarih"].Value.ToString();
                    frmMasaKapat.ShowDialog();
                }
                else if (dataGridView1.CurrentRow.Cells["BitisSaati"].Value == null)
                {
                    MessageBox.Show("Önce Hesaplama Yapılmalıdır", "Uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }


            }
        }
        string istek = "";
        private void yöneticiÇağırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istek = "Yöneticiyi çağrıyor.";
            Istekler();
        }

        private void Istekler()
        {
            SqlCommand komut = new SqlCommand();
            string sqlsorgu = "INSERT INTO TBLHareketler (KullanıcıID, MasaID, AcilisTuru, Aciklama, Tarih) " +
                 "VALUES ('" + Kullanici.KullaniciID + "','" + btn.Text.Substring(5) + "','" + btn.Text + "','"
                 + istek + "','Yapılmadı', CONVERT(DATETIME, '" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "', 104))";

            VeriTabani.ESG(komut, sqlsorgu);
            VeriTabani.ListViewdeKayitlariGoster(listView1);
        }

        private void süresizMasaAçmaİsteğiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istek = "Süresiz Masa Açma İsteği Gönderdi"; ;
            Istekler();
        }

        private void masaDeğiştirmeİsteğiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istek = "Masa değiştime isteği gönderdi.";
            Istekler();
        }
        ToolStripMenuItem item;
        private void SeruliIstekSecilirse(object sender, EventArgs e)
        {
            item = sender as ToolStripMenuItem;
            istek = item.Text + " dk süre ile masa açma isteği gönderdi";
            Istekler();
        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if (sayac > 29)
            {
                if (cmbSaatUcret.Text != "")
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        DateTime BitisTarihi = DateTime.Now;
                        DateTime BaslangicTarihi = DateTime.Parse(dataGridView1.Rows[i].Cells["BaslamaSaati"].Value.ToString());
                        TimeSpan saatfarki = BitisTarihi - BaslangicTarihi;
                        double saatfarki2 = saatfarki.TotalHours;
                        dataGridView1.Rows[i].Cells["Sure"].Value = saatfarki2.ToString("0.00");
                        double toplamtutar = saatfarki2 * double.Parse(cmbSaatUcret.Text);
                        dataGridView1.Rows[i].Cells["Tutar"].Value = toplamtutar.ToString("0.00");
                        dataGridView1.Rows[i].Cells["BitisSaati"].Value = BitisTarihi;

                    }

                }
                if (cmbSaatUcret.Text == "")
                {
                    MessageBox.Show("Önce Saat Ücreti Seçiniz", "Uyarı");
                }
            }
        }

        private void btnMasaDegistir_Click(object sender, EventArgs e)
        {
            int SepetID = int.Parse(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            int MasaID = int.Parse(dataGridView1.CurrentRow.Cells["Masa_ID"].Value.ToString());
            string sql = "update TBLSepet set MasaID='" + int.Parse(cmbBosMasalar.Text.Substring(5)) + "',Masa='" + cmbBosMasalar.Text + "' where SepetID='" + SepetID + "' ";
            SqlCommand cmd = new SqlCommand();
            VeriTabani.ESG(cmd, sql);
            /////////////////////////
            string sql2 = "update TBLMasalar set  Durumu='BOŞ' where MasaID='" + MasaID + "'";
            SqlCommand cmd2 = new SqlCommand();
            VeriTabani.ESG(cmd2, sql2);
            /////////////////////////
            string sql3 = "update TBLMasalar set  Durumu='DOLU' where MasaID='" + int.Parse(cmbBosMasalar.Text.Substring(5)) + "'";
            SqlCommand cmd3 = new SqlCommand();
            VeriTabani.ESG(cmd3, sql3);
            yenile();
            MessageBox.Show("Masa Değiştirme İşlemi Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Sure"].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells["AcilisTuru"].Value.ToString() != "Süresiz")
                    {
                        double sure = double.Parse(dataGridView1.Rows[i].Cells["Sure"].Value.ToString()) * 60;
                        double Acilisturu = double.Parse(dataGridView1.Rows[i].Cells["AcilisTuru"].Value.ToString());
                        if (Acilisturu - sure <= 5.0)
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            FrmSatislariListele frm = new FrmSatislariListele();
            frm.btnGeriAl.Enabled = true;
            frm.ShowDialog();
        }

        private void btnSatisRapor_Click(object sender, EventArgs e)
        {
            FrmSatisRaporu frm = new FrmSatisRaporu();
            frm.ShowDialog();
        }

        private void btnHareketRapor_Click(object sender, EventArgs e)
        {
            FrmHareketRaporu frm = new FrmHareketRaporu();
            frm.ShowDialog();
        }
    }
}
