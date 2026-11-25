namespace LibraryManagement
{
    partial class BookEditForm
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
            labelName = new Label();
            textBoxName = new TextBox();
            labelType = new Label();
            comboBoxType = new ComboBox();
            labelCount = new Label();
            numericUpDownCount = new NumericUpDown();
            buttonSave = new Button();
            buttonCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCount).BeginInit();
            SuspendLayout();
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(22, 9);
            labelName.Name = "labelName";
            labelName.Size = new Size(141, 25);
            labelName.TabIndex = 0;
            labelName.Text = "Название книги";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(22, 52);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(301, 31);
            textBoxName.TabIndex = 1;
            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.Location = new Point(19, 97);
            labelType.Name = "labelType";
            labelType.Size = new Size(92, 25);
            labelType.TabIndex = 2;
            labelType.Text = "Тип книги";
            // 
            // comboBoxType
            // 
            comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxType.FormattingEnabled = true;
            comboBoxType.Location = new Point(19, 125);
            comboBoxType.Name = "comboBoxType";
            comboBoxType.Size = new Size(304, 33);
            comboBoxType.TabIndex = 3;
            // 
            // labelCount
            // 
            labelCount.AutoSize = true;
            labelCount.Location = new Point(19, 172);
            labelCount.Name = "labelCount";
            labelCount.Size = new Size(107, 25);
            labelCount.TabIndex = 4;
            labelCount.Text = "Количество";
            // 
            // numericUpDownCount
            // 
            numericUpDownCount.Location = new Point(21, 200);
            numericUpDownCount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDownCount.Name = "numericUpDownCount";
            numericUpDownCount.Size = new Size(302, 31);
            numericUpDownCount.TabIndex = 5;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(22, 256);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(112, 34);
            buttonSave.TabIndex = 6;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(211, 256);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(112, 34);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // BookEditForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(385, 450);
            Controls.Add(buttonCancel);
            Controls.Add(buttonSave);
            Controls.Add(numericUpDownCount);
            Controls.Add(labelCount);
            Controls.Add(comboBoxType);
            Controls.Add(labelType);
            Controls.Add(textBoxName);
            Controls.Add(labelName);
            Name = "BookEditForm";
            Text = "BookEditForm";
            ((System.ComponentModel.ISupportInitialize)numericUpDownCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelName;
        private TextBox textBoxName;
        private Label labelType;
        private ComboBox comboBoxType;
        private Label labelCount;
        private NumericUpDown numericUpDownCount;
        private Button buttonSave;
        private Button buttonCancel;
    }
}