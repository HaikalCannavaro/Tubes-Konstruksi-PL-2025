using System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
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
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            // Set default selection untuk ComboBox
            cbJenisInput.SelectedIndex = 0;
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
                    lblHasil.Text = "Hasil Data: Belum ada data barang";
                    lblHasil.ForeColor = Color.Orange;
                    dgvTransaksiMasuk.Rows.Clear();
                    return;
                }

                PopulateDataGridView(daftarBarang);
                lblHasil.Text = $"Hasil Data: {daftarBarang.Count} barang ditemukan";
                lblHasil.ForeColor = Color.FromArgb(40, 167, 69);
            }
            catch (Exception ex)
            {
                lblHasil.Text = "Hasil Data: Error memuat data";
                lblHasil.ForeColor = Color.Red;
                MessageBox.Show($"Gagal memuat data barang:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                // Validasi input
                if (cbJenisInput.SelectedItem == null)
                {
                    MessageBox.Show("Silakan pilih jenis input (ID atau Nama).", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string input = txtInput.Text.Trim();
                int jumlahMasuk = (int)numJumlah.Value;
                string keterangan = txtKeterangan.Text.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    MessageBox.Show("Input ID atau Nama barang tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInput.Focus();
                    return;
                }

                if (jumlahMasuk <= 0)
                {
                    MessageBox.Show("Jumlah barang masuk harus lebih dari 0.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numJumlah.Focus();
                    return;
                }

                // Loading state
                btnTambahStok.Enabled = false;
                btnTambahStok.Text = "Memproses...";

                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (_, _, _, _) => true
                })
                {
                    BaseAddress = new Uri("https://localhost:7123")
                };

                // Ambil data barang terbaru
                var barangList = await client.GetFromJsonAsync<List<Barang>>("/api/Barang");
                if (barangList == null || barangList.Count == 0)
                {
                    MessageBox.Show("Tidak ada data barang yang tersedia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cari barang berdasarkan input
                Barang barangDitemukan = null;
                string jenisInput = cbJenisInput.SelectedItem.ToString();

                if (jenisInput == "ID")
                {
                    barangDitemukan = barangList.FirstOrDefault(b => b.Id.Equals(input, StringComparison.OrdinalIgnoreCase));
                }
                else if (jenisInput == "Nama")
                {
                    barangDitemukan = barangList.FirstOrDefault(b => b.Nama.Equals(input, StringComparison.OrdinalIgnoreCase));
                }

                if (barangDitemukan == null)
                {
                    MessageBox.Show($"Barang dengan {jenisInput} '{input}' tidak ditemukan.\nPastikan {jenisInput} yang dimasukkan sudah benar.",
                        "Barang Tidak Ditemukan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInput.Focus();
                    return;
                }

                // Buat objek transaksi
                var transaksiBaru = new Transaksi
                {
                    BarangId = barangDitemukan.Id,
                    Jenis = "Masuk",
                    Jumlah = jumlahMasuk,
                    Keterangan = string.IsNullOrEmpty(keterangan) ? $"Penambahan stok {barangDitemukan.Nama}" : keterangan,
                    Tanggal = DateTime.Now
                };

                // Kirim transaksi ke server - coba endpoint /masuk dulu, jika gagal coba endpoint biasa
                HttpResponseMessage response;
                try
                {
                    // Coba endpoint khusus masuk seperti di ModulTransaksi
                    response = await client.PostAsJsonAsync("/api/Transaksi/masuk", transaksiBaru);
                }
                catch
                {
                    // Jika gagal, coba endpoint biasa
                    response = await client.PostAsJsonAsync("/api/Transaksi", transaksiBaru);
                }

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Transaksi masuk berhasil!\n\nDetail:\n" +
                        $"- Barang: {barangDitemukan.Nama}\n" +
                        $"- Jumlah Masuk: {jumlahMasuk}\n" +
                        $"- Keterangan: {transaksiBaru.Keterangan}",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset form
                    txtInput.Clear();
                    numJumlah.Value = 1;
                    txtKeterangan.Clear();
                    cbJenisInput.SelectedIndex = 0;

                    // Refresh data
                    await LoadDataTransaksiMasuk();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Gagal melakukan transaksi masuk.\n\nError: {errorMessage}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Gagal terhubung ke server:\n{httpEx.Message}\n\nPastikan server API sudah berjalan.",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Kembalikan state button
                btnTambahStok.Enabled = true;
                btnTambahStok.Text = "Tambah Stok";
            }
        }

        private void FormTransaksiMasuk_Load(object sender, EventArgs e)
        {

        }
    }
}