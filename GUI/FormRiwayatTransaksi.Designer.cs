namespace GUI
{
    partial class FormRiwayatTransaksi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonMuatRiwayat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(50, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(700, 250);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(270, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Riwayat Transaksi";
            // 
            // buttonMuatRiwayat
            // 
            this.buttonMuatRiwayat.Location = new System.Drawing.Point(340, 350);
            this.buttonMuatRiwayat.Name = "buttonMuatRiwayat";
            this.buttonMuatRiwayat.Size = new System.Drawing.Size(120, 30);
            this.buttonMuatRiwayat.TabIndex = 2;
            this.buttonMuatRiwayat.Text = "Muat Riwayat";
            this.buttonMuatRiwayat.UseVisualStyleBackColor = true;
            this.buttonMuatRiwayat.Click += new System.EventHandler(this.buttonMuatRiwayat_Click);
            // 
            // FormRiwayatTransaksi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 420);
            this.Controls.Add(this.buttonMuatRiwayat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormRiwayatTransaksi";
            this.Text = "Form Riwayat Transaksi";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonMuatRiwayat;
    }
}
