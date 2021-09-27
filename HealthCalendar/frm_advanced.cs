using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCalendar
{
    public partial class frm_advanced : Form
    {
        public enum OpenModes
        {
            ForAdvancedSettings=0,
            ForGroupsSelection
        }
        private OpenModes _openMode = OpenModes.ForAdvancedSettings;
        public OpenModes openMode
        {
            get { return _openMode; }
            set { _openMode = value; }
        }

        public frm_advanced()
        {
            InitializeComponent();
        }

        private void frm_advanced_Load(object sender, EventArgs e)
        {
            // Loading values into controls

            if (_openMode == OpenModes.ForAdvancedSettings)
            {
                pnl_AdvancedSettings.Enabled = true;
                pnl_groups.Enabled = false;
            }
            else if (_openMode == OpenModes.ForGroupsSelection)
            {
                pnl_AdvancedSettings.Enabled = false;
                pnl_groups.Enabled = true;
            }

            // fill check list box

            checkedListBox_groups.DataSource = cls_network.ListAllDomainGroups();//new List<string> { "Edari", "Mali", "khanevadeh", "mohit", "pishgiri", "amwal", "behvarzi", "jafal", "badrani", "om el ghazlan" };
            // then add User-Selection Groups
            try
            {
                string[] sGroups = cls_settings.SecurityFilteringAdv.Split(new char[] { ';' });                                

                foreach (string sGroup in sGroups)
                {
                    if (sGroup.ToLower().Contains("authenticated"))
                    {
                        chk_selectAll.Checked = true;
                        break;
                    }
                    checkedListBox_groups.SetItemChecked(checkedListBox_groups.Items.IndexOf(sGroup), true);
                }

                lbl_groupsStat.Text = checkedListBox_groups.CheckedItems.Count.ToString() + "/" + checkedListBox_groups.Items.Count.ToString();
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in Selecting Saved Groups. " + ex.Message);
            }

            // fill schedule time
            try
            {
                string[] sClockTime = cls_settings.scheduleTime.Split(':');
                num_hour.Value = Convert.ToInt32(sClockTime[0]);
                num_minute.Value = Convert.ToInt32(sClockTime[1]);
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in Filling scheduleTime Controls.(Advanced Settings Form) " + ex.Message);
            }

            // fill Font Size
            try
            {                
                num_fontSizeSet.Value = Convert.ToInt32(cls_settings.fontSize);                
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in Filling FontSize Controls.(Advanced Settings Form) " + ex.Message);
            }

        }

        private void chk_selectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= checkedListBox_groups.Items.Count - 1; i++)
            {
                checkedListBox_groups.SetItemChecked(i, chk_selectAll.Checked);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (_openMode == OpenModes.ForGroupsSelection)
            {
                string sGroups = null;

                foreach (object item in this.checkedListBox_groups.CheckedItems)
                {
                    if (sGroups != null && sGroups != string.Empty)
                        sGroups += ";";
                    sGroups += Convert.ToString(item);
                }

                if (sGroups == null || sGroups == string.Empty)
                {
                    sGroups = "(non)";
                }
                if (checkedListBox_groups.Items.Count == checkedListBox_groups.CheckedItems.Count)
                {
                    sGroups = "Authenticated Users";
                }
                // security filtering            
                cls_settings.SecurityFiltering = sGroups;
                cls_settings.SecurityFilteringAdv = sGroups;
            }
            else if (_openMode == OpenModes.ForAdvancedSettings)
            {

                // schedule time
                try
                {
                    cls_settings.scheduleTime = DateTime.Parse(num_hour.Value.ToString() + ":" + num_minute.Value.ToString()).ToString("HH:mm");
                }
                catch (Exception ex)
                {
                    cls_utility.Log("Error in storing time in ScheduleTime Property of Settings Class. " + ex.Message);
                }

                // Font Size
                try
                {
                    cls_settings.fontSize = (Int32)num_fontSizeSet.Value;
                }
                catch (Exception ex)
                {
                    cls_utility.Log("Error in storing FontSizeSet in FontSize Property of Settings Class. " + ex.Message);
                }
            }
        }

        private void checkedListBox_groups_SelectedIndexChanged(object sender, EventArgs e)
        {            
        }

        private void checkedListBox_groups_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            lbl_groupsStat.Text = checkedListBox_groups.CheckedItems.Count.ToString() + "/" + checkedListBox_groups.Items.Count.ToString();
        }

        private void num_fontSizeSet_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
