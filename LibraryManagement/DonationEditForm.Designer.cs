namespace LibraryManagement
{
    partial class DonationEditForm
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
            comboBoxClient = new ComboBox();
            comboBoxBook = new ComboBox();
            numericUpDownQuantity = new NumericUpDown();
            dateTimePickerDonation = new DateTimePicker();
            buttonSave = new Button();
            buttonCancel = new Button();
            labelName = new Label();
            labelBook = new Label();
            labelCount = new Label();
            labelDate = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownQuantity).BeginInit();
            SuspendLayout();
            // 
            // comboBoxClient
            // 
            comboBoxClient.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxClient.FormattingEnabled = true;
            comboBoxClient.Location = new Point(12, 52);
            comboBoxClient.Name = "comboBoxClient";
            comboBoxClient.Size = new Size(300, 33);
            comboBoxClient.TabIndex = 0;
            // 
            // comboBoxBook
            // 
            comboBoxBook.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBook.FormattingEnabled = true;
            comboBoxBook.Location = new Point(12, 139);
            comboBoxBook.Name = "comboBoxBook";
            comboBoxBook.Size = new Size(300, 33);
            comboBoxBook.TabIndex = 1;
            // 
            // numericUpDownQuantity
            // 
            numericUpDownQuantity.Location = new Point(12, 221);
            numericUpDownQuantity.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownQuantity.Name = "numericUpDownQuantity";
            numericUpDownQuantity.Size = new Size(300, 31);
            numericUpDownQuantity.TabIndex = 2;
            numericUpDownQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dateTimePickerDonation
            // 
            dateTimePickerDonation.Format = DateTimePickerFormat.Short;
            dateTimePickerDonation.Location = new Point(12, 305);
            dateTimePickerDonation.Name = "dateTimePickerDonation";
            dateTimePickerDonation.Size = new Size(300, 31);
            dateTimePickerDonation.TabIndex = 3;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(12, 364);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(112, 34);
            buttonSave.TabIndex = 4;
            buttonSave.Text = "Добавить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(200, 364);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(112, 34);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(12, 9);
            labelName.Name = "labelName";
            labelName.Size = new Size(88, 25);
            labelName.TabIndex = 6;
            labelName.Text = "Читатель:";
            // 
            // labelBook
            // 
            labelBook.AutoSize = true;
            labelBook.Location = new Point(12, 98);
            labelBook.Name = "labelBook";
            labelBook.Size = new Size(62, 25);
            labelBook.TabIndex = 7;
            labelBook.Text = "Книга:";
            // 
            // labelCount
            // 
            labelCount.AutoSize = true;
            labelCount.Location = new Point(12, 184);
            labelCount.Name = "labelCount";
            labelCount.Size = new Size(219, 25);
            labelCount.TabIndex = 8;
            labelCount.Text = "Количество экземпляров";
            // 
            // labelDate
            // 
            labelDate.AutoSize = true;
            labelDate.Location = new Point(12, 266);
            labelDate.Name = "labelDate";
            labelDate.Size = new Size(184, 25);
            labelDate.TabIndex = 9;
            labelDate.Text = "Дата пожертвования";
            // 
            // DonationEditForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(371, 471);
            Controls.Add(labelDate);
            Controls.Add(labelCount);
            Controls.Add(labelBook);
            Controls.Add(labelName);
            Controls.Add(buttonCancel);
            Controls.Add(buttonSave);
            Controls.Add(dateTimePickerDonation);
            Controls.Add(numericUpDownQuantity);
            Controls.Add(comboBoxBook);
            Controls.Add(comboBoxClient);
            Name = "DonationEditForm";
            Text = "DonationEditForm";
            ((System.ComponentModel.ISupportInitialize)numericUpDownQuantity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxClient;
        private ComboBox comboBoxBook;
        private NumericUpDown numericUpDownQuantity;
        private DateTimePicker dateTimePickerDonation;
        private Button buttonSave;
        private Button buttonCancel;
        private Label labelName;
        private Label labelBook;
        private Label labelCount;
        private Label labelDate;
    }
}