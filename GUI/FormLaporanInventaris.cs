using API.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Json;

namespace GUI
{
    public partial class FormLaporanInventaris : Form
    {
        private HttpClient httpClient;
        public FormLaporanInventaris()
        {
            InitializeComponent();

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            httpClient = new HttpClient(handler);
            httpClient.BaseAddress = new Uri("https://localhost:7123");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<LaporanInventaris> data = await AmbilLaporanInventarisAsync();
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengambil data dari server:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<List<LaporanInventaris>> AmbilLaporanInventarisAsync()
        {
            var result = await httpClient.GetFromJsonAsync<List<LaporanInventaris>>("/api/laporan");
            return result ?? new List<LaporanInventaris>();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    public class LaporanInventaris
    {
        public string NamaBarang { get; set; }
        public string KodeBarang { get; set; }
        public int Stok { get; set; }
        public decimal Harga { get; set; }
        public DateTime TanggalMasuk { get; set; }
    }
}
