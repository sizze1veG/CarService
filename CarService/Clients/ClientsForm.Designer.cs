namespace CarService.Clients
{
    partial class ClientsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsForm));
            this.dataGridViewClients = new System.Windows.Forms.DataGridView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.pictureBoxUpdate = new System.Windows.Forms.PictureBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.ID = new System.Windows.Forms.DataGridViewLinkColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpdate)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewClients
            // 
            this.dataGridViewClients.AllowUserToAddRows = false;
            this.dataGridViewClients.AllowUserToDeleteRows = false;
            this.dataGridViewClients.AllowUserToResizeColumns = false;
            this.dataGridViewClients.AllowUserToResizeRows = false;
            this.dataGridViewClients.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewClients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewClients.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.FirstName,
            this.LastName,
            this.Phone});
            this.dataGridViewClients.Location = new System.Drawing.Point(12, 56);
            this.dataGridViewClients.MultiSelect = false;
            this.dataGridViewClients.Name = "dataGridViewClients";
            this.dataGridViewClients.ReadOnly = true;
            this.dataGridViewClients.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewClients.Size = new System.Drawing.Size(717, 249);
            this.dataGridViewClients.TabIndex = 0;
            this.dataGridViewClients.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewClients_CellContentClick);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(958, 56);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(216, 35);
            this.buttonAdd.TabIndex = 22;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // pictureBoxUpdate
            // 
            this.pictureBoxUpdate.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxUpdate.Image")));
            this.pictureBoxUpdate.Location = new System.Drawing.Point(735, 56);
            this.pictureBoxUpdate.Name = "pictureBoxUpdate";
            this.pictureBoxUpdate.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxUpdate.TabIndex = 23;
            this.pictureBoxUpdate.TabStop = false;
            this.pictureBoxUpdate.Click += new System.EventHandler(this.pictureBoxUpdate_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSearch.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(12, 12);
            this.textBoxSearch.MaxLength = 50;
            this.textBoxSearch.Multiline = true;
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(216, 28);
            this.textBoxSearch.TabIndex = 24;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 60;
            // 
            // FirstName
            // 
            this.FirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FirstName.HeaderText = "Имя";
            this.FirstName.Name = "FirstName";
            this.FirstName.ReadOnly = true;
            this.FirstName.Width = 200;
            // 
            // LastName
            // 
            this.LastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LastName.HeaderText = "Фамилия";
            this.LastName.Name = "LastName";
            this.LastName.ReadOnly = true;
            this.LastName.Width = 200;
            // 
            // Phone
            // 
            this.Phone.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Phone.HeaderText = "Телефон";
            this.Phone.Name = "Phone";
            this.Phone.ReadOnly = true;
            this.Phone.Width = 200;
            // 
            // ClientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 573);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.pictureBoxUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.dataGridViewClients);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClientsForm";
            this.Text = "ClientsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpdate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewClients;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.PictureBox pictureBoxUpdate;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.DataGridViewLinkColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
    }
}