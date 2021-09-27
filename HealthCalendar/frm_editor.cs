using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
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
    public partial class frm_editor : Form
    {
        public string sRTF;
        public Bitmap theImage = null;
        public int nHeightDifference;
        public int nWidthDifference;
        public bool doNotMakeTransparentChoosen = false;
        //private Image image_separator = null;

        private PersianDateTime persianNow = PersianDateTime.Now;
        private string persianFullDate = string.Empty;
        private string persianDate = string.Empty;

        // definition of TEXTFORMAT class
        public class TextFormat
        {
            public Color foreColor;
            public Color backColor;
            public Font font;

            public TextFormat(Color fColor, Color bColor, Font fnt)
            {
                foreColor = fColor;
                backColor = bColor;
                font = fnt;
            }
        }
        private TextFormat textFormat = null;

        public frm_editor()
        {
            
            InitializeComponent();

            nHeightDifference = (this.Height - richTextBox1.Height);
            nWidthDifference = (this.Width - richTextBox1.Width);

            try
            {
                persianNow = PersianDateTime.Now;
                persianFullDate = persianNow.ToString("dddd d MMMM yyyy");
                persianDate = persianNow.ToString(PersianDateTimeFormat.Date);
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in Initializing PersianDate. " + ex.Message);
            }



            pbox_icon1.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon1.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon1.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon2.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon2.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon2.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon3.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon3.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon3.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon4.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon4.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon4.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon5.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon5.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon5.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon6.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon6.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon6.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon7.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon7.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon7.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon8.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon8.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon8.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon9.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon9.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon9.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon10.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon10.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon10.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon11.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon11.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon11.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon12.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon12.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon12.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon13.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon13.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon13.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon14.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon14.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon14.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);

            pbox_icon15.Click += new EventHandler(pbox_icon_click_event);
            pbox_icon15.MouseMove += new MouseEventHandler(pbox_icon_mousemove_event);
            pbox_icon15.MouseLeave += new EventHandler(pbox_icon_mouseleave_event);



            // seperators
            pbox_sep1.Click += new EventHandler(pbox_sep_click_event);
            pbox_sep1.MouseMove += new MouseEventHandler(pbox_sep_mousemove_event);
            pbox_sep1.MouseLeave += new EventHandler(pbox_sep_mouseleave_event);

            pbox_sep2.Click += new EventHandler(pbox_sep_click_event);
            pbox_sep2.MouseMove += new MouseEventHandler(pbox_sep_mousemove_event);
            pbox_sep2.MouseLeave += new EventHandler(pbox_sep_mouseleave_event);

            pbox_sep3.Click += new EventHandler(pbox_sep_click_event);
            pbox_sep3.MouseMove += new MouseEventHandler(pbox_sep_mousemove_event);
            pbox_sep3.MouseLeave += new EventHandler(pbox_sep_mouseleave_event);

            pbox_sep4.Click += new EventHandler(pbox_sep_click_event);
            pbox_sep4.MouseMove += new MouseEventHandler(pbox_sep_mousemove_event);
            pbox_sep4.MouseLeave += new EventHandler(pbox_sep_mouseleave_event);

            pbox_sep5.Click += new EventHandler(pbox_sep_click_event);
            pbox_sep5.MouseMove += new MouseEventHandler(pbox_sep_mousemove_event);
            pbox_sep5.MouseLeave += new EventHandler(pbox_sep_mouseleave_event);

            pbox_sep6.Click += new EventHandler(pbox_sep_click_event);
            pbox_sep6.MouseMove += new MouseEventHandler(pbox_sep_mousemove_event);
            pbox_sep6.MouseLeave += new EventHandler(pbox_sep_mouseleave_event);

            pbox_sep7.Click += new EventHandler(pbox_sep_click_event);
            pbox_sep7.MouseMove += new MouseEventHandler(pbox_sep_mousemove_event);
            pbox_sep7.MouseLeave += new EventHandler(pbox_sep_mouseleave_event);

            pbox_sep8.Click += new EventHandler(pbox_sep_click_event);
            pbox_sep8.MouseMove += new MouseEventHandler(pbox_sep_mousemove_event);
            pbox_sep8.MouseLeave += new EventHandler(pbox_sep_mouseleave_event);

            pbox_sep9.Click += new EventHandler(pbox_sep_click_event);
            pbox_sep9.MouseMove += new MouseEventHandler(pbox_sep_mousemove_event);
            pbox_sep9.MouseLeave += new EventHandler(pbox_sep_mouseleave_event);

            pbox_sep10.Click += new EventHandler(pbox_sep_click_event);
            pbox_sep10.MouseMove += new MouseEventHandler(pbox_sep_mousemove_event);
            pbox_sep10.MouseLeave += new EventHandler(pbox_sep_mouseleave_event);

            pbox_clipart1.Click += new EventHandler(pbox_clipart1_click_event);
            pbox_clipart1.MouseMove += new MouseEventHandler(pbox_clipart1_mousemove_event);
            pbox_clipart1.MouseLeave += new EventHandler(pbox_clipart1_mouseleave_event);
        }

        private void pbox_clipart1_mouseleave_event(object sender, EventArgs e)
        {
            PictureBox pbox = sender as PictureBox;
            //pbox_icon.BackColor = Color.White;
            pbox.BorderStyle = BorderStyle.None;
        }

        private void pbox_clipart1_mousemove_event(object sender, MouseEventArgs e)
        {
            PictureBox pbox = sender as PictureBox;
            //pbox_icon.BackColor = Color.White;
            pbox.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pbox_clipart1_click_event(object sender, EventArgs e)
        {
            PictureBox pbox_clipart = sender as PictureBox;

            pnl_separators.Visible = false;

            embedImage(pbox_clipart.Image);
        }        

        private void pbox_icon_mouseleave_event(object sender, EventArgs e)
        {
            PictureBox pbox_icon = sender as PictureBox;
            //pbox_icon.BackColor = Color.White;
            pbox_icon.BorderStyle = BorderStyle.None;
        }

        private void pbox_icon_mousemove_event(object sender, EventArgs e)
        {
            PictureBox pbox_icon = sender as PictureBox;
            //pbox_icon.BackColor = Color.LightBlue;
            pbox_icon.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pbox_icon_click_event(object sender, EventArgs e)
        {
            PictureBox pbox_icon = sender as PictureBox;

            pnl_separators.Visible = false;

            embedImage(pbox_icon.Image);
        }

        private void pbox_sep_click_event(object sender, EventArgs e)
        {
            PictureBox pbox_sep = sender as PictureBox;

            pnl_separators.Visible = false;

            embedImage(pbox_sep.Image);
        }

        private void pbox_sep_mouseleave_event(object sender, EventArgs e)
        {
            PictureBox pbox_sep = sender as PictureBox;
            //pbox_icon.BackColor = Color.LightBlue;
            pbox_sep.BorderStyle = BorderStyle.None;
        }

        private void pbox_sep_mousemove_event(object sender, MouseEventArgs e)
        {
            PictureBox pbox_sep = sender as PictureBox;
            //pbox_icon.BackColor = Color.LightBlue;
            pbox_sep.BorderStyle = BorderStyle.FixedSingle;
        }        

        private void frm_editor_Load(object sender, EventArgs e)
        {            
            List<string> fonts = new List<string>();
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                fonts.Add(font.Name);
            }

            cmb_fontFaces.DataSource = fonts;

            todateFormat1ToolStripMenuItem.Text = " درج " + persianFullDate;
            todateFormat2ToolStripMenuItem.Text = " درج " + persianDate;

            richTextBox1.Rtf = sRTF;
            //this.Width = sizeX;
            //this.Height = sizeY;

            try
            {
                // Create an instance of HookProc.
                MouseHookProcedure = new HookProc(MouseHookProc);

                hHook = SetWindowsHookEx(WH_MOUSE,
                            MouseHookProcedure,
                            (IntPtr)0,
                            AppDomain.GetCurrentThreadId());

                //If the SetWindowsHookEx function fails.
                if (hHook == 0)
                {
                    MessageBox.Show("SetWindowsHookEx Failed");
                    return;
                }
            }
            catch { }

            chk_copyFormat.Appearance = Appearance.Button;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            //RtfToPicture2 r = new RtfToPicture2();
            ////r.RTFtoPictureBox(ref richTextBox1, ref pictureBox1);
            //Bitmap bitmap = new Bitmap(300, 450);
            //r.RTFtoBitmap(ref richTextBox1, ref bitmap);
            //pictureBox1.Invalidate();
            //var g = Graphics.FromImage(bmp);
            //pictureBox1_Paint(sender, new PaintEventArgs(g, bmp.GetBounds());
            //bmp.Save("D:\\rtfImage1.png", ImageFormat.Png);
            //// now we create a 4x larger one:
            //Rectangle rect2 = new Rectangle(0, 0, sz.Width * 4, sz.Height * 4);
            //Bitmap bmp2 = new Bitmap(rect2.Width, rect2.Height);
            //// we need to temporarily enlarge the panel:
            //panel1.ClientSize = rect2.Size;
            //// now we can let the routine draw
            //panel1.DrawToBitmap(bmp2, rect2);
            //// and before saving we optionally can set the dpi resolution
            //bmp2.SetResolution(300, 300);
            //// save text always as png; jpg is only for fotos!
            //bmp2.Save("D:\\rtfImage2.png", ImageFormat.Png);
            // restore the panels size
            //panel1.ClientSize = sz; 


            //if (cmb_templates.SelectedIndex != -1)
            //{
            //    richTextBox1.BackColor = Color.Black;
            //    richTextBox1.SelectAll();
            //    richTextBox1.SelectionColor = Color.White;
            //}

            SaveChanges();
        }        

        private void btn_backColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.BackColor = colorDialog1.Color;
            }
        }

        private void btn_textFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = richTextBox1.SelectionFont;
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void btn_textbackColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.SelectionBackColor = colorDialog1.Color;
            }
        }

        private void btn_textForeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void btn_leftAlign_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void btn_centerAlign_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void btn_rightAlign_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void btn_bold_Click(object sender, EventArgs e)
        {            
            try
            {
                Font new1, old1;
                old1 = richTextBox1.SelectionFont;
                if (old1 != null)
                {
                    if (old1.Bold)
                        new1 = new Font(old1, old1.Style & ~FontStyle.Bold);
                    else
                        new1 = new Font(old1, old1.Style | FontStyle.Bold);

                    richTextBox1.SelectionFont = new1;
                }
                else
                {
                    SetFontOption(richTextBox1, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("TEXTEDITOR ERROR: " + ex.Message);
            }
            richTextBox1.Focus();
        }

        private void btn_italic_Click(object sender, EventArgs e)
        {
            try
            {
                Font new1, old1;
                old1 = richTextBox1.SelectionFont;
                if (old1 != null)
                {
                    if (old1.Italic)
                        new1 = new Font(old1, old1.Style & ~FontStyle.Italic);
                    else
                        new1 = new Font(old1, old1.Style | FontStyle.Italic);

                    richTextBox1.SelectionFont = new1;
                }
                else
                {
                    SetFontOption(richTextBox1, FontStyle.Italic);
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("TEXTEDITOR ERROR: " + ex.Message);
            }
            richTextBox1.Focus();
        }

        private void btn_underline_Click(object sender, EventArgs e)
        {
            Font new1, old1;
            old1 = richTextBox1.SelectionFont;
            if (old1 != null)
            {
                if (old1.Underline)
                    new1 = new Font(old1, old1.Style & ~FontStyle.Underline);
                else
                    new1 = new Font(old1, old1.Style | FontStyle.Underline);

                richTextBox1.SelectionFont = new1;
            }
            else
            {
                SetFontOption(richTextBox1, FontStyle.Underline);
            }
            
            richTextBox1.Focus();
        }

        private void btn_undo_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void btn_redo_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void SetFontOption(RichTextBox rtb, float size)
        {
            try
            {
                int selectionStart = rtb.SelectionStart;
                int selectionLength = rtb.SelectionLength;
                int selectionEnd = selectionStart + selectionLength;
                for (int x = selectionStart; x < selectionEnd; ++x)
                {
                    // Set temporary selection
                    rtb.Select(x, 1);
                    // Toggle font style of the selection   
                    rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, size, rtb.SelectionFont.Style);
                }
                // Restore the original selection
                rtb.Select(selectionStart, selectionLength);
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in SetFontOption(). " + ex.Message);
            }
        }
        private void SetFontOption(RichTextBox rtb, FontStyle style)
        {
            try
            {
                int selectionStart = rtb.SelectionStart;
                int selectionLength = rtb.SelectionLength;
                int selectionEnd = selectionStart + selectionLength;
                for (int x = selectionStart; x < selectionEnd; ++x)
                {
                    // Set temporary selection
                    rtb.Select(x, 1);
                    // Toggle font style of the selection
                    FontStyle new1;
                    
                    if ((rtb.SelectionFont.Style & style) == FontStyle.Bold)
                        new1 = rtb.SelectionFont.Style & ~FontStyle.Bold;
                    else if ((rtb.SelectionFont.Style & style) == FontStyle.Italic)
                        new1 = rtb.SelectionFont.Style & ~FontStyle.Italic;
                    else if ((rtb.SelectionFont.Style & style) == FontStyle.Underline)
                        new1 = rtb.SelectionFont.Style & ~FontStyle.Underline;
                    else
                        new1 = rtb.SelectionFont.Style | style;
                    rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size, new1);
                }
                // Restore the original selection
                rtb.Select(selectionStart, selectionLength);
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in SetFontOption(). " + ex.Message);
            }
        }
        private void SetFontOption(RichTextBox rtb, FontFamily family)
        {
            try
            {
                int selectionStart = rtb.SelectionStart;
                int selectionLength = rtb.SelectionLength;
                int selectionEnd = selectionStart + selectionLength;
                for (int x = selectionStart; x < selectionEnd; ++x)
                {
                    // Set temporary selection
                    rtb.Select(x, 1);                    
                    rtb.SelectionFont = new Font(family, rtb.SelectionFont.Size, rtb.SelectionFont.Style);
                }
                // Restore the original selection
                rtb.Select(selectionStart, selectionLength);
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in SetFontOption(). " + ex.Message);
            }
        }

        private void ApplyFontSizeFromCombobox()
        {
            int num;
            if (!Int32.TryParse(cmb_fontSize.Text.Trim(), out num))
            {
                MessageBox.Show("Invalid Font Size, it must be Float Number", "Invalid Font Size", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmb_fontSize.Text = "10";
                return;
            }
            if (cmb_fontSize.Text.Trim() != "")
            {
                try
                {
                    Font new1, old1;
                    old1 = richTextBox1.SelectionFont;
                    if (old1 != null)
                    {
                        new1 = new Font(old1.FontFamily, float.Parse(cmb_fontSize.Text.Trim()), old1.Style);
                        richTextBox1.SelectionFont = new1;
                    }
                    else
                    {
                        SetFontOption(richTextBox1, float.Parse(cmb_fontSize.Text.Trim()));
                    }
                }
                catch (Exception ex)
                {
                    cls_utility.Log("TEXTEDITOR ERROR: " + ex.Message);
                }
            }
            richTextBox1.Focus();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//13 == Enter
            {
                ApplyFontSizeFromCombobox();
            }
        }

        private void cmb_fontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFontSizeFromCombobox();
        }

        private void ApplyFontFamilyFromCombobox()
        {
            try
            {
                Font new1, old1;
                old1 = richTextBox1.SelectionFont;
                if (old1 != null)
                {
                    new1 = new Font(cmb_fontFaces.Text, old1.Size, old1.Style);
                    richTextBox1.SelectionFont = new1;
                }
                else
                {
                    SetFontOption(richTextBox1, new FontFamily(cmb_fontFaces.Text));
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("TEXTEDITOR ERROR: " + ex.Message);
            }
            richTextBox1.Focus();
        }

        private void cmb_fontFaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFontFamilyFromCombobox();
        }

        private void cmb_fontFaces_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//13 == Enter
            {
                ApplyFontFamilyFromCombobox();
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            

            try
            {
                cmb_fontFaces.Text = richTextBox1.SelectionFont.FontFamily.Name;
                cmb_fontSize.Text = richTextBox1.SelectionFont.Size.ToString();                
            }
            catch (Exception ex)
            {
                cls_utility.Log("TEXTEDITOR ERROR: " + ex.Message);
            }
        }


        /// <summary>
        /// INSERTING IMAGE
        /// </summary>

        private enum EmfToWmfBitsFlags
        {
            EmfToWmfBitsFlagsDefault = 0x00000000,
            EmfToWmfBitsFlagsEmbedEmf = 0x00000001,
            EmfToWmfBitsFlagsIncludePlaceable = 0x00000002,
            EmfToWmfBitsFlagsNoXORClip = 0x00000004
        };

        private struct RtfFontFamilyDef
        {
            public const string Unknown = @"\fnil";
            public const string Roman = @"\froman";
            public const string Swiss = @"\fswiss";
            public const string Modern = @"\fmodern";
            public const string Script = @"\fscript";
            public const string Decor = @"\fdecor";
            public const string Technical = @"\ftech";
            public const string BiDirect = @"\fbidi";
        }

        [DllImport("gdiplus.dll")]
        private static extern uint GdipEmfToWmfBits(IntPtr _hEmf,
          uint _bufferSize, byte[] _buffer,
          int _mappingMode, EmfToWmfBitsFlags _flags);


        private string GetFontTable(Font font)
        {
            var fontTable = new StringBuilder();
            // Append table control string
            fontTable.Append(@"{\fonttbl{\f0");
            fontTable.Append(@"\");
            var rtfFontFamily = new HybridDictionary();
            rtfFontFamily.Add(FontFamily.GenericMonospace.Name, RtfFontFamilyDef.Modern);
            rtfFontFamily.Add(FontFamily.GenericSansSerif, RtfFontFamilyDef.Swiss);
            rtfFontFamily.Add(FontFamily.GenericSerif, RtfFontFamilyDef.Roman);
            rtfFontFamily.Add("UNKNOWN", RtfFontFamilyDef.Unknown);

            // If the font's family corresponds to an RTF family, append the
            // RTF family name, else, append the RTF for unknown font family.
            fontTable.Append(rtfFontFamily.Contains(font.FontFamily.Name) ? rtfFontFamily[font.FontFamily.Name] : rtfFontFamily["UNKNOWN"]);
            // \fcharset specifies the character set of a font in the font table.
            // 0 is for ANSI.
            fontTable.Append(@"\fcharset0 ");
            // Append the name of the font
            fontTable.Append(font.Name);
            // Close control string
            fontTable.Append(@";}}");
            return fontTable.ToString();
        }

        private string GetImagePrefix(Image _image)
        {
            float xDpi, yDpi;
            var rtf = new StringBuilder();
            using (Graphics graphics = CreateGraphics())
            {
                xDpi = graphics.DpiX;
                yDpi = graphics.DpiY;
            }
            // Calculate the current width of the image in (0.01)mm
            var picw = (int)Math.Round((_image.Width / xDpi) * 2540);
            // Calculate the current height of the image in (0.01)mm
            var pich = (int)Math.Round((_image.Height / yDpi) * 2540);
            // Calculate the target width of the image in twips
            var picwgoal = (int)Math.Round((_image.Width / xDpi) * 1440);
            // Calculate the target height of the image in twips
            var pichgoal = (int)Math.Round((_image.Height / yDpi) * 1440);
            // Append values to RTF string
            rtf.Append(@"{\pict\wmetafile8");
            rtf.Append(@"\picw");
            rtf.Append(picw);
            rtf.Append(@"\pich");
            rtf.Append(pich);
            rtf.Append(@"\picwgoal");
            rtf.Append(picwgoal);
            rtf.Append(@"\pichgoal");
            rtf.Append(pichgoal);
            rtf.Append(" ");

            return rtf.ToString();
        }

        private string getRtfImage(Image image)
        {
            // Used to store the enhanced metafile
            MemoryStream stream = null;
            // Used to create the metafile and draw the image
            Graphics graphics = null;
            // The enhanced metafile
            Metafile metaFile = null;
            try
            {
                var rtf = new StringBuilder();
                stream = new MemoryStream();
                // Get a graphics context from the RichTextBox
                using (graphics = CreateGraphics())
                {
                    // Get the device context from the graphics context
                    IntPtr hdc = graphics.GetHdc();
                    // Create a new Enhanced Metafile from the device context
                    metaFile = new Metafile(stream, hdc);
                    // Release the device context
                    graphics.ReleaseHdc(hdc);
                }

                // Get a graphics context from the Enhanced Metafile
                using (graphics = Graphics.FromImage(metaFile))
                {
                    //graphics.SmoothingMode = SmoothingMode.HighQuality;
                    //graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    // Draw the image on the Enhanced Metafile
                    graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
                }

                // Get the handle of the Enhanced Metafile
                IntPtr hEmf = metaFile.GetHenhmetafile();
                // A call to EmfToWmfBits with a null buffer return the size of the
                // buffer need to store the WMF bits.  Use this to get the buffer
                // size.
                uint bufferSize = GdipEmfToWmfBits(hEmf, 0, null, 8, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
                // Create an array to hold the bits
                var buffer = new byte[bufferSize];
                // A call to EmfToWmfBits with a valid buffer copies the bits into the
                // buffer an returns the number of bits in the WMF.  
                uint _convertedSize = GdipEmfToWmfBits(hEmf, bufferSize, buffer, 8, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
                // Append the bits to the RTF string
                foreach (byte t in buffer)
                {
                    rtf.Append(String.Format("{0:X2}", t));
                }
                return rtf.ToString();
            }
            finally
            {
                if (graphics != null)
                    graphics.Dispose();
                if (metaFile != null)
                    metaFile.Dispose();
                if (stream != null)
                    stream.Close();
            }
        }
        private void embedImage(Image img)
        {
            var rtf = new StringBuilder();

            // Append the RTF header
            rtf.Append(@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033");
            // Create the font table using the RichTextBox's current font and append
            // it to the RTF string
            rtf.Append(GetFontTable(this.Font));
            // Create the image control string and append it to the RTF string
            rtf.Append(GetImagePrefix(img));
            // Create the Windows Metafile and append its bytes in HEX format
            rtf.Append(getRtfImage(img));
            // Close the RTF image control string
            rtf.Append(@"}");
            richTextBox1.SelectedRtf = rtf.ToString();

           
        }

        private bool CheckIfImage(string filename)
        {
            var valids = new[] { ".jpeg", ".jpg", ".png", ".ico", ".gif", ".bmp", ".emp", ".wmf", ".tiff" };
            return valids.Contains(System.IO.Path.GetExtension(filename));
        }

        private void btn_seperatorHorizontal_Click(object sender, EventArgs e)
        {
            pnl_separators.Left = btn_seperatorHorizontal.Left;
            pnl_separators.Top = btn_seperatorHorizontal.Top + btn_seperatorHorizontal.Height;
            pnl_separators.Visible = !pnl_separators.Visible;

            //if (CheckIfImage(openFileDialog2.FileName.ToLower()) == true)
            //embedImage(pbox_sep1.Image);
            //else
            //    MessageBox.Show("Invalid Image File Selected");
        }

        private void todateFormat1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectedText = persianFullDate;
            }
            catch (Exception ex)
            {
                cls_utility.Log("TEXTEDITOR ERROR: " + ex.Message);
            }
        }

        private void todateFormat2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectedText = persianDate;
            }
            catch (Exception ex)
            {
                cls_utility.Log("TEXTEDITOR ERROR: " + ex.Message);
            }
        }

        private void btn_todate_Click(object sender, EventArgs e)
        {
            Point p = new Point();
            p.X = btn_todate.Left;
            p.Y = btn_todate.Top + btn_todate.Height;
            contextMenuStripDateFormat.Show(PointToScreen(p));
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.Clear(richTextBox1.BackColor);
            //e.Graphics.DrawRtfText(this.richTextBox1.Rtf, this.pictureBox1.ClientRectangle);            
            //Bitmap newBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);            
            //Graphics g = Graphics.FromImage(newBitmap);

            //g.SmoothingMode = SmoothingMode.HighQuality;
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            //g.InterpolationMode = InterpolationMode.High;
            //g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //g.Clear(richTextBox1.BackColor);
            //g.DrawRtfText(this.richTextBox1.Rtf, this.pictureBox1.ClientRectangle);


            //base.OnPaint(e);


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.Clear(richTextBox1.BackColor);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Padding pad = new Padding(10, 0, -10, 0);  // pick your own numbers!
            Size sz = panel1.ClientSize;
            Rectangle rect = new Rectangle(pad.Left, pad.Top,
                                           sz.Width - pad.Horizontal, sz.Height - pad.Vertical);

            e.Graphics.DrawRtfText(this.richTextBox1.Rtf, rect);
        }

        private void btn_insertImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                embedImage(Image.FromFile(openFileDialog1.FileName));
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }



        // Panel hiding

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        //Declare the hook handle as an int.
        static int hHook = 0;

        //Declare the mouse hook constant.
        //For other hook types, you can obtain these values from Winuser.h in the Microsoft SDK.
        public const int WH_MOUSE = 7;

        //Mouse up message.
        private const int WM_LBUTTONUP = 0x0202;

        //Declare MouseHookProcedure as a HookProc type.
        HookProc MouseHookProcedure;

        //Declare the wrapper managed POINT class.
        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;
        }

        //Declare the wrapper managed MouseHookStruct class.
        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        //This is the Import for the SetWindowsHookEx function.
        //Use this function to install a thread-specific hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn,
        IntPtr hInstance, int threadId);

        //This is the Import for the UnhookWindowsHookEx function.
        //Call this function to uninstall the hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //This is the Import for the CallNextHookEx function.
        //Use this function to pass the hook information to the next hook procedure in chain.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode,
        IntPtr wParam, IntPtr lParam);

        public int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //Marshall the data from the callback.
            MouseHookStruct MyMouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));

            if (nCode >= 0 && wParam.ToInt32() == WM_LBUTTONUP)
            {
                //Get the mouse position.
                Point mousePosition = this.PointToClient(Control.MousePosition);
                //Get the child control under the mouse position.
                Control ctrl = this.GetChildAtPoint(mousePosition);
                //Hide the Panel if it is visible and the mouse position is not over the panel.
                if (ctrl != this.pnl_separators && pnl_separators.Visible == true)
                    pnl_separators.Visible = false;
            }

            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        private void frm_editor_Resize(object sender, EventArgs e)
        {
            
        }

        private void frm_editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sRTF != richTextBox1.Rtf && this.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                DialogResult result = MessageBox.Show("Changes Not Saved. do you want to save ?" + Environment.NewLine + " YES) save and exit." + Environment.NewLine + "NO) exit without save." + Environment.NewLine + "Cancel) remain in the window.", "Settings", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveChanges();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void SaveChanges()
        {
            try
            {
                panel1.Size = richTextBox1.Size;

                sRTF = richTextBox1.Rtf;

                Size sz = panel1.ClientSize;
                // first we (optionally) create a bitmap in the original panel size:
                Rectangle rect1 = panel1.ClientRectangle;
                theImage = new Bitmap(rect1.Width, rect1.Height);
                panel1.DrawToBitmap(theImage, rect1);

            }catch(Exception ex)
            {
                cls_utility.Log("Error in save changes of adv text editor. " + ex.Message);
            }
        }

        private void btn_defaultSize_Click(object sender, EventArgs e)
        {
            this.Width = cls_settings.default_rectWidthAdv;
            this.Height = cls_settings.default_rectHeightAdv;
        }

        private void chk_copyFormat_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_copyFormat.Checked)
            {
                textFormat = new TextFormat(richTextBox1.SelectionColor, richTextBox1.SelectionBackColor, richTextBox1.SelectionFont);
            }
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (chk_copyFormat.Checked)
                {
                    chk_copyFormat.Checked = false;
                    richTextBox1.SelectionColor = textFormat.foreColor;
                    richTextBox1.SelectionBackColor = textFormat.backColor;
                    richTextBox1.SelectionFont = textFormat.font;
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("TEXTEDITOR ERROR: Error in Applying Copied Format at richTextBox1_SelectionChanged(). " + ex.Message);
            }

        }

        private void cmb_templates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_templates.SelectedIndex == 0)
            {
                doNotMakeTransparentChoosen = true;
            }
            else
            {
                doNotMakeTransparentChoosen = true;
            }
        }

        
    }
}
