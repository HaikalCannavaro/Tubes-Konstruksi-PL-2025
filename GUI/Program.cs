using AplikasiInventarisToko.GUI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        private Label lblTitle;
        private Button btnTambahBarang;
        private Button btnEditBarang;
        private Button btnHapusBarang;
        private Button btnCariBarang;
        private Button btnTampilkanBarang;
        private Button btnTransaksiMasuk;
        private Button btnTransaksiKeluar;
        private Button btnRiwayatTransaksi;
        private Button btnLaporanInventaris;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form settings
            this.Text = "InvenApp - Homepage";
            this.Size = new Size(460, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title Label
            lblTitle = new Label();
            lblTitle.Text = "InvenApp";
            lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 80;
            lblTitle.Padding = new Padding(0, 20, 0, 20);

            // Panel container for buttons with grid layout (3x3)
            var panelButtons = new TableLayoutPanel();
            panelButtons.RowCount = 3;
            panelButtons.ColumnCount = 3;
            panelButtons.Dock = DockStyle.Fill;
            panelButtons.Padding = new Padding(20);
            panelButtons.AutoSize = false;
            panelButtons.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            panelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            panelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            panelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            panelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            panelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            panelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            panelButtons.BackColor = Color.Transparent;

            // Create Buttons with consistent styling
            btnTambahBarang = CreateButton("1. Tambah Barang", BtnTambahBarang_Click);
            btnEditBarang = CreateButton("2. Edit Barang", BtnEditBarang_Click);
            btnHapusBarang = CreateButton("3. Hapus Barang", BtnHapusBarang_Click);
            btnCariBarang = CreateButton("4. Cari Barang", BtnCariBarang_Click);
            btnTampilkanBarang = CreateButton("5. Tampilkan Semua Barang", BtnTampilkanBarang_Click);
            btnTransaksiMasuk = CreateButton("6. Transaksi Barang Masuk", BtnTransaksiMasuk_Click);
            btnTransaksiKeluar = CreateButton("7. Transaksi Barang Keluar", BtnTransaksiKeluar_Click);
            btnRiwayatTransaksi = CreateButton("8. Riwayat Transaksi", BtnRiwayatTransaksi_Click);
            btnLaporanInventaris = CreateButton("9. Laporan Inventaris", BtnLaporanInventaris_Click);

            // Add buttons to panel in order (3 columns x 3 rows)
            panelButtons.Controls.Add(btnTambahBarang, 0, 0);
            panelButtons.Controls.Add(btnEditBarang, 1, 0);
            panelButtons.Controls.Add(btnHapusBarang, 2, 0);
            panelButtons.Controls.Add(btnCariBarang, 0, 1);
            panelButtons.Controls.Add(btnTampilkanBarang, 1, 1);
            panelButtons.Controls.Add(btnTransaksiMasuk, 2, 1);
            panelButtons.Controls.Add(btnTransaksiKeluar, 0, 2);
            panelButtons.Controls.Add(btnRiwayatTransaksi, 1, 2);
            panelButtons.Controls.Add(btnLaporanInventaris, 2, 2);

            // Add controls to form
            this.Controls.Add(panelButtons);
            this.Controls.Add(lblTitle);
        }

        private Button CreateButton(string text, EventHandler onClick)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btn.Dock = DockStyle.Fill;
            btn.Margin = new Padding(10);
            btn.Height = 60;
            btn.Click += onClick;
            return btn;
        }

        // Event Handlers for buttons

        private void BtnTambahBarang_Click(object sender, EventArgs e)
        {
            FormTambahBarang formTambah = new FormTambahBarang();
            formTambah.ShowDialog();
        }

        private void BtnEditBarang_Click(object sender, EventArgs e)
        {
            FormEditBarang form = new FormEditBarang();
            form.ShowDialog();
        }

        private void BtnHapusBarang_Click(object sender, EventArgs e)
        {
            FormHapusBarang form = new FormHapusBarang();
            form.ShowDialog();
        }

        private void BtnCariBarang_Click(object sender, EventArgs e)
        {
            FormCariBarang form = new FormCariBarang();
            form.ShowDialog();
        }

        private void BtnTampilkanBarang_Click(object sender, EventArgs e)
        {
            FormTampilkanBarang formTampilkan = new FormTampilkanBarang();
            formTampilkan.ShowDialog();
        }

        private void BtnTransaksiMasuk_Click(object sender, EventArgs e)
        {
            FormTransaksiMasuk form = new FormTransaksiMasuk();
            form.ShowDialog();
        }

        private void BtnTransaksiKeluar_Click(object sender, EventArgs e)
        {
            FormTransaksiKeluar form = new FormTransaksiKeluar();
            form.ShowDialog();
        }

        private void BtnRiwayatTransaksi_Click(object sender, EventArgs e)
        {
            FormRiwayatTransaksi form = new FormRiwayatTransaksi();
            form.ShowDialog();
        }

        private void BtnLaporanInventaris_Click(object sender, EventArgs e)
        {
            FormLaporanInventaris form = new FormLaporanInventaris();
            form.ShowDialog();
        }
    }
}

