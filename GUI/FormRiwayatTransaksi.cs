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

        private async void buttonMuatRiwayat_Click(object sender, EventArgs e)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7123");

            try
            {
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

                var barangResponse = await client.GetAsync("/api/Barang");
                var daftarBarang = await barangResponse.Content.ReadFromJsonAsync<List<Barang>>() ?? new();
                var barangDict = daftarBarang.ToDictionary(b => b.Id, b => b.Nama);

                var bindingList = new BindingList<dynamic>();

                foreach (var t in transaksiList)
                {
                    string namaBarang = barangDict.ContainsKey(t.BarangId) ? barangDict[t.BarangId] : "Tidak diketahui";
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

                dataGridView1.DataSource = bindingList;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }
    }
}