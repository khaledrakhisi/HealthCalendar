using FarsiLibrary.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCalendar
{
    public partial class Form1 : Form
    {

        private BindingSource bs_advs = new BindingSource();
        private BindingSource bs_borders = new BindingSource();

        private frm_editor _frm_editor = null;

        public Form1()
        {
            InitializeComponent();

            bs_advs.PositionChanged += new EventHandler(bs_advs_position_changed_event);
            bs_borders.PositionChanged += new EventHandler(bs_borders_position_changed_event);


            //Init lbl_imageInfo
            //var pos = this.PointToScreen(lbl_ImageInfo.Location);
            //pos = pbox_resultPreview.PointToClient(pos);

            lbl_imageInfo.Parent = pbox_resultPreview;
            lbl_imageInfo.Location = new Point(2, 2);
            lbl_imageInfo.BackColor = Color.Transparent;

            lbl_imageInfo2.Parent = pbox_previewAdv;
            lbl_imageInfo2.Location = new Point(2, 2);
            lbl_imageInfo2.BackColor = Color.Transparent;


            //INIT the combobox
            cmb_themes.Items.Clear();
            cmb_themes.Items.Add("(Automatic)");
            foreach (cls_theme theme in cls_settings.themes)
            {
                cmb_themes.Items.Add(theme.name);
            }
            cmb_themes.Items.Add("Custom Theme...");

            // advs listview items themes

        }

        private void bs_borders_position_changed_event(object sender, EventArgs e)
        {
            try
            {
                cls_utility.advs.Rows[bs_advs.Position]["border_id"] = cls_utility.borders.Rows[bs_borders.Position]["border_id"];

                //cls_utility.DrawBorder(bs_borders.Position);
                DrawEntirePreview();
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in bs_borders_position_changed_event(). " + ex.Message);
            }
        }

        private void bs_advs_position_changed_event(object sender, EventArgs e)
        {
            try
            {
                cls_utility.LoadAdvSettingsFromDB(bs_advs.Position);

                Image image;
                string sImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Res\advs\", cls_utility.advs.Rows[bs_advs.Position]["image"].ToString());
                if (System.IO.File.Exists(sImagePath))
                {
                    // load adv content image
                    using (var stream = File.OpenRead(sImagePath))
                    {
                        image = Image.FromStream(stream);
                    }
                    pbox_advPreview.Image = image;
                }
                else
                {
                    pbox_advPreview.Image = pbox_noImageAvailable.Image;
                }
                DrawEntirePreview();
                InitAdvsControls();
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in bs_advs_position_changed_event(). " + ex.Message);
            }
        }

        private void InitAdvsControls()
        {
            // adv eanbled check box
            chk_enableAdvertisement.Checked = cls_settings.advsEnabled;

            // init advs controls
            trackBar_advAlpha.Value = cls_settings.advAlpha;
            //num_sizeXadv.Value = cls_settings.rectWidthAdv;
            //num_sizeYadv.Value = cls_settings.rectHeightAdv;

            cmb_advBorder.SelectedIndex = bs_borders.Find("border_id", cls_settings.advBorderID);
            pbox_advRectColor.BackColor = Color.FromArgb(255, cls_settings.advRectColor);
            linkLabel_groups.Text = cls_settings.SecurityFilteringAdv;
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            if (btn_browse.Tag == null)
            {
                try
                {
                    Process.Start(textBox1.Text);
                }
                catch (Exception ex)
                {
                    cls_utility.Log("Error in openning pix folder. " + ex.Message);
                }
            }
            else if (btn_browse.Tag.ToString() == "" || btn_browse.Tag.ToString() == string.Empty)
            {
                try
                {
                    Process.Start(textBox1.Text);
                }
                catch (Exception ex)
                {
                    cls_utility.Log("Error in openning pix folder. " + ex.Message);
                }
            }
            else
            {
                folderBrowserDialog1.SelectedPath = cls_settings.imagesPlace;
                folderBrowserDialog1.ShowDialog();
                if (folderBrowserDialog1.SelectedPath != String.Empty)
                {
                    textBox1.Text = folderBrowserDialog1.SelectedPath;
                    cls_settings.imagesPlace = textBox1.Text;
                    cls_utility.GetFilesListAndLoadImage();
                    FillTheListBox();
                }
            }
        }

        private void btn_putRect_Click(object sender, EventArgs e)
        {
            cls_utility.SaveTheImage(0);
        }

        private void SelectRandomTheme()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, cls_settings.themes.Length);
            cls_settings.ApplyTheme(cls_settings.themes[randomNumber]);

            textBox1.Text = cls_settings.imagesPlace;
            trackBar_alpha.Value = cls_settings.nAlpha;
            btn_mainframeColor.BackColor = Color.FromArgb(255, cls_settings.mainRectColor);
            pbox_TodayOccasions.BackColor = Color.FromArgb(255, cls_settings.todayOccasionsRectColor);
            pbox_NextOccasions.BackColor = Color.FromArgb(255, cls_settings.nextOccasionsRectColor);
            pbox_foreColor.BackColor = Color.FromArgb(255, cls_settings.foreColor);
        }

        private void InitAdvsTab()
        {
            try
            {
                // binding borders combo box
                cls_utility.borders = cls_utility.db.Select("borders", "", "border_id");
                bs_borders.DataSource = cls_utility.borders;
                cmb_advBorder.DataSource = bs_borders;
                cmb_advBorder.DisplayMember = "border_name";
                cmb_advBorder.ValueMember = "border_id";

                // binding datagridview
                cls_utility.advs = sinceTodayAdvs(false);
                bs_advs.DataSource = cls_utility.advs;
                PopulateDatagridView();

                cls_utility.LoadAdvSettingsFromDB(bs_advs.Position);
                InitAdvsControls();
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in InitAdvsTab(). " + ex.Message);
            }
        }

        private DataTable sinceTodayAdvs(bool showAll)
        {
            return showAll ? cls_utility.db.Select("advs", "visible = true", "priority, pDateBegin") : cls_utility.db.Select("advs", "(pDateEnd >= '" + cls_utility.persianDate + "') AND (visible = true)", "priority, pDateBegin", "ASC");
        }

        private void UpdateAdvControls()
        {
            //try
            //{
            //    dateTimePicker1.Value = PersianDateTime.Parse(cls_utility.fromNowonAdvs[lvw_Advs.SelectedItems[0].Index].pDateBegin).ToDateTime();
            //    dateTimePicker2.Value = PersianDateTime.Parse(cls_utility.fromNowonAdvs[lvw_Advs.SelectedItems[0].Index].pDateEnd).ToDateTime();
            //    tbx_advImagePath.Text = cls_utility.fromNowonAdvs[lvw_Advs.SelectedItems[0].Index].imageOriginalPath;
            //    lbl_advID.Text = cls_utility.fromNowonAdvs[lvw_Advs.SelectedItems[0].Index].id;

            //    cls_utility.DrawImage(cls_utility.fromNowonAdvs[lvw_Advs.SelectedItems[0].Index].imagePath);

            //    DrawCalendarPreview();

            //    btn_addAdv.Text = "Save Changes";
            //    lvw_Advs.Enabled = false;
            //}
            //catch (Exception ex)
            //{
            //    cls_utility.Log("Error in initializing AdvsTabs Controls. " + ex.Message);
            //}
        }

        private class ListViewItemTheme
        {
            public Color foreColor;
            public Color backColor;
            public ListViewItemTheme(Color f, Color b)
            {
                foreColor = f;
                backColor = b;
            }
        }

        private void PopulateDatagridView()
        {
            // changing PersianDatePicker theme
            try
            {
                datagridColumnBeginDate.Theme = FarsiLibrary.Win.Enums.ThemeTypes.Office2007;
                datagridColumnEndDate.Theme = FarsiLibrary.Win.Enums.ThemeTypes.Office2007;
            }
            catch { }


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bs_advs; // dataset

            try
            {

                dataGridView1.Columns["datagridColumnID"].DataPropertyName = "id";
                dataGridView1.Columns["datagridColumnAdv"].DataPropertyName = "adv";
                dataGridView1.Columns["datagridColumnBeginDate"].DataPropertyName = "pDateBegin";
                dataGridView1.Columns["datagridColumnEndDate"].DataPropertyName = "pDateEnd";
                dataGridView1.Columns["datagridColumnPriority"].DataPropertyName = "priority";
                dataGridView1.Columns["datagridColumnEnabled"].DataPropertyName = "enabled";
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in binding datagridview columns. " + ex.Message);
            }
            //dataGridView1.Columns["datagridColumnImagePath"].DataPropertyName = "image";
            //dataGridView1.Columns["datagridColumnSizeX"].DataPropertyName = "sizeX";
            //dataGridView1.Columns["datagridColumnSizeY"].DataPropertyName = "sizeY";
            //dataGridView1.Columns["datagridColumnPosX"].DataPropertyName = "posX";
            //dataGridView1.Columns["datagridColumnPosY"].DataPropertyName = "posY";
            //dataGridView1.Columns["datagridColumnBackcolor"].DataPropertyName = "backcolor";
            //dataGridView1.Columns["datagridColumnAlpha"].DataPropertyName = "alpha";
            //dataGridView1.Columns["datagridColumnVisible"].DataPropertyName = "visible";

            //// hide the one row that invisible
            //foreach (DataGridViewRow dr in dataGridView1.Rows)
            //{
            //    if (dr.Cells["datagridColumnVisible"].Value.ToString() == "False")
            //    {
            //        dr.Visible = false;
            //    }
            //}            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                cls_utility.Log("Reseting Log file...", false);
            }
            catch { }

            cls_utility.InitSettings();

            num_sizeX.Value = cls_settings.rectWidth;
            num_sizeY.Value = cls_settings.rectHeight;
            lbl_calendarPosition.Text = "X: " + cls_settings.baseX.ToString() + Environment.NewLine + "Y: " + cls_settings.baseY.ToString();

            //Theme Selection
            if (cls_settings.themeID == 0)//0 == Automatic Theme
            {
                SelectRandomTheme();
                cmb_themes.SelectedIndex = 0;
            }
            else if (cls_settings.themeID == -1)//-1 == Custom Theme
            {

            }
            else
            {
                try
                {
                    cmb_themes.SelectedIndex = cmb_themes.FindString(cls_settings.themes[cls_settings.themeID - 1].name);
                }
                catch { }
            }

            cls_utility.GetFilesListAndLoadImage();
            if (!cls_utility.DoTheRoutine())
            {
                DrawEntirePreview();
            }
            FillTheListBox();
            InitAdvsTab();

            UpdateLabelsImageInfo();

            btn_saveSettings.PerformClick();
        }

        private void FillTheListBox()
        {
            try
            {
                //fill the listbox
                if (cls_utility.filesList != null)
                {
                    listBox1.Items.Clear();
                    foreach (string sFileFullName in cls_utility.filesList)
                    {
                        listBox1.Items.Add(Path.GetFileName(sFileFullName));
                    }
                }

                if (cls_settings.pictureName == "" || cls_settings.pictureName == string.Empty || cls_settings.pictureName == null)
                {
                    chk_randomPicture.Checked = true;
                }
                else
                {
                    chk_randomPicture.Checked = false;
                    // Find the item in the list and store the index to the item.
                    int index = listBox1.FindString(Path.GetFileName(cls_settings.pictureName));
                    // Determine if a valid index is returned. Select the item if it is valid.
                    if (index != -1)
                        listBox1.SetSelected(index, true);
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in FillTheListBox(). " + ex.Message);
            }
        }

        private void DrawAdvertisementPreview()
        {
            try
            {
                if (cls_utility.advs != null)
                    if (cls_utility.image != null && cls_utility.advs.Rows.Count > 0)
                    {
                        if (cls_settings.advsEnabled)
                        {
                            int nPos = bs_advs.Position;
                            nPos = nPos == -1 ? 0 : nPos;// choose first record if no record has been selected                            
                            string sFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"res\advs", cls_utility.advs.Rows[nPos]["image"].ToString());
                            int nBorderID = Convert.ToInt32(cls_utility.advs.Rows[nPos]["border_id"]);
                            int ww = 0, hh = 0;
                            bool hasBorder = nBorderID == 0 ? false : true;

                            // draw the background rectangle
                            cls_utility.DrawAdvMainRectangle(hasBorder);

                            // draw the adv content
                            using (Bitmap overlayImage = (Bitmap)Image.FromFile(sFile))
                            {
                                DataRow border = cls_utility.FindBorderByID(nBorderID);
                                Padding padding = new Padding(0);
                                try
                                {
                                    int l = Convert.ToInt32(border["border_leftPadding"]),
                                        h = Convert.ToInt32(border["border_rightPadding"]),
                                        t = Convert.ToInt32(border["border_topPadding"]),
                                        v = Convert.ToInt32(border["border_bottomPadding"]);
                                    padding = new Padding(l, t, h, v);
                                }
                                catch { }
                                cls_utility.DrawImage(overlayImage, padding);
                                ww = overlayImage.Width;
                                hh = overlayImage.Height;
                            }

                            // draw the border
                            cls_utility.DrawBorder(nBorderID, ww, hh);
                        }
                        LoadAdvPreviewClickable(cls_utility.image);
                    }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in DrawAdvertisementPreview()." + ex.Message);
            }
        }

        private void LoadAdvPreviewClickable(Image img)
        {
            try
            {
                pbox_previewAdv.Image = img;
                var imageSize = pbox_advPreview.Image.Size;
                var fitSize = pbox_advPreview.ClientSize;
                pbox_advPreview.SizeMode = imageSize.Width > fitSize.Width || imageSize.Height > fitSize.Height ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.Normal;
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in LoadAdvPreview(). " + ex.Message);
            }
        }

        private void DrawEntirePreview()
        {
            if (cls_utility.image != null)
            {
                cls_utility.LoadImageFromFile(cls_settings.imageFullName);
                DrawCalendarPreview();
                DrawAdvertisementPreview();
            }
        }

        private void DrawCalendarPreview()
        {
            if (cls_utility.image != null)
            {

                cls_utility.DrawMainRectangle();
                cls_utility.DrawMainText();
                if (pbox_resultPreview.Image != null)
                    pbox_resultPreview.Image.Dispose();
                pbox_resultPreview.Image = cls_utility.image;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cls_settings.imageFullName = Path.Combine(cls_settings.imagesPlace, listBox1.SelectedItem.ToString());
                cls_settings.pictureName = cls_settings.imageFullName;
                DrawEntirePreview();
                UpdateLabelsImageInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Loading From ListBox. " + ex.Message);
            }
        }

        private void UpdateLabelsImageInfo()
        {
            try
            {
                lbl_imageInfo.Text = cls_utility.image.Width.ToString() + " x " + cls_utility.image.Height.ToString() + " pixels";
                long length = new System.IO.FileInfo(cls_settings.imagesPlace + "\\" + listBox1.SelectedItem.ToString()).Length;
                lbl_imageInfo.Text += " ---  " + length / 1024 + " KB";

                lbl_imageInfo2.Text = cls_utility.image.Width.ToString() + " x " + cls_utility.image.Height.ToString() + " pixels";
                length = new System.IO.FileInfo(cls_settings.imagesPlace + "\\" + listBox1.SelectedItem.ToString()).Length;
                lbl_imageInfo2.Text += " ---  " + length / 1024 + " KB";
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in UpdateLabelsImageInfo(). " + ex.Message);
            }
        }


        private void pbox_MainFrame_DoubleClick(object sender, EventArgs e)
        {
            colorDialog1.Color = btn_mainframeColor.BackColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cls_settings.mainRectColor = Color.FromArgb(cls_settings.nAlpha, colorDialog1.Color);
                btn_mainframeColor.BackColor = colorDialog1.Color;

                DrawEntirePreview();
            }
        }

        private void pbox_TodayOccasions_DoubleClick(object sender, EventArgs e)
        {
            colorDialog1.Color = pbox_TodayOccasions.BackColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cls_settings.todayOccasionsRectColor = Color.FromArgb(55, colorDialog1.Color);
                pbox_TodayOccasions.BackColor = colorDialog1.Color;

                DrawEntirePreview();
            }
        }

        private void pbox_NextOccasions_DoubleClick(object sender, EventArgs e)
        {
            colorDialog1.Color = pbox_NextOccasions.BackColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cls_settings.nextOccasionsRectColor = Color.FromArgb(55, colorDialog1.Color);
                pbox_NextOccasions.BackColor = colorDialog1.Color;

                DrawEntirePreview();
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (!cls_settings.isSaved)
            {
                DialogResult result = MessageBox.Show("Changes Not Saved. do you want to save ?" + Environment.NewLine + " YES) save and exit." + Environment.NewLine + "NO) exit without save." + Environment.NewLine + "Cancel) remain in the window.", "Settings", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    cls_settings.SaveAllSettings();
                    this.Close();

                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    this.Close();
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                {

                }
            }
            else if (cls_settings.isSaved)
            {
                this.Close();
            }
        }

        private void pbox_MainFrame_Click(object sender, EventArgs e)
        {

        }

        private void num_alpha_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_resetSettings_Click(object sender, EventArgs e)
        {
            cls_settings.LoadDefaultValues(true);

            btn_mainframeColor.BackColor = cls_settings.mainRectColor;
            pbox_TodayOccasions.BackColor = cls_settings.todayOccasionsRectColor;
            pbox_NextOccasions.BackColor = cls_settings.nextOccasionsRectColor;

            pbox_advRectColor.BackColor = cls_settings.advRectColor;

            trackBar_alpha.Value = cls_settings.nAlpha;
            num_sizeX.Value = cls_settings.rectWidth;
            num_sizeY.Value = cls_settings.rectHeight;

            //num_advRectAlpha.Value = cls_settings.advAlpha;
            trackBar_advAlpha.Value = cls_settings.advAlpha;
            //num_sizeXadv.Value = cls_settings.rectWidthAdv;
            //num_sizeYadv.Value = cls_settings.rectHeightAdv;

            DrawEntirePreview();

            lbl_calendarPosition.Text = "X: " + cls_settings.baseX.ToString() + Environment.NewLine + "Y: " + cls_settings.baseY.ToString();
            lbl_advPosition.Text = "X: " + cls_settings.baseXadv.ToString() + Environment.NewLine + "Y: " + cls_settings.baseYadv.ToString();
        }

        private void btn_moveLeft_Click(object sender, EventArgs e)
        {
            cls_settings.baseX++;
            //cls_utility.LoadImageFromFile(cls_settings.imageFullName);
            DrawEntirePreview();

            lbl_calendarPosition.Text = "X: " + cls_settings.baseX.ToString() + Environment.NewLine + "Y: " + cls_settings.baseY.ToString();
        }

        private void btn_moveRight_Click(object sender, EventArgs e)
        {
            cls_settings.baseX--;
            //cls_utility.LoadImageFromFile(cls_settings.imageFullName);
            DrawEntirePreview();

            lbl_calendarPosition.Text = "X: " + cls_settings.baseX.ToString() + Environment.NewLine + "Y: " + cls_settings.baseY.ToString();
        }

        private void btn_moveUp_Click(object sender, EventArgs e)
        {
            cls_settings.baseY--;
            //cls_utility.LoadImageFromFile(cls_settings.imageFullName);
            DrawEntirePreview();

            lbl_calendarPosition.Text = "X: " + cls_settings.baseX.ToString() + Environment.NewLine + "Y: " + cls_settings.baseY.ToString();
        }

        private void btn_moveDown_Click(object sender, EventArgs e)
        {
            cls_settings.baseY++;
            //cls_utility.LoadImageFromFile(cls_settings.imageFullName);
            DrawEntirePreview();

            lbl_calendarPosition.Text = "X: " + cls_settings.baseX.ToString() + Environment.NewLine + "Y: " + cls_settings.baseY.ToString();
        }

        private void btn_saveSettings_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            cls_settings.SaveAllSettings();
            //cls_net.ManipulateGPO("August-HCalendarGPO");

            SaveAdvs();

            Cursor.Current = Cursors.Default;
        }

        private void SaveAdvs()
        {
            //dataGridView1.BeginEdit(true);

            //dataGridView1.CurrentRow.Cells["datagridColumnPosX"].Value = cls_settings.baseXadv;
            //dataGridView1.CurrentRow.Cells["datagridColumnPosY"].Value = cls_settings.baseYadv;            
            //dataGridView1.CurrentRow.Cells["datagridColumnAlpha"].Value = cls_settings.advAlpha;
            //string hexColor = ColorTranslator.ToHtml(cls_settings.advRectColor);
            //dataGridView1.CurrentRow.Cells["datagridColumnBackcolor"].Value = hexColor;

            //DataRow dr = cls_utility.advs.Rows[bs.Position];
            //string hexColor = ColorTranslator.ToHtml(cls_settings.advRectColor);
            //cls_utility.advs.Rows[bs.Position]["backcolor"] = hexColor;
            //cls_utility.advs.Rows[bs.Position]["alpha"] = cls_settings.advAlpha;

            this.Validate();
            bs_advs.EndEdit();
            cls_utility.db.UpdateData(cls_utility.advs);

            //// open excel file
            //cls_excel myExcel = new cls_excel(AppDomain.CurrentDomain.BaseDirectory + @"Res\res.xlsx", 2, false);
            //List<cls_advertisment> advs = myExcel.GetAdvs(3, ">=" + cls_utility.persianDate);

            //myExcel.DeleteEntireRange();

            //myExcel.ClearFilter();

            ////foreach (cls_advertisment adv in cls_utility.fromNowonAdvs)
            ////{
            ////    myExcel.InsertRowAtEnd(adv.id, adv.adv, adv.pDateBegin, adv.pDateEnd, adv.imagePath, adv.imageOriginalPath, adv.backColor.ToString());
            ////} 

            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (row.Cells["datagridColumnAdv"].Value == null) continue;

            //    var advID = row.Cells["datagridColumnID"].Value.ToString();                
            //    var advItself = row.Cells["datagridColumnAdv"].Value.ToString();
            //    var advDateBegin = row.Cells["datagridColumnBeginDate"].Value.ToString();
            //    var advDateEnd = row.Cells["datagridColumnEndDate"].Value.ToString();                
            //    var advImagePath = row.Cells["datagridColumnImagePath"].Value.ToString();
            //    var advBackcolor = row.Cells["datagridColumnBackcolor"].Value.ToString();

            //    myExcel.InsertRowAtEnd(advID, advItself, advDateBegin, advDateEnd, advImagePath, null, advBackcolor);
            //}

            //myExcel.SaveChanges();
            //// close and release excel file
            //myExcel.CloseAndRelease();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //cls_utility.Release();
        }

        private void btn_AdvancedSettings_Click(object sender, EventArgs e)
        {
            using (frm_advanced _frm_advancedSettings = new frm_advanced())
            {
                _frm_advancedSettings.openMode = frm_advanced.OpenModes.ForAdvancedSettings;
                if (_frm_advancedSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DrawEntirePreview();
                }
            }
        }


        private void Collapse_PreviewPanel(bool collapse)
        {
            if (collapse)
            {
                pnl_preview.Top = pnl_settings.Top + pnl_settings.Height;
                pnl_preview.Height = pnl_filesList.Height - pnl_settings.Height;
            }
            else
            {
                pnl_preview.Top = pnl_settings.Top;
                pnl_preview.Height = pnl_filesList.Height;
            }
        }

        private void cmb_themes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_themes.SelectedItem.ToString().ToLower().Contains("custom"))
            {
                Collapse_PreviewPanel(true);
                cls_settings.themeID = -1;
            }
            else if (cmb_themes.SelectedItem.ToString().ToLower().Contains("automatic"))
            {
                Collapse_PreviewPanel(false);
                cls_settings.themeID = 0;
            }
            else
            {
                Collapse_PreviewPanel(false);
                try
                {
                    cls_theme selected_theme = Array.Find(cls_settings.themes, tm => tm.name == cmb_themes.SelectedItem.ToString());
                    cls_settings.ApplyTheme(selected_theme);
                    cls_settings.themeID = cmb_themes.SelectedIndex;

                    btn_mainframeColor.BackColor = cls_settings.mainRectColor;
                    trackBar_alpha.Value = cls_settings.nAlpha;
                    pbox_TodayOccasions.BackColor = cls_settings.todayOccasionsRectColor;
                    pbox_NextOccasions.BackColor = cls_settings.nextOccasionsRectColor;
                    pbox_foreColor.BackColor = cls_settings.foreColor;
                }
                catch (Exception ex)
                {
                    cls_utility.Log("Error in applying selected theme. " + ex.Message);
                }
                DrawEntirePreview();
            }
        }

        private void btn_openCloseFilesList_Click(object sender, EventArgs e)
        {
            if (pnl_filesList.Left < tab_main.Left + tab_main.Width) //if is in spreaded mode
            {
                //then collapse
                pnl_filesList.Left = tab_main.Left + tab_main.Width;
                btn_openCloseFilesList.Text = "«";
            }
            else
            {
                //then spread
                pnl_filesList.Left = this.Width - pnl_filesList.Width;
                btn_openCloseFilesList.Text = "»";
            }
        }

        private void btn_resetPosition_Click(object sender, EventArgs e)
        {
            cls_settings.baseX = cls_settings.default_baseX;
            cls_settings.baseY = cls_settings.default_baseY;
            //cls_utility.LoadImageFromFile(cls_settings.imageFullName);
            DrawEntirePreview();

            lbl_calendarPosition.Text = "X: " + cls_settings.baseX.ToString() + Environment.NewLine + "Y: " + cls_settings.baseY.ToString();
        }

        private void pbox_resultPreview_DoubleClick(object sender, EventArgs e)
        {
            //Theme Selection
            if (cls_settings.themeID == 0 || cls_settings.themeID == -1)//0 == Automatic Theme  -1 == Custom Theme
            {
                SelectRandomTheme();

            }

            DrawEntirePreview();

        }

        private void num_sizeX_ValueChanged(object sender, EventArgs e)
        {
            cls_settings.rectWidth = (int)num_sizeX.Value;
            DrawEntirePreview();
        }

        private void num_sizeY_ValueChanged(object sender, EventArgs e)
        {
            cls_settings.rectHeight = (int)num_sizeY.Value;
            DrawEntirePreview();
        }

        private void btn_about_Click(object sender, EventArgs e)
        {

        }

        private void rdu_disableAdv_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btn_browseAdvImage_Click(object sender, EventArgs e)
        {
            //openFileDialog1.FileName = "";
            //openFileDialog1.Filter = "JPEG format|*.jpg|PNG Format|*.png";
            //openFileDialog1.Multiselect = false;
            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    tbx_advImagePath.Text = openFileDialog1.FileName;                
            //}
        }

        private void btn_addAdv_Click(object sender, EventArgs e)
        {
            //PersianDateTime pDateBegin = new PersianDateTime(dateTimePicker1.Value);
            //PersianDateTime pDateEnd = new PersianDateTime(dateTimePicker2.Value);

            //if (btn_addAdv.Tag.ToString().ToLower().Contains("add")) // add adv
            //{
            //    string newID = Guid.NewGuid().ToString("N");
            //    string newPath = AppDomain.CurrentDomain.BaseDirectory + @"Res\advs\" + Path.GetRandomFileName();
            //    if (tbx_advImagePath.Text == "" || tbx_advImagePath.Text == string.Empty)
            //    {
            //        MessageBox.Show("no image selected", "Adding advs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    cls_utility.fromNowonAdvs.Add(new cls_advertisment()
            //    {
            //        id = newID,
            //        adv = rdu_watermark.Checked ? _frm_editor.sRTF : null,
            //        pDateBegin = pDateBegin.ToString("yyyy/MM/dd"),
            //        pDateEnd = pDateEnd.ToString("yyyy/MM/dd"),
            //        imagePath = newPath.Replace(Path.GetExtension(newPath), ".jpg"),
            //        imageOriginalPath = tbx_advImagePath.Text,
            //        backColor = richTextBoxBackColor.ToArgb().ToString()

            //    });
            //    UpdateAdvListbox();
            //}
            //else if (btn_addAdv.Tag.ToString().ToLower().Contains("save")) // edit adv
            //{
            //    cls_utility.fromNowonAdvs[lvw_Advs.SelectedItems[0].Index].pDateBegin = pDateBegin.ToString("yyyy/MM/dd");
            //    cls_utility.fromNowonAdvs[lvw_Advs.SelectedItems[0].Index].pDateEnd = pDateEnd.ToString("yyyy/MM/dd");
            //    cls_utility.fromNowonAdvs[lvw_Advs.SelectedItems[0].Index].imageOriginalPath = tbx_advImagePath.Text;

            //    tbx_advImagePath.Text = "";
            //    dateTimePicker1.Value = DateTime.Now;
            //    dateTimePicker2.Value = DateTime.Now;
            //    lbl_advID.Text = "";

            //    btn_addAdv.Tag = "Add";
            //    btn_addAdv.Tag = "add";
            //    lvw_Advs.Enabled = true;
            //}
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show("No item selected for delete", "delete adv", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //cls_utility.advs.Rows[bs_advs.Position].
            bs_advs.RemoveCurrent();
            //bs_advs.EndEdit();
            //bs_advs_position_changed_event(sender, e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.BeginEdit(true);
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in Edit dataGridView item. " + ex.Message);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //if (btn_addAdv.Tag.ToString().ToLower().Contains("save")) // edit adv
                //{                    
                //    tbx_advImagePath.Text = "";
                //    lbl_advID.Text = "";

                //    btn_addAdv.Text = "Add";
                //    btn_addAdv.Tag = "add";
                //    //lvw_Advs.Enabled = true;
                //}
            }
        }

        private void rdu_watermark_CheckedChanged(object sender, EventArgs e)
        {
            //pnl_advImage.Visible = false;
            //pnl_watermark.Visible = true;
        }

        private void btn_editAdvWatermark_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItemAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                PersianDateTime pDateBegin = new PersianDateTime(DateTime.Now);
                PersianDateTime pDateEnd = new PersianDateTime(DateTime.Now);
                string newID = Guid.NewGuid().ToString("N");
                string newPath = Path.GetRandomFileName();

                DataRow row = cls_utility.advs.NewRow();
                row["id"] = newID;
                row["adv"] = null;
                row["pDateBegin"] = pDateBegin.ToString("yyyy/MM/dd");
                row["pDateEnd"] = pDateEnd.ToString("yyyy/MM/dd");
                row["image"] = newPath.Replace(Path.GetExtension(newPath), ".png");
                row["sizeX"] = cls_settings.default_rectWidthAdv - 30;
                row["sizeY"] = cls_settings.default_rectHeightAdv - 160;
                row["alpha"] = cls_settings.default_AdvAlpha;
                row["backcolor"] = Color.White.ToArgb().ToString();
                row["border_id"] = cls_settings.default_advBorderID;
                row["posX"] = cls_settings.default_baseXadv;
                row["posY"] = cls_settings.default_baseYadv;
                row["visible"] = true;
                row["securityFiltering"] = "Authenticated Users";
                row["priority"] = cls_settings.default_advPriority;
                row["enabled"] = true;

                cls_utility.advs.Rows.Add(row);

                bs_advs.MoveLast();
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in Appending item. " + ex.Message);
            }
        }

        private void lvw_Advs_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show(lvw_Advs.SelectedItems[0].SubItems[0].Text);
        }



        private void richTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox_PreviewAdv_TextChanged(object sender, EventArgs e)
        {

        }

        private string ConvertToPersianDateFormat(object value)
        {
            string DatePersian = string.Empty;
            try
            {
                DateTime dt;
                if (value is DateTime)
                {
                    dt = (DateTime)value;
                    FarsiLibrary.Utils.PersianDate pd = new PersianDate(dt);
                    DatePersian = pd.ToString("d");
                }
                else if (value is PersianDate)
                {
                    PersianDate pd = (PersianDate)value;
                    DatePersian = pd.ToString("d");
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in converting to persiandate format. " + ex.Message);
            }
            return DatePersian;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                object value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

                string DatePersian = ConvertToPersianDateFormat(value);

                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DatePersian;

                ValidatingDates();
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in validating dataGridView cell. " + ex.Message);
            }
        }

        private void ValidatingDates()
        {
            try
            {
                object beginDate = DateTime.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["datagridColumnBeginDate"].Value.ToString());
                object endDate = DateTime.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["datagridColumnEndDate"].Value.ToString());

                string sDateBegin = ConvertToPersianDateFormat(beginDate);
                string sDateEnd = ConvertToPersianDateFormat(endDate);

                if (sDateBegin.CompareTo(sDateEnd) > 0)
                {
                    MessageBox.Show("تاریخ شروع را از تاریخ پایان بزرگتر در نظر گرفتید به همین دلیل برنامه تاریخ پایان را مساوی مقدار تاریخ شروع تنظیم میکند", "Valiating", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["datagridColumnEndDate"].Value = sDateBegin;
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in ValidatingDates() " + ex.Message);
            }
        }

        private void pbox_advRectColor_Click(object sender, EventArgs e)
        {
            if (bs_advs.Count <= 0)
            {
                return;
            }

            colorDialog1.Color = pbox_advRectColor.BackColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cls_settings.advRectColor = Color.FromArgb(cls_settings.advAlpha, colorDialog1.Color);
                string hexColor = ColorTranslator.ToHtml(cls_settings.advRectColor);
                cls_utility.advs.Rows[bs_advs.Position]["backcolor"] = hexColor;

                pbox_advRectColor.BackColor = colorDialog1.Color;

                DrawEntirePreview();
            }
        }

        private void btn_moveUpAdv_Click(object sender, EventArgs e)
        {
            if (bs_advs.Count <= 0)
            {
                return;
            }

            cls_settings.baseYadv -= 3;
            cls_utility.advs.Rows[bs_advs.Position]["posY"] = cls_settings.baseYadv;

            DrawEntirePreview();

            lbl_advPosition.Text = "X: " + cls_settings.baseXadv.ToString() + Environment.NewLine + "Y: " + cls_settings.baseYadv.ToString();
        }

        private void btn_moveDownAdv_Click(object sender, EventArgs e)
        {
            if (bs_advs.Count <= 0)
            {
                return;
            }

            cls_settings.baseYadv += 3;
            cls_utility.advs.Rows[bs_advs.Position]["posY"] = cls_settings.baseYadv;
            DrawEntirePreview();

            lbl_advPosition.Text = "X: " + cls_settings.baseXadv.ToString() + Environment.NewLine + "Y: " + cls_settings.baseYadv.ToString();
        }

        private void btn_moveLeftAdv_Click(object sender, EventArgs e)
        {
            if (bs_advs.Count <= 0)
            {
                return;
            }

            cls_settings.baseXadv += 3;
            cls_utility.advs.Rows[bs_advs.Position]["posX"] = cls_settings.baseXadv;
            DrawEntirePreview();

            lbl_advPosition.Text = "X: " + cls_settings.baseXadv.ToString() + Environment.NewLine + "Y: " + cls_settings.baseYadv.ToString();
        }

        private void btn_moveRightAdv_Click(object sender, EventArgs e)
        {
            if (bs_advs.Count <= 0)
            {
                return;
            }

            cls_settings.baseXadv -= 3;
            cls_utility.advs.Rows[bs_advs.Position]["posX"] = cls_settings.baseXadv;
            DrawEntirePreview();

            lbl_advPosition.Text = "X: " + cls_settings.baseXadv.ToString() + Environment.NewLine + "Y: " + cls_settings.baseYadv.ToString();
        }

        private void btn_resetPositionadv_Click(object sender, EventArgs e)
        {
            if (bs_advs.Count <= 0)
            {
                return;
            }

            cls_settings.baseXadv = cls_settings.default_baseXadv;
            cls_settings.baseYadv = cls_settings.default_baseYadv;
            cls_utility.advs.Rows[bs_advs.Position]["posX"] = cls_settings.baseXadv;
            cls_utility.advs.Rows[bs_advs.Position]["posY"] = cls_settings.baseYadv;
            DrawEntirePreview();

            lbl_advPosition.Text = "X: " + cls_settings.baseXadv.ToString() + Environment.NewLine + "Y: " + cls_settings.baseYadv.ToString();
        }

        private void num_advRectAlpha_ValueChanged(object sender, EventArgs e)
        {
            cls_settings.advAlpha = trackBar_advAlpha.Value; //Convert.ToInt32(num_advRectAlpha.Value);
            cls_settings.advRectColor = Color.FromArgb(cls_settings.advAlpha, cls_settings.advRectColor.R, cls_settings.advRectColor.G, cls_settings.advRectColor.B);
            DrawEntirePreview();
        }

        private void num_advSizeX_ValueChanged(object sender, EventArgs e)
        {
            //cls_settings.rectWidthAdv = (int)num_sizeXadv.Value;
            //DrawEntirePreview();
        }

        private void num_sizeYadv_ValueChanged(object sender, EventArgs e)
        {
            //cls_settings.rectHeightAdv = (int)num_sizeYadv.Value;
            //DrawEntirePreview();
        }

        private void viewAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (viewAllToolStripMenuItem.Tag.ToString() == "notall")
            {
                cls_utility.advs = sinceTodayAdvs(true);
                bs_advs.DataSource = cls_utility.advs;

                viewAllToolStripMenuItem.Tag = "all";
                viewAllToolStripMenuItem.Text = "عدم نمایش همه";
                dataGridView1.BackgroundColor = Color.LightBlue;
            }
            else if (viewAllToolStripMenuItem.Tag.ToString() == "all")
            {
                cls_utility.advs = sinceTodayAdvs(false);
                bs_advs.DataSource = cls_utility.advs;

                viewAllToolStripMenuItem.Tag = "notall";
                viewAllToolStripMenuItem.Text = "نمایش همه";

                dataGridView1.BackgroundColor = Color.White;
            }
        }


        private void chk_enableAdvertisement_CheckedChanged(object sender, EventArgs e)
        {
            //pnl_advImage.Enabled = !rdu_disableAdv.Checked;
            pnl_manageAdvs.Enabled = chk_enableAdvertisement.Checked;
            pnl_previewAdv.Enabled = chk_enableAdvertisement.Checked;
            pnl_watermark.Enabled = chk_enableAdvertisement.Checked;

            //pnl_advImage.Visible = false;
            //pnl_advImage.Visible = false;
            cls_settings.advsEnabled = chk_enableAdvertisement.Checked;
            DrawEntirePreview();
        }

        private void trackBar_advAlpha_Scroll(object sender, EventArgs e)
        {
            try
            {
                cls_settings.advAlpha = trackBar_advAlpha.Value;
                cls_settings.advRectColor = Color.FromArgb(cls_settings.advAlpha, cls_settings.advRectColor.R, cls_settings.advRectColor.G, cls_settings.advRectColor.B);
                DrawEntirePreview();

                cls_utility.advs.Rows[bs_advs.Position]["alpha"] = cls_settings.advAlpha;
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in trackBar_advAlpha_Scroll(). " + ex.Message);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            cls_settings.nAlpha = trackBar_alpha.Value;
            cls_settings.mainRectColor = Color.FromArgb(cls_settings.nAlpha, cls_settings.mainRectColor.R, cls_settings.mainRectColor.G, cls_settings.mainRectColor.B);
            DrawEntirePreview();
        }

        private void pbox_foreColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = pbox_foreColor.BackColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cls_settings.foreColor = colorDialog1.Color;
                pbox_foreColor.BackColor = colorDialog1.Color;

                DrawEntirePreview();
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

            //_isSortAscending = (_sortColumn == null || _isSortAscending == false);

            //string direction = _isSortAscending ? "ASC" : "DESC";

            //bs.DataSource = _context.MyEntities.OrderBy(
            //   string.Format("it.{0} {1}", column.DataPropertyName, direction)).ToList();

            //if (_sortColumn != null) _sortColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
            //column.HeaderCell.SortGlyphDirection = _isSortAscending ? SortOrder.Ascending : SortOrder.Descending;
            //_sortColumn = column;
        }

        public DataGridViewColumn _sortColumn { get; set; }

        public bool _isSortAscending { get; set; }

        private void pbox_advPreview_MouseMove(object sender, MouseEventArgs e)
        {
            pbox_advPreview.BackColor = Color.LightBlue;
        }

        private void pbox_advPreview_MouseLeave(object sender, EventArgs e)
        {
            pbox_advPreview.BackColor = Color.Transparent;
        }

        private void pbox_advPreview_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
                if (dataGridView1.CurrentCell.RowIndex > -1)
                {
                    _frm_editor = new frm_editor();
                    _frm_editor.sRTF = cls_settings.AdvRTF;
                    _frm_editor.Width = cls_settings.rectWidthAdv + _frm_editor.nWidthDifference;
                    _frm_editor.Height = cls_settings.rectHeightAdv + _frm_editor.nHeightDifference;
                    if (_frm_editor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        cls_utility.advs.Rows[bs_advs.Position]["adv"] = _frm_editor.sRTF;
                        cls_utility.advs.Rows[bs_advs.Position]["sizeX"] = _frm_editor.richTextBox1.Width;
                        cls_utility.advs.Rows[bs_advs.Position]["sizeY"] = _frm_editor.richTextBox1.Height;//_frm_editor.richTextBox1.Height;//_frm_editor.Height - (_frm_editor.Height - _frm_editor.richTextBox1.Height);
                        //int nw = _frm_editor.Height - (_frm_editor.Height - _frm_editor.richTextBox1.Height);

                        string sDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Res\Advs");
                        //create Assets Folder if it doesnt exist.
                        // Determine whether the directory exists.
                        if (!Directory.Exists(sDir))
                        {
                            Directory.CreateDirectory(sDir);
                        }
                        try
                        {
                            string sImageThumbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Res\Advs\" + cls_utility.advs.Rows[bs_advs.Position]["image"].ToString());

                            if (sImageThumbPath != "" && sImageThumbPath != string.Empty && sImageThumbPath != null)
                            {
                                ImageCodecInfo pngEncoder = cls_utility.GetEncoder(ImageFormat.Png);
                                // Create an Encoder object based on the GUID
                                // for the Quality parameter category.
                                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

                                // Create an EncoderParameters object.
                                // An EncoderParameters object has an array of EncoderParameter
                                // objects. In this case, there is only one
                                // EncoderParameter object in the array.
                                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 254L);
                                myEncoderParameters.Param[0] = myEncoderParameter;
                                if (!_frm_editor.doNotMakeTransparentChoosen)
                                    _frm_editor.theImage.MakeTransparent(_frm_editor.richTextBox1.BackColor);
                                _frm_editor.theImage.Save(sImageThumbPath, pngEncoder, myEncoderParameters);

                                pbox_advPreview.Image = _frm_editor.theImage;
                            }
                        }
                        catch (Exception ex)
                        {
                            cls_utility.Log("Error in Saving ADV (" + cls_utility.advs.Rows[bs_advs.Position]["image"].ToString() + "). " + ex.Message);
                        }

                        cls_utility.LoadAdvSettingsFromDB(bs_advs.Position);
                        DrawEntirePreview();
                    }
                }
        }

        private void linkLabel_groups_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (bs_advs.Count <= 0)
            {
                return;
            }

            using (frm_advanced _frm_advancedSettings = new frm_advanced())
            {
                _frm_advancedSettings.openMode = frm_advanced.OpenModes.ForGroupsSelection;
                if (_frm_advancedSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    linkLabel_groups.Text = cls_settings.SecurityFilteringAdv;
                    cls_utility.advs.Rows[bs_advs.Position]["SecurityFiltering"] = cls_settings.SecurityFilteringAdv;
                }
            }
        }

        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PersianDateTime pDateBegin = new PersianDateTime(DateTime.Now);
                PersianDateTime pDateEnd = new PersianDateTime(DateTime.Now);
                string newID = Guid.NewGuid().ToString("N");
                string newPath = Path.GetRandomFileName();

                DataRow row = cls_utility.advs.NewRow();
                row["id"] = newID;
                row["adv"] = cls_utility.advs.Rows[bs_advs.Position]["adv"].ToString();
                row["pDateBegin"] = pDateBegin.ToString("yyyy/MM/dd");
                row["pDateEnd"] = pDateEnd.ToString("yyyy/MM/dd");
                row["image"] = newPath.Replace(Path.GetExtension(newPath), ".png");
                row["sizeX"] = cls_utility.advs.Rows[bs_advs.Position]["sizeX"];
                row["sizeY"] = cls_utility.advs.Rows[bs_advs.Position]["sizeY"];
                row["alpha"] = cls_utility.advs.Rows[bs_advs.Position]["alpha"];
                row["backcolor"] = cls_utility.advs.Rows[bs_advs.Position]["backcolor"];
                row["border_id"] = cls_utility.advs.Rows[bs_advs.Position]["border_id"];
                row["posX"] = cls_utility.advs.Rows[bs_advs.Position]["posX"];
                row["posY"] = cls_utility.advs.Rows[bs_advs.Position]["posY"];
                row["visible"] = true;
                row["securityFiltering"] = cls_utility.advs.Rows[bs_advs.Position]["securityFiltering"];
                row["priority"] = cls_utility.advs.Rows[bs_advs.Position]["priority"];

                cls_utility.advs.Rows.Add(row);

                bs_advs.MoveLast();
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in make clone of item. " + ex.Message);
            }
        }

        private void chk_randomPicture_CheckedChanged(object sender, EventArgs e)
        {

            listBox1.Enabled = !chk_randomPicture.Checked;
            if (chk_randomPicture.Checked)
            {
                cls_settings.pictureName = "";
            }
        }

        private void enabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (bs_advs.Count > 0)
                {
                    cls_settings.advEnabled = !cls_settings.advEnabled;
                    cls_utility.advs.Rows[bs_advs.Position]["enabled"] = cls_settings.advEnabled;
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in enabling/disabling item. " + ex.Message);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow Myrow in dataGridView1.Rows)
                {
                    //Here 2 cell is target value and 1 cell is Volume
                    if (!Convert.ToBoolean(Myrow.Cells["datagridColumnEnabled"].Value))// Or your condition 
                    {
                        Myrow.DefaultCellStyle.BackColor = Color.LightGray;
                        Myrow.DefaultCellStyle.ForeColor = Color.Gray;
                    }
                    else
                    {
                        Myrow.DefaultCellStyle.BackColor = Color.White;
                        Myrow.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in dataGridView1_CellFormatting(). " + ex.Message);
            }
        }

    }  
}
