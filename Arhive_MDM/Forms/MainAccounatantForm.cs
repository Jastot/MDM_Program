using Arhive_MDM.Data.Repositories;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace Arhive_MDM.Forms
{
    public partial class MainAccounatantForm : Form
    {

        private readonly int _currentUserId;
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IWorkerRepository _workerRepository;
        private FileManager localFileManager;

        private void refreshGridWidth()
        {
            dataGridViewOrders.Columns[0].Width = 30;
            dataGridViewOrders.Columns[1].Width = 50;
            dataGridViewOrders.Columns[2].Width = 50;
            dataGridViewOrders.Columns[3].Width = 80;
            dataGridViewOrders.Columns[4].Width = 80;

            dataGridViewDocuments.Columns[0].Width = 10;
            dataGridViewDocuments.Columns[1].Width = 70;
            dataGridViewDocuments.Columns[2].Width = 300;
        }

        public MainAccounatantForm(int userId, FileManager fileManager)
        {
            _currentUserId = userId;
            _documentsRepository = (IDocumentsRepository)Program.ServiceProvider.GetService(typeof(IDocumentsRepository));
            _ordersRepository = (IOrdersRepository)Program.ServiceProvider.GetService(typeof(IOrdersRepository));
            _clientsRepository = (IClientsRepository)Program.ServiceProvider.GetService(typeof(IClientsRepository));
            _workerRepository = (IWorkerRepository)Program.ServiceProvider.GetService(typeof(IWorkerRepository));
            localFileManager = fileManager;

            InitializeComponent();



            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.ColumnCount = 5;
            dataGridViewOrders.Columns[0].HeaderText = "Код";
            dataGridViewOrders.Columns[1].HeaderText = "Сумма";
            dataGridViewOrders.Columns[2].HeaderText = "Оплачено";
            dataGridViewOrders.Columns[3].HeaderText = "Дата создания";
            dataGridViewOrders.Columns[4].HeaderText = "Дата выполнения";

            dataGridViewDocuments.RowHeadersVisible = false;
            dataGridViewDocuments.ColumnCount = 3;
            dataGridViewDocuments.Columns[0].HeaderText = "Код";
            dataGridViewDocuments.Columns[1].HeaderText = "Название фаила";
            dataGridViewDocuments.Columns[2].HeaderText = "Ссылка на фаил";
            dataGridViewDocuments.Columns[0].Visible = false;
            dataGridViewDocuments.Columns[1].ReadOnly = true;

            refreshGridWidth();
            UpdateDataGridViewOrders(dateTimePickerFrom.Value,dateTimePickerTo.Value);
        }

        private async void addButton_Click(object sender, EventArgs e)
        {
            refreshGridWidth();
            if (!VerifyDocumentsValues(out var name))
            {
                return;
            }
            var document = new Models.Documents()
            {
                FileName = name
            };

            var folder = localFileManager.CreateFileFolder("Document_" + document.Id.ToString());
            folder = $@"{folder}\{name}" + ".pdf";
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(folder, FileMode.Create)) ; 
            doc.Open();

            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            BaseFont baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font font = new Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            var dateFrom = dateTimePickerFrom.Value;
            var dateTo = dateTimePickerTo.Value;

            var currentOrders = await _ordersRepository.GetOrdersinDates(dateFrom,dateTo);
            
            for (int i = 0; i < currentOrders.Count; i++)
            {
                if (currentOrders[i].TimeCompleted != DateTime.MinValue)
                {
                    var currentuser = await _clientsRepository.GetClient(currentOrders[i].ClientId);
                    var currentworker = await _workerRepository.GetWorker(currentOrders[i].WorkerId);
                    PdfPTable table = new PdfPTable(2);

                    PdfPCell cell = new PdfPCell(new Phrase("Заказ " + $"{currentOrders[i].Id}", font));

                    cell.Colspan = 2;
                    cell.HorizontalAlignment = 1;
                    cell.Border = 0;
                    table.AddCell(cell);
                    table.AddCell(new Phrase("ID", font));
                    table.AddCell(new Phrase(currentOrders[i].Id.ToString(), font));
                    table.AddCell(new Phrase("ФИО заказчика", font));
                    table.AddCell(new Phrase(currentuser.FIO, font));
                    table.AddCell(new Phrase("телефон заказчика", font));
                    table.AddCell(new Phrase(currentuser.ContactNumber, font));
                    table.AddCell(new Phrase("ФИО исполнителя", font));
                    table.AddCell(new Phrase(currentworker.FIO, font));

                    var col = new PdfPCell(new Phrase(currentOrders[i].Payment.ToString(), font));
                    var col2 = new PdfPCell(new Phrase(currentOrders[i].PaymentIsDone.ToString(), font));
                    if(currentOrders[i].Payment > currentOrders[i].PaymentIsDone)
                    {
                        col.BackgroundColor = BaseColor.RED;
                        col2.BackgroundColor = BaseColor.RED;
                    }
                    else
                    {
                        col.BackgroundColor = BaseColor.WHITE;
                        col2.BackgroundColor = BaseColor.WHITE;
                    }
                    table.AddCell(new Phrase("Сумма к оплате", font));
                    table.AddCell(col);
                    table.AddCell(new Phrase("Оплачено", font));
                    table.AddCell(col2);


                    table.AddCell(new Phrase("Дата завершения", font));
                    table.AddCell(new Phrase(currentOrders[i].TimeCompleted.ToString(), font));

                    doc.Add(table);
                }
            }
            doc.Close();

            document.FileLink = folder;
            document.TimeCreated = DateTime.Now;
            await _documentsRepository.CreateDocuments(document);
            await UpdateDataGridViewDocuments(dateTimePickerFrom.Value, dateTimePickerTo.Value);
            await UpdateDataGridViewOrders(dateTimePickerFrom.Value, dateTimePickerTo.Value);
        }
        private bool VerifyDocumentsValues(out string name)
        {
            refreshGridWidth();
            name = textBoxFileName.Text;

            var message = "";
            message += name.Length < 2 ? "Поле \"Наименование\" должно содержать минимум 2 символа." : "";

            if (!message.Equals(""))
            {
                MessageBox.Show(message, "Валидация данных о расходных материалах");
                return false;
            }
            return true;
        }
        private void dataGridViewCases_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewOrders.Columns[0].Width = 40;
            var selectedOrdersRow = dataGridViewOrders.SelectedRows;
            var orderSelected = selectedOrdersRow.Count > 0;


            if (orderSelected)
            {
                var selectedRow = selectedOrdersRow[0];
                if (selectedRow.Cells[0].Value != null)
                {
                    UpdateDataGridViewDocuments(dateTimePickerFrom.Value, dateTimePickerTo.Value);
                }
            }
        }
        private async Task UpdateDataGridViewOrders(DateTime dataFrom, DateTime dataTo)
        {
            refreshGridWidth();
            var orders = await _ordersRepository.GetOrdersinDates(dataFrom,dataTo);
            dataGridViewOrders.Rows.Clear();
            dataGridViewOrders.Columns[1].Width = orders.Count > 4 ? 348 : 365;
            foreach (var order in orders)
            { 
                dataGridViewOrders.Rows.Add(new[]
                {
                    order.Id.ToString(),
                    order.Payment.ToString(),
                    order.PaymentIsDone.ToString(),
                    order.TimeCreated.ToString(),
                    order.TimeCompleted.ToString()
                    }) ;
            }
            ClearDataGridViewOrdersSelection();
        }

        private async Task UpdateDataGridViewDocuments(DateTime dataFrom, DateTime dataTo)
        {
            refreshGridWidth();
            var documents = await _documentsRepository.GetDocumentsByTime(dataFrom, dataTo);
            dataGridViewDocuments.Rows.Clear();
            dataGridViewDocuments.Columns[1].Width = documents.Count > 4 ? 348 : 365;
            foreach (var document in documents)
            {
                dataGridViewDocuments.Rows.Add(new[] { 
                    document.Id.ToString(),
                    document.FileName.ToString(),
                    document.FileLink.ToString() });
            }
            ClearDataGridViewDocumentsSelection();
        }

        private void ClearDataGridViewOrdersSelection()
        {
            refreshGridWidth();
            dataGridViewOrders.ClearSelection();
            dataGridViewOrders.CurrentCell = null;
        }

        private void ClearDataGridViewDocumentsSelection()
        {
            refreshGridWidth();
            dataGridViewDocuments.ClearSelection();
            dataGridViewDocuments.CurrentCell = null;
        }

   

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            UpdateDataGridViewOrders(dateTimePickerFrom.Value, dateTimePickerTo.Value);
            UpdateDataGridViewDocuments(dateTimePickerFrom.Value, dateTimePickerTo.Value);
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            UpdateDataGridViewOrders(dateTimePickerFrom.Value, dateTimePickerTo.Value);
            UpdateDataGridViewDocuments(dateTimePickerFrom.Value, dateTimePickerTo.Value);
        }

        private void dataGridViewDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Process.Start(dataGridViewDocuments.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }

        private void MainAccounatantForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.LoginForm.Show();
        }
    }
}
