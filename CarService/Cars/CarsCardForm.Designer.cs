namespace CarService.Cars
{
    partial class CarsCardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarsCardForm));
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.textBoxBrand = new System.Windows.Forms.TextBox();
            this.labelYear = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.labelBrand = new System.Windows.Forms.Label();
            this.pictureBoxClient = new System.Windows.Forms.PictureBox();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.labelLicensePlate = new System.Windows.Forms.Label();
            this.comboBoxClient = new System.Windows.Forms.ComboBox();
            this.labelClient = new System.Windows.Forms.Label();
            this.maskedTextBoxLicensePlate = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClient)).BeginInit();
            this.SuspendLayout();
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
            this.buttonDelete.TabIndex = 36;
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
            this.buttonUpdate.TabIndex = 35;
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
            this.buttonAdd.TabIndex = 34;
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
            this.pictureBox1.TabIndex = 33;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // textBoxModel
            // 
            this.textBoxModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.textBoxModel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxModel.Font = new System.Drawing.Font("Nirmala UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxModel.Location = new System.Drawing.Point(417, 187);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(363, 36);
            this.textBoxModel.TabIndex = 1;
            // 
            // textBoxBrand
            // 
            this.textBoxBrand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.textBoxBrand.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxBrand.Font = new System.Drawing.Font("Nirmala UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBrand.Location = new System.Drawing.Point(417, 98);
            this.textBoxBrand.Name = "textBoxBrand";
            this.textBoxBrand.Size = new System.Drawing.Size(363, 36);
            this.textBoxBrand.TabIndex = 0;
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYear.Location = new System.Drawing.Point(412, 233);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(59, 30);
            this.labelYear.TabIndex = 28;
            this.labelYear.Text = "Год";
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModel.Location = new System.Drawing.Point(412, 144);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(110, 30);
            this.labelModel.TabIndex = 29;
            this.labelModel.Text = "Модель";
            // 
            // labelBrand
            // 
            this.labelBrand.AutoSize = true;
            this.labelBrand.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBrand.Location = new System.Drawing.Point(412, 55);
            this.labelBrand.Name = "labelBrand";
            this.labelBrand.Size = new System.Drawing.Size(94, 30);
            this.labelBrand.TabIndex = 30;
            this.labelBrand.Text = "Марка";
            // 
            // pictureBoxClient
            // 
            this.pictureBoxClient.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxClient.Image")));
            this.pictureBoxClient.Location = new System.Drawing.Point(46, 48);
            this.pictureBoxClient.Name = "pictureBoxClient";
            this.pictureBoxClient.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxClient.TabIndex = 27;
            this.pictureBoxClient.TabStop = false;
            // 
            // textBoxYear
            // 
            this.textBoxYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.textBoxYear.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxYear.Font = new System.Drawing.Font("Nirmala UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxYear.Location = new System.Drawing.Point(417, 277);
            this.textBoxYear.MaxLength = 4;
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(363, 36);
            this.textBoxYear.TabIndex = 2;
            this.textBoxYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYear_KeyPress);
            // 
            // labelLicensePlate
            // 
            this.labelLicensePlate.AutoSize = true;
            this.labelLicensePlate.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLicensePlate.Location = new System.Drawing.Point(867, 55);
            this.labelLicensePlate.Name = "labelLicensePlate";
            this.labelLicensePlate.Size = new System.Drawing.Size(96, 30);
            this.labelLicensePlate.TabIndex = 28;
            this.labelLicensePlate.Text = "Номер";
            // 
            // comboBoxClient
            // 
            this.comboBoxClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.comboBoxClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxClient.Font = new System.Drawing.Font("Nirmala UI", 20.25F);
            this.comboBoxClient.FormattingEnabled = true;
            this.comboBoxClient.Location = new System.Drawing.Point(872, 184);
            this.comboBoxClient.Name = "comboBoxClient";
            this.comboBoxClient.Size = new System.Drawing.Size(363, 45);
            this.comboBoxClient.TabIndex = 4;
            // 
            // labelClient
            // 
            this.labelClient.AutoSize = true;
            this.labelClient.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClient.Location = new System.Drawing.Point(867, 144);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(103, 30);
            this.labelClient.TabIndex = 28;
            this.labelClient.Text = "Клиент";
            // 
            // maskedTextBoxLicensePlate
            // 
            this.maskedTextBoxLicensePlate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.maskedTextBoxLicensePlate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxLicensePlate.Font = new System.Drawing.Font("Nirmala UI", 20.25F);
            this.maskedTextBoxLicensePlate.Location = new System.Drawing.Point(872, 98);
            this.maskedTextBoxLicensePlate.Mask = "L000LL00";
            this.maskedTextBoxLicensePlate.Name = "maskedTextBoxLicensePlate";
            this.maskedTextBoxLicensePlate.Size = new System.Drawing.Size(363, 36);
            this.maskedTextBoxLicensePlate.TabIndex = 3;
            // 
            // CarsCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 683);
            this.Controls.Add(this.maskedTextBoxLicensePlate);
            this.Controls.Add(this.comboBoxClient);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxYear);
            this.Controls.Add(this.textBoxModel);
            this.Controls.Add(this.textBoxBrand);
            this.Controls.Add(this.labelClient);
            this.Controls.Add(this.labelLicensePlate);
            this.Controls.Add(this.labelYear);
            this.Controls.Add(this.labelModel);
            this.Controls.Add(this.labelBrand);
            this.Controls.Add(this.pictureBoxClient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CarsCardForm";
            this.Text = "CarsCardForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClient)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.TextBox textBoxBrand;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.Label labelBrand;
        private System.Windows.Forms.PictureBox pictureBoxClient;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Label labelLicensePlate;
        private System.Windows.Forms.ComboBox comboBoxClient;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxLicensePlate;
    }
}