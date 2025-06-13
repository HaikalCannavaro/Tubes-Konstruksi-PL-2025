using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using AplikasiInventarisToko.Helpers; 
using AplikasiInventarisToko.Models; 

namespace AplikasiInventarisToko.GUI
{
    public partial class FormTambahBarang : Form
    {
        public FormTambahBarang()
        {
            InitializeComponent();
        }

        private async void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(textBoxStokAwal.Text, out int stok))
                {
                    MessageBox.Show("Stok harus berupa angka bulat.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBoxHargaBeli.Text, out decimal hargaBeli))
                {
                    MessageBox.Show("Harga beli harus berupa angka desimal.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBoxHargaJual.Text, out decimal hargaJual))
                {
                    MessageBox.Show("Harga jual harus berupa angka desimal.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gunakan Design Pattern Factory Method untuk membuat objek Barang
                Barang barang = BarangFactory.Create(
                    textBoxNamaBarang.Text,
                    textBoxKategori.Text,
                    stok,
                    hargaBeli,
                    hargaJual,
                    textBoxSupplier.Text
                );

                using var client = new HttpClient();
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
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
