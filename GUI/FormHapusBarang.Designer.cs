using System.Windows.Forms;

namespace GUI
{
    public partial class FormHapusBarang : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            labelJudulForm = new Label();
            dataGridViewDaftarBarang = new DataGridView();
            labelDaftarBarang = new Label();
            labelPilihBarang = new Label();
            textBoxIdBarang = new TextBox();
            btnCari = new Button();
            btnBatal = new Button();
            panelDetailBarang = new Panel();
            labelDetailBarang = new Label();
            labelId = new Label();
            labelNama = new Label();
            labelKategori = new Label();
            labelStok = new Label();
            labelHargaBeli = new Label();
            labelHargaJual = new Label();
            labelSupplier = new Label();
            textBoxId = new TextBox();
            textBoxNama = new TextBox();
            textBoxKategori = new TextBox();
            textBoxStok = new TextBox();
            textBoxHargaBeli = new TextBox();
            textBoxHargaJual = new TextBox();
            textBoxSupplier = new TextBox();
            btnHapus = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDaftarBarang).BeginInit();
            panelDetailBarang.SuspendLayout();
            SuspendLayout();
            // 
            // labelJudulForm
            // 
            labelJudulForm.Font = new Font("Segoe UI", 16F);
            labelJudulForm.Location = new Point(300, 9);
            labelJudulForm.Name = "labelJudulForm";
            labelJudulForm.Size = new Size(260, 44);
            labelJudulForm.TabIndex = 0;
            labelJudulForm.Text = "Hapus Barang";
            labelJudulForm.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataGridViewDaftarBarang
            // 
            dataGridViewDaftarBarang.AllowUserToAddRows = false;
            dataGridViewDaftarBarang.AllowUserToDeleteRows = false;
            dataGridViewDaftarBarang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDaftarBarang.BackgroundColor = Color.White;
            dataGridViewDaftarBarang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDaftarBarang.Location = new Point(35, 80);
            dataGridViewDaftarBarang.Name = "dataGridViewDaftarBarang";
            dataGridViewDaftarBarang.ReadOnly = true;
            dataGridViewDaftarBarang.RowHeadersWidth = 51;
            dataGridViewDaftarBarang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDaftarBarang.Size = new Size(755, 200);
            dataGridViewDaftarBarang.TabIndex = 1;
            dataGridViewDaftarBarang.SelectionChanged += dataGridViewDaftarBarang_SelectionChanged;
            // 
            // labelDaftarBarang
            // 
            labelDaftarBarang.AutoSize = true;
            labelDaftarBarang.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelDaftarBarang.Location = new Point(35, 50);
            labelDaftarBarang.Name = "labelDaftarBarang";
            labelDaftarBarang.Size = new Size(146, 28);
            labelDaftarBarang.TabIndex = 2;
            labelDaftarBarang.Text = "Daftar Barang:";
            // 
            // labelPilihBarang
            // 
            labelPilihBarang.AutoSize = true;
            labelPilihBarang.Location = new Point(35, 300);
            labelPilihBarang.Name = "labelPilihBarang";
            labelPilihBarang.Size = new Size(100, 20);
            labelPilihBarang.TabIndex = 3;
            labelPilihBarang.Text = "ID Barang    : ";
            // 
            // textBoxIdBarang
            // 
            textBoxIdBarang.BackColor = SystemColors.Window;
            textBoxIdBarang.Location = new Point(140, 297);
            textBoxIdBarang.Name = "textBoxIdBarang";
            textBoxIdBarang.Size = new Size(254, 27);
            textBoxIdBarang.TabIndex = 4;
            // 
            // btnCari
            // 
            btnCari.BackColor = Color.FromArgb(23, 162, 184);
            btnCari.ForeColor = Color.White;
            btnCari.Location = new Point(420, 295);
            btnCari.Name = "btnCari";
            btnCari.Size = new Size(100, 35);
            btnCari.TabIndex = 5;
            btnCari.Text = "🔍 Cari";
            btnCari.UseVisualStyleBackColor = false;
            btnCari.Click += btnCari_Click;
            // 
            // btnBatal
            // 
            btnBatal.BackColor = Color.FromArgb(220, 53, 69);
            btnBatal.ForeColor = Color.White;
            btnBatal.Location = new Point(690, 500);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new Size(100, 35);
            btnBatal.TabIndex = 7;
            btnBatal.Text = "❌ Tutup";
            btnBatal.UseVisualStyleBackColor = false;
            btnBatal.Click += btnBatal_Click;
            // 
            // panelDetailBarang
            // 
            panelDetailBarang.BorderStyle = BorderStyle.FixedSingle;
            panelDetailBarang.Controls.Add(textBoxSupplier);
            panelDetailBarang.Controls.Add(textBoxHargaJual);
            panelDetailBarang.Controls.Add(textBoxHargaBeli);
            panelDetailBarang.Controls.Add(textBoxStok);
            panelDetailBarang.Controls.Add(textBoxKategori);
            panelDetailBarang.Controls.Add(textBoxNama);
            panelDetailBarang.Controls.Add(textBoxId);
            panelDetailBarang.Controls.Add(labelSupplier);
            panelDetailBarang.Controls.Add(labelHargaJual);
            panelDetailBarang.Controls.Add(labelHargaBeli);
            panelDetailBarang.Controls.Add(labelStok);
            panelDetailBarang.Controls.Add(labelKategori);
            panelDetailBarang.Controls.Add(labelNama);
            panelDetailBarang.Controls.Add(labelId);
            panelDetailBarang.Controls.Add(labelDetailBarang);
            panelDetailBarang.Location = new Point(35, 350);
            panelDetailBarang.Name = "panelDetailBarang";
            panelDetailBarang.Size = new Size(755, 150);
            panelDetailBarang.TabIndex = 8;
            // 
            // labelDetailBarang
            // 
            labelDetailBarang.AutoSize = true;
            labelDetailBarang.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelDetailBarang.Location = new Point(10, 10);
            labelDetailBarang.Name = "labelDetailBarang";
            labelDetailBarang.Size = new Size(118, 23);
            labelDetailBarang.TabIndex = 0;
            labelDetailBarang.Text = "Detail Barang";
            // 
            // labelId
            // 
            labelId.AutoSize = true;
            labelId.Location = new Point(15, 45);
            labelId.Name = "labelId";
            labelId.Size = new Size(27, 20);
            labelId.TabIndex = 1;
            labelId.Text = "ID:";
            // 
            // labelNama
            // 
            labelNama.AutoSize = true;
            labelNama.Location = new Point(15, 75);
            labelNama.Name = "labelNama";
            labelNama.Size = new Size(52, 20);
            labelNama.TabIndex = 2;
            labelNama.Text = "Nama:";
            // 
            // labelKategori
            // 
            labelKategori.AutoSize = true;
            labelKategori.Location = new Point(15, 105);
            labelKategori.Name = "labelKategori";
            labelKategori.Size = new Size(72, 20);
            labelKategori.TabIndex = 3;
            labelKategori.Text = "Kategori:";
            // 
            // labelStok
            // 
            labelStok.AutoSize = true;
            labelStok.Location = new Point(280, 45);
            labelStok.Name = "labelStok";
            labelStok.Size = new Size(42, 20);
            labelStok.TabIndex = 4;
            labelStok.Text = "Stok:";
            // 
            // labelHargaBeli
            // 
            labelHargaBeli.AutoSize = true;
            labelHargaBeli.Location = new Point(280, 75);
            labelHargaBeli.Name = "labelHargaBeli";
            labelHargaBeli.Size = new Size(83, 20);
            labelHargaBeli.TabIndex = 5;
            labelHargaBeli.Text = "Harga Beli:";
            // 
            // labelHargaJual
            // 
            labelHargaJual.AutoSize = true;
            labelHargaJual.Location = new Point(280, 105);
            labelHargaJual.Name = "labelHargaJual";
            labelHargaJual.Size = new Size(86, 20);
            labelHargaJual.TabIndex = 6;
            labelHargaJual.Text = "Harga Jual:";
            // 
            // labelSupplier
            // 
            labelSupplier.AutoSize = true;
            labelSupplier.Location = new Point(530, 45);
            labelSupplier.Name = "labelSupplier";
            labelSupplier.Size = new Size(69, 20);
            labelSupplier.TabIndex = 7;
            labelSupplier.Text = "Supplier:";
            // 
            // textBoxId
            // 
            textBoxId.BackColor = SystemColors.Control;
            textBoxId.BorderStyle = BorderStyle.None;
            textBoxId.Location = new Point(100, 45);
            textBoxId.Name = "textBoxId";
            textBoxId.ReadOnly = true;
            textBoxId.Size = new Size(150, 20);
            textBoxId.TabIndex = 8;
            // 
            // textBoxNama
            // 
            textBoxNama.BackColor = SystemColors.Control;
            textBoxNama.BorderStyle = BorderStyle.None;
            textBoxNama.Location = new Point(100, 75);
            textBoxNama.Name = "textBoxNama";
            textBoxNama.ReadOnly = true;
            textBoxNama.Size = new Size(150, 20);
            textBoxNama.TabIndex = 9;
            // 
            // textBoxKategori
            // 
            textBoxKategori.BackColor = SystemColors.Control;
            textBoxKategori.BorderStyle = BorderStyle.None;
            textBoxKategori.Location = new Point(100, 105);
            textBoxKategori.Name = "textBoxKategori";
            textBoxKategori.ReadOnly = true;
            textBoxKategori.Size = new Size(150, 20);
            textBoxKategori.TabIndex = 10;
            // 
            // textBoxStok
            // 
            textBoxStok.BackColor = SystemColors.Control;
            textBoxStok.BorderStyle = BorderStyle.None;
            textBoxStok.Location = new Point(370, 45);
            textBoxStok.Name = "textBoxStok";
            textBoxStok.ReadOnly = true;
            textBoxStok.Size = new Size(150, 20);
            textBoxStok.TabIndex = 11;
            // 
            // textBoxHargaBeli
            // 
            textBoxHargaBeli.BackColor = SystemColors.Control;
            textBoxHargaBeli.BorderStyle = BorderStyle.None;
            textBoxHargaBeli.Location = new Point(370, 75);
            textBoxHargaBeli.Name = "textBoxHargaBeli";
            textBoxHargaBeli.ReadOnly = true;
            textBoxHargaBeli.Size = new Size(150, 20);
            textBoxHargaBeli.TabIndex = 12;
            // 
            // textBoxHargaJual
            // 
            textBoxHargaJual.BackColor = SystemColors.Control;
            textBoxHargaJual.BorderStyle = BorderStyle.None;
            textBoxHargaJual.Location = new Point(370, 105);
            textBoxHargaJual.Name = "textBoxHargaJual";
            textBoxHargaJual.ReadOnly = true;
            textBoxHargaJual.Size = new Size(150, 20);
            textBoxHargaJual.TabIndex = 13;
            // 
            // textBoxSupplier
            // 
            textBoxSupplier.BackColor = SystemColors.Control;
            textBoxSupplier.BorderStyle = BorderStyle.None;
            textBoxSupplier.Location = new Point(610, 45);
            textBoxSupplier.Name = "textBoxSupplier";
            textBoxSupplier.ReadOnly = true;
            textBoxSupplier.Size = new Size(130, 20);
            textBoxSupplier.TabIndex = 14;
            // 
            // btnHapus
            // 
            btnHapus.BackColor = Color.FromArgb(220, 53, 69);
            btnHapus.ForeColor = Color.White;
            btnHapus.Location = new Point(540, 295);
            btnHapus.Name = "btnHapus";
            btnHapus.Size = new Size(100, 35);
            btnHapus.TabIndex = 6;
            btnHapus.Text = "🗑️ Hapus";
            btnHapus.UseVisualStyleBackColor = false;
            btnHapus.Click += btnHapus_Click;
            // 
            // FormHapusBarang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 550);
            Controls.Add(btnHapus);
            Controls.Add(panelDetailBarang);
            Controls.Add(btnBatal);
            Controls.Add(btnCari);
            Controls.Add(textBoxIdBarang);
            Controls.Add(labelPilihBarang);
            Controls.Add(labelDaftarBarang);
            Controls.Add(dataGridViewDaftarBarang);
            Controls.Add(labelJudulForm);
            Name = "FormHapusBarang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hapus Barang - InvenApp";
            Load += FormHapusBarang_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewDaftarBarang).EndInit();
            panelDetailBarang.ResumeLayout(false);
            panelDetailBarang.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelJudulForm;
        private DataGridView dataGridViewDaftarBarang;
        private Label labelDaftarBarang;
        private Label labelPilihBarang;
        private TextBox textBoxIdBarang;
        private Button btnCari;
        private Button btnBatal;
        private Panel panelDetailBarang;
        private TextBox textBoxSupplier;
        private TextBox textBoxHargaJual;
        private TextBox textBoxHargaBeli;
        private TextBox textBoxStok;
        private TextBox textBoxKategori;
        private TextBox textBoxNama;
        private TextBox textBoxId;
        private Label labelSupplier;
        private Label labelHargaJual;
        private Label labelHargaBeli;
        private Label labelStok;
        private Label labelKategori;
        private Label labelNama;
        private Label labelId;
        private Label labelDetailBarang;
        private Button btnHapus;
    }
}