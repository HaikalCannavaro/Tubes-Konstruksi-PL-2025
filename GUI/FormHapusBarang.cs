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
    public partial class FormHapusBarang : Form
    {
        private Barang _barangTerpilih;

        public FormHapusBarang()
        {
            InitializeComponent();
            _barangTerpilih = null;
        }

        private async void FormHapusBarang_Load(object sender, EventArgs e)
        {
            await MuatDaftarBarang();
        }

        private async Task MuatDaftarBarang()
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:7123");

                var response = await client.GetAsync("/api/Barang");
                if (response.IsSuccessStatusCode)
                {
                    var daftarBarang = await response.Content.ReadFromJsonAsync<List<Barang>>();
                    dataGridViewDaftarBarang.DataSource = daftarBarang;
                }
                else
                {
                    MessageBox.Show("Gagal memuat daftar barang.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewDaftarBarang_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewDaftarBarang.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewDaftarBarang.SelectedRows[0];
                _barangTerpilih = selectedRow.DataBoundItem as Barang;
                textBoxIdBarang.Text = _barangTerpilih?.Id;
                TampilkanDetailBarang(_barangTerpilih);
            }
        }

        private void TampilkanDetailBarang(Barang barang)
        {
            if (barang == null)
            {
                textBoxId.Text = string.Empty;
                textBoxNama.Text = string.Empty;
                textBoxKategori.Text = string.Empty;
                textBoxStok.Text = string.Empty;
                textBoxHargaBeli.Text = string.Empty;
                textBoxHargaJual.Text = string.Empty;
                textBoxSupplier.Text = string.Empty;
                return;
            }

            textBoxId.Text = barang.Id;
            textBoxNama.Text = barang.Nama;
            textBoxKategori.Text = barang.Kategori;
            textBoxStok.Text = barang.Stok.ToString();
            textBoxHargaBeli.Text = barang.HargaBeli.ToString("C");
            textBoxHargaJual.Text = barang.HargaJual.ToString("C");
            textBoxSupplier.Text = barang.Supplier;
        }

        private async void btnCari_Click(object sender, EventArgs e)
        {
            string idBarang = textBoxIdBarang.Text.Trim();
            if (string.IsNullOrEmpty(idBarang))
            {
                MessageBox.Show("Masukkan ID barang yang ingin dicari.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri("https://localhost:7123");

                var response = await client.GetAsync($"/api/Barang/{idBarang}");
                if (response.IsSuccessStatusCode)
                {
                    _barangTerpilih = await response.Content.ReadFromJsonAsync<Barang>();
                    TampilkanDetailBarang(_barangTerpilih);
                }
                else
                {
                    MessageBox.Show("Barang tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnHapus_Click(object sender, EventArgs e)
        {
            if (_barangTerpilih == null)
            {
                MessageBox.Show("Pilih barang yang ingin dihapus terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var konfirmasi = MessageBox.Show($"Apakah Anda yakin ingin menghapus barang:\n\n" +
                $"ID: {_barangTerpilih.Id}\n" +
                $"Nama: {_barangTerpilih.Nama}\n\n" +
                $"Tindakan ini tidak dapat dibatalkan!", "Konfirmasi Hapus",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (konfirmasi == DialogResult.Yes)
            {
                try
                {
                    var handler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                    };

                    using var client = new HttpClient(handler);
                    client.BaseAddress = new Uri("https://localhost:7123");

                    var response = await client.DeleteAsync($"/api/Barang/{_barangTerpilih.Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Barang berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _barangTerpilih = null;
                        TampilkanDetailBarang(null);
                        textBoxIdBarang.Text = string.Empty;
                        await MuatDaftarBarang();
                    }
                    else
                    {
                        var msg = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Gagal menghapus barang: {msg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}