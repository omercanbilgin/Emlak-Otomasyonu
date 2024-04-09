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
    public partial class Form3 : Form
    {
        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=EmlakOtomasyonu";
        public Form3()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                (!radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("Lütfen tüm filtreleri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string ilanTuru = radioButton1.Checked ? "Satılık" : "Kiralık";

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT ev.ev_id AS \"Ev İd\", ev.ev_turu AS \"Ev Türü\", mahalle.mahalle_adi AS \"Mahalle\", ilce.ilce_adi AS \"İlçe\", sehir.sehir_adi AS \"Şehir\", ev.ilan_turu AS \"İlan Türü\", ev.metrekare AS \"Metrekare\", ev.oda_sayisi AS \"Oda Sayısı\", ev.kat_numarasi AS \"Kat Numarası\", ev.fiyat AS \"Fiyat\" FROM ev INNER JOIN mahalle ON ev.mahalle_id = mahalle.mahalle_id INNER JOIN ilce ON mahalle.ilce_id = ilce.ilce_id INNER JOIN sehir ON ilce.sehir_id = sehir.sehir_id WHERE sehir.sehir_adi = @sehir_adi AND ilce.ilce_adi = @ilce_adi AND ev.ilan_turu = @ilan_turu", connection))
                    {
                        command.Parameters.AddWithValue("@sehir_adi", textBox1.Text);
                        command.Parameters.AddWithValue("@ilce_adi", textBox2.Text);
                        command.Parameters.AddWithValue("@ilan_turu", ilanTuru);

                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedEvId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Ev İd"].Value);

                Form5 formDuzenle = new Form5();
                formDuzenle.IlaniGetir(selectedEvId);
                formDuzenle.ShowDialog();

                button1_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz ilanı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedEvId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Ev İd"].Value);

                    try
                    {
                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();

                            using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM ev WHERE ev_id = @ev_id", connection))
                            {
                                command.Parameters.AddWithValue("@ev_id", selectedEvId);
                                command.ExecuteNonQuery();
                                button1_Click(sender, e);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Silme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silmek istediğiniz ilanı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
        }
    }
}