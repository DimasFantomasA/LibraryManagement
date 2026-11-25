namespace LibraryManagement
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            CenterQuokka();

            this.Resize += (sender, e) => CenterQuokka();
        }

        private void êíèãèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BooksForm booksForm = new BooksForm();
            booksForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void ñïğàâî÷íèêèToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ÷èòàòåëèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientsForm clientsForm = new ClientsForm();
            clientsForm.ShowDialog();
        }

        private void òèïûÊíèãToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BookTypesForm form = new BookTypesForm();
            form.ShowDialog();
        }

        private void âûäà÷àToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new JournalForm();
            form.ShowDialog(this);
        }

        private void ïîæåğòâîâàíèåÊíèãToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new DonationsForm();
            form.ShowDialog(this);
        }

        private void ñàìûåÏîïóëÿğíûåÊíèãèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new PopularBooksReportForm();
            form.ShowDialog(this);
        }

        private void øòğàôûÇàÏğîñğî÷êóToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new FinesReportForm();
            form.ShowDialog(this);
        }

        private void CenterQuokka()
        {
            pictureBoxQuokka.Left =
                (ClientSize.Width - pictureBoxQuokka.Width) / 2;

            pictureBoxQuokka.Top =
                menuStrip1.Bottom +
                (ClientSize.Height - menuStrip1.Bottom -
                 pictureBoxQuokka.Height) / 2;
        }

    }
}
