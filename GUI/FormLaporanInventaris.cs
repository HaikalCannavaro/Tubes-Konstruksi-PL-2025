using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Models;
using AplikasiInventarisToko.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormLaporanInventaris : Form
    {
        private DataTable _currentDataTable;

        public FormLaporanInventaris()
        {
            InitializeComponent();
        }

        private async void btnMuatData_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7123")
                };

                var daftarBarang = await client.GetFromJsonAsync<List<Barang>>("/api/Barang");
                var daftarTransaksi = await client.GetFromJsonAsync<List<Transaksi>>("/api/Transaksi");

                if (daftarBarang == null || daftarBarang.Count == 0)
                {
                    MessageBox.Show("Tidak ada data barang.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var dataTable = new DataTable();
                dataTable.Columns.Add("ID");
                dataTable.Columns.Add("Nama");
                dataTable.Columns.Add("Tanggal Masuk");
                dataTable.Columns.Add("Lama di Gudang");
                dataTable.Columns.Add("Stok");
                dataTable.Columns.Add("Status");

                foreach (var barang in daftarBarang)
                {
                    int stokAwal = StokHelper.HitungStokAwal(barang, daftarTransaksi);
                    double persentaseStok = StokHelper.HitungPersentaseStok(barang, stokAwal);
                    string status = StokHelper.TentukanStatus(barang, persentaseStok, KonfigurasiAplikasi.Load());
                    int lama = (int)(DateTime.Now - barang.TanggalMasuk).TotalDays;

                    dataTable.Rows.Add(barang.Id, barang.Nama, barang.TanggalMasuk.ToShortDateString(),
                                       $"{lama} hari", $"{barang.Stok}/{stokAwal}", status);
                }

                _currentDataTable = dataTable;
                dgvInventaris.DataSource = _currentDataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat memuat data:\n" + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            if (_currentDataTable == null || _currentDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk diekspor.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Simpan Laporan Inventaris";
                saveFileDialog.FileName = $"laporan_inventaris_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                        {
                            // Header
                            var columnNames = _currentDataTable.Columns.Cast<DataColumn>()
                                                   .Select(col => col.ColumnName);
                            sw.WriteLine(string.Join(",", columnNames));

                            // Data
                            foreach (DataRow row in _currentDataTable.Rows)
                            {
                                var fields = row.ItemArray.Select(field => $"\"{field}\"");
                                sw.WriteLine(string.Join(",", fields));
                            }
                        }

                        MessageBox.Show("Data berhasil diekspor ke file CSV.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengekspor data:\n" + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
