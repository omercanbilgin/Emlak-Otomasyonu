using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection conn;
        public Form1()
        {
            InitializeComponent();
            conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=1234;Database=EmlakOtomasyonu");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                string kullaniciAdi = textBox1.Text;
                string sifre = textBox2.Text;

                if (!string.IsNullOrWhiteSpace(kullaniciAdi) && !string.IsNullOrWhiteSpace(sifre))
                {
                    if (CheckUserCredentials(kullaniciAdi, sifre))
                    {
                        Form2 form2 = new Form2();
                        form2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen kullanıcı adı ve şifreyi doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private bool CheckUserCredentials(string kullaniciAdi, string sifre)
        {
            try
            {
                conn.Open();

                string query = "SELECT * FROM kullanici WHERE kullanici_adi = @kullaniciAdi AND sifre = @sifre";
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, conn);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@sifre", sifre);

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                return dataTable.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
