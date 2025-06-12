using AplikasiInventarisToko.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiInventarisToko.GUI
{
    public partial class FormCariBarang : Form
    {
        private HttpClient httpClient;

        public FormCariBarang()
        {
            InitializeComponent();
            InitializeHttpClient();
        }

        private void InitializeHttpClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            httpClient = new HttpClient(handler);
            httpClient.BaseAddress = new Uri("https://localhost:7123");
        }

        private void FormCariBarang_Load(object sender, EventArgs e)
        {
            // Set default selection
            comboBoxKriteria.SelectedIndex = 1; // Default to "nama"

            // Configure DataGridView
            ConfigureDataGridView();

            // Load all items initially
            _ = LoadAllBarangAsync();
        }

        private void ConfigureDataGridView()
        {
            dataGridViewHasil.AutoGenerateColumns = false;
            dataGridViewHasil.Columns.Clear();

            dataGridViewHasil.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 80
            });

            dataGridViewHasil.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nama",
                HeaderText = "Nama Barang",
                DataPropertyName = "Nama",
                Width = 150
            });

            dataGridViewHasil.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Kategori",
                HeaderText = "Kategori",
                DataPropertyName = "Kategori",
                Width = 100
            });

            dataGridViewHasil.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stok",
                HeaderText = "Stok",
                DataPropertyName = "Stok",
                Width = 80
            });

            dataGridViewHasil.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HargaBeli",
                HeaderText = "Harga Beli",
                DataPropertyName = "HargaBeli",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            dataGridViewHasil.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HargaJual",
                HeaderText = "Harga Jual",
                DataPropertyName = "HargaJual",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            dataGridViewHasil.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Supplier",
                HeaderText = "Supplier",
                DataPropertyName = "Supplier",
                Width = 120
            });

            dataGridViewHasil.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TanggalMasuk",
                HeaderText = "Tanggal Masuk",
                DataPropertyName = "TanggalMasuk",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
        }

        private async Task LoadAllBarangAsync()
        {
            try
            {
                btnCari.Enabled = false;
                btnCari.Text = "⏳ Loading...";

                var response = await httpClient.GetAsync("/api/Barang");

                if (response.IsSuccessStatusCode)
                {
                    var daftarBarang = await response.Content.ReadFromJsonAsync<List<Barang>>();
                    DisplayResults(daftarBarang);
                }
                else
                {
                    MessageBox.Show("Gagal mengambil data barang dari server.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCari.Enabled = true;
                btnCari.Text = "🔍 Cari";
            }
        }

        private async void btnCari_Click(object sender, EventArgs e)
        {
            if (comboBoxKriteria.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih kriteria pencarian terlebih dahulu.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxKriteria.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxNilaiPencarian.Text))
            {
                MessageBox.Show("Silakan masukkan nilai pencarian.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNilaiPencarian.Focus();
                return;
            }

            await CariBarangAsync();
        }

        private async Task CariBarangAsync()
        {
            try
            {
                btnCari.Enabled = false;
                btnCari.Text = "⏳ Mencari...";

                string kriteria = comboBoxKriteria.SelectedItem.ToString();
                string nilai = textBoxNilaiPencarian.Text.Trim();

                var response = await httpClient.GetAsync($"/api/Barang/search?kriteria={kriteria}&nilai={nilai}");

                if (response.IsSuccessStatusCode)
                {
                    var hasil = await response.Content.ReadFromJsonAsync<List<Barang>>();
                    DisplayResults(hasil);

                    if (hasil == null || hasil.Count == 0)
                    {
                        MessageBox.Show($"Tidak ditemukan barang dengan {kriteria}: '{nilai}'",
                            "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Gagal mencari barang: {errorMessage}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCari.Enabled = true;
                btnCari.Text = "🔍 Cari";
            }
        }

        private void DisplayResults(List<Barang> hasil)
        {
            if (hasil == null)
            {
                hasil = new List<Barang>();
            }

            dataGridViewHasil.DataSource = hasil;
            labelHasil.Text = $"Hasil Pencarian: {hasil.Count} barang ditemukan";

            // Update row colors based on stock level
            foreach (DataGridViewRow row in dataGridViewHasil.Rows)
            {
                if (row.DataBoundItem is Barang barang)
                {
                    if (barang.Stok == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235); // Light red
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(155, 0, 0); // Dark red
                    }
                    else if (barang.Stok <= 5)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 235); // Light orange
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(155, 85, 0); // Dark orange
                    }
                }
            }
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            // Clear search inputs
            comboBoxKriteria.SelectedIndex = 1; // Reset to "nama"
            textBoxNilaiPencarian.Clear();

            // Reload all items
            await LoadAllBarangAsync();

            textBoxNilaiPencarian.Focus();
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void textBoxNilaiPencarian_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow Enter key to trigger search
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (comboBoxKriteria.SelectedItem != null && !string.IsNullOrWhiteSpace(textBoxNilaiPencarian.Text))
                {
                    await CariBarangAsync();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                httpClient?.Dispose();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void dataGridViewHasil_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}