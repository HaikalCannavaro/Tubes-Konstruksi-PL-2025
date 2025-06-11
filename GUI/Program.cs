using AplikasiInventarisToko.GUI;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private Label lblSubtitle;
        private Button btnTambahBarang;
        private Button btnEditBarang;
        private Button btnHapusBarang;
        private Button btnCariBarang;
        private Button btnTampilkanBarang;
        private Button btnTransaksiMasuk;
        private Button btnTransaksiKeluar;
        private Button btnRiwayatTransaksi;
        private Button btnLaporanInventaris;
        private Panel headerPanel;
        private Panel contentPanel;

        private readonly Color primaryColor = Color.FromArgb(74, 144, 226); // Blue
        private readonly Color secondaryColor = Color.FromArgb(80, 200, 120); // Green
        private readonly Color accentColor = Color.FromArgb(255, 87, 87); // Red
        private readonly Color warningColor = Color.FromArgb(255, 193, 7); // Yellow
        private readonly Color infoColor = Color.FromArgb(23, 162, 184); // Cyan
        private readonly Color backgroundColor = Color.FromArgb(248, 249, 250); // Light gray
        private readonly Color cardColor = Color.White;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(74, 144, 226),
                Color.FromArgb(142, 68, 173),
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            base.OnPaint(e);
        }

        private void InitializeComponent()
        {
            this.Text = "InvenApp - Sistem Inventaris Toko";
            this.Size = new Size(520, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.BackColor = backgroundColor;

            headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 120;
            headerPanel.BackColor = Color.Transparent;

            lblTitle = new Label();
            lblTitle.Text = "📦 InvenApp";
            lblTitle.Font = new Font("Segoe UI", 32F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 70;
            lblTitle.Padding = new Padding(0, 15, 0, 0);

            lblSubtitle = new Label();
            lblSubtitle.Text = "Sistem Manajemen Inventaris Toko";
            lblSubtitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblSubtitle.ForeColor = Color.FromArgb(230, 230, 230);
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            lblSubtitle.Dock = DockStyle.Fill;

            headerPanel.Controls.Add(lblSubtitle);
            headerPanel.Controls.Add(lblTitle);

            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.Transparent;
            contentPanel.Padding = new Padding(30, 20, 30, 30);

            var panelButtons = new TableLayoutPanel();
            panelButtons.RowCount = 3;
            panelButtons.ColumnCount = 3;
            panelButtons.Dock = DockStyle.Fill;
            panelButtons.AutoSize = false;
            panelButtons.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            panelButtons.BackColor = Color.Transparent;

            for (int i = 0; i < 3; i++)
            {
                panelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                panelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            }

            btnTambahBarang = CreateModernButton("➕ Tambah\nBarang", secondaryColor, BtnTambahBarang_Click);
            btnEditBarang = CreateModernButton("✏️ Edit\nBarang", primaryColor, BtnEditBarang_Click);
            btnHapusBarang = CreateModernButton("🗑️ Hapus\nBarang", accentColor, BtnHapusBarang_Click);
            btnCariBarang = CreateModernButton("🔍 Cari\nBarang", infoColor, BtnCariBarang_Click);
            btnTampilkanBarang = CreateModernButton("📋 Tampilkan\nSemua", Color.FromArgb(108, 117, 125), BtnTampilkanBarang_Click);
            btnTransaksiMasuk = CreateModernButton("📥 Transaksi\nMasuk", secondaryColor, BtnTransaksiMasuk_Click);
            btnTransaksiKeluar = CreateModernButton("📤 Transaksi\nKeluar", warningColor, BtnTransaksiKeluar_Click);
            btnRiwayatTransaksi = CreateModernButton("📊 Riwayat\nTransaksi", Color.FromArgb(111, 66, 193), BtnRiwayatTransaksi_Click);
            btnLaporanInventaris = CreateModernButton("📈 Laporan\nInventaris", Color.FromArgb(220, 53, 69), BtnLaporanInventaris_Click);

            panelButtons.Controls.Add(btnTambahBarang, 0, 0);
            panelButtons.Controls.Add(btnEditBarang, 1, 0);
            panelButtons.Controls.Add(btnHapusBarang, 2, 0);
            panelButtons.Controls.Add(btnCariBarang, 0, 1);
            panelButtons.Controls.Add(btnTampilkanBarang, 1, 1);
            panelButtons.Controls.Add(btnTransaksiMasuk, 2, 1);
            panelButtons.Controls.Add(btnTransaksiKeluar, 0, 2);
            panelButtons.Controls.Add(btnRiwayatTransaksi, 1, 2);
            panelButtons.Controls.Add(btnLaporanInventaris, 2, 2);

            contentPanel.Controls.Add(panelButtons);

            this.Controls.Add(contentPanel);
            this.Controls.Add(headerPanel);
        }

        private Button CreateModernButton(string text, Color backgroundColor, EventHandler onClick)
        {
            var btn = new CustomButton();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn.BackColor = backgroundColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backgroundColor, 0.2f);
            btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backgroundColor, 0.1f);
            btn.Dock = DockStyle.Fill;
            btn.Margin = new Padding(8);
            btn.Cursor = Cursors.Hand;
            btn.Click += onClick;

            return btn;
        }

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

    public class CustomButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath grPath = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            int radius = 15;

            grPath.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            grPath.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            grPath.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            grPath.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            grPath.CloseAllFigures();

            this.Region = new System.Drawing.Region(grPath);

            base.OnPaint(pevent);
        }
    }
}