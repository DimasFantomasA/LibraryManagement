namespace LibraryManagement
{
    partial class JournalForm
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
            dataGridViewJournal = new DataGridView();
            panel1 = new Panel();
            buttonClose = new Button();
            buttonDelete = new Button();
            buttonReturn = new Button();
            buttonIssue = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewJournal).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewJournal
            // 
            dataGridViewJournal.AllowUserToAddRows = false;
            dataGridViewJournal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewJournal.Dock = DockStyle.Fill;
            dataGridViewJournal.Location = new Point(0, 0);
            dataGridViewJournal.Name = "dataGridViewJournal";
            dataGridViewJournal.ReadOnly = true;
            dataGridViewJournal.RowHeadersWidth = 62;
            dataGridViewJournal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewJournal.Size = new Size(800, 450);
            dataGridViewJournal.TabIndex = 0;
            dataGridViewJournal.CellContentClick += dataGridViewJournal_CellContentClick;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonClose);
            panel1.Controls.Add(buttonDelete);
            panel1.Controls.Add(buttonReturn);
            panel1.Controls.Add(buttonIssue);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 390);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 60);
            panel1.TabIndex = 1;
            // 
            // buttonClose
            // 
            buttonClose.Location = new Point(666, 3);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(112, 34);
            buttonClose.TabIndex = 3;
            buttonClose.Text = "Закрыть";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(316, 3);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(144, 34);
            buttonDelete.TabIndex = 2;
            buttonDelete.Text = "Удалить запись";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonReturn
            // 
            buttonReturn.Location = new Point(167, 3);
            buttonReturn.Name = "buttonReturn";
            buttonReturn.Size = new Size(143, 34);
            buttonReturn.TabIndex = 1;
            buttonReturn.Text = "Вернуть книгу";
            buttonReturn.UseVisualStyleBackColor = true;
            buttonReturn.Click += buttonReturn_Click;
            // 
            // buttonIssue
            // 
            buttonIssue.Location = new Point(22, 1);
            buttonIssue.Name = "buttonIssue";
            buttonIssue.Size = new Size(139, 34);
            buttonIssue.TabIndex = 0;
            buttonIssue.Text = "Выдать книгу";
            buttonIssue.UseVisualStyleBackColor = true;
            buttonIssue.Click += buttonIssue_Click;
            // 
            // JournalForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(dataGridViewJournal);
            Name = "JournalForm";
            Text = "Журнал выдачи и возврата книг";
            ((System.ComponentModel.ISupportInitialize)dataGridViewJournal).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewJournal;
        private Panel panel1;
        private Button buttonClose;
        private Button buttonDelete;
        private Button buttonReturn;
        private Button buttonIssue;
    }
}