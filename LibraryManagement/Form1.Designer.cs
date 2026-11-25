namespace LibraryManagement
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            справочникиToolStripMenuItem = new ToolStripMenuItem();
            книгиToolStripMenuItem = new ToolStripMenuItem();
            читателиToolStripMenuItem = new ToolStripMenuItem();
            типыКнигToolStripMenuItem = new ToolStripMenuItem();
            журналыToolStripMenuItem = new ToolStripMenuItem();
            выдачаToolStripMenuItem = new ToolStripMenuItem();
            пожертвованиеКнигToolStripMenuItem = new ToolStripMenuItem();
            отчетыToolStripMenuItem = new ToolStripMenuItem();
            самыеПопулярныеКнигиToolStripMenuItem = new ToolStripMenuItem();
            штрафыЗаПросрочкуToolStripMenuItem = new ToolStripMenuItem();
            pictureBoxQuokka = new PictureBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxQuokka).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { справочникиToolStripMenuItem, журналыToolStripMenuItem, отчетыToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(978, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            справочникиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { книгиToolStripMenuItem, читателиToolStripMenuItem, типыКнигToolStripMenuItem });
            справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            справочникиToolStripMenuItem.Size = new Size(139, 29);
            справочникиToolStripMenuItem.Text = "Справочники";
            справочникиToolStripMenuItem.Click += справочникиToolStripMenuItem_Click;
            // 
            // книгиToolStripMenuItem
            // 
            книгиToolStripMenuItem.Name = "книгиToolStripMenuItem";
            книгиToolStripMenuItem.Size = new Size(197, 34);
            книгиToolStripMenuItem.Text = "Книги";
            книгиToolStripMenuItem.Click += книгиToolStripMenuItem_Click;
            // 
            // читателиToolStripMenuItem
            // 
            читателиToolStripMenuItem.Name = "читателиToolStripMenuItem";
            читателиToolStripMenuItem.Size = new Size(197, 34);
            читателиToolStripMenuItem.Text = "Читатели";
            читателиToolStripMenuItem.Click += читателиToolStripMenuItem_Click;
            // 
            // типыКнигToolStripMenuItem
            // 
            типыКнигToolStripMenuItem.Name = "типыКнигToolStripMenuItem";
            типыКнигToolStripMenuItem.Size = new Size(197, 34);
            типыКнигToolStripMenuItem.Text = "Типы книг";
            типыКнигToolStripMenuItem.Click += типыКнигToolStripMenuItem_Click;
            // 
            // журналыToolStripMenuItem
            // 
            журналыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { выдачаToolStripMenuItem, пожертвованиеКнигToolStripMenuItem });
            журналыToolStripMenuItem.Name = "журналыToolStripMenuItem";
            журналыToolStripMenuItem.Size = new Size(105, 29);
            журналыToolStripMenuItem.Text = "Журналы";
            // 
            // выдачаToolStripMenuItem
            // 
            выдачаToolStripMenuItem.Name = "выдачаToolStripMenuItem";
            выдачаToolStripMenuItem.Size = new Size(302, 34);
            выдачаToolStripMenuItem.Text = "Выдача и возврат книг";
            выдачаToolStripMenuItem.Click += выдачаToolStripMenuItem_Click;
            // 
            // пожертвованиеКнигToolStripMenuItem
            // 
            пожертвованиеКнигToolStripMenuItem.Name = "пожертвованиеКнигToolStripMenuItem";
            пожертвованиеКнигToolStripMenuItem.Size = new Size(302, 34);
            пожертвованиеКнигToolStripMenuItem.Text = "Пожертвование книг";
            пожертвованиеКнигToolStripMenuItem.Click += пожертвованиеКнигToolStripMenuItem_Click;
            // 
            // отчетыToolStripMenuItem
            // 
            отчетыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { самыеПопулярныеКнигиToolStripMenuItem, штрафыЗаПросрочкуToolStripMenuItem });
            отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            отчетыToolStripMenuItem.Size = new Size(88, 29);
            отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // самыеПопулярныеКнигиToolStripMenuItem
            // 
            самыеПопулярныеКнигиToolStripMenuItem.Name = "самыеПопулярныеКнигиToolStripMenuItem";
            самыеПопулярныеКнигиToolStripMenuItem.Size = new Size(326, 34);
            самыеПопулярныеКнигиToolStripMenuItem.Text = "Самые популярные книги";
            самыеПопулярныеКнигиToolStripMenuItem.Click += самыеПопулярныеКнигиToolStripMenuItem_Click;
            // 
            // штрафыЗаПросрочкуToolStripMenuItem
            // 
            штрафыЗаПросрочкуToolStripMenuItem.Name = "штрафыЗаПросрочкуToolStripMenuItem";
            штрафыЗаПросрочкуToolStripMenuItem.Size = new Size(326, 34);
            штрафыЗаПросрочкуToolStripMenuItem.Text = "Штрафы за просрочку";
            штрафыЗаПросрочкуToolStripMenuItem.Click += штрафыЗаПросрочкуToolStripMenuItem_Click;
            // 
            // pictureBoxQuokka
            // 
            pictureBoxQuokka.Anchor = AnchorStyles.None;
            pictureBoxQuokka.Image = Properties.Resources.kwokka;
            pictureBoxQuokka.Location = new Point(256, 36);
            pictureBoxQuokka.Name = "pictureBoxQuokka";
            pictureBoxQuokka.Size = new Size(450, 450);
            pictureBoxQuokka.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxQuokka.TabIndex = 1;
            pictureBoxQuokka.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 594);
            Controls.Add(pictureBoxQuokka);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(800, 500);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Библиотека — система управления";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxQuokka).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem справочникиToolStripMenuItem;
        private ToolStripMenuItem журналыToolStripMenuItem;
        private ToolStripMenuItem отчетыToolStripMenuItem;
        private ToolStripMenuItem книгиToolStripMenuItem;
        private ToolStripMenuItem читателиToolStripMenuItem;
        private ToolStripMenuItem типыКнигToolStripMenuItem;
        private ToolStripMenuItem выдачаToolStripMenuItem;
        private ToolStripMenuItem пожертвованиеКнигToolStripMenuItem;
        private ToolStripMenuItem самыеПопулярныеКнигиToolStripMenuItem;
        private ToolStripMenuItem штрафыЗаПросрочкуToolStripMenuItem;
        private PictureBox pictureBoxQuokka;
    }
}
