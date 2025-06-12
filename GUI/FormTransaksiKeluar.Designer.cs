namespace GUI
{
    partial class FormTransaksiKeluar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvTransaksiKeluar;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTutup;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblHasil;
        private System.Windows.Forms.Panel panelButton;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvTransaksiKeluar = new DataGridView();
            btnRefresh = new Button();
            btnTutup = new Button();
            lblHeader = new Label();
            lblHasil = new Label();
            panelButton = new Panel();
            lblJenisInput = new Label();
            cbJenisInput = new ComboBox();
            lblInput = new Label();
            txtInput = new TextBox();
            lblJumlah = new Label();
            numJumlah = new NumericUpDown();
            lblKeterangan = new Label();
            txtKeterangan = new TextBox();
            btnKurangiStok = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTransaksiKeluar).BeginInit();
            panelButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numJumlah).BeginInit();
            SuspendLayout();
            // 
            // dgvTransaksiKeluar
            // 
            dgvTransaksiKeluar.ColumnHeadersHeight = 29;
            dgvTransaksiKeluar.Location = new Point(34, 253);
            dgvTransaksiKeluar.Margin = new Padding(3, 4, 3, 4);
            dgvTransaksiKeluar.Name = "dgvTransaksiKeluar";
            dgvTransaksiKeluar.ReadOnly = true;
            dgvTransaksiKeluar.RowHeadersWidth = 51;
            dgvTransaksiKeluar.Size = new Size(891, 467);
            dgvTransaksiKeluar.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(40, 167, 69);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(23, 20);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(114, 40);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnTutup
            // 
            btnTutup.BackColor = Color.FromArgb(220, 53, 69);
            btnTutup.FlatStyle = FlatStyle.Flat;
            btnTutup.ForeColor = Color.White;
            btnTutup.Location = new Point(743, 20);
            btnTutup.Margin = new Padding(3, 4, 3, 4);
            btnTutup.Name = "btnTutup";
            btnTutup.Size = new Size(114, 40);
            btnTutup.TabIndex = 2;
            btnTutup.Text = "Tutup";
            btnTutup.UseVisualStyleBackColor = false;
            btnTutup.Click += btnTutup_Click;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(34, 40);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(354, 41);
            lblHeader.TabIndex = 3;
            lblHeader.Text = "Transaksi Keluar Barang";
            // 
            // lblHasil
            // 
            lblHasil.AutoSize = true;
            lblHasil.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHasil.Location = new Point(34, 213);
            lblHasil.Name = "lblHasil";
            lblHasil.Size = new Size(115, 28);
            lblHasil.TabIndex = 4;
            lblHasil.Text = "Hasil Data:";
            // 
            // panelButton
            // 
            panelButton.Controls.Add(btnRefresh);
            panelButton.Controls.Add(btnTutup);
            panelButton.Location = new Point(34, 107);
            panelButton.Margin = new Padding(3, 4, 3, 4);
            panelButton.Name = "panelButton";
            panelButton.Size = new Size(891, 80);
            panelButton.TabIndex = 5;
            // 
            // lblJenisInput
            // 
            lblJenisInput.Location = new Point(34, 740);
            lblJenisInput.Name = "lblJenisInput";
            lblJenisInput.Size = new Size(150, 30);
            lblJenisInput.TabIndex = 0;
            lblJenisInput.Text = "Pilih Jenis Input:";
            // 
            // cbJenisInput
            // 
            cbJenisInput.DropDownStyle = ComboBoxStyle.DropDownList;
            cbJenisInput.Items.AddRange(new object[] { "ID", "Nama" });
            cbJenisInput.Location = new Point(200, 740);
            cbJenisInput.Name = "cbJenisInput";
            cbJenisInput.Size = new Size(200, 28);
            cbJenisInput.TabIndex = 1;
            // 
            // lblInput
            // 
            lblInput.Location = new Point(34, 780);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(150, 30);
            lblInput.TabIndex = 2;
            lblInput.Text = "Masukkan ID/Nama:";
            // 
            // txtInput
            // 
            txtInput.Location = new Point(200, 780);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(300, 27);
            txtInput.TabIndex = 3;
            // 
            // lblJumlah
            // 
            lblJumlah.Location = new Point(34, 820);
            lblJumlah.Name = "lblJumlah";
            lblJumlah.Size = new Size(150, 30);
            lblJumlah.TabIndex = 4;
            lblJumlah.Text = "Jumlah Keluar:";
            // 
            // numJumlah
            // 
            numJumlah.Location = new Point(200, 820);
            numJumlah.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numJumlah.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numJumlah.Name = "numJumlah";
            numJumlah.Size = new Size(100, 27);
            numJumlah.TabIndex = 5;
            numJumlah.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblKeterangan
            // 
            lblKeterangan.Location = new Point(34, 860);
            lblKeterangan.Name = "lblKeterangan";
            lblKeterangan.Size = new Size(150, 30);
            lblKeterangan.TabIndex = 6;
            lblKeterangan.Text = "Keterangan:";
            // 
            // txtKeterangan
            // 
            txtKeterangan.Location = new Point(200, 860);
            txtKeterangan.Name = "txtKeterangan";
            txtKeterangan.Size = new Size(400, 27);
            txtKeterangan.TabIndex = 7;
            // 
            // btnKurangiStok
            // 
            btnKurangiStok.BackColor = Color.Orange;
            btnKurangiStok.ForeColor = Color.White;
            btnKurangiStok.Location = new Point(620, 860);
            btnKurangiStok.Name = "btnKurangiStok";
            btnKurangiStok.Size = new Size(150, 40);
            btnKurangiStok.TabIndex = 8;
            btnKurangiStok.Text = "Kurangi Stok";
            btnKurangiStok.UseVisualStyleBackColor = false;
            btnKurangiStok.Click += btnKurangiStok_Click;
            // 
            // FormTransaksiKeluar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 929);
            Controls.Add(lblJenisInput);
            Controls.Add(cbJenisInput);
            Controls.Add(lblInput);
            Controls.Add(txtInput);
            Controls.Add(lblJumlah);
            Controls.Add(numJumlah);
            Controls.Add(lblKeterangan);
            Controls.Add(txtKeterangan);
            Controls.Add(btnKurangiStok);
            Controls.Add(panelButton);
            Controls.Add(lblHasil);
            Controls.Add(lblHeader);
            Controls.Add(dgvTransaksiKeluar);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormTransaksiKeluar";
            Text = "Transaksi Masuk - InvenApp";
            Load += FormTransaksiKeluar_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTransaksiKeluar).EndInit();
            panelButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numJumlah).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Label lblJenisInput;
        private ComboBox cbJenisInput;
        private Label lblInput;
        private TextBox txtInput;
        private Label lblJumlah;
        private NumericUpDown numJumlah;
        private Label lblKeterangan;
        private TextBox txtKeterangan;
        private Button btnKurangiStok;
    }
}