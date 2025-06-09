using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        private Panel topNavPanel;
        private Label logoLabel;
        private Panel sidebarPanel;
        private Panel mainContentPanel;
        private Panel welcomePanel;
        private Panel currentFeaturePanel;

        private string[] featureNames = new string[]
        {
            "Tambah Barang",
            "Edit Barang",
            "Hapus Barang",
            "Cari Barang",
            "Tampilkan Semua Barang",
            "Transaksi Barang Masuk",
            "Transaksi Barang Keluar",
            "Riwayat Transaksi",
            "Laporan Inventaris"
        };

        private string[] featureIcons = new string[]
        {
            "➕", "✏️", "❌", "🔍", "📋", "⬇️", "⬆️", "⏲️", "📊"
        };

        private Button[] featureButtons;
        private Button activeButton;

        public MainForm()
        {
            InitializeComponent();
            ShowWelcomeContent();

            // Debug: Verify all features are loaded
            System.Diagnostics.Debug.WriteLine($"Total features loaded: {featureNames.Length}");
            for (int i = 0; i < featureNames.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine($"Feature {i}: {featureNames[i]}");
            }
        }

        private void InitializeComponent()
        {
            // Form Setup
            this.Text = "Manajemen Inventaris - InvenTrack";
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(1200, 700);
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.StartPosition = FormStartPosition.CenterScreen;

            // IMPORTANT: Order matters for docking!
            // 1. First add Top Navigation (Dock.Top)
            CreateTopNavigation();

            // 2. Then add Main Content (Dock.Fill) 
            CreateMainContent();

            // 3. Finally add Sidebar (Dock.Left) - this will dock to remaining space
            CreateSidebar();
        }

        private void CreateTopNavigation()
        {
            topNavPanel = new Panel()
            {
                BackColor = Color.White,
                Height = 70,
                Dock = DockStyle.Top,
                Padding = new Padding(30, 0, 30, 0),
            };

            topNavPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    e.Graphics.DrawLine(pen, 0, topNavPanel.Height - 1, topNavPanel.Width, topNavPanel.Height - 1);
                }
            };

            this.Controls.Add(topNavPanel);

            // Logo Label
            logoLabel = new Label()
            {
                Text = "InvenTrack",
                Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(37, 99, 235),
                AutoSize = true,
                Dock = DockStyle.Left,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 15, 0, 15),
            };
            topNavPanel.Controls.Add(logoLabel);

            // Version Label
            var versionLabel = new Label()
            {
                Text = "v1.0",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(156, 163, 175),
                AutoSize = true,
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 25, 0, 15),
            };
            topNavPanel.Controls.Add(versionLabel);
        }

        private void CreateSidebar()
        {
            sidebarPanel = new Panel()
            {
                BackColor = Color.White,
                Width = 300,
                Dock = DockStyle.Left,
                Padding = new Padding(0, 20, 0, 20),
            };

            sidebarPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    e.Graphics.DrawLine(pen, sidebarPanel.Width - 1, 0, sidebarPanel.Width - 1, sidebarPanel.Height);
                }
            };

            this.Controls.Add(sidebarPanel);

            // Sidebar Header
            var sidebarHeader = new Label()
            {
                Text = "Fitur Utama",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(55, 65, 81),
                Height = 40,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.BottomLeft,
                Padding = new Padding(20, 0, 20, 10),
            };
            sidebarPanel.Controls.Add(sidebarHeader);

            // Features Container
            var featuresContainer = new Panel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10, 10, 10, 10),
                BackColor = Color.White, // Make background visible for debugging
            };
            sidebarPanel.Controls.Add(featuresContainer);

            // Create feature buttons
            featureButtons = new Button[featureNames.Length];
            for (int i = 0; i < featureNames.Length; i++)
            {
                var button = CreateFeatureButton(featureNames[i], featureIcons[i], i);
                button.Top = i * 55;
                button.Left = 0; // Ensure proper positioning
                featuresContainer.Controls.Add(button);
                featureButtons[i] = button;

                // Debug: Show that all features are being created
                System.Diagnostics.Debug.WriteLine($"Created button {i}: {featureNames[i]}");
            }

            // Set the container's AutoScrollMinSize to ensure all buttons are accessible
            featuresContainer.AutoScrollMinSize = new Size(0, featureNames.Length * 55 + 20);
        }

        private Button CreateFeatureButton(string title, string icon, int index)
        {
            var button = new Button()
            {
                Text = $"  {icon}  {title}",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(75, 85, 99),
                BackColor = Color.Transparent,
                Height = 50,
                Width = 270,
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = index,
                Margin = new Padding(0, 2, 0, 2),
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 244, 246);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(229, 231, 235);

            button.Click += FeatureButton_Click;
            button.MouseEnter += (s, e) =>
            {
                if (button != activeButton)
                {
                    button.BackColor = Color.FromArgb(243, 244, 246);
                }
            };
            button.MouseLeave += (s, e) =>
            {
                if (button != activeButton)
                {
                    button.BackColor = Color.Transparent;
                }
            };

            return button;
        }

        private void CreateMainContent()
        {
            mainContentPanel = new Panel()
            {
                BackColor = Color.FromArgb(248, 250, 252),
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 30, 40, 30), // Left padding reduced, right padding increased
            };
            this.Controls.Add(mainContentPanel);
        }

        private void FeatureButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            // Update active button appearance
            if (activeButton != null)
            {
                activeButton.BackColor = Color.Transparent;
                activeButton.ForeColor = Color.FromArgb(75, 85, 99);
                activeButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            }

            activeButton = clickedButton;
            activeButton.BackColor = Color.FromArgb(37, 99, 235);
            activeButton.ForeColor = Color.White;
            activeButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);

            int featureIndex = (int)clickedButton.Tag;
            ShowFeatureContent(featureNames[featureIndex], featureIndex);
        }

        private void ShowWelcomeContent()
        {
            mainContentPanel.Controls.Clear();

            welcomePanel = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(40),
                Margin = new Padding(10), // Add margin for separation
            };

            welcomePanel.Paint += (s, e) =>
            {
                var rect = new Rectangle(0, 0, welcomePanel.Width - 1, welcomePanel.Height - 1);
                using (var pen = new Pen(Color.FromArgb(229, 231, 235), 1))
                {
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };

            mainContentPanel.Controls.Add(welcomePanel);

            // Welcome Title
            var welcomeTitle = new Label()
            {
                Text = "Selamat Datang di InvenTrack",
                Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(0, 50),
            };
            welcomePanel.Controls.Add(welcomeTitle);

            // Welcome Description
            var welcomeDesc = new Label()
            {
                Text = "Pilih salah satu fitur dari sidebar untuk memulai mengelola inventaris Anda.\n\nSistem ini menyediakan berbagai fitur lengkap untuk:\n• Mengelola stok barang\n• Melakukan transaksi masuk dan keluar\n• Melihat riwayat transaksi\n• Membuat laporan inventaris",
                Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(107, 114, 128),
                MaximumSize = new Size(700, 0),
                AutoSize = true,
                Location = new Point(0, 120),
            };
            welcomePanel.Controls.Add(welcomeDesc);

            // Feature Cards Preview
            var previewLabel = new Label()
            {
                Text = "Fitur Utama:",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(0, 280),
            };
            welcomePanel.Controls.Add(previewLabel);

            // Mini feature cards
            string[] mainFeatures = { "Tambah Barang", "Cari Barang", "Transaksi", "Laporan" };
            string[] mainIcons = { "➕", "🔍", "📦", "📊" };

            for (int i = 0; i < mainFeatures.Length; i++)
            {
                var miniCard = new Panel()
                {
                    Width = 150,
                    Height = 80,
                    Location = new Point(i * 170, 330),
                    BackColor = Color.FromArgb(249, 250, 251),
                    Cursor = Cursors.Hand,
                };

                miniCard.Paint += (s, e) =>
                {
                    var rect = new Rectangle(0, 0, miniCard.Width - 1, miniCard.Height - 1);
                    using (var pen = new Pen(Color.FromArgb(209, 213, 219), 1))
                    {
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                };

                var iconLabel = new Label()
                {
                    Text = mainIcons[i],
                    Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point),
                    ForeColor = Color.FromArgb(37, 99, 235),
                    AutoSize = true,
                    Location = new Point(15, 15),
                };
                miniCard.Controls.Add(iconLabel);

                var nameLabel = new Label()
                {
                    Text = mainFeatures[i],
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point),
                    ForeColor = Color.FromArgb(55, 65, 81),
                    AutoSize = true,
                    Location = new Point(15, 45),
                };
                miniCard.Controls.Add(nameLabel);

                welcomePanel.Controls.Add(miniCard);
            }
        }

        private void ShowFeatureContent(string featureName, int featureIndex)
        {
            mainContentPanel.Controls.Clear();

            currentFeaturePanel = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(40),
                Margin = new Padding(10), // Add margin for separation
            };

            currentFeaturePanel.Paint += (s, e) =>
            {
                var rect = new Rectangle(0, 0, currentFeaturePanel.Width - 1, currentFeaturePanel.Height - 1);
                using (var pen = new Pen(Color.FromArgb(229, 231, 235), 1))
                {
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };

            mainContentPanel.Controls.Add(currentFeaturePanel);

            // Feature Title
            var featureTitle = new Label()
            {
                Text = $"{featureIcons[featureIndex]} {featureName}",
                Font = new Font("Segoe UI", 32F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(0, 20),
            };
            currentFeaturePanel.Controls.Add(featureTitle);

            // Feature Description
            string description = GetFeatureDescription(featureIndex);
            var featureDesc = new Label()
            {
                Text = description,
                Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(107, 114, 128),
                MaximumSize = new Size(800, 0),
                AutoSize = true,
                Location = new Point(0, 80),
            };
            currentFeaturePanel.Controls.Add(featureDesc);

            // Action Buttons
            CreateActionButtons(featureIndex);

            // Add specific content based on feature
            CreateFeatureSpecificContent(featureIndex);
        }

        private void CreateActionButtons(int featureIndex)
        {
            var buttonPanel = new Panel()
            {
                Height = 60,
                Width = 400,
                Location = new Point(0, 180),
                BackColor = Color.Transparent,
            };
            currentFeaturePanel.Controls.Add(buttonPanel);

            var primaryButton = new Button()
            {
                Text = GetPrimaryButtonText(featureIndex),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(37, 99, 235),
                Height = 45,
                Width = 150,
                Location = new Point(0, 0),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
            };
            primaryButton.FlatAppearance.BorderSize = 0;
            primaryButton.Click += (s, e) => HandleFeatureAction(featureIndex);
            buttonPanel.Controls.Add(primaryButton);

            var secondaryButton = new Button()
            {
                Text = "Lihat Detail",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(55, 65, 81),
                BackColor = Color.White,
                Height = 45,
                Width = 120,
                Location = new Point(170, 0),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
            };
            secondaryButton.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            secondaryButton.Click += (s, e) => MessageBox.Show($"Detail lengkap untuk {featureNames[featureIndex]}:\n\n{GetFeatureDescription(featureIndex)}", "Detail Fitur", MessageBoxButtons.OK, MessageBoxIcon.Information);
            buttonPanel.Controls.Add(secondaryButton);
        }

        private void CreateFeatureSpecificContent(int featureIndex)
        {
            // Add feature-specific content below the buttons
            var contentPanel = new Panel()
            {
                Location = new Point(0, 260),
                Width = currentFeaturePanel.Width - 80,
                Height = currentFeaturePanel.Height - 300,
                BackColor = Color.FromArgb(249, 250, 251),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
            };

            contentPanel.Paint += (s, e) =>
            {
                var rect = new Rectangle(0, 0, contentPanel.Width - 1, contentPanel.Height - 1);
                using (var pen = new Pen(Color.FromArgb(209, 213, 219), 1))
                {
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };

            var contentLabel = new Label()
            {
                Text = GetFeatureContentText(featureIndex),
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = false,
                Size = new Size(contentPanel.Width - 40, contentPanel.Height - 40),
                Location = new Point(20, 20),
                TextAlign = ContentAlignment.TopLeft,
            };

            contentPanel.Controls.Add(contentLabel);
            currentFeaturePanel.Controls.Add(contentPanel);
        }

        private void HandleFeatureAction(int featureIndex)
        {
            string message = "";
            switch (featureIndex)
            {
                case 0: // Tambah Barang
                    message = "Form untuk menambah barang baru akan ditampilkan di sini.\n\nFitur yang akan tersedia:\n- Input nama barang\n- Pilih kategori\n- Set harga\n- Tentukan stok awal";
                    break;
                case 1: // Edit Barang
                    message = "Form untuk mengedit barang akan ditampilkan di sini.";
                    break;
                case 2: // Hapus Barang
                    message = "Dialog konfirmasi penghapusan barang akan ditampilkan di sini.";
                    break;
                case 3: // Cari Barang
                    message = "Form pencarian barang akan ditampilkan di sini.";
                    break;
                default:
                    message = $"Fungsi {featureNames[featureIndex]} akan diimplementasikan di sini.";
                    break;
            }

            MessageBox.Show(message, $"Info - {featureNames[featureIndex]}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetFeatureDescription(int index)
        {
            string[] descriptions = {
                "Menambahkan barang baru ke dalam sistem inventaris. Anda dapat memasukkan informasi lengkap seperti nama, kategori, harga, dan stok awal.",
                "Mengubah informasi barang yang sudah ada dalam sistem. Fitur ini memungkinkan Anda memperbarui data barang dengan mudah.",
                "Menghapus barang dari sistem inventaris. Pastikan barang tidak memiliki stok tersisa sebelum dihapus.",
                "Mencari barang berdasarkan nama, kategori, atau kode barang. Fitur pencarian yang cepat dan akurat.",
                "Menampilkan daftar lengkap semua barang dalam inventaris dengan informasi detail dan status stok.",
                "Mencatat transaksi barang masuk untuk menambah stok. Fitur ini penting untuk tracking barang yang baru datang.",
                "Mencatat transaksi barang keluar untuk mengurangi stok. Berguna untuk tracking penjualan atau penggunaan barang.",
                "Melihat riwayat semua transaksi yang pernah dilakukan, baik transaksi masuk maupun keluar.",
                "Membuat laporan inventaris yang lengkap untuk analisis bisnis dan pelaporan manajemen."
            };

            return index >= 0 && index < descriptions.Length ? descriptions[index] : "Deskripsi fitur tidak tersedia.";
        }

        private string GetFeatureContentText(int index)
        {
            string[] contentTexts = {
                "Area ini akan berisi form untuk menambah barang baru:\n\n• Field nama barang (wajib diisi)\n• Dropdown kategori barang\n• Input harga satuan\n• Input stok awal\n• Field deskripsi barang\n• Tombol simpan dan batal\n\nSemua data akan disimpan ke database sistem inventaris.",
                "Area ini akan berisi form untuk mengedit data barang:\n\n• Pencarian barang yang akan diedit\n• Form edit dengan data yang sudah terisi\n• Validasi perubahan data\n• Konfirmasi penyimpanan\n\nRiwayat perubahan akan dicatat untuk audit trail.",
                "Area ini akan berisi konfirmasi penghapusan barang:\n\n• Informasi barang yang akan dihapus\n• Peringatan jika masih ada stok\n• Konfirmasi keamanan\n• Log penghapusan\n\nBarang yang sudah dihapus tidak dapat dikembalikan.",
                "Area ini akan berisi form pencarian barang:\n\n• Input kata kunci pencarian\n• Filter berdasarkan kategori\n• Filter berdasarkan status stok\n• Hasil pencarian dalam tabel\n• Export hasil pencarian\n\nPencarian mendukung multiple keyword.",
                "Area ini akan menampilkan tabel semua barang:\n\n• Daftar lengkap inventaris\n• Sorting berdasarkan kolom\n• Pagination untuk data besar\n• Status stok real-time\n• Quick action buttons\n\nData dapat di-export ke Excel atau PDF.",
                "Area ini akan berisi form transaksi barang masuk:\n\n• Pilih barang yang masuk\n• Input jumlah masuk\n• Tanggal transaksi\n• Keterangan/supplier\n• Update stok otomatis\n\nSetiap transaksi akan tercatat dalam riwayat.",
                "Area ini akan berisi form transaksi barang keluar:\n\n• Pilih barang yang keluar\n• Input jumlah keluar\n• Validasi stok tersedia\n• Tujuan/customer\n• Update stok otomatis\n\nSistem akan warning jika stok tidak mencukupi.",
                "Area ini akan menampilkan riwayat transaksi:\n\n• Tabel semua transaksi\n• Filter berdasarkan periode\n• Filter berdasarkan jenis transaksi\n• Detail setiap transaksi\n• Export ke berbagai format\n\nData dapat dianalisis untuk trend bisnis.",
                "Area ini akan berisi generator laporan:\n\n• Pilih jenis laporan\n• Set periode laporan\n• Filter berdasarkan kategori\n• Preview sebelum export\n• Multiple format output\n\nLaporan dapat dijadwalkan otomatis."
            };

            return index >= 0 && index < contentTexts.Length ? contentTexts[index] : "Konten fitur tidak tersedia.";
        }

        private string GetPrimaryButtonText(int index)
        {
            string[] buttonTexts = {
                "Tambah Barang",
                "Edit Barang",
                "Hapus Barang",
                "Mulai Cari",
                "Tampilkan Semua",
                "Catat Masuk",
                "Catat Keluar",
                "Lihat Riwayat",
                "Buat Laporan"
            };

            return index >= 0 && index < buttonTexts.Length ? buttonTexts[index] : "Mulai";
        }
    }
}