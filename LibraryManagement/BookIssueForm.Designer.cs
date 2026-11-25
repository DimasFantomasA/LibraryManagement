namespace LibraryManagement
{
    partial class BookIssueForm
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
            comboBoxBook = new ComboBox();
            comboBoxClient = new ComboBox();
            dateTimePickerIssue = new DateTimePicker();
            dateTimePickerDue = new DateTimePicker();
            buttonSave = new Button();
            buttonCancel = new Button();
            labelBook = new Label();
            labelClient = new Label();
            labelIssue = new Label();
            labelDue = new Label();
            SuspendLayout();
            // 
            // comboBoxBook
            // 
            comboBoxBook.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBook.FormattingEnabled = true;
            comboBoxBook.Location = new Point(28, 37);
            comboBoxBook.Name = "comboBoxBook";
            comboBoxBook.Size = new Size(300, 33);
            comboBoxBook.TabIndex = 0;
            comboBoxBook.SelectedIndexChanged += comboBoxBook_SelectedIndexChanged;
            // 
            // comboBoxClient
            // 
            comboBoxClient.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxClient.FormattingEnabled = true;
            comboBoxClient.Location = new Point(28, 115);
            comboBoxClient.Name = "comboBoxClient";
            comboBoxClient.Size = new Size(300, 33);
            comboBoxClient.TabIndex = 1;
            comboBoxClient.SelectedIndexChanged += comboBoxClient_SelectedIndexChanged;
            // 
            // dateTimePickerIssue
            // 
            dateTimePickerIssue.Location = new Point(28, 190);
            dateTimePickerIssue.Name = "dateTimePickerIssue";
            dateTimePickerIssue.Size = new Size(300, 31);
            dateTimePickerIssue.TabIndex = 2;
            dateTimePickerIssue.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // dateTimePickerDue
            // 
            dateTimePickerDue.Enabled = false;
            dateTimePickerDue.Location = new Point(28, 263);
            dateTimePickerDue.Name = "dateTimePickerDue";
            dateTimePickerDue.Size = new Size(300, 31);
            dateTimePickerDue.TabIndex = 3;
            dateTimePickerDue.ValueChanged += dateTimePickerDue_ValueChanged;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(28, 338);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(112, 34);
            buttonSave.TabIndex = 4;
            buttonSave.Text = "Выдать";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(216, 338);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(112, 34);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // labelBook
            // 
            labelBook.AutoSize = true;
            labelBook.Location = new Point(28, 9);
            labelBook.Name = "labelBook";
            labelBook.Size = new Size(118, 25);
            labelBook.TabIndex = 6;
            labelBook.Text = "Выбор книги";
            // 
            // labelClient
            // 
            labelClient.AutoSize = true;
            labelClient.Location = new Point(28, 87);
            labelClient.Name = "labelClient";
            labelClient.Size = new Size(142, 25);
            labelClient.TabIndex = 7;
            labelClient.Text = "Выбор читателя";
            // 
            // labelIssue
            // 
            labelIssue.AutoSize = true;
            labelIssue.Location = new Point(28, 162);
            labelIssue.Name = "labelIssue";
            labelIssue.Size = new Size(116, 25);
            labelIssue.TabIndex = 8;
            labelIssue.Text = "Дата выдачи";
            // 
            // labelDue
            // 
            labelDue.AutoSize = true;
            labelDue.Location = new Point(28, 235);
            labelDue.Name = "labelDue";
            labelDue.Size = new Size(103, 25);
            labelDue.TabIndex = 9;
            labelDue.Text = "Вернуть до";
            // 
            // BookIssueForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 450);
            Controls.Add(labelDue);
            Controls.Add(labelIssue);
            Controls.Add(labelClient);
            Controls.Add(labelBook);
            Controls.Add(buttonCancel);
            Controls.Add(buttonSave);
            Controls.Add(dateTimePickerDue);
            Controls.Add(dateTimePickerIssue);
            Controls.Add(comboBoxClient);
            Controls.Add(comboBoxBook);
            Name = "BookIssueForm";
            Text = "BookIssueForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxBook;
        private ComboBox comboBoxClient;
        private DateTimePicker dateTimePickerIssue;
        private DateTimePicker dateTimePickerDue;
        private Button buttonSave;
        private Button buttonCancel;
        private Label labelBook;
        private Label labelClient;
        private Label labelIssue;
        private Label labelDue;
    }
}