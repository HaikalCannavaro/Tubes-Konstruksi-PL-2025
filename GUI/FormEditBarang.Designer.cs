namespace AplikasiInventarisToko.GUI
{
    partial class FormEditBarang
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
            dataGridViewBarang = new DataGridView();
            labelNamaBaru = new Label();
            labelKategoriBaru = new Label();
            labelStokBaru = new Label();
            labelHargaBeliBaru = new Label();
            labelHargaJualBaru = new Label();
            labelSupplierBaru = new Label();
            textBoxNamaBaru = new TextBox();
            textBoxKategoriBaru = new TextBox();
            textBoxStokBaru = new TextBox();
            textBoxHargaBeliBaru = new TextBox();
            textBoxHargaJualBaru = new TextBox();
            textBoxSupplierBaru = new TextBox();
            btnSimpan = new Button();
            btnBatal = new Button();
            labelKriteria = new Label();
            comboBoxKriteria = new ComboBox();
            labelNilaiPencarian = new Label();
            textBoxNilaiPencarian = new TextBox();
            btnCari = new Button();
            btnReset = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBarang).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewBarang
            // 
            dataGridViewBarang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewBarang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBarang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBarang.Location = new Point(400, 150);
            dataGridViewBarang.Name = "dataGridViewBarang";
            dataGridViewBarang.RowHeadersWidth = 51;
            dataGridViewBarang.Size = new Size(755, 485);
            dataGridViewBarang.TabIndex = 0;
            // 
            // labelNamaBaru
            // 
            labelNamaBaru.AutoSize = true;
            labelNamaBaru.Location = new Point(12, 185);
            labelNamaBaru.Name = "labelNamaBaru";
            labelNamaBaru.Size = new Size(92, 20);
            labelNamaBaru.TabIndex = 1;
            labelNamaBaru.Text = "Nama         : ";
            // 
            // labelKategoriBaru
            // 
            labelKategoriBaru.AutoSize = true;
            labelKategoriBaru.Location = new Point(12, 237);
            labelKategoriBaru.Name = "labelKategoriBaru";
            labelKategoriBaru.Size = new Size(93, 20);
            labelKategoriBaru.TabIndex = 2;
            labelKategoriBaru.Text = "Kategori     : ";
            // 
            // labelStokBaru
            // 
            labelStokBaru.AutoSize = true;
            labelStokBaru.Location = new Point(12, 283);
            labelStokBaru.Name = "labelStokBaru";
            labelStokBaru.Size = new Size(89, 20);
            labelStokBaru.TabIndex = 3;
            labelStokBaru.Text = "Stok            :";
            // 
            // labelHargaBeliBaru
            // 
            labelHargaBeliBaru.AutoSize = true;
            labelHargaBeliBaru.Location = new Point(12, 330);
            labelHargaBeliBaru.Name = "labelHargaBeliBaru";
            labelHargaBeliBaru.Size = new Size(94, 20);
            labelHargaBeliBaru.TabIndex = 4;
            labelHargaBeliBaru.Text = "Harga Beli  : ";
            // 
            // labelHargaJualBaru
            // 
            labelHargaJualBaru.AutoSize = true;
            labelHargaJualBaru.Location = new Point(12, 378);
            labelHargaJualBaru.Name = "labelHargaJualBaru";
            labelHargaJualBaru.Size = new Size(90, 20);
            labelHargaJualBaru.TabIndex = 5;
            labelHargaJualBaru.Text = "Harga Jual  :";
            // 
            // labelSupplierBaru
            // 
            labelSupplierBaru.AutoSize = true;
            labelSupplierBaru.Location = new Point(12, 425);
            labelSupplierBaru.Name = "labelSupplierBaru";
            labelSupplierBaru.Size = new Size(87, 20);
            labelSupplierBaru.TabIndex = 6;
            labelSupplierBaru.Text = "Supplier     :";
            // 
            // textBoxNamaBaru
            // 
            textBoxNamaBaru.Location = new Point(112, 178);
            textBoxNamaBaru.Name = "textBoxNamaBaru";
            textBoxNamaBaru.Size = new Size(278, 27);
            textBoxNamaBaru.TabIndex = 7;
            // 
            // textBoxKategoriBaru
            // 
            textBoxKategoriBaru.Location = new Point(110, 230);
            textBoxKategoriBaru.Name = "textBoxKategoriBaru";
            textBoxKategoriBaru.Size = new Size(278, 27);
            textBoxKategoriBaru.TabIndex = 8;
            // 
            // textBoxStokBaru
            // 
            textBoxStokBaru.Location = new Point(110, 276);
            textBoxStokBaru.Name = "textBoxStokBaru";
            textBoxStokBaru.Size = new Size(278, 27);
            textBoxStokBaru.TabIndex = 9;
            // 
            // textBoxHargaBeliBaru
            // 
            textBoxHargaBeliBaru.Location = new Point(112, 323);
            textBoxHargaBeliBaru.Name = "textBoxHargaBeliBaru";
            textBoxHargaBeliBaru.Size = new Size(278, 27);
            textBoxHargaBeliBaru.TabIndex = 10;
            // 
            // textBoxHargaJualBaru
            // 
            textBoxHargaJualBaru.Location = new Point(112, 371);
            textBoxHargaJualBaru.Name = "textBoxHargaJualBaru";
            textBoxHargaJualBaru.Size = new Size(278, 27);
            textBoxHargaJualBaru.TabIndex = 11;
            // 
            // textBoxSupplierBaru
            // 
            textBoxSupplierBaru.Location = new Point(112, 418);
            textBoxSupplierBaru.Name = "textBoxSupplierBaru";
            textBoxSupplierBaru.Size = new Size(278, 27);
            textBoxSupplierBaru.TabIndex = 12;
            // 
            // btnSimpan
            // 
            btnSimpan.BackColor = Color.Lime;
            btnSimpan.Location = new Point(110, 491);
            btnSimpan.Name = "btnSimpan";
            btnSimpan.Size = new Size(126, 52);
            btnSimpan.TabIndex = 14;
            btnSimpan.Text = "Edit";
            btnSimpan.UseVisualStyleBackColor = false;
            // 
            // btnBatal
            // 
            btnBatal.BackColor = Color.Red;
            btnBatal.Location = new Point(264, 491);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new Size(126, 52);
            btnBatal.TabIndex = 15;
            btnBatal.Text = "Tutup";
            btnBatal.UseVisualStyleBackColor = false;
            // 
            // labelKriteria
            // 
            labelKriteria.AutoSize = true;
            labelKriteria.Location = new Point(504, 30);
            labelKriteria.Name = "labelKriteria";
            labelKriteria.Size = new Size(135, 20);
            labelKriteria.TabIndex = 16;
            labelKriteria.Text = "Kriteria Pencarian : ";
            // 
            // comboBoxKriteria
            // 
            comboBoxKriteria.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxKriteria.FormattingEnabled = true;
            comboBoxKriteria.Items.AddRange(new object[] { "ID", "Nama", "Kategori", "Supplier" });
            comboBoxKriteria.Location = new Point(645, 22);
            comboBoxKriteria.Name = "comboBoxKriteria";
            comboBoxKriteria.Size = new Size(254, 28);
            comboBoxKriteria.TabIndex = 17;
            // 
            // labelNilaiPencarian
            // 
            labelNilaiPencarian.AutoSize = true;
            labelNilaiPencarian.Location = new Point(504, 74);
            labelNilaiPencarian.Name = "labelNilaiPencarian";
            labelNilaiPencarian.Size = new Size(130, 20);
            labelNilaiPencarian.TabIndex = 18;
            labelNilaiPencarian.Text = "Nilai Pencarian    : ";
            // 
            // textBoxNilaiPencarian
            // 
            textBoxNilaiPencarian.BackColor = SystemColors.Window;
            textBoxNilaiPencarian.Location = new Point(645, 67);
            textBoxNilaiPencarian.Name = "textBoxNilaiPencarian";
            textBoxNilaiPencarian.Size = new Size(254, 27);
            textBoxNilaiPencarian.TabIndex = 19;
            // 
            // btnCari
            // 
            btnCari.BackColor = Color.FromArgb(23, 162, 184);
            btnCari.ForeColor = Color.White;
            btnCari.Location = new Point(677, 109);
            btnCari.Name = "btnCari";
            btnCari.Size = new Size(100, 35);
            btnCari.TabIndex = 20;
            btnCari.Text = "🔍 Cari";
            btnCari.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(255, 193, 7);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(799, 109);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 35);
            btnReset.TabIndex = 21;
            btnReset.Text = "🔄 Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(110, 50);
            label1.Name = "label1";
            label1.Size = new Size(190, 46);
            label1.TabIndex = 22;
            label1.Text = "Edit Barang";
            // 
            // FormEditBarang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 655);
            Controls.Add(label1);
            Controls.Add(btnReset);
            Controls.Add(btnCari);
            Controls.Add(textBoxNilaiPencarian);
            Controls.Add(labelNilaiPencarian);
            Controls.Add(comboBoxKriteria);
            Controls.Add(labelKriteria);
            Controls.Add(btnBatal);
            Controls.Add(btnSimpan);
            Controls.Add(textBoxSupplierBaru);
            Controls.Add(textBoxHargaJualBaru);
            Controls.Add(textBoxHargaBeliBaru);
            Controls.Add(textBoxStokBaru);
            Controls.Add(textBoxKategoriBaru);
            Controls.Add(textBoxNamaBaru);
            Controls.Add(labelSupplierBaru);
            Controls.Add(labelHargaJualBaru);
            Controls.Add(labelHargaBeliBaru);
            Controls.Add(labelStokBaru);
            Controls.Add(labelKategoriBaru);
            Controls.Add(labelNamaBaru);
            Controls.Add(dataGridViewBarang);
            Name = "FormEditBarang";
            Text = "FormEditBarang";
            Load += FormEditBarang_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewBarang).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private DataGridView dataGridViewBarang;
        private Label labelNamaBaru;
        private Label labelKategoriBaru;
        private Label labelStokBaru;
        private Label labelHargaBeliBaru;
        private Label labelHargaJualBaru;
        private Label labelSupplierBaru;
        private TextBox textBoxNamaBaru;
        private TextBox textBoxKategoriBaru;
        private TextBox textBoxStokBaru;
        private TextBox textBoxHargaBeliBaru;
        private TextBox textBoxHargaJualBaru;
        private TextBox textBoxSupplierBaru;
        private Button btnSimpan;
        private Button btnBatal;
        private Label labelKriteria;
        private ComboBox comboBoxKriteria;
        private Label labelNilaiPencarian;
        private TextBox textBoxNilaiPencarian;
        private Button btnCari;
        private Button btnReset;
        private Label label1;
    }
}