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
                // Validasi input sebelum parsing
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

                var barang = new
                {
                    Nama = textBoxNamaBarang.Text,
                    Kategori = textBoxKategori.Text,
                    Stok = stok,
                    HargaBeli = hargaBeli,
                    HargaJual = hargaJual,
                    Supplier = textBoxSupplier.Text,
                    StokAwal = stok
                };

                // Menggunakan HttpClient tanpa menonaktifkan validasi SSL
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

