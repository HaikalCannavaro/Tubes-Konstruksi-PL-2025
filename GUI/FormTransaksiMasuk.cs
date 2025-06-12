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
    public partial class FormTransaksiMasuk : Form
    {
        private List<Barang> daftarBarang;

        public FormTransaksiMasuk()
        {
            InitializeComponent();
            SetupColumns();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadDataTransaksiMasuk();
        }

        private void SetupColumns()
        {
            dgvTransaksiMasuk.Columns.Clear();

            dgvTransaksiMasuk.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ID",
                HeaderText = "ID",
                Width = 60
            });

            dgvTransaksiMasuk.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "NamaBarang",
                HeaderText = "Nama Barang",
                Width = 180
            });

            dgvTransaksiMasuk.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Kategori",
                HeaderText = "Kategori",
                Width = 120
            });

            dgvTransaksiMasuk.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "StokMasuk",
                HeaderText = "Stok Masuk",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvTransaksiMasuk.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "HargaBeli",
                HeaderText = "Harga Beli",
                Width = 120,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvTransaksiMasuk.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Supplier",
                HeaderText = "Supplier",
                Width = 130
            });

            dgvTransaksiMasuk.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TanggalMasuk",
                HeaderText = "Tanggal Masuk",
                Width = 120,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
        }

        private async Task LoadDataTransaksiMasuk()
        {
            try
            {
                lblHasil.Text = "Memuat data transaksi masuk...";
                lblHasil.ForeColor = Color.Blue;

                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (_, _, _, _) => true
                })
                { BaseAddress = new Uri("https://localhost:7123") };

                daftarBarang = await client.GetFromJsonAsync<List<Barang>>("/api/Barang");

                if (daftarBarang == null || daftarBarang.Count == 0)
                {
                    lblHasil.Text = "Hasil Data: Belum ada data transaksi masuk";
                    lblHasil.ForeColor = Color.Orange;
                    dgvTransaksiMasuk.Rows.Clear();
                    return;
                }

                PopulateDataGridView(daftarBarang);
                lblHasil.Text = $"Hasil Data: {daftarBarang.Count} transaksi masuk ditemukan";
                lblHasil.ForeColor = Color.FromArgb(40, 167, 69);
            }
            catch (Exception ex)
            {
                lblHasil.Text = "Hasil Data: Error memuat data";
                lblHasil.ForeColor = Color.Red;
                MessageBox.Show($"Gagal memuat data transaksi masuk:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateDataGridView(List<Barang> barangList)
        {
            dgvTransaksiMasuk.Rows.Clear();

            foreach (var barang in barangList)
            {
                dgvTransaksiMasuk.Rows.Add(
                    barang.Id,
                    barang.Nama,
                    barang.Kategori,
                    barang.Stok,
                    barang.HargaBeli.ToString("C"),
                    barang.Supplier,
                    DateTime.Now.ToString("dd/MM/yyyy")
                );
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadDataTransaksiMasuk();
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnTambahStok_Click(object sender, EventArgs e)
        {
            var cbJenisInput = Controls.Find("cbJenisInput", true).FirstOrDefault() as ComboBox;
            var txtInput = Controls.Find("txtInput", true).FirstOrDefault() as TextBox;
            var numJumlah = Controls.Find("numJumlah", true).FirstOrDefault() as NumericUpDown;
            var txtKeterangan = Controls.Find("txtKeterangan", true).FirstOrDefault() as TextBox;

            if (cbJenisInput == null || txtInput == null || numJumlah == null || txtKeterangan == null)
                return;

            string input = txtInput.Text.Trim();
            int jumlahMasuk = (int)numJumlah.Value;

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

            var transaksiBaru = new Transaksi
            {
                BarangId = barang.Id,
                Jenis = "Masuk",
                Jumlah = jumlahMasuk,
                Keterangan = txtKeterangan.Text,
                Tanggal = DateTime.Now
            };

            var response = await client.PostAsJsonAsync("/api/Transaksi", transaksiBaru);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Transaksi masuk berhasil.");
                await LoadDataTransaksiMasuk();
            }
            else
            {
                MessageBox.Show("Gagal melakukan transaksi masuk.");
            }
        }

        private void FormTransaksiMasuk_Load(object sender, EventArgs e)
        {

        }
    }
}
