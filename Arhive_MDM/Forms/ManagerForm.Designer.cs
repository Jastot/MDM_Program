
namespace Arhive_MDM.Forms
{
    partial class ManagerForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maskedTextBoxTelephone = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIdClient = new System.Windows.Forms.TextBox();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.textBoxFIO = new System.Windows.Forms.TextBox();
            this.buttonCancelClient = new System.Windows.Forms.Button();
            this.buttonChangeClient = new System.Windows.Forms.Button();
            this.buttonAddNewClient = new System.Windows.Forms.Button();
            this.dataGridViewClients = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownPayed = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSumm = new System.Windows.Forms.NumericUpDown();
            this.textBoxOrderId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCancelOrder = new System.Windows.Forms.Button();
            this.buttonChangeOrder = new System.Windows.Forms.Button();
            this.buttonAddOrder = new System.Windows.Forms.Button();
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxOrderContentId = new System.Windows.Forms.TextBox();
            this.buttonCancelContent = new System.Windows.Forms.Button();
            this.buttonChangeContent = new System.Windows.Forms.Button();
            this.buttonAddContent = new System.Windows.Forms.Button();
            this.dataGridViewOrderContent = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPayed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSumm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrderContent)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maskedTextBoxTelephone);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxIdClient);
            this.groupBox1.Controls.Add(this.textBoxAddress);
            this.groupBox1.Controls.Add(this.textBoxFIO);
            this.groupBox1.Controls.Add(this.buttonCancelClient);
            this.groupBox1.Controls.Add(this.buttonChangeClient);
            this.groupBox1.Controls.Add(this.buttonAddNewClient);
            this.groupBox1.Controls.Add(this.dataGridViewClients);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 562);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clients";
            // 
            // maskedTextBoxTelephone
            // 
            this.maskedTextBoxTelephone.Location = new System.Drawing.Point(157, 452);
            this.maskedTextBoxTelephone.Mask = "+7 (000) 000-0000";
            this.maskedTextBoxTelephone.Name = "maskedTextBoxTelephone";
            this.maskedTextBoxTelephone.Size = new System.Drawing.Size(415, 20);
            this.maskedTextBoxTelephone.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(6, 488);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Адрес";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(6, 452);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Телефон";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 416);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "ФИО";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Код";
            // 
            // textBoxIdClient
            // 
            this.textBoxIdClient.Enabled = false;
            this.textBoxIdClient.Location = new System.Drawing.Point(157, 381);
            this.textBoxIdClient.Name = "textBoxIdClient";
            this.textBoxIdClient.Size = new System.Drawing.Size(101, 20);
            this.textBoxIdClient.TabIndex = 7;
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(157, 488);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(415, 20);
            this.textBoxAddress.TabIndex = 6;
            // 
            // textBoxFIO
            // 
            this.textBoxFIO.Location = new System.Drawing.Point(157, 416);
            this.textBoxFIO.Name = "textBoxFIO";
            this.textBoxFIO.Size = new System.Drawing.Size(415, 20);
            this.textBoxFIO.TabIndex = 4;
            // 
            // buttonCancelClient
            // 
            this.buttonCancelClient.Location = new System.Drawing.Point(482, 533);
            this.buttonCancelClient.Name = "buttonCancelClient";
            this.buttonCancelClient.Size = new System.Drawing.Size(90, 23);
            this.buttonCancelClient.TabIndex = 3;
            this.buttonCancelClient.Text = "Отмена";
            this.buttonCancelClient.UseVisualStyleBackColor = true;
            this.buttonCancelClient.Click += new System.EventHandler(this.buttonCancelClient_Click);
            // 
            // buttonChangeClient
            // 
            this.buttonChangeClient.Location = new System.Drawing.Point(157, 533);
            this.buttonChangeClient.Name = "buttonChangeClient";
            this.buttonChangeClient.Size = new System.Drawing.Size(101, 23);
            this.buttonChangeClient.TabIndex = 2;
            this.buttonChangeClient.Text = "Изменить";
            this.buttonChangeClient.UseVisualStyleBackColor = true;
            this.buttonChangeClient.Click += new System.EventHandler(this.buttonChangeClient_Click);
            // 
            // buttonAddNewClient
            // 
            this.buttonAddNewClient.Location = new System.Drawing.Point(6, 533);
            this.buttonAddNewClient.Name = "buttonAddNewClient";
            this.buttonAddNewClient.Size = new System.Drawing.Size(145, 23);
            this.buttonAddNewClient.TabIndex = 1;
            this.buttonAddNewClient.Text = "Добавить клиента";
            this.buttonAddNewClient.UseVisualStyleBackColor = true;
            this.buttonAddNewClient.Click += new System.EventHandler(this.buttonAddNewClient_Click);
            // 
            // dataGridViewClients
            // 
            this.dataGridViewClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClients.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewClients.MultiSelect = false;
            this.dataGridViewClients.Name = "dataGridViewClients";
            this.dataGridViewClients.ReadOnly = true;
            this.dataGridViewClients.RowHeadersWidth = 10;
            this.dataGridViewClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewClients.Size = new System.Drawing.Size(566, 340);
            this.dataGridViewClients.TabIndex = 0;
            this.dataGridViewClients.SelectionChanged += new System.EventHandler(this.dataGridViewClients_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownPayed);
            this.groupBox2.Controls.Add(this.numericUpDownSumm);
            this.groupBox2.Controls.Add(this.textBoxOrderId);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.buttonCancelOrder);
            this.groupBox2.Controls.Add(this.buttonChangeOrder);
            this.groupBox2.Controls.Add(this.buttonAddOrder);
            this.groupBox2.Controls.Add(this.dataGridViewOrders);
            this.groupBox2.Location = new System.Drawing.Point(596, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(530, 562);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Orders";
            // 
            // numericUpDownPayed
            // 
            this.numericUpDownPayed.Location = new System.Drawing.Point(182, 449);
            this.numericUpDownPayed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownPayed.Name = "numericUpDownPayed";
            this.numericUpDownPayed.Size = new System.Drawing.Size(340, 20);
            this.numericUpDownPayed.TabIndex = 13;
            // 
            // numericUpDownSumm
            // 
            this.numericUpDownSumm.Location = new System.Drawing.Point(182, 417);
            this.numericUpDownSumm.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSumm.Name = "numericUpDownSumm";
            this.numericUpDownSumm.Size = new System.Drawing.Size(340, 20);
            this.numericUpDownSumm.TabIndex = 12;
            // 
            // textBoxOrderId
            // 
            this.textBoxOrderId.Enabled = false;
            this.textBoxOrderId.Location = new System.Drawing.Point(182, 381);
            this.textBoxOrderId.Name = "textBoxOrderId";
            this.textBoxOrderId.Size = new System.Drawing.Size(103, 20);
            this.textBoxOrderId.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(7, 381);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "Код";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(7, 452);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Оплачено";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(7, 416);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Сумма";
            // 
            // buttonCancelOrder
            // 
            this.buttonCancelOrder.Location = new System.Drawing.Point(430, 532);
            this.buttonCancelOrder.Name = "buttonCancelOrder";
            this.buttonCancelOrder.Size = new System.Drawing.Size(92, 23);
            this.buttonCancelOrder.TabIndex = 3;
            this.buttonCancelOrder.Text = "Отмена";
            this.buttonCancelOrder.UseVisualStyleBackColor = true;
            this.buttonCancelOrder.Click += new System.EventHandler(this.buttonCancelOrder_Click);
            // 
            // buttonChangeOrder
            // 
            this.buttonChangeOrder.Location = new System.Drawing.Point(152, 533);
            this.buttonChangeOrder.Name = "buttonChangeOrder";
            this.buttonChangeOrder.Size = new System.Drawing.Size(113, 23);
            this.buttonChangeOrder.TabIndex = 2;
            this.buttonChangeOrder.Text = "Изменить";
            this.buttonChangeOrder.UseVisualStyleBackColor = true;
            this.buttonChangeOrder.Click += new System.EventHandler(this.buttonChangeOrder_Click);
            // 
            // buttonAddOrder
            // 
            this.buttonAddOrder.Location = new System.Drawing.Point(10, 533);
            this.buttonAddOrder.Name = "buttonAddOrder";
            this.buttonAddOrder.Size = new System.Drawing.Size(136, 23);
            this.buttonAddOrder.TabIndex = 1;
            this.buttonAddOrder.Text = "Добавить заказ";
            this.buttonAddOrder.UseVisualStyleBackColor = true;
            this.buttonAddOrder.Click += new System.EventHandler(this.buttonAddOrder_Click);
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(7, 20);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.ReadOnly = true;
            this.dataGridViewOrders.RowHeadersWidth = 51;
            this.dataGridViewOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOrders.Size = new System.Drawing.Size(515, 339);
            this.dataGridViewOrders.TabIndex = 0;
            this.dataGridViewOrders.SelectionChanged += new System.EventHandler(this.dataGridViewOrders_SelectionChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(6, 264);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Описание заказа";
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(7, 284);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(338, 242);
            this.textBoxInfo.TabIndex = 13;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxOrderContentId);
            this.groupBox3.Controls.Add(this.buttonCancelContent);
            this.groupBox3.Controls.Add(this.buttonChangeContent);
            this.groupBox3.Controls.Add(this.buttonAddContent);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBoxInfo);
            this.groupBox3.Controls.Add(this.dataGridViewOrderContent);
            this.groupBox3.Location = new System.Drawing.Point(1132, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 556);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Информация заказа";
            // 
            // textBoxOrderContentId
            // 
            this.textBoxOrderContentId.Enabled = false;
            this.textBoxOrderContentId.Location = new System.Drawing.Point(163, 264);
            this.textBoxOrderContentId.Name = "textBoxOrderContentId";
            this.textBoxOrderContentId.Size = new System.Drawing.Size(100, 20);
            this.textBoxOrderContentId.TabIndex = 17;
            this.textBoxOrderContentId.Visible = false;
            // 
            // buttonCancelContent
            // 
            this.buttonCancelContent.Location = new System.Drawing.Point(282, 533);
            this.buttonCancelContent.Name = "buttonCancelContent";
            this.buttonCancelContent.Size = new System.Drawing.Size(63, 23);
            this.buttonCancelContent.TabIndex = 16;
            this.buttonCancelContent.Text = "Отмена";
            this.buttonCancelContent.UseVisualStyleBackColor = true;
            this.buttonCancelContent.Click += new System.EventHandler(this.buttonCancelContent_Click);
            // 
            // buttonChangeContent
            // 
            this.buttonChangeContent.Location = new System.Drawing.Point(199, 532);
            this.buttonChangeContent.Name = "buttonChangeContent";
            this.buttonChangeContent.Size = new System.Drawing.Size(77, 23);
            this.buttonChangeContent.TabIndex = 15;
            this.buttonChangeContent.Text = "Изменить";
            this.buttonChangeContent.UseVisualStyleBackColor = true;
            this.buttonChangeContent.Click += new System.EventHandler(this.buttonChangeContent_Click);
            // 
            // buttonAddContent
            // 
            this.buttonAddContent.Location = new System.Drawing.Point(7, 533);
            this.buttonAddContent.Name = "buttonAddContent";
            this.buttonAddContent.Size = new System.Drawing.Size(186, 23);
            this.buttonAddContent.TabIndex = 14;
            this.buttonAddContent.Text = "Добавить информацию к заказу";
            this.buttonAddContent.UseVisualStyleBackColor = true;
            this.buttonAddContent.Click += new System.EventHandler(this.buttonAddContent_Click);
            // 
            // dataGridViewOrderContent
            // 
            this.dataGridViewOrderContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrderContent.Location = new System.Drawing.Point(7, 20);
            this.dataGridViewOrderContent.Name = "dataGridViewOrderContent";
            this.dataGridViewOrderContent.RowHeadersWidth = 51;
            this.dataGridViewOrderContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOrderContent.Size = new System.Drawing.Size(337, 241);
            this.dataGridViewOrderContent.TabIndex = 0;
            this.dataGridViewOrderContent.SelectionChanged += new System.EventHandler(this.dataGridViewOrderContent_SelectionChanged);
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1489, 586);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ManagerForm";
            this.Text = "ИС Архив - Менеджер";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManagerForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPayed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSumm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrderContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCancelClient;
        private System.Windows.Forms.Button buttonChangeClient;
        private System.Windows.Forms.Button buttonAddNewClient;
        private System.Windows.Forms.DataGridView dataGridViewClients;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewOrders;
        private System.Windows.Forms.Button buttonCancelOrder;
        private System.Windows.Forms.Button buttonChangeOrder;
        private System.Windows.Forms.Button buttonAddOrder;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.TextBox textBoxFIO;
        private System.Windows.Forms.TextBox textBoxIdClient;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonCancelContent;
        private System.Windows.Forms.Button buttonChangeContent;
        private System.Windows.Forms.Button buttonAddContent;
        private System.Windows.Forms.DataGridView dataGridViewOrderContent;
        private System.Windows.Forms.TextBox textBoxOrderId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxOrderContentId;
        private System.Windows.Forms.NumericUpDown numericUpDownPayed;
        private System.Windows.Forms.NumericUpDown numericUpDownSumm;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxTelephone;
    }
}