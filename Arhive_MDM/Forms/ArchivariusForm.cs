using Arhive_MDM.Data.Repositories;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace Arhive_MDM.Forms
{
    public partial class ArchivariusForm : Form
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IDocumentsRepository _documentsRepository;
        private DataGridViewSelectedRowCollection selectedOrdersRow;
        private string copyLink;
        private FileManager localFileManager;

        public ArchivariusForm(int userId, FileManager fileManager)
        {
            _ordersRepository = (IOrdersRepository)Program.ServiceProvider.GetService(typeof(IOrdersRepository));
            _documentsRepository = (IDocumentsRepository)Program.ServiceProvider.GetService(typeof(IDocumentsRepository));
            localFileManager = fileManager;


            InitializeComponent();
            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.ColumnCount = 5;
            dataGridViewOrders.Columns[0].HeaderText = "Код";
            dataGridViewOrders.Columns[1].HeaderText = "Сумма";
            dataGridViewOrders.Columns[2].HeaderText = "Оплачено";
            dataGridViewOrders.Columns[3].HeaderText = "Дата создания";
            dataGridViewOrders.Columns[4].HeaderText = "Дата выполнения";
            dataGridViewOrders.Columns[0].Visible = false;

            dataGridViewDocuments.RowHeadersVisible = false;
            dataGridViewDocuments.ColumnCount = 3;
            dataGridViewDocuments.Columns[0].HeaderText = "КодКонтента";
            dataGridViewDocuments.Columns[1].HeaderText = "Код";
            dataGridViewDocuments.Columns[2].HeaderText = "Ссылка на фаил";
            dataGridViewDocuments.Columns[0].Visible = false;
            dataGridViewDocuments.Columns[1].Visible = false;
            dataGridViewDocuments.Columns[1].ReadOnly = true;
            dataGridViewDocuments.Width = 300;


            dataGridViewOrderContent.RowHeadersVisible = false;
            dataGridViewOrderContent.ColumnCount = 2;
            dataGridViewOrderContent.Columns[0].HeaderText = "Код";
            dataGridViewOrderContent.Columns[1].HeaderText = "Информация";

            UpdateDataGridViewOrders(userId);

        }

        private void ArchivariusForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.LoginForm.Show();
        }
        private bool VerifyDocumentsValues(out string name)
        {
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

        private async void buttonNewPDFCreate_Click(object sender, EventArgs e)
        {
            dataGridViewDocuments.Width = 300;
            if (!VerifyDocumentsValues(out var name))
            {
                return;
            }
            var selectedRow = selectedOrdersRow[0];
            var selectedOrderContentRow = dataGridViewOrderContent.SelectedRows[0];
            var ordercontet = await _ordersRepository.GetOrdersContent(Convert.ToInt32(selectedOrderContentRow.Cells[0].Value));

            var folder = localFileManager.CreateFileFolder("OrderContet_" + ordercontet.Id.ToString());
            folder = $@"{folder}\{name}" + ".pdf";
            
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(folder, FileMode.Create));
            doc.Open();

            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            BaseFont baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font font = new Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            
            doc.Add(new Phrase(textBoxNewText.Text,font));
            doc.Close();

            MessageBox.Show("Pdf-документ сохранен");
           
            var order = await _ordersRepository.GetOrder(Convert.ToInt32(selectedRow.Cells[0].Value));
            order.TimeCompleted = DateTime.Now;
            ordercontet.FileLink = folder;

            await _ordersRepository.UpdateOrderContent(ordercontet);
            await UpdateDataGridViewDocuments(Convert.ToInt32(selectedOrderContentRow.Cells[0].Value));
        }

        private async void buttonRestore_Click(object sender, EventArgs e)
        {
            dataGridViewDocuments.Width = 300;
            selectedOrdersRow = dataGridViewOrders.SelectedRows;
            
            var orderSelected = selectedOrdersRow.Count > 0;
            if (orderSelected)
            {
                var selectedRow = selectedOrdersRow[0];
                if (selectedRow.Cells[0].Value != null)
                {
                    OpenFileDialog OPF = new OpenFileDialog();
                    OPF.Multiselect = false;
                    if (OPF.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string file in OPF.FileNames)
                        {
                            copyLink = file;
                        }
                    }
                    //название и расширение требуемого файла
                    string FileName = Path.GetFileName(copyLink);
                    //Место складирования

                    var selectedOrderContentRow = dataGridViewOrderContent.SelectedRows[0];
                    var ordercontet =await _ordersRepository.GetOrdersContent(Convert.ToInt32(selectedOrderContentRow.Cells[0].Value));

                    var folder = localFileManager.CreateFileFolder("OrderContet_" + ordercontet.Id.ToString());
                    var fileLink = $@"{folder}\{FileName}";
                    File.Copy(copyLink, fileLink, true);

                   
                    var order = await _ordersRepository.GetOrder(Convert.ToInt32(selectedRow.Cells[0].Value));
                    order.TimeCompleted = DateTime.Now;
                    ordercontet.FileLink = fileLink;
                    await _ordersRepository.UpdateOrderContent(ordercontet);
                    await UpdateDataGridViewDocuments(Convert.ToInt32(selectedOrderContentRow.Cells[0].Value));
                }
            }
            
        }

        private void ClearDataGridViewOrderContentSelection()
        {
            dataGridViewOrderContent.ClearSelection();
            dataGridViewOrderContent.CurrentCell = null;

        }
        private void ClearDataGridViewOrderDocSelection()
        {
            dataGridViewDocuments.ClearSelection();
            dataGridViewDocuments.CurrentCell = null;
        }
        private void dataGridViewOrders_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewDocuments.Width = 300;
            dataGridViewOrders.Columns[0].Width = 40;
            dataGridViewOrderContent.Columns[0].Width = 40;
            ClearDataGridViewOrderContentSelection();
            ClearDataGridViewOrderDocSelection();
            selectedOrdersRow = dataGridViewOrders.SelectedRows;

            var orderSelected = selectedOrdersRow.Count > 0;

           
            if (orderSelected)
            {
                var selectedRow = selectedOrdersRow[0];
                if (selectedRow.Cells[0].Value != null)
                {  
                    UpdateDataGridViewOrderContents(Convert.ToInt32(selectedRow.Cells[0].Value));
                }
            }   
        }

        private void dataGridViewOrderContent_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewDocuments.Width = 300;
            dataGridViewOrders.Columns[0].Width = 40;
            dataGridViewOrderContent.Columns[0].Width = 40;
            selectedOrdersRow = dataGridViewOrders.SelectedRows;
            var selectedOrderContentRows = dataGridViewOrderContent.SelectedRows;
            var orderSelected = selectedOrdersRow.Count > 0;
            var orderContentSelected = selectedOrderContentRows.Count > 0;
            if (selectedOrdersRow.Count != 0)
            {
                if (orderSelected)
                {
                    if (orderContentSelected)
                    {
                        var selectedRow = selectedOrderContentRows[0];
                        var selectedOrder = selectedOrdersRow[0];
                        if (selectedRow.Cells[0].Value != null)
                        {
                            textBoxInfo.Text = selectedRow.Cells[1].Value.ToString();
                            UpdateDataGridViewDocuments(Convert.ToInt32(selectedRow.Cells[0].Value));
                        }
                    }
                }
            }
            
        }

        private async Task UpdateDataGridViewOrders(int workerId)
        {
            var orders = await _ordersRepository.GetOrdersWithWorker(workerId);
            dataGridViewOrders.Rows.Clear();
            dataGridViewOrders.Columns[0].Width = orders.Count > 4 ? 348 : 365;
            foreach (var order in orders)
            {
                dataGridViewOrders.Rows.Add(new[] {
                    order.Id.ToString(),
                    order.Payment.ToString(),
                    order.PaymentIsDone.ToString(),
                    order.TimeCreated.ToString(),
                    order.TimeCompleted.ToString()
                });
            }
            ClearDataGridViewOrdersSelection();
        }
        private async Task UpdateDataGridViewOrderContents(int orderId)
        {
            var orderscontents = await _ordersRepository.GetListOrdersContent(orderId);
            dataGridViewOrderContent.Rows.Clear();
            dataGridViewOrderContent.Columns[0].Width = orderscontents.Count > 4 ? 348 : 365;
            foreach (var ordercontent in orderscontents)
            {
                dataGridViewOrderContent.Rows.Add(new[] {
                    ordercontent.Id.ToString(),
                    ordercontent.Info.ToString()
                });
            }
            ClearDataGridViewOrderContentSelection();
        }

        private async Task UpdateDataGridViewDocuments(int ordercontentId)
        { 
            var ordercontent = await _ordersRepository.GetOrdersContent(ordercontentId);
            dataGridViewDocuments.Rows.Clear();
                dataGridViewDocuments.Rows.Add(new[] {
                    ordercontent.Id.ToString(),
                    ordercontent.FileId.ToString(),
                    ordercontent.FileLink.ToString() });

            ClearDataGridViewDocumentsSelection();
        }
        private void ClearDataGridViewOrdersSelection()
        {
            dataGridViewOrders.ClearSelection();
            dataGridViewOrders.CurrentCell = null;

        }

        private void ClearDataGridViewDocumentsSelection()
        { 
            dataGridViewDocuments.ClearSelection();
            dataGridViewDocuments.CurrentCell = null;
        }

        private void dataGridViewDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Process.Start(dataGridViewDocuments.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }
    }
}
