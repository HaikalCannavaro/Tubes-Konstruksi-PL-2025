using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using AplikasiInventarisToko.Models;

namespace GUI
{
    public partial class FormTransaksiKeluar : Form
    {
        private List<Barang> daftarBarang;

        public FormTransaksiKeluar()
        {
            InitializeComponent();
            SetupColumns();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadDataTransaksiKeluar();
        }

        private void SetupColumns()
        {
            dgvTransaksiKeluar.Columns.Clear();

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ID",
                HeaderText = "ID",
                Width = 60
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "NamaBarang",
                HeaderText = "Nama Barang",
                Width = 180
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Kategori",
                HeaderText = "Kategori",
                Width = 120
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "StokMasuk",
                HeaderText = "Stok Masuk",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "HargaBeli",
                HeaderText = "Harga Beli",
                Width = 120,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Supplier",
                HeaderText = "Supplier",
                Width = 130
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TanggalMasuk",
                HeaderText = "Tanggal Masuk",
                Width = 120,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
        }

        private async Task LoadDataTransaksiKeluar()
        {
            try
            {
                lblHasil.Text = "Memuat data transaksi keluar...";
                lblHasil.ForeColor = Color.Blue;

                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (_, _, _, _) => true
                })
                { BaseAddress = new Uri("https://localhost:7123") };

                var transaksiList = await client.GetFromJsonAsync<List<Transaksi>>("/api/Transaksi");
                var barangList = await client.GetFromJsonAsync<List<Barang>>("/api/Barang");

                if (transaksiList == null || barangList == null)
                {
                    lblHasil.Text = "Hasil Data: Tidak ada data ditemukan";
                    lblHasil.ForeColor = Color.Orange;
                    dgvTransaksiKeluar.Rows.Clear();
                    return;
                }

                var transaksiKeluar = transaksiList
                    .Where(t => t.Jenis.Equals("Keluar", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (transaksiKeluar.Count == 0)
                {
                    lblHasil.Text = "Hasil Data: Belum ada transaksi keluar";
                    lblHasil.ForeColor = Color.Orange;
                    dgvTransaksiKeluar.Rows.Clear();
                    return;
                }

                dgvTransaksiKeluar.Rows.Clear();
                foreach (var t in transaksiKeluar)
                {
                    var barang = barangList.FirstOrDefault(b => b.Id == t.BarangId);
                    if (barang == null) continue;

                    dgvTransaksiKeluar.Rows.Add(
                        t.Id,
                        barang.Nama,
                        barang.Kategori,
                        t.Jumlah,
                        barang.HargaJual.ToString("C"),
                        barang.Supplier,
                        t.Tanggal.ToString("dd/MM/yyyy HH:mm")
                    );
                }

                lblHasil.Text = $"Hasil Data: {transaksiKeluar.Count} transaksi keluar ditemukan";
                lblHasil.ForeColor = Color.FromArgb(40, 167, 69);
            }
            catch (Exception ex)
            {
                lblHasil.Text = "Hasil Data: Error memuat data";
                lblHasil.ForeColor = Color.Red;
                MessageBox.Show($"Gagal memuat data transaksi keluar:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadDataTransaksiKeluar();
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnKurangiStok_Click(object sender, EventArgs e)
        {
            var cbJenisInput = Controls.Find("cbJenisInput", true).FirstOrDefault() as ComboBox;
            var txtInput = Controls.Find("txtInput", true).FirstOrDefault() as TextBox;
            var numJumlah = Controls.Find("numJumlah", true).FirstOrDefault() as NumericUpDown;
            var txtKeterangan = Controls.Find("txtKeterangan", true).FirstOrDefault() as TextBox;

            if (cbJenisInput == null || txtInput == null || numJumlah == null || txtKeterangan == null)
                return;

            string input = txtInput.Text.Trim();
            int jumlahKeluar = (int)numJumlah.Value;

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Input tidak boleh kosong.");
                return;
            }

            using var client = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true })
            {
                BaseAddress = new Uri("https://localhost:7123")
            };

            var barangList = await client.GetFromJsonAsync<List<Barang>>("/api/Barang");
            if (barangList == null) return;

            Barang barang = null;
            if (cbJenisInput.SelectedItem?.ToString() == "ID")
                barang = barangList.FirstOrDefault(b => b.Id == input);
            else
                barang = barangList.FirstOrDefault(b => b.Nama.Equals(input, StringComparison.OrdinalIgnoreCase));

            if (barang == null)
            {
                MessageBox.Show("Barang tidak ditemukan.");
                return;
            }

            if (barang.Stok < jumlahKeluar)
            {
                MessageBox.Show("Stok tidak mencukupi.");
                return;
            }

            var transaksiBaru = new Transaksi
            {
                BarangId = barang.Id,
                Jenis = "Keluar",
                Jumlah = jumlahKeluar,
                Keterangan = txtKeterangan.Text,
                Tanggal = DateTime.Now
            };

            var response = await client.PostAsJsonAsync("/api/Transaksi", transaksiBaru);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Transaksi keluar berhasil.");
                await LoadDataTransaksiKeluar();
            }
            else
            {
                MessageBox.Show("Gagal melakukan transaksi.");
            }
        }

        private void FormTransaksiKeluar_Load(object sender, EventArgs e)
        {

        }
    }
}
