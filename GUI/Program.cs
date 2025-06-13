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
        private Panel sidebarPanel;
        private Panel mainPanel;
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblWelcome;
        private Label lblDateTime;
        private Button btnToggleSidebar;
        private System.Windows.Forms.Timer timeTimer;

        // Sidebar buttons
        private Button btnTambahBarang;
        private Button btnEditBarang;
        private Button btnHapusBarang;
        private Button btnCariBarang;
        private Button btnTampilkanBarang;
        private Button btnTransaksiMasuk;
        private Button btnTransaksiKeluar;
        private Button btnRiwayatTransaksi;
        private Button btnLaporanInventaris;

        // Colors
        private readonly Color sidebarColor = Color.FromArgb(52, 73, 94);
        private readonly Color sidebarHoverColor = Color.FromArgb(74, 144, 226);
        private readonly Color mainPanelColor = Color.FromArgb(236, 240, 241);
        private readonly Color headerColor = Color.FromArgb(255, 255, 255);
        private readonly Color textColor = Color.FromArgb(44, 62, 80);
        private readonly Color accentColor = Color.FromArgb(74, 144, 226);

        private bool sidebarExpanded = true;
        private const int sidebarExpandedWidth = 250;
        private const int sidebarCollapsedWidth = 70;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeComponent()
        {
            // Form settings
            this.Text = "InvenApp - Sistem Inventaris Toko";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(900, 600);
            this.BackColor = mainPanelColor;

            // Initialize panels
            InitializeSidebar();
            InitializeMainPanel();
            InitializeHeader();

            // Menambahkan panels to form
            this.Controls.Add(mainPanel);
            this.Controls.Add(sidebarPanel);
        }

        private void InitializeSidebar()
        {
            sidebarPanel = new Panel();
            sidebarPanel.Width = sidebarExpandedWidth;
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.BackColor = sidebarColor;

            // Logo/Title section
            var logoPanel = new Panel();
            logoPanel.Height = 80;
            logoPanel.Dock = DockStyle.Top;
            logoPanel.BackColor = Color.FromArgb(44, 62, 80);

            lblTitle = new Label();
            lblTitle.Text = "📦 InvenApp";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Fill;

            logoPanel.Controls.Add(lblTitle);

            // Menu buttons container dengan scrollable panel
            var menuPanel = new Panel();
            menuPanel.Dock = DockStyle.Fill;
            menuPanel.BackColor = Color.Transparent;
            menuPanel.AutoScroll = true;
            menuPanel.Padding = new Padding(5, 10, 5, 10);

            // Membuat sidebar buttons
            btnTambahBarang = CreateSidebarButton("➕  Tambah Barang", BtnTambahBarang_Click);
            btnEditBarang = CreateSidebarButton("✏️  Edit Barang", BtnEditBarang_Click);
            btnHapusBarang = CreateSidebarButton("🗑️  Hapus Barang", BtnHapusBarang_Click);
            btnCariBarang = CreateSidebarButton("🔍  Cari Barang", BtnCariBarang_Click);
            btnTampilkanBarang = CreateSidebarButton("📋  Tampilkan Semua", BtnTampilkanBarang_Click);
            btnTransaksiMasuk = CreateSidebarButton("📥  Transaksi Masuk", BtnTransaksiMasuk_Click);
            btnTransaksiKeluar = CreateSidebarButton("📤  Transaksi Keluar", BtnTransaksiKeluar_Click);
            btnRiwayatTransaksi = CreateSidebarButton("📊  Riwayat Transaksi", BtnRiwayatTransaksi_Click);
            btnLaporanInventaris = CreateSidebarButton("📈  Laporan Inventaris", BtnLaporanInventaris_Click);

            // Position buttons vertically
            var buttons = new[] { btnTambahBarang, btnEditBarang, btnHapusBarang, btnCariBarang,
                                btnTampilkanBarang, btnTransaksiMasuk, btnTransaksiKeluar,
                                btnRiwayatTransaksi, btnLaporanInventaris };

            int yPosition = 10;
            foreach (var btn in buttons)
            {
                btn.Location = new Point(5, yPosition);
                btn.Width = sidebarExpandedWidth - 10;
                menuPanel.Controls.Add(btn);
                yPosition += 55; // Spacing antar tombol
            }

            // Menambahkan panels to sidebar
            sidebarPanel.Controls.Add(menuPanel);
            sidebarPanel.Controls.Add(logoPanel);
        }

        private void InitializeMainPanel()
        {
            mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = mainPanelColor;
        }

        private void InitializeHeader()
        {
            headerPanel = new Panel();
            headerPanel.Height = 140;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.BackColor = headerColor;
            headerPanel.Padding = new Padding(30, 20, 30, 20);

            // Toggle button
            btnToggleSidebar = new Button();
            btnToggleSidebar.Text = "☰";
            btnToggleSidebar.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            btnToggleSidebar.Size = new Size(40, 40);
            btnToggleSidebar.Location = new Point(15, 40);
            btnToggleSidebar.FlatStyle = FlatStyle.Flat;
            btnToggleSidebar.BackColor = accentColor;
            btnToggleSidebar.ForeColor = Color.White;
            btnToggleSidebar.Cursor = Cursors.Hand;
            btnToggleSidebar.Click += BtnToggleSidebar_Click;

            // Welcome message
            lblWelcome = new Label();
            lblWelcome.Text = "Selamat Datang di Sistem Inventaris Toko";
            lblWelcome.Font = new Font("Segoe UI", 15F, FontStyle.Regular); // Ubah dari Bold ke Regular
            lblWelcome.ForeColor = textColor;
            lblWelcome.Location = new Point(70, 25);
            lblWelcome.Size = new Size(600, 30); // Memberikan ukuran yang cukup
            lblWelcome.AutoSize = false;

            // Subtitle
            lblSubtitle = new Label();
            lblSubtitle.Text = "Kelola inventaris toko Anda dengan mudah dan efisien";
            lblSubtitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            lblSubtitle.ForeColor = Color.FromArgb(127, 140, 141);
            lblSubtitle.Location = new Point(70, 60);
            lblSubtitle.Size = new Size(500, 25);
            lblSubtitle.AutoSize = false;

            // Date and time
            lblDateTime = new Label();
            lblDateTime.Font = new Font("Segoe UI", 11F);
            lblDateTime.ForeColor = Color.FromArgb(127, 140, 141);
            lblDateTime.Location = new Point(70, 85);
            lblDateTime.Size = new Size(300, 20);
            lblDateTime.AutoSize = false;

            headerPanel.Controls.Add(btnToggleSidebar);
            headerPanel.Controls.Add(lblWelcome);
            headerPanel.Controls.Add(lblSubtitle);
            headerPanel.Controls.Add(lblDateTime);

            // Content area
            var contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.Transparent;
            contentPanel.Padding = new Padding(30);

            // Dashboard cards
            CreateDashboardCards(contentPanel);

            mainPanel.Controls.Add(contentPanel);
            mainPanel.Controls.Add(headerPanel);
        }

        private void CreateDashboardCards(Panel parent)
        {
            var cardsPanel = new Panel();
            cardsPanel.Dock = DockStyle.Fill;
            cardsPanel.BackColor = Color.Transparent;
            cardsPanel.Padding = new Padding(0, 20, 0, 0);

            // Membuat dashboard cards
            var cards = new[]
            {
                new { Title = "Total Barang", Value = "1,234", Icon = "📦", Color = Color.FromArgb(74, 144, 226) },
                new { Title = "Transaksi Hari Ini", Value = "56", Icon = "💰", Color = Color.FromArgb(80, 200, 120) },
                new { Title = "Stok Menipis", Value = "12", Icon = "⚠️", Color = Color.FromArgb(255, 193, 7) },
                new { Title = "Nilai Inventaris", Value = "Rp 45.2M", Icon = "📊", Color = Color.FromArgb(111, 66, 193) }
            };

            int cardWidth = 220;
            int cardHeight = 130;
            int spacing = 20;
            int xPos = 0;

            foreach (var card in cards)
            {
                var cardPanel = CreateDashboardCard(card.Title, card.Value, card.Icon, card.Color);
                cardPanel.Size = new Size(cardWidth, cardHeight);
                cardPanel.Location = new Point(xPos, 0);
                cardsPanel.Controls.Add(cardPanel);
                xPos += cardWidth + spacing;
            }

            parent.Controls.Add(cardsPanel);
        }

        private Panel CreateDashboardCard(string title, string value, string icon, Color color)
        {
            var card = new Panel();
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.None;

            // Menambahkan efek shadow
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(Color.FromArgb(220, 220, 220), 1))
                {
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };

            // Icon
            var iconLabel = new Label();
            iconLabel.Text = icon;
            iconLabel.Font = new Font("Segoe UI", 25F);
            iconLabel.ForeColor = color;
            iconLabel.Location = new Point(20, 20);
            iconLabel.Size = new Size(50, 60);
            iconLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Value
            var valueLabel = new Label();
            valueLabel.Text = value;
            valueLabel.Font = new Font("Segoe UI", 15F, FontStyle.Regular);
            valueLabel.ForeColor = textColor;
            valueLabel.Location = new Point(85, 20);
            valueLabel.Size = new Size(120, 35);
            valueLabel.TextAlign = ContentAlignment.MiddleLeft;

            // Title
            var titleLabel = new Label();
            titleLabel.Text = title;
            titleLabel.Font = new Font("Segoe UI", 11F);
            titleLabel.ForeColor = Color.FromArgb(127, 140, 141);
            titleLabel.Location = new Point(85, 55);
            titleLabel.Size = new Size(120, 25);
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;

            card.Controls.Add(iconLabel);
            card.Controls.Add(valueLabel);
            card.Controls.Add(titleLabel);

            return card;
        }

        private Button CreateSidebarButton(string text, EventHandler onClick)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btn.ForeColor = Color.White;
            btn.BackColor = Color.Transparent;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = sidebarHoverColor;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 15, 0);
            btn.Size = new Size(sidebarExpandedWidth - 10, 50);
            btn.Cursor = Cursors.Hand;
            btn.Click += onClick;

            // Menambahkan efek hover
            btn.MouseEnter += (s, e) => btn.BackColor = sidebarHoverColor;
            btn.MouseLeave += (s, e) => btn.BackColor = Color.Transparent;

            return btn;
        }

        private void InitializeTimer()
        {
            timeTimer = new System.Windows.Forms.Timer();
            timeTimer.Interval = 1000;
            timeTimer.Tick += (s, e) => {
                lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            };
            timeTimer.Start();
        }

        private void BtnToggleSidebar_Click(object sender, EventArgs e)
        {
            sidebarExpanded = !sidebarExpanded;

            if (sidebarExpanded)
            {
                sidebarPanel.Width = sidebarExpandedWidth;
                lblTitle.Text = "📦 InvenApp";

                // Restore full text on buttons
                btnTambahBarang.Text = "➕  Tambah Barang";
                btnEditBarang.Text = "✏️  Edit Barang";
                btnHapusBarang.Text = "🗑️  Hapus Barang";
                btnCariBarang.Text = "🔍  Cari Barang";
                btnTampilkanBarang.Text = "📋  Tampilkan Semua";
                btnTransaksiMasuk.Text = "📥  Transaksi Masuk";
                btnTransaksiKeluar.Text = "📤  Transaksi Keluar";
                btnRiwayatTransaksi.Text = "📊  Riwayat Transaksi";
                btnLaporanInventaris.Text = "📈  Laporan Inventaris";

                var buttons = new[] { btnTambahBarang, btnEditBarang, btnHapusBarang, btnCariBarang,
                                    btnTampilkanBarang, btnTransaksiMasuk, btnTransaksiKeluar,
                                    btnRiwayatTransaksi, btnLaporanInventaris };

                foreach (var btn in buttons)
                {
                    btn.Width = sidebarExpandedWidth - 10;
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    btn.Padding = new Padding(15, 0, 15, 0);
                    btn.Location = new Point(5, btn.Location.Y);
                }
            }
            else
            {
                sidebarPanel.Width = sidebarCollapsedWidth;
                lblTitle.Text = "📦";

                // Show only icons on buttons
                btnTambahBarang.Text = "➕";
                btnEditBarang.Text = "✏️";
                btnHapusBarang.Text = "🗑️";
                btnCariBarang.Text = "🔍";
                btnTampilkanBarang.Text = "📋";
                btnTransaksiMasuk.Text = "📥";
                btnTransaksiKeluar.Text = "📤";
                btnRiwayatTransaksi.Text = "📊";
                btnLaporanInventaris.Text = "📈";

                var buttons = new[] { btnTambahBarang, btnEditBarang, btnHapusBarang, btnCariBarang,
                                    btnTampilkanBarang, btnTransaksiMasuk, btnTransaksiKeluar,
                                    btnRiwayatTransaksi, btnLaporanInventaris };

                foreach (var btn in buttons)
                {
                    btn.Width = sidebarCollapsedWidth - 10;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.Padding = new Padding(0);
                    btn.Location = new Point(5, btn.Location.Y);
                }
            }
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timeTimer?.Stop();
            timeTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}