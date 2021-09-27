namespace HealthCalendar
{
    partial class frm_AdvsManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.datagridColumnImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.datagridColumnRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datagridColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datagridColumnAdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datagridColumnBeginDate = new FarsiLibrary.Win.Controls.DataGridViewFADateTimePickerColumn();
            this.datagridColumnEndDate = new FarsiLibrary.Win.Controls.DataGridViewFADateTimePickerColumn();
            this.datagridColumnImagePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datagridColumnBackcolor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBox_PreviewAdv = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("B Nazanin", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.datagridColumnImage,
            this.datagridColumnRow,
            this.datagridColumnID,
            this.datagridColumnAdv,
            this.datagridColumnBeginDate,
            this.datagridColumnEndDate,
            this.datagridColumnImagePath,
            this.datagridColumnBackcolor});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("B Nazanin", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(12, 58);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("B Nazanin", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(349, 426);
            this.dataGridView1.TabIndex = 26;
            // 
            // datagridColumnImage
            // 
            this.datagridColumnImage.HeaderText = "";
            this.datagridColumnImage.Name = "datagridColumnImage";
            this.datagridColumnImage.ReadOnly = true;
            this.datagridColumnImage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridColumnImage.Width = 40;
            // 
            // datagridColumnRow
            // 
            this.datagridColumnRow.HeaderText = "#";
            this.datagridColumnRow.Name = "datagridColumnRow";
            this.datagridColumnRow.ReadOnly = true;
            this.datagridColumnRow.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridColumnRow.Width = 40;
            // 
            // datagridColumnID
            // 
            this.datagridColumnID.HeaderText = "شناسه";
            this.datagridColumnID.Name = "datagridColumnID";
            this.datagridColumnID.Visible = false;
            this.datagridColumnID.Width = 10;
            // 
            // datagridColumnAdv
            // 
            this.datagridColumnAdv.HeaderText = "متن";
            this.datagridColumnAdv.Name = "datagridColumnAdv";
            this.datagridColumnAdv.Visible = false;
            // 
            // datagridColumnBeginDate
            // 
            this.datagridColumnBeginDate.HeaderText = "تاریخ شروع";
            this.datagridColumnBeginDate.Name = "datagridColumnBeginDate";
            // 
            // datagridColumnEndDate
            // 
            this.datagridColumnEndDate.HeaderText = "تاریخ پایان";
            this.datagridColumnEndDate.Name = "datagridColumnEndDate";
            // 
            // datagridColumnImagePath
            // 
            this.datagridColumnImagePath.HeaderText = "مسیر عکس";
            this.datagridColumnImagePath.Name = "datagridColumnImagePath";
            this.datagridColumnImagePath.Visible = false;
            // 
            // datagridColumnBackcolor
            // 
            this.datagridColumnBackcolor.HeaderText = "رنگ پس زمینه";
            this.datagridColumnBackcolor.Name = "datagridColumnBackcolor";
            this.datagridColumnBackcolor.Visible = false;
            // 
            // richTextBox_PreviewAdv
            // 
            this.richTextBox_PreviewAdv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_PreviewAdv.BackColor = System.Drawing.Color.White;
            this.richTextBox_PreviewAdv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_PreviewAdv.Location = new System.Drawing.Point(367, 58);
            this.richTextBox_PreviewAdv.Name = "richTextBox_PreviewAdv";
            this.richTextBox_PreviewAdv.ReadOnly = true;
            this.richTextBox_PreviewAdv.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.richTextBox_PreviewAdv.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox_PreviewAdv.Size = new System.Drawing.Size(355, 261);
            this.richTextBox_PreviewAdv.TabIndex = 27;
            this.richTextBox_PreviewAdv.Text = "";
            this.richTextBox_PreviewAdv.ZoomFactor = 0.16F;
            // 
            // frm_AdvsManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 496);
            this.Controls.Add(this.richTextBox_PreviewAdv);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frm_AdvsManager";
            this.Text = "frm_AdvsManager";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewImageColumn datagridColumnImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn datagridColumnRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn datagridColumnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn datagridColumnAdv;
        private FarsiLibrary.Win.Controls.DataGridViewFADateTimePickerColumn datagridColumnBeginDate;
        private FarsiLibrary.Win.Controls.DataGridViewFADateTimePickerColumn datagridColumnEndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn datagridColumnImagePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn datagridColumnBackcolor;
        private System.Windows.Forms.RichTextBox richTextBox_PreviewAdv;
    }
}