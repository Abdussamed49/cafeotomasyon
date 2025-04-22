using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Internet_Cafe
{
    class Kullanici
    {
        public static int KullaniciID = 0;
        public static bool durum = false;
        public static SqlDataReader KullaniciGirisi(TextBox KullaniciAdi,TextBox Sifre)
        {
            VeriTabani.baglanti.Open();
            SqlCommand cmd = new SqlCommand("select * from TBLKullanici where KullaniciAdi='" + KullaniciAdi.Text + "' and Sifre='" + Sifre.Text + "'", VeriTabani.baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                durum = true;
                KullaniciID = int.Parse(dr["KullaniciID"].ToString());
            }
            else
            {
                durum = false;
            }
            VeriTabani.baglanti.Close();
            return dr;
        }
    }
}
