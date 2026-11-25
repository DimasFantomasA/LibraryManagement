namespace LibraryManagement
{
    partial class ClientsForm
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
            dataGridViewClients = new DataGridView();
            panelClients = new Panel();
            buttonClose = new Button();
            buttonDelete = new Button();
            buttonEdit = new Button();
            buttonAdd = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClients).BeginInit();
            panelClients.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewClients
            // 
            dataGridViewClients.AllowUserToAddRows = false;
            dataGridViewClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewClients.Dock = DockStyle.Fill;
            dataGridViewClients.Location = new Point(0, 0);
            dataGridViewClients.Name = "dataGridViewClients";
            dataGridViewClients.ReadOnly = true;
            dataGridViewClients.RowHeadersWidth = 62;
            dataGridViewClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewClients.Size = new Size(928, 494);
            dataGridViewClients.TabIndex = 0;
            // 
            // panelClients
            // 
            panelClients.Controls.Add(buttonClose);
            panelClients.Controls.Add(buttonDelete);
            panelClients.Controls.Add(buttonEdit);
            panelClients.Controls.Add(buttonAdd);
            panelClients.Dock = DockStyle.Bottom;
            panelClients.Location = new Point(0, 434);
            panelClients.Name = "panelClients";
            panelClients.Size = new Size(928, 60);
            panelClients.TabIndex = 1;
            // 
            // buttonClose
            // 
            buttonClose.Location = new Point(804, 3);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(112, 34);
            buttonClose.TabIndex = 3;
            buttonClose.Text = "Закрыть";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(276, 3);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(112, 34);
            buttonDelete.TabIndex = 2;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(136, 3);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(112, 34);
            buttonEdit.TabIndex = 1;
            buttonEdit.Text = "Изменить";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(3, 3);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(112, 34);
            buttonAdd.TabIndex = 0;
            buttonAdd.Text = "Добавить";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // ClientsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(928, 494);
            Controls.Add(panelClients);
            Controls.Add(dataGridViewClients);
            Name = "ClientsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Читатели библиотеки";
            ((System.ComponentModel.ISupportInitialize)dataGridViewClients).EndInit();
            panelClients.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewClients;
        private Panel panelClients;
        private Button buttonClose;
        private Button buttonDelete;
        private Button buttonEdit;
        private Button buttonAdd;
    }
}