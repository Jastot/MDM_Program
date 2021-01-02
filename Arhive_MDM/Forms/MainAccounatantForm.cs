using Arhive_MDM.Data.Repositories;
using System;
using System.Windows.Forms;

namespace Arhive_MDM.Forms
{
    public partial class MainAccounatantForm : Form
    {

        private readonly int _currentUserId;
        private readonly ICaseRepository _caseRepository;
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IOrdersRepository _ordersRepository;
        public MainAccounatantForm(int userId)
        {
            _currentUserId = userId;
            _caseRepository = (ICaseRepository)Program.ServiceProvider.GetService(typeof(ICaseRepository));
            _documentsRepository = (IDocumentsRepository)Program.ServiceProvider.GetService(typeof(IDocumentsRepository));
            _ordersRepository = (IOrdersRepository)Program.ServiceProvider.GetService(typeof(IOrdersRepository));

            InitializeComponent();

            dataGridViewCases.RowHeadersVisible = false;
            dataGridViewCases.ColumnCount = 3;
            dataGridViewCases.Columns[0].HeaderText = "Код";
            dataGridViewCases.Columns[1].HeaderText = "Исполнитель";
            dataGridViewCases.Columns[2].HeaderText = "Клиент";

            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.ColumnCount = 3;
            dataGridViewOrders.Columns[0].HeaderText = "Код";
            dataGridViewOrders.Columns[1].HeaderText = "Сумма";
            dataGridViewOrders.Columns[2].HeaderText = "Оплачено";
            
            dataGridViewDocuments.RowHeadersVisible = false;
            dataGridViewDocuments.ColumnCount = 3;
            dataGridViewDocuments.Columns[0].HeaderText = "Код";
            dataGridViewDocuments.Columns[1].HeaderText = "Название фаила";
            dataGridViewDocuments.Columns[2].HeaderText = "Ссылка на фаил";


        }



        private void addButton_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewCases_SelectionChanged(object sender, EventArgs e)
        {
            var selectedRows = dataGridViewCases.SelectedRows;

            var consumablesSelected = selectedRows.Count > 0;
            buttonAddConsumables.Enabled = !consumablesSelected;
            buttonAddConsumablesToOrder.Enabled = consumablesSelected;
            buttonEditConsumables.Enabled = consumablesSelected;
            buttonRemoveConsumables.Enabled = consumablesSelected;

            if (consumablesSelected)
            {
                var selectedRow = selectedRows[0];
                textBoxConsumablesId.Text = selectedRow.Cells[0].Value.ToString();
                textBoxConsumablesName.Text = selectedRow.Cells[1].Value.ToString();
                numericUpDownConsumablesAmount.Value = Convert.ToInt32(selectedRow.Cells[2].Value.ToString());
            }
        }
    }
}
