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
    public partial class Form5 : Form
    {
        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=EmlakOtomasyonu";
        private int selectedEvId;
        private int evId;
        public Form5()
        {
            InitializeComponent();
            selectedEvId = evId;
            IlaniGetir(evId);
        }
        public void IlaniGetir(int evId)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM ev WHERE ev_id = @ev_id", connection))
                    {
                        command.Parameters.AddWithValue("@ev_id", evId);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox1.Text = reader["sehir"].ToString();
                                textBox2.Text = reader["ilce"].ToString();
                                textBox3.Text = reader["mahalle"].ToString();

                                string ilanTuru = reader["ilan_turu"].ToString();
                                if (ilanTuru == "Satılık")
                                    radioButton1.Checked = true;
                                else if (ilanTuru == "Kiralık")
                                    radioButton2.Checked = true;

                                comboBox1.SelectedItem = reader["metrekare"].ToString();

                                comboBox2.SelectedItem = reader["oda_sayisi"].ToString();

                                comboBox3.SelectedItem = reader["kat_numarasi"].ToString();

                                comboBox4.SelectedItem = reader["ev_turu"].ToString();

                                textBox4.Text = reader["fiyat"].ToString();
                                textBox5.Text = reader["ilan_sahibi"].ToString();
                                textBox6.Text = reader["telefon"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İlan getirme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("UPDATE ev SET sehir = @sehir, ilce = @ilce, mahalle = @mahalle, ilan_turu = @ilan_turu, metrekare = @metrekare, oda_sayisi = @oda_sayisi, kat_numarasi = @kat_numarasi, ev_turu = @ev_turu, fiyat = @fiyat, ilan_sahibi = @ilan_sahibi, telefon = @telefon WHERE ev_id = @ev_id", connection))
                    {
                        command.Parameters.AddWithValue("@ev_id", evId);
                        command.Parameters.AddWithValue("@sehir", textBox1.Text);
                        command.Parameters.AddWithValue("@ilce", textBox2.Text);
                        command.Parameters.AddWithValue("@mahalle", textBox3.Text);

                        string ilanTuru = radioButton1.Checked ? "Satılık" : "Kiralık";
                        command.Parameters.AddWithValue("@ilan_turu", ilanTuru);

                        command.Parameters.AddWithValue("@metrekare", comboBox1.SelectedItem.ToString());

                        command.Parameters.AddWithValue("@oda_sayisi", comboBox2.SelectedItem.ToString());

                        command.Parameters.AddWithValue("@kat_numarasi", comboBox3.SelectedItem.ToString());

                        command.Parameters.AddWithValue("@ev_turu", comboBox4.SelectedItem.ToString());

                        command.Parameters.AddWithValue("@fiyat", textBox4.Text);
                        command.Parameters.AddWithValue("@ilan_sahibi", textBox5.Text);
                        command.Parameters.AddWithValue("@telefon", textBox6.Text);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("İlan başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}