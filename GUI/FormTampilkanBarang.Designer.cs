namespace GUI
{
    partial class FormTampilkanBarang
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
            buttonKembali = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBarang).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewBarang
            // 
            dataGridViewBarang.Anchor = AnchorStyles.Top;
            dataGridViewBarang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBarang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBarang.Location = new Point(97, 12);
            dataGridViewBarang.Name = "dataGridViewBarang";
            dataGridViewBarang.RowHeadersWidth = 61;
            dataGridViewBarang.Size = new Size(934, 472);
            dataGridViewBarang.TabIndex = 0;
            // 
            // buttonKembali
            // 
            buttonKembali.Anchor = AnchorStyles.Bottom;
            buttonKembali.AutoSize = true;
            buttonKembali.Font = new Font("Segoe UI", 16F);
            buttonKembali.Location = new Point(480, 561);
            buttonKembali.Name = "buttonKembali";
            buttonKembali.Size = new Size(182, 65);
            buttonKembali.TabIndex = 1;
            buttonKembali.Text = "Kembali";
            buttonKembali.UseVisualStyleBackColor = true;
            buttonKembali.Click += buttonKembali_Click;
            // 
            // FormTampilkanBarang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1141, 751);
            Controls.Add(buttonKembali);
            Controls.Add(dataGridViewBarang);
            Name = "FormTampilkanBarang";
            Text = "FormTampilkanBarang";
            ((System.ComponentModel.ISupportInitialize)dataGridViewBarang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewBarang;
        private Button buttonKembali;
    }
}