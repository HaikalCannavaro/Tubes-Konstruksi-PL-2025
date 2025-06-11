using AplikasiInventarisToko.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormTampilkanBarang : Form
    {

        private void AturUkuranDataGridView()
        {
            int lebarForm = this.ClientSize.Width;

            // Lebar 90% dari form
            dataGridViewBarang.Width = (int)(lebarForm * 0.90);

            // Jarak kiri 5% dari form
            dataGridViewBarang.Left = (int)(lebarForm * 0.05);

            // Posisikan tombol Kembali di tengah
            buttonKembali.Left = (lebarForm - buttonKembali.Width) / 2;
        }

        public FormTampilkanBarang()
        {
            InitializeComponent();
            this.Load += FormTampilkanBarang_Load;
            this.Resize += (s, e) => AturUkuranDataGridView();
        }


        private async void FormTampilkanBarang_Load(object sender, EventArgs e)
        {
            AturUkuranDataGridView(); 

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender2, cert, chain, sslPolicyErrors) => true
                };

                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:7123");

                var response = await client.GetAsync("/api/Barang");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<Barang>>();
                    dataGridViewBarang.DataSource = data;
                }
                else
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Gagal mengambil data: " + msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }


        private void buttonKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
