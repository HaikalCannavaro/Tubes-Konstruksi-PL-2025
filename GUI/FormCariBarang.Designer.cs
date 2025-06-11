using System.Windows.Forms;

namespace AplikasiInventarisToko.GUI
{
    public partial class FormCariBarang : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        
        protected void Dis(bool disposing)
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
            labelKriteria = new Label();
            labelNilaiPencarian = new Label();
            comboBoxKriteria = new ComboBox();
            textBoxNilaiPencarian = new TextBox();
            btnCari = new Button();
            btnBatal = new Button();
            dataGridViewHasil = new DataGridView();
            labelHasil = new Label();
            btnReset = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewHasil).BeginInit();
            SuspendLayout();
            // 
            // labelJudulForm
            // 
            labelJudulForm.Font = new Font("Segoe UI", 16F);
            labelJudulForm.Location = new Point(300, 9);
            labelJudulForm.Name = "labelJudulForm";
            labelJudulForm.Size = new Size(260, 44);
            labelJudulForm.TabIndex = 0;
            labelJudulForm.Text = "Cari Barang";
            labelJudulForm.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelKriteria
            // 
            labelKriteria.AutoSize = true;
            labelKriteria.Location = new Point(35, 80);
            labelKriteria.Name = "labelKriteria";
            labelKriteria.Size = new Size(135, 20);
            labelKriteria.TabIndex = 1;
            labelKriteria.Text = "Kriteria Pencarian : ";
            // 
            // labelNilaiPencarian
            // 
            labelNilaiPencarian.AutoSize = true;
            labelNilaiPencarian.Location = new Point(35, 120);
            labelNilaiPencarian.Name = "labelNilaiPencarian";
            labelNilaiPencarian.Size = new Size(130, 20);
            labelNilaiPencarian.TabIndex = 2;
            labelNilaiPencarian.Text = "Nilai Pencarian    : ";
            // 
            // comboBoxKriteria
            // 
            comboBoxKriteria.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxKriteria.FormattingEnabled = true;
            comboBoxKriteria.Items.AddRange(new object[] { "id", "nama", "kategori", "supplier" });
            comboBoxKriteria.Location = new Point(156, 77);
            comboBoxKriteria.Name = "comboBoxKriteria";
            comboBoxKriteria.Size = new Size(254, 28);
            comboBoxKriteria.TabIndex = 3;
            // 
            // textBoxNilaiPencarian
            // 
            textBoxNilaiPencarian.BackColor = SystemColors.Window;
            textBoxNilaiPencarian.Location = new Point(156, 117);
            textBoxNilaiPencarian.Name = "textBoxNilaiPencarian";
            textBoxNilaiPencarian.Size = new Size(254, 27);
            textBoxNilaiPencarian.TabIndex = 4;
            textBoxNilaiPencarian.KeyPress += textBoxNilaiPencarian_KeyPress;
            // 
            // btnCari
            // 
            btnCari.BackColor = Color.FromArgb(23, 162, 184);
            btnCari.ForeColor = Color.White;
            btnCari.Location = new Point(450, 77);
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
            btnBatal.Location = new Point(690, 77);
            btnBatal.Name = "btnBatal";
            btnBatal.Size = new Size(100, 35);
            btnBatal.TabIndex = 7;
            btnBatal.Text = "❌ Tutup";
            btnBatal.UseVisualStyleBackColor = false;
            btnBatal.Click += btnBatal_Click;
            // 
            // dataGridViewHasil
            // 
            dataGridViewHasil.AllowUserToAddRows = false;
            dataGridViewHasil.AllowUserToDeleteRows = false;
            dataGridViewHasil.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewHasil.BackgroundColor = Color.White;
            dataGridViewHasil.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewHasil.Location = new Point(35, 210);
            dataGridViewHasil.Name = "dataGridViewHasil";
            dataGridViewHasil.ReadOnly = true;
            dataGridViewHasil.RowHeadersWidth = 51;
            dataGridViewHasil.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewHasil.Size = new Size(755, 350);
            dataGridViewHasil.TabIndex = 9;
            // 
            // labelHasil
            // 
            labelHasil.AutoSize = true;
            labelHasil.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelHasil.Location = new Point(35, 170);
            labelHasil.Name = "labelHasil";
            labelHasil.Size = new Size(162, 28);
            labelHasil.TabIndex = 8;
            labelHasil.Text = "Hasil Pencarian:";
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(255, 193, 7);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(570, 77);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 35);
            btnReset.TabIndex = 6;
            btnReset.Text = "🔄 Reset";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // FormCariBarang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 580);
            Controls.Add(dataGridViewHasil);
            Controls.Add(labelHasil);
            Controls.Add(btnBatal);
            Controls.Add(btnReset);
            Controls.Add(btnCari);
            Controls.Add(textBoxNilaiPencarian);
            Controls.Add(comboBoxKriteria);
            Controls.Add(labelNilaiPencarian);
            Controls.Add(labelKriteria);
            Controls.Add(labelJudulForm);
            Name = "FormCariBarang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cari Barang - InvenApp";
            Load += FormCariBarang_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewHasil).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelJudulForm;
        private Label labelKriteria;
        private Label labelNilaiPencarian;
        private ComboBox comboBoxKriteria;
        private TextBox textBoxNilaiPencarian;
        private Button btnCari;
        private Button btnBatal;
        private DataGridView dataGridViewHasil;
        private Label labelHasil;
        private Button btnReset;
    }
}