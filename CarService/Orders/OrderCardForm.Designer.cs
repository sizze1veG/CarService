namespace CarService.Orders
{
    partial class OrderCardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderCardForm));
            this.comboBoxClient = new System.Windows.Forms.ComboBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelClient = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.pictureBoxClient = new System.Windows.Forms.PictureBox();
            this.dateTimePickerOrderDate = new System.Windows.Forms.DateTimePicker();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.labelCar = new System.Windows.Forms.Label();
            this.comboBoxCar = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClient)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxClient
            // 
            this.comboBoxClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.comboBoxClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxClient.Font = new System.Drawing.Font("Nirmala UI", 20.25F);
            this.comboBoxClient.FormattingEnabled = true;
            this.comboBoxClient.Location = new System.Drawing.Point(872, 268);
            this.comboBoxClient.Name = "comboBoxClient";
            this.comboBoxClient.Size = new System.Drawing.Size(363, 45);
            this.comboBoxClient.TabIndex = 3;
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(344, 546);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(225, 52);
            this.buttonDelete.TabIndex = 51;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.buttonUpdate.FlatAppearance.BorderSize = 0;
            this.buttonUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpdate.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUpdate.ForeColor = System.Drawing.Color.White;
            this.buttonUpdate.Location = new System.Drawing.Point(46, 546);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(225, 52);
            this.buttonUpdate.TabIndex = 50;
            this.buttonUpdate.Text = "Обновить";
            this.buttonUpdate.UseVisualStyleBackColor = false;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(46, 546);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(225, 52);
            this.buttonAdd.TabIndex = 49;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // labelClient
            // 
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClient.Location = new System.Drawing.Point(867, 228);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(103, 30);
            this.labelClient.TabIndex = 43;
            this.labelClient.Text = "Клиент";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(412, 228);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(99, 30);
            this.labelStatus.TabIndex = 46;
            this.labelStatus.Text = "Статус";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.Location = new System.Drawing.Point(412, 55);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(168, 30);
            this.labelDate.TabIndex = 47;
            this.labelDate.Text = "Дата заказа";
            // 
            // pictureBoxClient
            // 
            this.pictureBoxClient.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxClient.Image")));
            this.pictureBoxClient.Location = new System.Drawing.Point(46, 48);
            this.pictureBoxClient.Name = "pictureBoxClient";
            this.pictureBoxClient.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxClient.TabIndex = 42;
            this.pictureBoxClient.TabStop = false;
            // 
            // dateTimePickerOrderDate
            // 
            this.dateTimePickerOrderDate.Font = new System.Drawing.Font("Nirmala UI", 20.25F);
            this.dateTimePickerOrderDate.Location = new System.Drawing.Point(417, 98);
            this.dateTimePickerOrderDate.Name = "dateTimePickerOrderDate";
            this.dateTimePickerOrderDate.Size = new System.Drawing.Size(363, 43);
            this.dateTimePickerOrderDate.TabIndex = 0;
            this.dateTimePickerOrderDate.Value = new System.DateTime(2024, 5, 23, 0, 0, 0, 0);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxStatus.Font = new System.Drawing.Font("Nirmala UI", 20.25F);
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "Ожидает подтверждения",
            "В процессе выполнения",
            "Выполнен",
            "Отменен"});
            this.comboBoxStatus.Location = new System.Drawing.Point(417, 268);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(363, 45);
            this.comboBoxStatus.TabIndex = 1;
            // 
            // labelCar
            // 
            this.labelCar.AutoSize = true;
            this.labelCar.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCar.Location = new System.Drawing.Point(867, 55);
            this.labelCar.Name = "labelCar";
            this.labelCar.Size = new System.Drawing.Size(72, 30);
            this.labelCar.TabIndex = 43;
            this.labelCar.Text = "Авто";
            // 
            // comboBoxCar
            // 
            this.comboBoxCar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.comboBoxCar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxCar.Font = new System.Drawing.Font("Nirmala UI", 20.25F);
            this.comboBoxCar.FormattingEnabled = true;
            this.comboBoxCar.Location = new System.Drawing.Point(872, 95);
            this.comboBoxCar.Name = "comboBoxCar";
            this.comboBoxCar.Size = new System.Drawing.Size(363, 45);
            this.comboBoxCar.TabIndex = 2;
            // 
            // OrderCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 618);
            this.Controls.Add(this.dateTimePickerOrderDate);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.comboBoxCar);
            this.Controls.Add(this.comboBoxClient);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelCar);
            this.Controls.Add(this.labelClient);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.pictureBoxClient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OrderCardForm";
            this.Text = "OrderCardForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClient)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxClient;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.PictureBox pictureBoxClient;
        private System.Windows.Forms.DateTimePicker dateTimePickerOrderDate;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label labelCar;
        private System.Windows.Forms.ComboBox comboBoxCar;
    }
}