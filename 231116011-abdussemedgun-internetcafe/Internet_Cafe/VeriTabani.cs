﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internet_Cafe
{
    class VeriTabani
    {
        public static SqlConnection baglanti = new SqlConnection("Data Source=ABDUSSAMED\\sqlexpress;Initial Catalog=InternetCafe;Integrated Security=True;Pooling=False");
        public static DataTable SepetListele(DataGridView gridview)
        {
            SqlDataAdapter adtr = new SqlDataAdapter("select * from TBLSepet", baglanti);
            DataTable tbl = new DataTable();
            adtr.Fill(tbl);
            gridview.DataSource = tbl;
            baglanti.Close();
            return tbl;
        }
        public static DataTable Listele(DataGridView gridview,string sorgu)
        {
            SqlDataAdapter adtr = new SqlDataAdapter(sorgu, baglanti);
            DataTable tbl = new DataTable();
            adtr.Fill(tbl);
            gridview.DataSource = tbl;
            baglanti.Close();
            return tbl;
        }
        public static DataTable CombayaBosmasaGetir(ComboBox combo)
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from TBLMasalar where durumu='BOŞ'", baglanti);
            DataTable tbl = new DataTable();
            adtr.Fill(tbl);
            combo.DataSource = tbl;
            combo.DisplayMember = "Masalar";
            combo.ValueMember = "MasaID";
            baglanti.Close();
            return tbl;
        }
        public static void ESG(SqlCommand command, string sorgu)
        {
            baglanti.Open();
            command.Connection = baglanti;
            command.CommandText = sorgu;
            command.ExecuteNonQuery();
            baglanti.Close();

        }
        public static SqlDataReader ListViewdeKayitlariGoster(ListView list)
        {
            list.Items.Clear();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select * from TBLHareketler where Tarih>=@Tarih", baglanti);
            cmd.Parameters.AddWithValue("@Tarih", DateTime.Parse(DateTime.Now.ToShortDateString()));
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr[0].ToString();
                ekle.SubItems.Add(dr[1].ToString());
                ekle.SubItems.Add(dr[2].ToString());
                ekle.SubItems.Add(dr[3].ToString());
                ekle.SubItems.Add(dr[4].ToString());
                ekle.SubItems.Add(dr[5].ToString());
                ekle.SubItems.Add(dr[6].ToString());
                list.Items.Add(ekle);

            }
            baglanti.Close();
            return dr;
        }
    }
}
