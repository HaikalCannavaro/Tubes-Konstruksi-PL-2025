using System.Windows.Forms;

namespace AplikasiInventarisToko.GUI
{
    public partial class FormTambahBarang : Form
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
            labelNamaBarang = new Label();
            labelKategori = new Label();
            labelStokAwal = new Label();
            labelHargaBeli = new Label();
            labelHargaJual = new Label();
            labelSupplier = new Label();
            textBoxNamaBarang = new TextBox();
            textBoxKategori = new TextBox();
            textBoxStokAwal = new TextBox();
            textBoxHargaBeli = new TextBox();
            textBoxHargaJual = new TextBox();
            textBoxSupplier = new TextBox();
            btnSimpan = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // labelJudulForm
            // 
            labelJudulForm.Font = new Font("Segoe UI", 16F);
            labelJudulForm.Location = new Point(268, 9);
            labelJudulForm.Name = "labelJudulForm";
            labelJudulForm.Size = new Size(260, 44);
            labelJudulForm.TabIndex = 0;
            labelJudulForm.Text = "Tambah Barang Baru";
            // 
            // labelNamaBarang
            // 
            labelNamaBarang.AutoSize = true;
            labelNamaBarang.Location = new Point(35, 117);
            labelNamaBarang.Name = "labelNamaBarang";
            labelNamaBarang.Size = new Size(115, 20);
            labelNamaBarang.TabIndex = 1;
            labelNamaBarang.Text = "Nama Barang  : ";
            // 
            // labelKategori
            // 
            labelKategori.Location = new Point(35, 149);
            labelKategori.Name = "labelKategori";
            labelKategori.Size = new Size(111, 24);
            labelKategori.TabIndex = 2;
            labelKategori.Text = "Kategori          : ";
            // 
            // labelStokAwal
            // 
            labelStokAwal.Location = new Point(35, 185);
            labelStokAwal.Name = "labelStokAwal";
            labelStokAwal.Size = new Size(111, 20);
            labelStokAwal.TabIndex = 3;
            labelStokAwal.Text = "Stok Awal        : ";
            // 
            // labelHargaBeli
            // 
            labelHargaBeli.Location = new Point(35, 220);
            labelHargaBeli.Name = "labelHargaBeli";
            labelHargaBeli.Size = new Size(111, 20);
            labelHargaBeli.TabIndex = 4;
            labelHargaBeli.Text = "Harga Beli       :  ";
            // 
            // labelHargaJual
            // 
            labelHargaJual.Location = new Point(35, 257);
            labelHargaJual.Name = "labelHargaJual";
            labelHargaJual.Size = new Size(111, 20);
            labelHargaJual.TabIndex = 5;
            labelHargaJual.Text = "Harga Jual       : ";
            // 
            // labelSupplier
            // 
            labelSupplier.Location = new Point(35, 293);
            labelSupplier.Name = "labelSupplier";
            labelSupplier.Size = new Size(111, 20);
            labelSupplier.TabIndex = 6;
            labelSupplier.Text = "Supplier           : ";
            // 
            // textBoxNamaBarang
            // 
            textBoxNamaBarang.Location = new Point(156, 110);
            textBoxNamaBarang.Name = "textBoxNamaBarang";
            textBoxNamaBarang.Size = new Size(254, 27);
            textBoxNamaBarang.TabIndex = 7;
            // 
            // textBoxKategori
            // 
            textBoxKategori.Location = new Point(156, 146);
            textBoxKategori.Name = "textBoxKategori";
            textBoxKategori.Size = new Size(254, 27);
            textBoxKategori.TabIndex = 8;
            // 
            // textBoxStokAwal
            // 
            textBoxStokAwal.Location = new Point(156, 178);
            textBoxStokAwal.Name = "textBoxStokAwal";
            textBoxStokAwal.Size = new Size(254, 27);
            textBoxStokAwal.TabIndex = 9;
            // 
            // textBoxHargaBeli
            // 
            textBoxHargaBeli.Location = new Point(156, 213);
            textBoxHargaBeli.Name = "textBoxHargaBeli";
            textBoxHargaBeli.Size = new Size(254, 27);
            textBoxHargaBeli.TabIndex = 10;
            // 
            // textBoxHargaJual
            // 
            textBoxHargaJual.Location = new Point(156, 250);
            textBoxHargaJual.Name = "textBoxHargaJual";
            textBoxHargaJual.Size = new Size(254, 27);
            textBoxHargaJual.TabIndex = 11;
            // 
            // textBoxSupplier
            // 
            textBoxSupplier.Location = new Point(156, 286);
            textBoxSupplier.Name = "textBoxSupplier";
            textBoxSupplier.Size = new Size(254, 27);
            textBoxSupplier.TabIndex = 12;
            // 
            // btnSimpan
            // 
            btnSimpan.BackColor = Color.Lime;
            btnSimpan.Location = new Point(541, 126);
            btnSimpan.Name = "btnSimpan";
            btnSimpan.Size = new Size(126, 52);
            btnSimpan.TabIndex = 13;
            btnSimpan.Text = "Simpan";
            btnSimpan.UseVisualStyleBackColor = false;
            btnSimpan.Click += btnSimpan_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.Location = new Point(541, 220);
            button2.Name = "button2";
            button2.Size = new Size(126, 52);
            button2.TabIndex = 14;
            button2.Text = "Batal";
            button2.UseVisualStyleBackColor = false;
            button2.Click += btnBatal_Click;
            // 
            // FormTambahBarang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(btnSimpan);
            Controls.Add(textBoxSupplier);
            Controls.Add(textBoxHargaJual);
            Controls.Add(textBoxHargaBeli);
            Controls.Add(textBoxStokAwal);
            Controls.Add(textBoxKategori);
            Controls.Add(textBoxNamaBarang);
            Controls.Add(labelSupplier);
            Controls.Add(labelHargaJual);
            Controls.Add(labelHargaBeli);
            Controls.Add(labelStokAwal);
            Controls.Add(labelKategori);
            Controls.Add(labelNamaBarang);
            Controls.Add(labelJudulForm);
            Name = "FormTambahBarang";
            Text = "FormTambahBarang";
            Load += FormTambahBarang_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelJudulForm;
        private Label labelNamaBarang;
        private Label labelKategori;
        private Label labelStokAwal;
        private Label labelHargaBeli;
        private Label labelHargaJual;
        private Label labelSupplier;
        private TextBox textBoxNamaBarang;
        private TextBox textBoxKategori;
        private TextBox textBoxStokAwal;
        private TextBox textBoxHargaBeli;
        private TextBox textBoxHargaJual;
        private TextBox textBoxSupplier;
        private Button btnSimpan;
        private Button button2;
    }
}