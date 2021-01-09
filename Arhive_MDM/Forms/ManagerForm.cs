using Arhive_MDM.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arhive_MDM.Forms
{
    public partial class ManagerForm : Form
    {
        private readonly int _currentUserId;
        private readonly IClientsRepository _clientsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IWorkerRepository _workerRepository;
        private DataGridViewSelectedRowCollection selectedOrdersRow;
        private DataGridViewSelectedRowCollection selectedClientsRow;
        private bool orderSelected;
        private int buff = 10000;
        private int _chosenWorkerId;

        //Дописать выключение кнопок при условиях
        public ManagerForm(int userId)
        {
            _currentUserId = userId;
            _clientsRepository = (IClientsRepository)Program.ServiceProvider.GetService(typeof(IClientsRepository));
            _ordersRepository = (IOrdersRepository)Program.ServiceProvider.GetService(typeof(IOrdersRepository));
            _workerRepository = (IWorkerRepository)Program.ServiceProvider.GetService(typeof(IWorkerRepository));

            InitializeComponent();

            dataGridViewClients.RowHeadersVisible = false;
            dataGridViewClients.ColumnCount = 4;
           
            dataGridViewClients.Columns[0].HeaderText = "Код";
            dataGridViewClients.Columns[1].HeaderText = "ФИО";
            dataGridViewClients.Columns[2].HeaderText = "Телефон";
            dataGridViewClients.Columns[3].HeaderText = "Адрес";
            
            
            dataGridViewClients.Columns[1].Width = 120;
            dataGridViewClients.Columns[2].Width = 120;
            dataGridViewClients.Columns[3].Width = 120;

            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.ColumnCount = 5;
            dataGridViewOrders.Columns[0].HeaderText = "Код";
            dataGridViewOrders.Columns[1].HeaderText = "Сумма";
            dataGridViewOrders.Columns[2].HeaderText = "Оплачено";
            dataGridViewOrders.Columns[3].HeaderText = "Дата создания";
            dataGridViewOrders.Columns[4].HeaderText = "Дата завершения";
            

            //
            dataGridViewOrderContent.RowHeadersVisible = false;
            dataGridViewOrderContent.ColumnCount = 2;
            dataGridViewOrderContent.Columns[0].HeaderText = "Код";
            dataGridViewOrderContent.Columns[1].HeaderText = "Информация";
            

            UpdateDataGridViewClients();
            ClearClientsValues();
            ClearOrdersValues();
            ClearOrderContentValues();
        }

        private void ManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.LoginForm.Show();
        }

        private void dataGridViewClients_SelectionChanged(object sender, System.EventArgs e)
        {
            dataGridViewClients.Columns[0].Width = 40;
            dataGridViewOrders.Columns[0].Width = 40;
            dataGridViewOrderContent.Columns[0].Width = 40;
            ClearDataGridViewOrderContentSelection();
            ClearDataGridViewOrdersSelection();
            ClearOrderContentValues();
            ClearOrdersValues();
            selectedClientsRow = dataGridViewClients.SelectedRows;

            var clientsSelected = selectedClientsRow.Count > 0;
            //buttonAddNewClient.Enabled = !clientsSelected;
            //buttonChangeClient.Enabled = clientsSelected;

            if (clientsSelected)
            {

                var selectedRow = selectedClientsRow[0];
                if (selectedRow.Cells[0].Value != null)
                {
                textBoxIdClient.Text = selectedRow.Cells[0].Value.ToString();
                textBoxFIO.Text = selectedRow.Cells[1].Value.ToString();
                textBoxTelephone.Text = selectedRow.Cells[2].Value.ToString();
                textBoxAddress.Text = selectedRow.Cells[3].Value.ToString();
                UpdateDataGridViewOrders(Convert.ToInt32(textBoxIdClient.Text));
                    if(orderSelected)
                    UpdateDataGridViewOrderContents(Convert.ToInt32(textBoxOrderId.Text));
                    else
                    UpdateDataGridViewOrderContents(Convert.ToInt32(0));
                }
                
                
            }

            
        }

        private void dataGridViewOrders_SelectionChanged(object sender, System.EventArgs e)
        {
            dataGridViewClients.Columns[0].Width = 40;
            dataGridViewOrders.Columns[0].Width = 40;
            dataGridViewOrderContent.Columns[0].Width = 40;
            ClearDataGridViewOrderContentSelection();
            ClearOrderContentValues();
            selectedOrdersRow = dataGridViewOrders.SelectedRows;

            orderSelected = selectedOrdersRow.Count > 0;
            
            if(selectedClientsRow.Count != 0)
            { 
                if (orderSelected)
                {
                    var selectedRow = selectedOrdersRow[0];
                    if (selectedRow.Cells[0].Value != null)
                    {
                        textBoxOrderId.Text = selectedRow.Cells[0].Value.ToString();
                        textBoxOrderSumm.Text = selectedRow.Cells[1].Value.ToString();
                        textBoxOrderPayment.Text = selectedRow.Cells[2].Value.ToString();
                        UpdateDataGridViewOrderContents(Convert.ToInt32(textBoxOrderId.Text));
                    }
                    
                    
                }
            }
            
        }

        private void dataGridViewOrderContent_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewClients.Columns[0].Width = 40;
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
                    if (orderContentSelected) {
                        var selectedRow = selectedOrderContentRows[0];
                        if (selectedRow.Cells[0].Value != null)
                        {
                            textBoxOrderContentId.Text = selectedRow.Cells[0].Value.ToString();
                            textBoxInfo.Text = selectedRow.Cells[1].Value.ToString();
                        }
                    }
                }
            }
        }
        private bool VerifyOrdersValues(out string summ,out string payment)
        {
            summ = textBoxOrderSumm.Text;
            payment = textBoxOrderPayment.Text;
            

            var message = "";
            message += summ.Length == 0
                ? "Поле \"Сумма\" не должно быть пустым.\n"
                : "";
            message += payment.Length == 0
                ? "Поле \"Оплачено\" не должно быть пустым.\n"
                : "";
            

            if (!message.Equals(""))
            {
                MessageBox.Show(message, "Error");
                return false;
            }
            return true;
        }
        private bool VerifyClintsValues(out string fio, out string telephone, out string address, List<Models.Client> clients)
        {
            fio = textBoxFIO.Text;
            telephone = textBoxTelephone.Text;
            address = textBoxAddress.Text;

            var message = "";
            message += fio.Length == 0 
                ? "Поле \"ФИО Клиента\" не должно быть пустым.\n"
                : "";
            message += telephone.Length < 11 || telephone.Length > 13 
                ? "Поле \"Телефон\" должно быть заполнено в диапазоне от 11 до 13 символов\n" 
                : "";
            message += address.Length == 0
                ? "Поле \"Адресс\" не должно быть пустым.\n"
                : "";
            foreach (var client in clients)
            {
                if (client.ContactNumber == telephone)
                    message += "Данный контактый номер уже есть в системе\n";
            }
            if (!message.Equals(""))
            {
                MessageBox.Show(message, "Error");
                return false;
            }
            return true;
        }

        private async Task UpdateDataGridViewClients()
        {
            var clients = await _clientsRepository.GetClients();
            dataGridViewClients.Rows.Clear();
            dataGridViewClients.Columns[0].Width = clients.Count > 4 ? 348 : 365;
            foreach (var client in clients)
            {
                dataGridViewClients.Rows.Add(new[] {
                    client.Id.ToString(),
                    client.FIO,
                    client.ContactNumber.ToString(),
                    client.Address
                });
            }
            ClearDataGridViewClientsSelection();
        }
        private async Task UpdateDataGridViewOrders(int clientId)
        {
            var orders = await _ordersRepository.GetClientsOrders(clientId);
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
        private void ClearDataGridViewOrderContentSelection()
        {
            dataGridViewOrderContent.ClearSelection();
            dataGridViewOrderContent.CurrentCell = null;
            
        }
        private void ClearDataGridViewClientsSelection()
        {
            dataGridViewClients.ClearSelection();
            dataGridViewClients.CurrentCell = null;
            
        }
        private void ClearDataGridViewOrdersSelection()
        {
            dataGridViewOrders.ClearSelection();
            dataGridViewOrders.CurrentCell = null;
            
        }
        private void ClearClientsValues()
        {
            textBoxIdClient.Text = "";
            textBoxFIO.Text = "";
            textBoxAddress.Text = "";
            textBoxTelephone.Text = "";
        }
        private void ClearOrdersValues()
        {
            textBoxOrderId.Text = "";
            textBoxOrderSumm.Text = "";
            textBoxOrderPayment.Text = "";
        }

        private void ClearOrderContentValues()
        {
            textBoxOrderContentId.Text = "";
            textBoxInfo.Text = "";
        }

        private async void buttonAddNewClient_Click(object sender, System.EventArgs e)
        {
            
            if (!VerifyClintsValues(out var fio, out var telephone, out var address, await _clientsRepository.GetClients()))
            {
                return;
            }

            var client = new Models.Client()
            {
                FIO = fio,
                ContactNumber = telephone,
                Address = address
            };

            await _clientsRepository.CreateClient(client);
            await UpdateDataGridViewClients();
        }

        private async void buttonChangeClient_Click(object sender, System.EventArgs e)
        {
            if (!VerifyClintsValues(out var fio, out var telephone, out var address, await _clientsRepository.GetClients()))
            {
                return;
            }

            var client = await _clientsRepository.GetClient(Convert.ToInt32(textBoxIdClient.Text));
            client.FIO = fio;
            client.ContactNumber = telephone;
            client.Address = address;

            await _clientsRepository.UpdateClient(client);
            await UpdateDataGridViewClients();
        }

        private void buttonCancelClient_Click(object sender, System.EventArgs e)
        {
            ClearDataGridViewClientsSelection();
            ClearClientsValues();
        }

        private async void buttonAddOrder_Click(object sender, System.EventArgs e)
        {
            if (!VerifyOrdersValues(out var summ, out var payment))
            {
                return;
            }
            if (selectedClientsRow[0] == null)
            {
                return;
            }
            var selectedClientRow = selectedClientsRow[0];
            var workers = await _workerRepository.GetWorkersWhoRole("archivarius");
            
            foreach (var worker in workers)
            {
                if (worker.Orders != null)
                {
                    if (worker.Orders.Count < buff)
                    {
                        buff = worker.Orders.Count;
                        _chosenWorkerId = worker.Id;
                    }
                }
                else
                {
                    buff = 0;
                    _chosenWorkerId = worker.Id;
                }    
                    
            }

            var order = new Models.Orders()
            {
                ClientId = Convert.ToInt32(selectedClientRow.Cells[0].Value),
                Payment = Convert.ToInt32(summ),
                PaymentIsDone = Convert.ToInt32(payment),
                TimeCreated = DateTime.Now,
                WorkerId = _chosenWorkerId
            };

            await _ordersRepository.CreateOrder(order);
            await UpdateDataGridViewOrders(Convert.ToInt32(textBoxIdClient.Text));
        }

        private async void buttonChangeOrder_Click(object sender, System.EventArgs e)
        {
            if (selectedClientsRow[0] == null)
            {
                return;
            }
            if (!VerifyOrdersValues(out var summ, out var payment))
            {
                return;
            }

            var order = await _ordersRepository.GetOrder(Convert.ToInt32(textBoxOrderId.Text));
            order.Payment = Convert.ToInt32(summ);
            order.PaymentIsDone = Convert.ToInt32(payment);

            await _ordersRepository.UpdateOrder(order);
            await UpdateDataGridViewOrders(Convert.ToInt32(textBoxIdClient.Text));
        }

        private void buttonCancelOrder_Click(object sender, System.EventArgs e)
        {
            ClearDataGridViewOrdersSelection();
            ClearOrdersValues();
        }

        private async void buttonAddContent_Click(object sender, EventArgs e)
        {
            if (!VerifyOrdersValues(out var summ, out var payment))
            {
                return;
            }

            var selectedOrderRow = selectedOrdersRow[0];
            var ordercontent = new Models.OrderContent()
            {
                OrdersId = Convert.ToInt32(selectedOrderRow.Cells[0].Value),
                Info = textBoxInfo.Text,
                FileLink = ""
            };

            await _ordersRepository.CreateOrderContent(ordercontent);
            await UpdateDataGridViewOrderContents(Convert.ToInt32(textBoxOrderId.Text));
            
        }

        private async void buttonChangeContent_Click(object sender, EventArgs e)
        {

            var ordercontent = await _ordersRepository.GetOrdersContent(Convert.ToInt32(textBoxOrderContentId.Text));
            ordercontent.Info = textBoxInfo.Text;
            await _ordersRepository.UpdateOrderContent(ordercontent);
            await UpdateDataGridViewOrderContents(Convert.ToInt32(textBoxOrderId.Text));
        }

        private void buttonCancelContent_Click(object sender, EventArgs e)
        {
            ClearDataGridViewOrderContentSelection();
            ClearOrderContentValues();
        }
    }
}
