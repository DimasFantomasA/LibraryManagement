namespace LibraryManagement
{
    partial class DonationsForm
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
            dataGridViewDonations = new DataGridView();
            panel1 = new Panel();
            buttonAdd = new Button();
            buttonDelete = new Button();
            buttonClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDonations).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewDonations
            // 
            dataGridViewDonations.AllowUserToAddRows = false;
            dataGridViewDonations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDonations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDonations.Dock = DockStyle.Fill;
            dataGridViewDonations.Location = new Point(0, 0);
            dataGridViewDonations.Name = "dataGridViewDonations";
            dataGridViewDonations.ReadOnly = true;
            dataGridViewDonations.RowHeadersWidth = 62;
            dataGridViewDonations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDonations.Size = new Size(828, 394);
            dataGridViewDonations.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonClose);
            panel1.Controls.Add(buttonDelete);
            panel1.Controls.Add(buttonAdd);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 334);
            panel1.Name = "panel1";
            panel1.Size = new Size(828, 60);
            panel1.TabIndex = 1;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(25, 3);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(246, 34);
            buttonAdd.TabIndex = 0;
            buttonAdd.Text = "Добавить пожертвование";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(294, 3);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(153, 34);
            buttonDelete.TabIndex = 1;
            buttonDelete.Text = "Удалить запись";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonClose
            // 
            buttonClose.Location = new Point(667, 3);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(112, 34);
            buttonClose.TabIndex = 2;
            buttonClose.Text = "Закрыть";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // DonationsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(828, 394);
            Controls.Add(panel1);
            Controls.Add(dataGridViewDonations);
            Name = "DonationsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Журнал пожертвований книг";
            ((System.ComponentModel.ISupportInitialize)dataGridViewDonations).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewDonations;
        private Panel panel1;
        private Button buttonClose;
        private Button buttonDelete;
        private Button buttonAdd;
    }
}