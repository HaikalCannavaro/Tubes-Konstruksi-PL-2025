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

namespace AplikasiInventarisToko.GUI
{
    partial class FormTambahBarang : Form
    {
        public FormTambahBarang()
        {
            InitializeComponent();
        }

        private async void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                var barang = new
                {
                    Nama = textBoxNamaBarang.Text,
                    Kategori = textBoxKategori.Text,
                    Stok = int.Parse(textBoxStokAwal.Text),
                    HargaBeli = decimal.Parse(textBoxHargaBeli.Text),
                    HargaJual = decimal.Parse(textBoxHargaJual.Text),
                    Supplier = textBoxSupplier.Text,
                    StokAwal = int.Parse(textBoxStokAwal.Text)
                };

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender2, cert, chain, sslPolicyErrors) => true
                };

                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:7123");

                var response = await client.PostAsJsonAsync("/api/Barang", barang);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Barang berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Gagal menambahkan barang: " + errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTambahBarang_Load(object sender, EventArgs e)
        {

        }
    }
}

