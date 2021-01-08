namespace CulinaryRecipes
{
    partial class SingForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ChcAddDescription = new System.Windows.Forms.CheckBox();
            this.dgGrid = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtAddEmail = new System.Windows.Forms.TextBox();
            this.lblAdd = new System.Windows.Forms.Label();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbDecreaseSize = new System.Windows.Forms.PictureBox();
            this.pbIncreaseSize = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelGroup = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDecreaseSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIncreaseSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Corbel", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(91, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 72);
            this.label1.TabIndex = 0;
            this.label1.Text = "Culinary Recipes\r\n           E-Mail\r\n";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Maroon;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSend.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(111, 236);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(161, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Wyślij";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnNext_Click);
            this.btnSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSend_KeyDown);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtTo
            // 
            this.txtTo.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtTo.Location = new System.Drawing.Point(31, 182);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(312, 31);
            this.txtTo.TabIndex = 3;
            this.txtTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTo_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Corbel", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(34, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Odbiorca";
            // 
            // ChcAddDescription
            // 
            this.ChcAddDescription.AutoSize = true;
            this.ChcAddDescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChcAddDescription.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ChcAddDescription.ForeColor = System.Drawing.Color.White;
            this.ChcAddDescription.Location = new System.Drawing.Point(35, 342);
            this.ChcAddDescription.Name = "ChcAddDescription";
            this.ChcAddDescription.Size = new System.Drawing.Size(134, 18);
            this.ChcAddDescription.TabIndex = 4;
            this.ChcAddDescription.Text = "Dołącz Opis przepisu";
            this.ChcAddDescription.UseVisualStyleBackColor = true;
            // 
            // dgGrid
            // 
            this.dgGrid.AllowUserToAddRows = false;
            this.dgGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(57)))));
            this.dgGrid.ColumnHeadersHeight = 55;
            this.dgGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgGrid.ColumnHeadersVisible = false;
            this.dgGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(57)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Corbel", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(57)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(57)))));
            this.dgGrid.Location = new System.Drawing.Point(434, 119);
            this.dgGrid.Name = "dgGrid";
            this.dgGrid.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgGrid.RowHeadersVisible = false;
            this.dgGrid.RowHeadersWidth = 65;
            this.dgGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgGrid.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dgGrid.Size = new System.Drawing.Size(321, 181);
            this.dgGrid.TabIndex = 11;
            this.dgGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgGrid_CellClick);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Maroon;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(24, 13);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 23);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Dodaj";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(537, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "Lista Kontaktów";
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.Color.Maroon;
            this.btnModify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModify.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnModify.ForeColor = System.Drawing.Color.White;
            this.btnModify.Location = new System.Drawing.Point(146, 11);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(80, 23);
            this.btnModify.TabIndex = 14;
            this.btnModify.Text = "Edytuj";
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Maroon;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(263, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 23);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Usuń";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtAddEmail
            // 
            this.txtAddEmail.Enabled = false;
            this.txtAddEmail.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtAddEmail.Location = new System.Drawing.Point(434, 343);
            this.txtAddEmail.Name = "txtAddEmail";
            this.txtAddEmail.Size = new System.Drawing.Size(321, 31);
            this.txtAddEmail.TabIndex = 0;
            this.txtAddEmail.Visible = false;
            this.txtAddEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddEmail_KeyDown);
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAdd.ForeColor = System.Drawing.Color.White;
            this.lblAdd.Location = new System.Drawing.Point(434, 321);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(90, 19);
            this.lblAdd.TabIndex = 0;
            this.lblAdd.Text = "Wpisz Email";
            this.lblAdd.Visible = false;
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.BackColor = System.Drawing.Color.Maroon;
            this.btnDeleteAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteAll.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDeleteAll.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAll.Location = new System.Drawing.Point(675, 305);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(80, 20);
            this.btnDeleteAll.TabIndex = 0;
            this.btnDeleteAll.Text = "WSZYSTKO";
            this.btnDeleteAll.UseVisualStyleBackColor = false;
            this.btnDeleteAll.Visible = false;
            this.btnDeleteAll.Click += new System.EventHandler(this.button1_Click);
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Maroon;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(560, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 20);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "ANULUJ";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbDecreaseSize
            // 
            this.pbDecreaseSize.Image = ((System.Drawing.Image)(resources.GetObject("pbDecreaseSize.Image")));
            this.pbDecreaseSize.Location = new System.Drawing.Point(398, 173);
            this.pbDecreaseSize.Name = "pbDecreaseSize";
            this.pbDecreaseSize.Size = new System.Drawing.Size(23, 50);
            this.pbDecreaseSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDecreaseSize.TabIndex = 16;
            this.pbDecreaseSize.TabStop = false;
            this.pbDecreaseSize.Click += new System.EventHandler(this.pbDecreaseSize_Click);
            // 
            // pbIncreaseSize
            // 
            this.pbIncreaseSize.Image = ((System.Drawing.Image)(resources.GetObject("pbIncreaseSize.Image")));
            this.pbIncreaseSize.Location = new System.Drawing.Point(356, 173);
            this.pbIncreaseSize.Name = "pbIncreaseSize";
            this.pbIncreaseSize.Size = new System.Drawing.Size(23, 50);
            this.pbIncreaseSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIncreaseSize.TabIndex = 10;
            this.pbIncreaseSize.TabStop = false;
            this.pbIncreaseSize.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.OrangeRed;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // panelGroup
            // 
            this.panelGroup.Controls.Add(this.btnDelete);
            this.panelGroup.Controls.Add(this.btnModify);
            this.panelGroup.Controls.Add(this.btnAdd);
            this.panelGroup.Location = new System.Drawing.Point(414, 29);
            this.panelGroup.Name = "panelGroup";
            this.panelGroup.Size = new System.Drawing.Size(352, 50);
            this.panelGroup.TabIndex = 0;
            // 
            // SingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(57)))));
            this.ClientSize = new System.Drawing.Size(784, 391);
            this.Controls.Add(this.panelGroup);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.lblAdd);
            this.Controls.Add(this.txtAddEmail);
            this.Controls.Add(this.pbDecreaseSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgGrid);
            this.Controls.Add(this.pbIncreaseSize);
            this.Controls.Add(this.ChcAddDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Culinary Recipes  E-Mail ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SingForm_FormClosed);
            this.Load += new System.EventHandler(this.SingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDecreaseSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIncreaseSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.CheckBox ChcAddDescription;
        private System.Windows.Forms.PictureBox pbIncreaseSize;
        private System.Windows.Forms.PictureBox pbDecreaseSize;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgGrid;
        private System.Windows.Forms.Label lblAdd;
        private System.Windows.Forms.TextBox txtAddEmail;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelGroup;
    }
}