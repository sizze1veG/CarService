namespace CarService.Cars
{
    partial class CarsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarsForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.pictureBoxUpdate = new System.Windows.Forms.PictureBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dataGridViewCars = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LicensePlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientID = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCars)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSearch.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(16, 15);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSearch.MaxLength = 50;
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(288, 35);
            this.textBoxSearch.TabIndex = 28;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged_1);
            // 
            // pictureBoxUpdate
            // 
            this.pictureBoxUpdate.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxUpdate.Image")));
            this.pictureBoxUpdate.Location = new System.Drawing.Point(1329, 69);
            this.pictureBoxUpdate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxUpdate.Name = "pictureBoxUpdate";
            this.pictureBoxUpdate.Size = new System.Drawing.Size(32, 30);
            this.pictureBoxUpdate.TabIndex = 27;
            this.pictureBoxUpdate.TabStop = false;
            this.pictureBoxUpdate.Click += new System.EventHandler(this.pictureBoxUpdate_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(1395, 69);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(288, 43);
            this.buttonAdd.TabIndex = 26;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // dataGridViewCars
            // 
            this.dataGridViewCars.AllowUserToAddRows = false;
            this.dataGridViewCars.AllowUserToDeleteRows = false;
            this.dataGridViewCars.AllowUserToResizeColumns = false;
            this.dataGridViewCars.AllowUserToResizeRows = false;
            this.dataGridViewCars.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewCars.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCars.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCars.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Brand,
            this.Model,
            this.Year,
            this.LicensePlate,
            this.ClientID});
            this.dataGridViewCars.Location = new System.Drawing.Point(16, 69);
            this.dataGridViewCars.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewCars.MultiSelect = false;
            this.dataGridViewCars.Name = "dataGridViewCars";
            this.dataGridViewCars.ReadOnly = true;
            this.dataGridViewCars.RowHeadersWidth = 51;
            this.dataGridViewCars.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewCars.Size = new System.Drawing.Size(1293, 772);
            this.dataGridViewCars.TabIndex = 25;
            this.dataGridViewCars.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCars_CellContentClick);
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 60;
            // 
            // Brand
            // 
            this.Brand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Brand.HeaderText = "Марка";
            this.Brand.MinimumWidth = 6;
            this.Brand.Name = "Brand";
            this.Brand.ReadOnly = true;
            this.Brand.Width = 200;
            // 
            // Model
            // 
            this.Model.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Model.HeaderText = "Модель";
            this.Model.MinimumWidth = 6;
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Width = 200;
            // 
            // Year
            // 
            this.Year.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Year.HeaderText = "Год";
            this.Year.MinimumWidth = 6;
            this.Year.Name = "Year";
            this.Year.ReadOnly = true;
            this.Year.Width = 200;
            // 
            // LicensePlate
            // 
            this.LicensePlate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LicensePlate.HeaderText = "Номер";
            this.LicensePlate.MinimumWidth = 6;
            this.LicensePlate.Name = "LicensePlate";
            this.LicensePlate.ReadOnly = true;
            this.LicensePlate.Width = 200;
            // 
            // ClientID
            // 
            this.ClientID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ClientID.HeaderText = "Клиент";
            this.ClientID.MinimumWidth = 6;
            this.ClientID.Name = "ClientID";
            this.ClientID.ReadOnly = true;
            this.ClientID.Width = 200;
            // 
            // CarsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1859, 861);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.pictureBoxUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.dataGridViewCars);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CarsForm";
            this.Text = "CarsForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCars)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.PictureBox pictureBoxUpdate;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.DataGridView dataGridViewCars;
        private System.Windows.Forms.DataGridViewLinkColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicensePlate;
        private System.Windows.Forms.DataGridViewLinkColumn ClientID;
    }
}