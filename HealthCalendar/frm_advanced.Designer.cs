namespace HealthCalendar
{
    partial class frm_advanced
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
            this.checkedListBox_groups = new System.Windows.Forms.CheckedListBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.num_hour = new System.Windows.Forms.NumericUpDown();
            this.num_minute = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_selectAll = new System.Windows.Forms.CheckBox();
            this.lbl_groupsStat = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.num_fontSizeSet = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.num_sizeYadv = new System.Windows.Forms.NumericUpDown();
            this.num_sizeXadv = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.pbox_MainFrame = new System.Windows.Forms.PictureBox();
            this.pbox_foreColor = new System.Windows.Forms.PictureBox();
            this.pbox_TodayOccasions = new System.Windows.Forms.PictureBox();
            this.pbox_NextOccasions = new System.Windows.Forms.PictureBox();
            this.pbox_advRectColor = new System.Windows.Forms.PictureBox();
            this.pnl_AdvancedSettings = new System.Windows.Forms.Panel();
            this.pnl_groups = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.num_hour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_minute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_fontSizeSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_sizeYadv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_sizeXadv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_MainFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_foreColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_TodayOccasions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_NextOccasions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_advRectColor)).BeginInit();
            this.pnl_AdvancedSettings.SuspendLayout();
            this.pnl_groups.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox_groups
            // 
            this.checkedListBox_groups.CheckOnClick = true;
            this.checkedListBox_groups.FormattingEnabled = true;
            this.checkedListBox_groups.Location = new System.Drawing.Point(10, 65);
            this.checkedListBox_groups.Name = "checkedListBox_groups";
            this.checkedListBox_groups.Size = new System.Drawing.Size(244, 259);
            this.checkedListBox_groups.TabIndex = 26;
            this.checkedListBox_groups.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_groups_ItemCheck);
            this.checkedListBox_groups.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_groups_SelectedIndexChanged);
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Location = new System.Drawing.Point(321, 360);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(108, 27);
            this.btn_OK.TabIndex = 36;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(436, 360);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(103, 27);
            this.btn_cancel.TabIndex = 35;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Select groups to apply to :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Schedule at :";
            // 
            // num_hour
            // 
            this.num_hour.Enabled = false;
            this.num_hour.Location = new System.Drawing.Point(153, 11);
            this.num_hour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.num_hour.Name = "num_hour";
            this.num_hour.Size = new System.Drawing.Size(37, 20);
            this.num_hour.TabIndex = 41;
            // 
            // num_minute
            // 
            this.num_minute.Enabled = false;
            this.num_minute.Location = new System.Drawing.Point(205, 11);
            this.num_minute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.num_minute.Name = "num_minute";
            this.num_minute.Size = new System.Drawing.Size(37, 20);
            this.num_minute.TabIndex = 42;
            this.num_minute.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.Location = new System.Drawing.Point(192, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 15);
            this.label3.TabIndex = 43;
            this.label3.Text = ":";
            // 
            // chk_selectAll
            // 
            this.chk_selectAll.AutoSize = true;
            this.chk_selectAll.Location = new System.Drawing.Point(13, 42);
            this.chk_selectAll.Name = "chk_selectAll";
            this.chk_selectAll.Size = new System.Drawing.Size(70, 17);
            this.chk_selectAll.TabIndex = 44;
            this.chk_selectAll.Text = "Select All";
            this.chk_selectAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chk_selectAll.UseVisualStyleBackColor = true;
            this.chk_selectAll.CheckedChanged += new System.EventHandler(this.chk_selectAll_CheckedChanged);
            // 
            // lbl_groupsStat
            // 
            this.lbl_groupsStat.AutoSize = true;
            this.lbl_groupsStat.Location = new System.Drawing.Point(197, 46);
            this.lbl_groupsStat.Name = "lbl_groupsStat";
            this.lbl_groupsStat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_groupsStat.Size = new System.Drawing.Size(39, 13);
            this.lbl_groupsStat.TabIndex = 45;
            this.lbl_groupsStat.Text = "groups";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Font Size (entire theme) :";
            // 
            // num_fontSizeSet
            // 
            this.num_fontSizeSet.Location = new System.Drawing.Point(152, 40);
            this.num_fontSizeSet.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.num_fontSizeSet.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.num_fontSizeSet.Name = "num_fontSizeSet";
            this.num_fontSizeSet.Size = new System.Drawing.Size(90, 20);
            this.num_fontSizeSet.TabIndex = 51;
            this.num_fontSizeSet.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.num_fontSizeSet.ValueChanged += new System.EventHandler(this.num_fontSizeSet_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(54, 135);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 13);
            this.label14.TabIndex = 60;
            this.label14.Text = "Y : ";
            this.label14.Visible = false;
            // 
            // num_sizeYadv
            // 
            this.num_sizeYadv.Location = new System.Drawing.Point(81, 128);
            this.num_sizeYadv.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.num_sizeYadv.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.num_sizeYadv.Name = "num_sizeYadv";
            this.num_sizeYadv.Size = new System.Drawing.Size(103, 20);
            this.num_sizeYadv.TabIndex = 59;
            this.num_sizeYadv.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.num_sizeYadv.Visible = false;
            // 
            // num_sizeXadv
            // 
            this.num_sizeXadv.Location = new System.Drawing.Point(81, 106);
            this.num_sizeXadv.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.num_sizeXadv.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.num_sizeXadv.Name = "num_sizeXadv";
            this.num_sizeXadv.Size = new System.Drawing.Size(103, 20);
            this.num_sizeXadv.TabIndex = 58;
            this.num_sizeXadv.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.num_sizeXadv.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(22, 113);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 13);
            this.label15.TabIndex = 57;
            this.label15.Text = "Size    X :";
            this.label15.Visible = false;
            // 
            // pbox_MainFrame
            // 
            this.pbox_MainFrame.BackColor = System.Drawing.Color.Blue;
            this.pbox_MainFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbox_MainFrame.Enabled = false;
            this.pbox_MainFrame.Location = new System.Drawing.Point(96, 300);
            this.pbox_MainFrame.Name = "pbox_MainFrame";
            this.pbox_MainFrame.Size = new System.Drawing.Size(25, 24);
            this.pbox_MainFrame.TabIndex = 61;
            this.pbox_MainFrame.TabStop = false;
            // 
            // pbox_foreColor
            // 
            this.pbox_foreColor.BackColor = System.Drawing.Color.White;
            this.pbox_foreColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbox_foreColor.Enabled = false;
            this.pbox_foreColor.Location = new System.Drawing.Point(189, 300);
            this.pbox_foreColor.Name = "pbox_foreColor";
            this.pbox_foreColor.Size = new System.Drawing.Size(25, 24);
            this.pbox_foreColor.TabIndex = 64;
            this.pbox_foreColor.TabStop = false;
            // 
            // pbox_TodayOccasions
            // 
            this.pbox_TodayOccasions.BackColor = System.Drawing.Color.Green;
            this.pbox_TodayOccasions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbox_TodayOccasions.Enabled = false;
            this.pbox_TodayOccasions.Location = new System.Drawing.Point(127, 300);
            this.pbox_TodayOccasions.Name = "pbox_TodayOccasions";
            this.pbox_TodayOccasions.Size = new System.Drawing.Size(25, 24);
            this.pbox_TodayOccasions.TabIndex = 62;
            this.pbox_TodayOccasions.TabStop = false;
            // 
            // pbox_NextOccasions
            // 
            this.pbox_NextOccasions.BackColor = System.Drawing.Color.Yellow;
            this.pbox_NextOccasions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbox_NextOccasions.Enabled = false;
            this.pbox_NextOccasions.Location = new System.Drawing.Point(65, 300);
            this.pbox_NextOccasions.Name = "pbox_NextOccasions";
            this.pbox_NextOccasions.Size = new System.Drawing.Size(25, 24);
            this.pbox_NextOccasions.TabIndex = 63;
            this.pbox_NextOccasions.TabStop = false;
            // 
            // pbox_advRectColor
            // 
            this.pbox_advRectColor.BackColor = System.Drawing.Color.Blue;
            this.pbox_advRectColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbox_advRectColor.Enabled = false;
            this.pbox_advRectColor.Location = new System.Drawing.Point(159, 300);
            this.pbox_advRectColor.Name = "pbox_advRectColor";
            this.pbox_advRectColor.Size = new System.Drawing.Size(25, 24);
            this.pbox_advRectColor.TabIndex = 65;
            this.pbox_advRectColor.TabStop = false;
            // 
            // pnl_AdvancedSettings
            // 
            this.pnl_AdvancedSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_AdvancedSettings.Controls.Add(this.label4);
            this.pnl_AdvancedSettings.Controls.Add(this.pbox_advRectColor);
            this.pnl_AdvancedSettings.Controls.Add(this.label2);
            this.pnl_AdvancedSettings.Controls.Add(this.pbox_foreColor);
            this.pnl_AdvancedSettings.Controls.Add(this.num_hour);
            this.pnl_AdvancedSettings.Controls.Add(this.pbox_TodayOccasions);
            this.pnl_AdvancedSettings.Controls.Add(this.num_minute);
            this.pnl_AdvancedSettings.Controls.Add(this.pbox_NextOccasions);
            this.pnl_AdvancedSettings.Controls.Add(this.label3);
            this.pnl_AdvancedSettings.Controls.Add(this.pbox_MainFrame);
            this.pnl_AdvancedSettings.Controls.Add(this.num_fontSizeSet);
            this.pnl_AdvancedSettings.Controls.Add(this.label14);
            this.pnl_AdvancedSettings.Controls.Add(this.label15);
            this.pnl_AdvancedSettings.Controls.Add(this.num_sizeYadv);
            this.pnl_AdvancedSettings.Controls.Add(this.num_sizeXadv);
            this.pnl_AdvancedSettings.Location = new System.Drawing.Point(276, 6);
            this.pnl_AdvancedSettings.Name = "pnl_AdvancedSettings";
            this.pnl_AdvancedSettings.Size = new System.Drawing.Size(268, 342);
            this.pnl_AdvancedSettings.TabIndex = 66;
            // 
            // pnl_groups
            // 
            this.pnl_groups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_groups.Controls.Add(this.checkedListBox_groups);
            this.pnl_groups.Controls.Add(this.label1);
            this.pnl_groups.Controls.Add(this.chk_selectAll);
            this.pnl_groups.Controls.Add(this.lbl_groupsStat);
            this.pnl_groups.Location = new System.Drawing.Point(2, 6);
            this.pnl_groups.Name = "pnl_groups";
            this.pnl_groups.Size = new System.Drawing.Size(268, 342);
            this.pnl_groups.TabIndex = 67;
            // 
            // frm_advanced
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(551, 399);
            this.Controls.Add(this.pnl_groups);
            this.Controls.Add(this.pnl_AdvancedSettings);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_advanced";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Settings";
            this.Load += new System.EventHandler(this.frm_advanced_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_hour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_minute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_fontSizeSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_sizeYadv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_sizeXadv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_MainFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_foreColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_TodayOccasions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_NextOccasions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_advRectColor)).EndInit();
            this.pnl_AdvancedSettings.ResumeLayout(false);
            this.pnl_AdvancedSettings.PerformLayout();
            this.pnl_groups.ResumeLayout(false);
            this.pnl_groups.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_groups;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown num_hour;
        private System.Windows.Forms.NumericUpDown num_minute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chk_selectAll;
        private System.Windows.Forms.Label lbl_groupsStat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown num_fontSizeSet;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown num_sizeYadv;
        private System.Windows.Forms.NumericUpDown num_sizeXadv;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pbox_MainFrame;
        private System.Windows.Forms.PictureBox pbox_foreColor;
        private System.Windows.Forms.PictureBox pbox_TodayOccasions;
        private System.Windows.Forms.PictureBox pbox_NextOccasions;
        private System.Windows.Forms.PictureBox pbox_advRectColor;
        private System.Windows.Forms.Panel pnl_AdvancedSettings;
        private System.Windows.Forms.Panel pnl_groups;
    }
}