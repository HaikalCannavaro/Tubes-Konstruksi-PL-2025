using AplikasiInventarisToko.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace AplikasiInventarisToko.GUI
{
    public partial class FormEditBarang : Form
    {
        private readonly HttpClient httpClient;
        private Barang barangTerpilih;

        public FormEditBarang()
        {
            InitializeComponent();
            this.Resize += (s, e) => AturUkuranDataGridView();
            httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true
            })
            {
                BaseAddress = new Uri("https://localhost:7123")
            };
        }

        private void AturUkuranDataGridView()
        {
            // Optional jika ingin mengatur ukuran secara manual, tapi bisa dilewati kalau pakai Anchor
            dataGridViewBarang.Width = (int)(this.ClientSize.Width * 0.55);
            dataGridViewBarang.Height = (int)(this.ClientSize.Height * 0.65);
        }

        private async void FormEditBarang_Load(object sender, EventArgs e)
        {
            comboBoxKriteria.SelectedIndex = 0;
            TambahEventHandler();
            dataGridViewBarang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBarang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBarang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            await TampilkanSemuaBarangAsync();
        }

        #region Event Handler Tombol
        private void TambahEventHandler()
        {
            btnCari.Click += btnCari_Click;
            btnReset.Click += btnReset_Click;
            btnSimpan.Click += btnSimpan_Click;
            btnBatal.Click += (_, __) => Close();
            dataGridViewBarang.CellClick += DataGridViewBarang_CellClick;
        }

        private async void btnCari_Click(object sender, EventArgs e)
        {
            string kriteria = comboBoxKriteria.SelectedItem?.ToString();
            string nilai = textBoxNilaiPencarian.Text.Trim();

            if (string.IsNullOrWhiteSpace(kriteria) || string.IsNullOrWhiteSpace(nilai))
            {
                MessageBox.Show("Isi kriteria dan nilai pencarian.");
                return;
            }

            try
            {
                var response = await httpClient.GetAsync($"/api/Barang/search?kriteria={kriteria}&nilai={nilai}");
                if (response.IsSuccessStatusCode)
                {
                    var hasil = await response.Content.ReadFromJsonAsync<List<Barang>>();
                    dataGridViewBarang.DataSource = hasil;
                }
                else
                {
                    MessageBox.Show("Gagal mengambil data.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan saat pencarian: " + ex.Message);
            }
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            comboBoxKriteria.SelectedIndex = 0;
            textBoxNilaiPencarian.Clear();
            barangTerpilih = null;
            ClearFormInput();
            await TampilkanSemuaBarangAsync();
        }

        private async void btnSimpan_Click(object sender, EventArgs e)
        {
            if (barangTerpilih == null)
            {
                MessageBox.Show("Pilih barang dari tabel terlebih dahulu.");
                return;
            }

            if (!int.TryParse(textBoxStokBaru.Text, out int stok) ||
                !decimal.TryParse(textBoxHargaBeliBaru.Text, out decimal hargaBeli) ||
                !decimal.TryParse(textBoxHargaJualBaru.Text, out decimal hargaJual))
            {
                MessageBox.Show("Pastikan semua input angka valid.");
                return;
            }

            var updatedBarang = new Barang
            {
                Id = barangTerpilih.Id,
                Nama = textBoxNamaBaru.Text.Trim(),
                Kategori = textBoxKategoriBaru.Text.Trim(),
                Stok = stok,
                HargaBeli = hargaBeli,
                HargaJual = hargaJual,
                Supplier = textBoxSupplierBaru.Text.Trim(),
                TanggalMasuk = barangTerpilih.TanggalMasuk
            };

            try
            {
                var response = await httpClient.PutAsJsonAsync($"api/Barang/{updatedBarang.Id}", updatedBarang);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Barang berhasil diperbarui.");
                    barangTerpilih = null;
                    ClearFormInput();
                    await TampilkanSemuaBarangAsync();
                }
                else
                {
                    MessageBox.Show("Gagal memperbarui barang.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }
        #endregion

        #region DataGridView & Form Helper
        private async Task TampilkanSemuaBarangAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("/api/Barang");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<Barang>>(json);
                    dataGridViewBarang.DataSource = data;
                }
                else
                {
                    MessageBox.Show("Gagal mengambil data dari API.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan saat mengambil data: " + ex.Message);
            }
        }

        private void DataGridViewBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewBarang.Rows[e.RowIndex].DataBoundItem is Barang b)
            {
                barangTerpilih = b;
                IsiFormEdit(b);
            }
        }

        private void IsiFormEdit(Barang b)
        {
            textBoxNamaBaru.Text = b.Nama;
            textBoxKategoriBaru.Text = b.Kategori;
            textBoxStokBaru.Text = b.Stok.ToString();
            textBoxHargaBeliBaru.Text = b.HargaBeli.ToString();
            textBoxHargaJualBaru.Text = b.HargaJual.ToString();
            textBoxSupplierBaru.Text = b.Supplier;
        }

        private void ClearFormInput()
        {
            textBoxNamaBaru.Clear();
            textBoxKategoriBaru.Clear();
            textBoxStokBaru.Clear();
            textBoxHargaBeliBaru.Clear();
            textBoxHargaJualBaru.Clear();
            textBoxSupplierBaru.Clear();
        }
        #endregion
    }
}
