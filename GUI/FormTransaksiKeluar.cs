using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private bool isShowingBarang = true; // Flag untuk menentukan mode tampilan

        public FormTransaksiKeluar()
        {
            InitializeComponent();
            SetupColumnsForBarang();
            SetupComboBox();
        }

        private async void FormTransaksiKeluar_Load(object sender, EventArgs e)
        {
            await LoadDataBarang();
        }

        private void SetupComboBox()
        {
            cbJenisInput.Items.Clear();
            cbJenisInput.Items.Add("ID");
            cbJenisInput.Items.Add("Nama");
            cbJenisInput.SelectedIndex = 0; // Default pilih ID
        }

        private void SetupColumnsForBarang()
        {
            dgvTransaksiKeluar.Columns.Clear();
            dgvTransaksiKeluar.AutoGenerateColumns = false;

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Id",
                HeaderText = "ID Barang",
                Width = 100
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Nama",
                HeaderText = "Nama Barang",
                Width = 200
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Kategori",
                HeaderText = "Kategori",
                Width = 120
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Stok",
                HeaderText = "Stok Tersedia",
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
                Name = "HargaJual",
                HeaderText = "Harga Jual",
                Width = 120,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Supplier",
                HeaderText = "Supplier",
                Width = 150
            });

            isShowingBarang = true;
        }

        private void SetupColumnsForTransaksi()
        {
            dgvTransaksiKeluar.Columns.Clear();
            dgvTransaksiKeluar.AutoGenerateColumns = false;

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TransaksiId",
                HeaderText = "ID Transaksi",
                Width = 100
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BarangId",
                HeaderText = "ID Barang",
                Width = 80
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "NamaBarang",
                HeaderText = "Nama Barang",
                Width = 180
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "JumlahKeluar",
                HeaderText = "Jumlah Keluar",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "StokSetelah",
                HeaderText = "Stok Setelah",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TanggalKeluar",
                HeaderText = "Tanggal Keluar",
                Width = 140,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvTransaksiKeluar.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Keterangan",
                HeaderText = "Keterangan",
                Width = 150
            });

            isShowingBarang = false;
        }

        private async Task LoadDataBarang()
        {
            try
            {
                lblHasil.Text = "Memuat data barang...";
                lblHasil.ForeColor = Color.Blue;

                using var client = CreateHttpClient();
                daftarBarang = await client.GetFromJsonAsync<List<Barang>>("/api/Barang");

                if (daftarBarang == null || daftarBarang.Count == 0)
                {
                    lblHasil.Text = "Hasil Data: Tidak ada barang tersedia";
                    lblHasil.ForeColor = Color.Orange;
                    dgvTransaksiKeluar.Rows.Clear();
                    return;
                }

                // Pastikan kita dalam mode tampilan barang
                if (!isShowingBarang)
                {
                    SetupColumnsForBarang();
                }

                // Tampilkan data barang
                dgvTransaksiKeluar.Rows.Clear();
                foreach (var barang in daftarBarang.OrderBy(b => b.Nama))
                {
                    dgvTransaksiKeluar.Rows.Add(
                        barang.Id,
                        barang.Nama,
                        barang.Kategori,
                        barang.Stok,
                        barang.HargaBeli.ToString("C"),
                        barang.HargaJual.ToString("C"),
                        barang.Supplier
                    );
                }

                lblHasil.Text = $"Hasil Data: {daftarBarang.Count} barang tersedia untuk transaksi keluar";
                lblHasil.ForeColor = Color.FromArgb(40, 167, 69);
            }
            catch (Exception ex)
            {
                lblHasil.Text = "Hasil Data: Error memuat data barang";
                lblHasil.ForeColor = Color.Red;
                MessageBox.Show($"Gagal memuat data barang:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                daftarBarang = new List<Barang>();
            }
        }

        private async Task LoadDataTransaksiKeluar()
        {
            try
            {
                lblHasil.Text = "Memuat data transaksi keluar...";
                lblHasil.ForeColor = Color.Blue;

                using var client = CreateHttpClient();

                // Ambil data transaksi
                var transaksiList = await client.GetFromJsonAsync<List<Transaksi>>("/api/Transaksi");

                // Ambil data barang terbaru
                await LoadDataBarangSilent();

                if (transaksiList == null)
                {
                    lblHasil.Text = "Hasil Data: Tidak ada data transaksi ditemukan";
                    lblHasil.ForeColor = Color.Orange;
                    dgvTransaksiKeluar.Rows.Clear();
                    return;
                }

                // Filter transaksi keluar saja
                var transaksiKeluar = transaksiList
                    .Where(t => t.Jenis != null && t.Jenis.Equals("Keluar", StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(t => t.Tanggal)
                    .Take(10) 
                    .ToList();

                if (transaksiKeluar.Count == 0)
                {
                    lblHasil.Text = "Hasil Data: Belum ada transaksi keluar";
                    lblHasil.ForeColor = Color.Orange;
                    dgvTransaksiKeluar.Rows.Clear();
                    return;
                }

                // Pastikan kita dalam mode tampilan transaksi
                if (isShowingBarang)
                {
                    SetupColumnsForTransaksi();
                }

                // Tampilkan data transaksi
                dgvTransaksiKeluar.Rows.Clear();
                foreach (var transaksi in transaksiKeluar)
                {
                    var barang = daftarBarang?.FirstOrDefault(b => b.Id == transaksi.BarangId);

                    if (barang != null)
                    {
                        dgvTransaksiKeluar.Rows.Add(
                            transaksi.Id,
                            transaksi.BarangId,
                            barang.Nama,
                            transaksi.Jumlah,
                            barang.Stok, // Stok setelah transaksi
                            transaksi.Tanggal.ToString("dd/MM/yyyy HH:mm"),
                            transaksi.Keterangan ?? ""
                        );
                    }
                    else
                    {
                        dgvTransaksiKeluar.Rows.Add(
                            transaksi.Id,
                            transaksi.BarangId,
                            "Barang tidak ditemukan",
                            transaksi.Jumlah,
                            "-",
                            transaksi.Tanggal.ToString("dd/MM/yyyy HH:mm"),
                            transaksi.Keterangan ?? ""
                        );
                    }
                }

                lblHasil.Text = $"Hasil Data: {transaksiKeluar.Count} transaksi keluar terbaru";
                lblHasil.ForeColor = Color.FromArgb(40, 167, 69);
            }
            catch (Exception ex)
            {
                lblHasil.Text = "Hasil Data: Error memuat data transaksi";
                lblHasil.ForeColor = Color.Red;
                MessageBox.Show($"Gagal memuat data transaksi keluar:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadDataBarangSilent()
        {
            try
            {
                using var client = CreateHttpClient();
                daftarBarang = await client.GetFromJsonAsync<List<Barang>>("/api/Barang");

                if (daftarBarang == null)
                {
                    daftarBarang = new List<Barang>();
                }
            }
            catch (Exception)
            {
                if (daftarBarang == null)
                {
                    daftarBarang = new List<Barang>();
                }
            }
        }

        private HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7123")
            };

            return client;
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (isShowingBarang)
            {
                await LoadDataBarang();
            }
            else
            {
                await LoadDataTransaksiKeluar();
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnKurangiStok_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasi input
                if (cbJenisInput.SelectedItem == null)
                {
                    MessageBox.Show("Pilih jenis input terlebih dahulu.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbJenisInput.Focus();
                    return;
                }

                string input = txtInput.Text.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    MessageBox.Show("Input ID atau Nama barang tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInput.Focus();
                    return;
                }

                int jumlahKeluar = (int)numJumlah.Value;
                if (jumlahKeluar <= 0)
                {
                    MessageBox.Show("Jumlah keluar harus lebih dari 0.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numJumlah.Focus();
                    return;
                }

                // Pastikan data barang sudah dimuat
                if (daftarBarang == null || daftarBarang.Count == 0)
                {
                    await LoadDataBarangSilent();
                }

                // Cari barang berdasarkan input
                Barang barang = null;
                string jenisInput = cbJenisInput.SelectedItem.ToString();

                if (jenisInput == "ID")
                {
                    barang = daftarBarang.FirstOrDefault(b => b.Id == input);
                }
                else if (jenisInput == "Nama")
                {
                    barang = daftarBarang.FirstOrDefault(b => b.Nama.Equals(input, StringComparison.OrdinalIgnoreCase));
                }

                if (barang == null)
                {
                    MessageBox.Show($"Barang dengan {jenisInput} '{input}' tidak ditemukan.", "Barang Tidak Ditemukan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInput.Focus();
                    return;
                }

                // Cek stok
                if (barang.Stok < jumlahKeluar)
                {
                    MessageBox.Show($"Stok tidak mencukupi.\nStok tersedia: {barang.Stok}\nJumlah yang diminta: {jumlahKeluar}", "Stok Tidak Mencukupi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numJumlah.Focus();
                    return;
                }

                // Konfirmasi transaksi
                var confirmResult = MessageBox.Show(
                    $"Konfirmasi Transaksi Keluar:\n\n" +
                    $"Barang: {barang.Nama}\n" +
                    $"Stok Saat Ini: {barang.Stok}\n" +
                    $"Jumlah Keluar: {jumlahKeluar}\n" +
                    $"Stok Setelah Keluar: {barang.Stok - jumlahKeluar}\n" +
                    $"Keterangan: {txtKeterangan.Text.Trim()}\n\n" +
                    $"Apakah Anda yakin ingin melanjutkan?",
                    "Konfirmasi Transaksi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }

                // Nonaktifkan tombol sementara
                btnKurangiStok.Enabled = false;
                btnKurangiStok.Text = "Memproses...";

                // Buat transaksi baru
                var transaksiBaru = new Transaksi
                {
                    BarangId = barang.Id,
                    Jenis = "Keluar",
                    Jumlah = jumlahKeluar,
                    Keterangan = txtKeterangan.Text.Trim(),
                    Tanggal = DateTime.Now
                };

                // Kirim ke API
                using var client = CreateHttpClient();
                var response = await client.PostAsJsonAsync("/api/Transaksi/keluar", transaksiBaru);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        $"Transaksi keluar berhasil!\n\n" +
                        $"Barang: {barang.Nama}\n" +
                        $"Jumlah Keluar: {jumlahKeluar}\n" +
                        $"Stok Sebelum: {barang.Stok}\n" +
                        $"Stok Setelah: {barang.Stok - jumlahKeluar}",
                        "Transaksi Berhasil",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reset form
                    ResetForm();

                    // Tampilkan transaksi terbaru
                    await LoadDataTransaksiKeluar();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Gagal melakukan transaksi keluar:\n{errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Aktifkan kembali tombol
                btnKurangiStok.Enabled = true;
                btnKurangiStok.Text = "Kurangi Stok";
            }
        }

        private void ResetForm()
        {
            txtInput.Clear();
            numJumlah.Value = 1;
            txtKeterangan.Clear();
            cbJenisInput.SelectedIndex = 0;
            txtInput.Focus();
        }

        // Method tambahan untuk toggle antara tampilan barang dan transaksi
        public async void ShowDataBarang()
        {
            await LoadDataBarang();
        }

        public async void ShowDataTransaksi()
        {
            await LoadDataTransaksiKeluar();
        }
    }
}