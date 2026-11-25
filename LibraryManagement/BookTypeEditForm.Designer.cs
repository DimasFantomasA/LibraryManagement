namespace LibraryManagement
{
    partial class BookTypeEditForm
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
            textBoxType = new TextBox();
            numericUpDownFine = new NumericUpDown();
            numericUpDownDays = new NumericUpDown();
            labelType = new Label();
            labelFine = new Label();
            labelDays = new Label();
            buttonSave = new Button();
            buttonCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFine).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDays).BeginInit();
            SuspendLayout();
            // 
            // textBoxType
            // 
            textBoxType.Location = new Point(36, 58);
            textBoxType.MaxLength = 20;
            textBoxType.Name = "textBoxType";
            textBoxType.Size = new Size(246, 31);
            textBoxType.TabIndex = 0;
            textBoxType.TextChanged += textBoxType_TextChanged;
            // 
            // numericUpDownFine
            // 
            numericUpDownFine.Location = new Point(36, 139);
            numericUpDownFine.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownFine.Name = "numericUpDownFine";
            numericUpDownFine.Size = new Size(246, 31);
            numericUpDownFine.TabIndex = 1;
            numericUpDownFine.ValueChanged += numericUpDownFine_ValueChanged;
            // 
            // numericUpDownDays
            // 
            numericUpDownDays.Location = new Point(36, 222);
            numericUpDownDays.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
            numericUpDownDays.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownDays.Name = "numericUpDownDays";
            numericUpDownDays.Size = new Size(246, 31);
            numericUpDownDays.TabIndex = 2;
            numericUpDownDays.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownDays.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.Location = new Point(36, 21);
            labelType.Name = "labelType";
            labelType.Size = new Size(182, 25);
            labelType.TabIndex = 3;
            labelType.Text = "Название типа книги";
            // 
            // labelFine
            // 
            labelFine.AutoSize = true;
            labelFine.Location = new Point(36, 101);
            labelFine.Name = "labelFine";
            labelFine.Size = new Size(133, 25);
            labelFine.TabIndex = 4;
            labelFine.Text = "Штраф за день";
            // 
            // labelDays
            // 
            labelDays.AutoSize = true;
            labelDays.Location = new Point(36, 183);
            labelDays.Name = "labelDays";
            labelDays.Size = new Size(121, 25);
            labelDays.TabIndex = 5;
            labelDays.Text = "Срок выдачи";
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(33, 316);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(112, 34);
            buttonSave.TabIndex = 6;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(170, 316);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(112, 34);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // BookTypeEditForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(318, 450);
            Controls.Add(buttonCancel);
            Controls.Add(buttonSave);
            Controls.Add(labelDays);
            Controls.Add(labelFine);
            Controls.Add(labelType);
            Controls.Add(numericUpDownDays);
            Controls.Add(numericUpDownFine);
            Controls.Add(textBoxType);
            Name = "BookTypeEditForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BookTypeEditForm";
            ((System.ComponentModel.ISupportInitialize)numericUpDownFine).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDays).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxType;
        private NumericUpDown numericUpDownFine;
        private NumericUpDown numericUpDownDays;
        private Label labelType;
        private Label labelFine;
        private Label labelDays;
        private Button buttonSave;
        private Button buttonCancel;
    }
}