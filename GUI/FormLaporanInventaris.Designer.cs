namespace GUI
{
    partial class FormLaporanInventaris
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvInventaris = new System.Windows.Forms.DataGridView();
            this.btnMuatData = new System.Windows.Forms.Button();
            this.btnExportCSV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventaris)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvInventaris
            // 
            this.dgvInventaris.AllowUserToAddRows = false;
            this.dgvInventaris.AllowUserToDeleteRows = false;
            this.dgvInventaris.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventaris.Location = new System.Drawing.Point(12, 70);
            this.dgvInventaris.Name = "dgvInventaris";
            this.dgvInventaris.ReadOnly = true;
            this.dgvInventaris.RowTemplate.Height = 24;
            this.dgvInventaris.Size = new System.Drawing.Size(760, 370);
            this.dgvInventaris.TabIndex = 0;
            // 
            // btnMuatData
            // 
            this.btnMuatData.Location = new System.Drawing.Point(12, 20);
            this.btnMuatData.Name = "btnMuatData";
            this.btnMuatData.Size = new System.Drawing.Size(120, 30);
            this.btnMuatData.TabIndex = 1;
            this.btnMuatData.Text = "Muat Data";
            this.btnMuatData.UseVisualStyleBackColor = true;
            this.btnMuatData.Click += new System.EventHandler(this.btnMuatData_Click);
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.Location = new System.Drawing.Point(150, 20);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(140, 30);
            this.btnExportCSV.TabIndex = 2;
            this.btnExportCSV.Text = "Export ke CSV";
            this.btnExportCSV.UseVisualStyleBackColor = true;
            this.btnExportCSV.Click += new System.EventHandler(this.btnExportCSV_Click);
            // 
            // FormLaporanInventaris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnExportCSV);
            this.Controls.Add(this.btnMuatData);
            this.Controls.Add(this.dgvInventaris);
            this.Name = "FormLaporanInventaris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Laporan Inventaris Barang";
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventaris)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInventaris;
        private System.Windows.Forms.Button btnMuatData;
        private System.Windows.Forms.Button btnExportCSV;
    }
}
