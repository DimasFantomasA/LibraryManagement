namespace LibraryManagement
{
    partial class BookReturnForm
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
            labelBook = new Label();
            labelClient = new Label();
            labelDateRet = new Label();
            dateTimePickerReturn = new DateTimePicker();
            buttonSave = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // labelBook
            // 
            labelBook.AutoSize = true;
            labelBook.Location = new Point(41, 25);
            labelBook.Name = "labelBook";
            labelBook.Size = new Size(62, 25);
            labelBook.TabIndex = 0;
            labelBook.Text = "Книга:";
            // 
            // labelClient
            // 
            labelClient.AutoSize = true;
            labelClient.Location = new Point(41, 112);
            labelClient.Name = "labelClient";
            labelClient.Size = new Size(88, 25);
            labelClient.TabIndex = 1;
            labelClient.Text = "Читатель:";
            // 
            // labelDateRet
            // 
            labelDateRet.AutoSize = true;
            labelDateRet.Location = new Point(41, 182);
            labelDateRet.Name = "labelDateRet";
            labelDateRet.Size = new Size(246, 25);
            labelDateRet.TabIndex = 2;
            labelDateRet.Text = "Дата фактического возврата";
            // 
            // dateTimePickerReturn
            // 
            dateTimePickerReturn.Location = new Point(41, 228);
            dateTimePickerReturn.Name = "dateTimePickerReturn";
            dateTimePickerReturn.Size = new Size(353, 31);
            dateTimePickerReturn.TabIndex = 3;
            dateTimePickerReturn.ValueChanged += dateTimePickerReturn_ValueChanged;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(41, 316);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(203, 34);
            buttonSave.TabIndex = 4;
            buttonSave.Text = "Подтвердить возврат";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(282, 316);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(112, 34);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // BookReturnForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 450);
            Controls.Add(buttonCancel);
            Controls.Add(buttonSave);
            Controls.Add(dateTimePickerReturn);
            Controls.Add(labelDateRet);
            Controls.Add(labelClient);
            Controls.Add(labelBook);
            Name = "BookReturnForm";
            Text = "BookReturnForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelBook;
        private Label labelClient;
        private Label labelDateRet;
        private DateTimePicker dateTimePickerReturn;
        private Button buttonSave;
        private Button buttonCancel;
    }
}