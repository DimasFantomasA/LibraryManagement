namespace LibraryManagement
{
    partial class FinesReportForm
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
            dataGridViewReport = new DataGridView();
            panel1 = new Panel();
            buttonClose = new Button();
            buttonExport = new Button();
            buttonGenerate = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewReport
            // 
            dataGridViewReport.AllowUserToAddRows = false;
            dataGridViewReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewReport.Dock = DockStyle.Fill;
            dataGridViewReport.Location = new Point(0, 0);
            dataGridViewReport.Name = "dataGridViewReport";
            dataGridViewReport.ReadOnly = true;
            dataGridViewReport.RowHeadersWidth = 62;
            dataGridViewReport.Size = new Size(800, 450);
            dataGridViewReport.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonClose);
            panel1.Controls.Add(buttonExport);
            panel1.Controls.Add(buttonGenerate);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 382);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 68);
            panel1.TabIndex = 0;
            // 
            // buttonClose
            // 
            buttonClose.Location = new Point(666, 8);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(112, 34);
            buttonClose.TabIndex = 2;
            buttonClose.Text = "Закрыть";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // buttonExport
            // 
            buttonExport.Location = new Point(228, 8);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(171, 34);
            buttonExport.TabIndex = 1;
            buttonExport.Text = "Экспорт в TXT";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += buttonExport_Click;
            // 
            // buttonGenerate
            // 
            buttonGenerate.Location = new Point(17, 8);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new Size(205, 34);
            buttonGenerate.TabIndex = 0;
            buttonGenerate.Text = "Сформировать отчет";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += buttonGenerate_Click;
            // 
            // FinesReportForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(dataGridViewReport);
            Name = "FinesReportForm";
            Text = "FinesReportForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewReport;
        private Panel panel1;
        private Button buttonClose;
        private Button buttonExport;
        private Button buttonGenerate;
    }
}