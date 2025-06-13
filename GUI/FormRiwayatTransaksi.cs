using AplikasiInventarisToko.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using System.Linq;

namespace GUI
{
    public partial class FormRiwayatTransaksi : Form
    {
        public FormRiwayatTransaksi()
        {
            InitializeComponent();
        }

        /// Memuat dan menampilkan riwayat transaksi dari API ke DataGridView
        private async void buttonMuatRiwayat_Click(object sender, EventArgs e)
        {
            // Konfigurasi HTTP client handler untuk bypass SSL certificate validation
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7123");

            try
            {
                // Mengambil data transaksi dari API
                var response = await client.GetAsync("/api/Transaksi");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal mengambil data transaksi.");
                    return;
                }

                var transaksiList = await response.Content.ReadFromJsonAsync<List<Transaksi>>();
                if (transaksiList == null || transaksiList.Count == 0)
                {
                    MessageBox.Show("Belum ada transaksi.");
                    return;
                }

                // Mengambil data barang untuk mendapatkan nama barang berdasarkan ID
                var barangResponse = await client.GetAsync("/api/Barang");
                var daftarBarang = await barangResponse.Content.ReadFromJsonAsync<List<Barang>>() ?? new();

                // Membuat dictionary untuk mapping ID barang ke nama barang (untuk performa lookup yang lebih cepat)
                var barangDict = daftarBarang.ToDictionary(b => b.Id, b => b.Nama);

                // Membuat binding list untuk data yang akan ditampilkan di DataGridView
                var bindingList = new BindingList<dynamic>();

                // Memproses setiap transaksi untuk ditampilkan dengan format yang user-friendly
                foreach (var t in transaksiList)
                {
                    // Mendapatkan nama barang berdasarkan ID, dengan fallback jika tidak ditemukan
                    string namaBarang = barangDict.ContainsKey(t.BarangId) ? barangDict[t.BarangId] : "Tidak diketahui";

                    // Membuat objek anonymous untuk ditampilkan di grid
                    bindingList.Add(new
                    {
                        ID = t.Id,
                        Jenis = t.Jenis,
                        Barang = namaBarang,
                        Jumlah = t.Jumlah,
                        Tanggal = t.Tanggal.ToString("dd-MM-yyyy HH:mm"),
                        Keterangan = t.Keterangan
                    });
                }

                // Konfigurasi DataGridView untuk menampilkan data
                dataGridView1.DataSource = bindingList;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Kolom menyesuaikan lebar grid
                dataGridView1.ReadOnly = true; // Data hanya bisa dibaca, tidak bisa diedit
                dataGridView1.AllowUserToAddRows = false; // Mencegah user menambah baris baru
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }
    }
}