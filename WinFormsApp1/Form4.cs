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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form4 : Form
    {
        private const string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=EmlakOtomasyonu";
        public Form4()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox6.Text) ||
                comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null ||
                comboBox4.SelectedItem == null || (!radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    int sehirId;
                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO sehir(sehir_adi) VALUES (@sehir_adi) RETURNING sehir_id", connection))
                    {
                        command.Parameters.AddWithValue("@sehir_adi", textBox1.Text);
                        sehirId = (int)command.ExecuteScalar();
                    }

                    int ilceId;
                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO ilce(ilce_adi, sehir_id) VALUES (@ilce_adi, @sehir_id) RETURNING ilce_id", connection))
                    {
                        command.Parameters.AddWithValue("@ilce_adi", textBox2.Text);
                        command.Parameters.AddWithValue("@sehir_id", sehirId);
                        ilceId = (int)command.ExecuteScalar();
                    }

                    int mahalleId;
                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO mahalle(mahalle_adi, ilce_id) VALUES (@mahalle_adi, @ilce_id) RETURNING mahalle_id", connection))
                    {
                        command.Parameters.AddWithValue("@mahalle_adi", textBox3.Text);
                        command.Parameters.AddWithValue("@ilce_id", ilceId);
                        mahalleId = (int)command.ExecuteScalar();
                    }

                    int ilanSahibiId;
                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO ilan_sahibi(ad_soyad, telefon_numarasi) VALUES (@ad_soyad, @telefon_numarasi) RETURNING ilan_sahibi_id", connection))
                    {
                        command.Parameters.AddWithValue("@ad_soyad", textBox5.Text);
                        command.Parameters.AddWithValue("@telefon_numarasi", textBox6.Text);
                        ilanSahibiId = (int)command.ExecuteScalar();
                    }

                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO ev(ev_turu, metrekare, oda_sayisi, kat_numarasi, fiyat, ilan_sahibi_id, mahalle_id, ilan_turu) VALUES (@ev_turu, @metrekare, @oda_sayisi, @kat_numarasi, @fiyat, @ilan_sahibi_id, @mahalle_id, @ilan_turu)", connection))
                    {
                        command.Parameters.AddWithValue("@ev_turu", comboBox4.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@metrekare", int.Parse(comboBox1.SelectedItem.ToString()));
                        command.Parameters.AddWithValue("@oda_sayisi", comboBox2.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@kat_numarasi", int.Parse(comboBox3.SelectedItem.ToString()));
                        command.Parameters.AddWithValue("@fiyat", int.Parse(textBox4.Text));
                        command.Parameters.AddWithValue("@ilan_sahibi_id", ilanSahibiId);
                        command.Parameters.AddWithValue("@mahalle_id", mahalleId);
                        command.Parameters.AddWithValue("@ilan_turu", radioButton1.Checked ? "Satılık" : "Kiralık");

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    Form2 form2 = new Form2();
                    form2.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}