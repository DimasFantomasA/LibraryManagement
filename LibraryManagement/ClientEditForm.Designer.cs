namespace LibraryManagement
{
    partial class ClientEditForm
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
            textBoxFirstName = new TextBox();
            textBoxLastName = new TextBox();
            textBoxFatherName = new TextBox();
            textBoxPassportSeries = new TextBox();
            textBoxPassportNumber = new TextBox();
            labelName = new Label();
            labelLastName = new Label();
            labelFatherName = new Label();
            labelPassportSeries = new Label();
            labelPassportNumber = new Label();
            buttonSave = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // textBoxFirstName
            // 
            textBoxFirstName.Location = new Point(12, 37);
            textBoxFirstName.Name = "textBoxFirstName";
            textBoxFirstName.Size = new Size(271, 31);
            textBoxFirstName.TabIndex = 0;
            textBoxFirstName.TextChanged += textBoxFirstName_TextChanged;
            // 
            // textBoxLastName
            // 
            textBoxLastName.Location = new Point(12, 116);
            textBoxLastName.Name = "textBoxLastName";
            textBoxLastName.Size = new Size(271, 31);
            textBoxLastName.TabIndex = 1;
            textBoxLastName.TextChanged += textBoxLastName_TextChanged;
            // 
            // textBoxFatherName
            // 
            textBoxFatherName.Location = new Point(12, 189);
            textBoxFatherName.Name = "textBoxFatherName";
            textBoxFatherName.Size = new Size(271, 31);
            textBoxFatherName.TabIndex = 2;
            textBoxFatherName.TextChanged += textBoxFatherName_TextChanged;
            // 
            // textBoxPassportSeries
            // 
            textBoxPassportSeries.Location = new Point(12, 264);
            textBoxPassportSeries.MaxLength = 4;
            textBoxPassportSeries.Name = "textBoxPassportSeries";
            textBoxPassportSeries.Size = new Size(271, 31);
            textBoxPassportSeries.TabIndex = 3;
            textBoxPassportSeries.TextChanged += textBoxPassportSeries_TextChanged;
            // 
            // textBoxPassportNumber
            // 
            textBoxPassportNumber.Location = new Point(12, 341);
            textBoxPassportNumber.MaxLength = 6;
            textBoxPassportNumber.Name = "textBoxPassportNumber";
            textBoxPassportNumber.Size = new Size(271, 31);
            textBoxPassportNumber.TabIndex = 4;
            textBoxPassportNumber.TextChanged += textBoxPassportNumber_TextChanged;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(12, 9);
            labelName.Name = "labelName";
            labelName.Size = new Size(47, 25);
            labelName.TabIndex = 5;
            labelName.Text = "Имя";
            // 
            // labelLastName
            // 
            labelLastName.AutoSize = true;
            labelLastName.Location = new Point(12, 88);
            labelLastName.Name = "labelLastName";
            labelLastName.Size = new Size(85, 25);
            labelLastName.TabIndex = 6;
            labelLastName.Text = "Фамилия";
            // 
            // labelFatherName
            // 
            labelFatherName.AutoSize = true;
            labelFatherName.Location = new Point(12, 161);
            labelFatherName.Name = "labelFatherName";
            labelFatherName.Size = new Size(88, 25);
            labelFatherName.TabIndex = 7;
            labelFatherName.Text = "Отчество";
            // 
            // labelPassportSeries
            // 
            labelPassportSeries.AutoSize = true;
            labelPassportSeries.Location = new Point(12, 236);
            labelPassportSeries.Name = "labelPassportSeries";
            labelPassportSeries.Size = new Size(142, 25);
            labelPassportSeries.TabIndex = 8;
            labelPassportSeries.Text = "Серия паспорта";
            // 
            // labelPassportNumber
            // 
            labelPassportNumber.AutoSize = true;
            labelPassportNumber.Location = new Point(12, 313);
            labelPassportNumber.Name = "labelPassportNumber";
            labelPassportNumber.Size = new Size(149, 25);
            labelPassportNumber.TabIndex = 9;
            labelPassportNumber.Text = "Номер паспорта";
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(12, 392);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(112, 34);
            buttonSave.TabIndex = 10;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(171, 392);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(112, 34);
            buttonCancel.TabIndex = 11;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // ClientEditForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(317, 450);
            Controls.Add(buttonCancel);
            Controls.Add(buttonSave);
            Controls.Add(labelPassportNumber);
            Controls.Add(labelPassportSeries);
            Controls.Add(labelFatherName);
            Controls.Add(labelLastName);
            Controls.Add(labelName);
            Controls.Add(textBoxPassportNumber);
            Controls.Add(textBoxPassportSeries);
            Controls.Add(textBoxFatherName);
            Controls.Add(textBoxLastName);
            Controls.Add(textBoxFirstName);
            Name = "ClientEditForm";
            Text = "ClientEditForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxFirstName;
        private TextBox textBoxLastName;
        private TextBox textBoxFatherName;
        private TextBox textBoxPassportSeries;
        private TextBox textBoxPassportNumber;
        private Label labelName;
        private Label labelLastName;
        private Label labelFatherName;
        private Label labelPassportSeries;
        private Label labelPassportNumber;
        private Button buttonSave;
        private Button buttonCancel;
    }
}