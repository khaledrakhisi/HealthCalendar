using Microsoft.GroupPolicy;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCalendar
{

    static class cls_utility
    {
        /// <summary>
        /// image to manipulate
        /// </summary>
        public static Image image;
        public static string[] filesList;

        public static cls_database db = null;
        //public static DataSet ds = new DataSet();

        //public static List<cls_calendar> pastOccasions;
        //public static List<cls_calendar> todayOccasions;
        //public static List<cls_calendar> nextOccasions;
        //public static List<cls_advertisment> fromNowonAdvs;

        private static DataTable pastOccasions = null;
        private static DataTable todayOccasions = null;
        private static DataTable nextOccasions = null;
        public static DataTable advs = null;
        public static DataTable borders = null;

        private static PersianDateTime persianNow = PersianDateTime.Now;
        private static string persianFullDate = string.Empty;
        public static string persianDate = string.Empty;        
       
        //Settings        
        private static int scaleWidth;
        private static int scaleHeight;
        private static float newX, newY, newImageWidth, newImageHeight, newRectWidth, newRectHeight;

        public static int scaleWidthAdv;
        public static int scaleHeightAdv;
        private static float newXadv, newYadv, newImageWidthAdv, newImageHeightAdv;
        public static int newRectWidthAdv, newRectHeightAdv;

        //private static cls_excel myExcel = null;

        static cls_utility()
        {
            db = new cls_database(AppDomain.CurrentDomain.BaseDirectory + @"Res\res.accdb");
            // just to initiat advs datatable to prevent 'not set to an instance...' exception
            advs = db.Select("advs", "visible = false");

            
        }

        
        public static Bitmap BytesToImage(byte[] bytes)
        {
            // Convert a byte array into an image.
            using (MemoryStream image_stream = new MemoryStream(bytes))
            {
                Bitmap bm = new Bitmap(image_stream);
                image_stream.Close();
                return bm;
            }
        }

        public static Color InvertColor(Color sourceColor)
        {
            return Color.FromArgb(255 - sourceColor.R,
                                  255 - sourceColor.G,
                                  255 - sourceColor.B);
        }

        public static void Log(string sLog, bool append = true, string sFileFullName = "")
        {
            if (sFileFullName == "")
            {
                sFileFullName = AppDomain.CurrentDomain.BaseDirectory + @"HCalLog.txt";
            }
            using (StreamWriter sw = new StreamWriter(sFileFullName, append))
            {
                sw.WriteLine(sLog);
            }
        }

        
        public static void DrawImage(Bitmap overlayImage, Padding padding)
        {
            
            try
            {
                //using (Bitmap overlayImage = (Bitmap)Image.FromFile(sImagePath))
                //var finalImage = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                using (var graphics = Graphics.FromImage(image))
                {
                    graphics.CompositingMode = CompositingMode.SourceOver;

                    // Create rectangle for displaying image.
                    Rectangle destRect = new Rectangle((int)(newImageWidthAdv - newRectWidthAdv - newXadv - cls_settings.padding + padding.Left),
                                                       (int)(newYadv + (cls_settings.padding) + padding.Top),
                                                       (int)newRectWidthAdv + padding.Horizontal,
                                                       (int)newRectHeightAdv + padding.Vertical);


                    //ColorMatrix cm = new ColorMatrix();
                    //cm.Matrix33 = cls_settings.nAlpha / 100F;
                    //ImageAttributes ia = new ImageAttributes();
                    //ia.SetColorMatrix(cm);
                    
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(overlayImage, destRect, 0, 0, overlayImage.Width, overlayImage.Height, GraphicsUnit.Pixel);
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in put overlay image. " + ex.Message);
            }
        }

        private static SizeF MeasureText(string sText, Font font)
        {            
            Bitmap textBmp = null;
            Graphics textBmpG = null;            
            SizeF stringSize = new SizeF();
            textBmp = new Bitmap((int)newRectWidth, (int)newRectHeight);
            textBmpG = Graphics.FromImage(textBmp);
            // Measure string.
            stringSize = textBmpG.MeasureString(sText, font);

            return stringSize;
        }

        private static float DrawTextOnImage(string sText, Font font, float lineNumber, string sAlignment, bool hasBackground, Color foreColor, Color backgroundColor, bool hasTextShadow, bool hasBorder = false, float backgroundPadding = 0F, float verticalPadding = 0F, float horizontalPadding = 0f)
        {
            Bitmap textBmp = null;
            Graphics textBmpG = null;
            StringFormat stringFormat = null;
            Graphics origImgG = null;
            SizeF stringSize = new SizeF();

            try
            {
                font = new Font(font.Name, (newRectWidth / cls_settings.rectWidth) * font.Size / 6, font.Style);

                lineNumber = --lineNumber < 0 ? 0 : Math.Abs(lineNumber);

                textBmp = new Bitmap((int)newRectWidth, (int)newRectHeight);
                textBmpG = Graphics.FromImage(textBmp);
                // Measure string.
                stringSize = new SizeF();
                stringSize = textBmpG.MeasureString(sText, font);

                float xAlignment = 0;
                if (sAlignment.ToLower() == "right")
                {
                    xAlignment = newRectWidth - stringSize.Width - backgroundPadding;

                }
                else if (sAlignment.ToLower() == "center")
                {
                    xAlignment = newRectWidth / 2 - (stringSize.Width / 2);

                }
                else if (sAlignment.ToLower() == "left")
                {
                    xAlignment = 0 + backgroundPadding;
                }

                textBmpG.SmoothingMode = SmoothingMode.HighQuality;
                textBmpG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                textBmpG.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //draw the background
                if (hasBackground)
                {
                    SolidBrush brush = new SolidBrush(backgroundColor);
                    textBmpG.FillRectangle(brush, new RectangleF(backgroundPadding, (stringSize.Height * lineNumber) + verticalPadding, newRectWidth - (backgroundPadding * 2), stringSize.Height));
                }
                if (hasBorder)
                {
                    Pen brush = new Pen(Color.White);
                    textBmpG.DrawRectangle(brush, backgroundPadding, (stringSize.Height * lineNumber) + verticalPadding, newRectWidth - (backgroundPadding * 2), stringSize.Height);
                }

                SolidBrush foreBrush = new SolidBrush(foreColor);
                stringFormat = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                stringFormat.Alignment = StringAlignment.Far;

                if (hasTextShadow)
                {
                    //draw the text shadow
                    textBmpG.DrawString(sText, font, Brushes.Black, new PointF(xAlignment + horizontalPadding + 2, (stringSize.Height * lineNumber) + 2 + verticalPadding), stringFormat);
                }
                //draw the text
                textBmpG.DrawString(sText, font, foreBrush, new PointF(xAlignment + horizontalPadding, (stringSize.Height * lineNumber) + verticalPadding), stringFormat);

                origImgG = Graphics.FromImage(image);
                origImgG.DrawImage(textBmp, new RectangleF(newImageWidth - newX - newRectWidth - cls_settings.padding, newY + cls_settings.padding, newRectWidth, newRectHeight)
                                            , new RectangleF(0, 0, (int)newRectWidth, (int)newRectHeight)
                                            , GraphicsUnit.Pixel);


            }
            catch
            {
                cls_utility.Log("Draw text on image error ");
            }
            finally
            {
                stringFormat.Dispose();
                textBmp.Dispose();
                textBmpG.Dispose();
                origImgG.Dispose();
            }

            return (stringSize.Height)/(newRectHeight/10);

        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static string PopulateNumberToString(int number)
        {
            return String.Format("{0:000}", number);
        }

        public static void SaveTheImage(int augustHCalendar_index)
        {
            string sDir =Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets");
            //create Assets Folder if it doesnt exist.
            // Determine whether the directory exists.
            if (!Directory.Exists(sDir))
            {
                Directory.CreateDirectory(sDir);
            }

            //string newFileName = cls_settings.imageFullName;
            string sFileNameOnly = cls_settings.sOutputImageFileName + PopulateNumberToString(augustHCalendar_index) + Path.GetExtension(cls_settings.imageFullName);
            //newFileName = newFileName.Replace(oldFileName, sNewName);

            cls_utility.Log("Saving image to : " + Path.Combine(sDir, sFileNameOnly));
            try
            {
                ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                // Create an Encoder object based on the GUID
                // for the Quality parameter category.
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.
                // An EncoderParameters object has an array of EncoderParameter
                // objects. In this case, there is only one
                // EncoderParameter object in the array.
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 98L);
                myEncoderParameters.Param[0] = myEncoderParameter;


                sDir = Path.Combine(sDir, sFileNameOnly);
                image.Save(sDir, jgpEncoder, myEncoderParameters);
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in saveing the asset image. " + ex.Message);
            }
        }

        public static void DrawRectangle(float x, float y, float width, float height, bool isFilled, Color fillOrBorderColor, int borderThickness = 1)
        {

            using (Graphics g = Graphics.FromImage(image))
            {
                SolidBrush shadowBrush = new SolidBrush(fillOrBorderColor);
                Pen pen = new Pen(fillOrBorderColor, borderThickness);

                // Create array of rectangles.
                RectangleF[] rects = { new RectangleF(x, y, width, height) };

                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                if (isFilled)
                {

                    g.FillRectangles(shadowBrush, rects);

                }
                else
                {

                    g.DrawRectangles(pen, rects);

                }
                g.Dispose();
            }
            
        }

        public static DataRow FindBorderByID(int border_id)
        {
            DataColumn[] keyColumns = new DataColumn[1];
            keyColumns[0] = cls_utility.borders.Columns["border_id"];
            cls_utility.borders.PrimaryKey = keyColumns;
            DataRow r = cls_utility.borders.Rows.Find(border_id);
            if (r != null)
            {
                return r;
            }
            return null;
        }

        public static void DrawBorder(int border_id, int ww, int hh)
        {
            try
            {
                DataRow r = FindBorderByID(border_id);
                if (r != null)
                {
                    string sBorderImageFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"res\borders", r["border_image"].ToString());

                    // Load the image which should be scaled with the set up parameters
                    using (Image border = Image.FromFile(sBorderImageFullPath))
                    {
                        // Create the preview image which will be displayed in the picture box
                        Bitmap preview = new Bitmap(ww, hh);
                        Graphics gr = Graphics.FromImage(image);

                        // Disable smoothing of stretched drawings (like in Windows)
                        gr.InterpolationMode = InterpolationMode.NearestNeighbor;
                        gr.PixelOffsetMode = PixelOffsetMode.Half;


                        // Draw the image with source rectangle if it is defined

                        Padding padding = new Padding(0);
                        try
                        {
                            int l = Convert.ToInt32(r["border_leftMargin"]),
                                h = Convert.ToInt32(r["border_rightMargin"]),
                                t = Convert.ToInt32(r["border_topMargin"]),
                                v = Convert.ToInt32(r["border_bottomMargin"]);
                            padding = new Padding(l, t, h, v);
                        }
                        catch { }

                        gr.DrawImageWithPadding(border, new Rectangle(
                            new Point((int)(newImageWidthAdv - newRectWidthAdv - newXadv - cls_settings.padding), (int)(newYadv + (cls_settings.padding))), new Size(newRectWidthAdv, newRectHeightAdv)),//preview.Size),
                            new Rectangle(0, 0, border.Width, border.Height),
                            new Padding(150, 150, 150, 150));
                        //}
                        //else
                        //{
                        //    gr.DrawImageWithPadding(image, new Rectangle(Point.Empty, preview.Size),
                        //        new Padding((int)_nmLeft.Value, (int)_nmTop.Value, (int)_nmRight.Value, (int)_nmBottom.Value));
                        //}

                        // load border image
                        //using (var stream = File.OpenRead(sBorderImageFullPath))
                        //{
                        //    Image border = Image.FromStream(stream);

                        //    Padding padding = new Padding(0);
                        //    try
                        //    {
                        //        int l = Convert.ToInt32(r["border_leftMargin"]), 
                        //            h = Convert.ToInt32(r["border_rightMargin"]), 
                        //            t = Convert.ToInt32(r["border_topMargin"]), 
                        //            v = Convert.ToInt32(r["border_bottomMargin"]);
                        //        padding = new Padding(l, t, h, v);
                        //    }
                        //    catch { }

                        //    cls_utility.DrawImage((Bitmap)border, padding);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in DrawBorder(). " + ex.Message);
            }
        }

        public static void DrawMainText()
        {
            float lineY = 1F;
            string sText = String.Empty;
            Font stringFont = null;

            Color backgroundColor = Color.FromArgb(80, cls_settings.mainRectColor);
            Color foreColor = cls_settings.foreColor;
            try
            {                
                // Set up TITLE line
                sText = " امروز " + persianFullDate;
                stringFont = new Font("B Mitra", cls_settings.fontSize);
                DrawTextOnImage(sText, stringFont, lineY, "center", true, foreColor, backgroundColor, cls_settings.hasTextShadow);
                
                //PAST OCCASIONS
                lineY += .2f;
                if (pastOccasions != null){
                    for (int i = pastOccasions.Rows.Count-1; i >= 0; i--)
                    {
                        lineY += 1.2f;
                        sText = pastOccasions.Rows[i]["Occasion"].ToString();
                        string sDate = string.Empty;
                        if (pastOccasions.Rows[i]["pDateEnd"].ToString() != pastOccasions.Rows[i]["pDateBegin"].ToString())
                        {
                            try{
                                sDate = PersianDateTime.Parse(pastOccasions.Rows[i]["pDateBegin"].ToString()).Day.ToString() + " الی " +
                                    PersianDateTime.Parse(pastOccasions.Rows[i]["pDateEnd"].ToString()).Day.ToString() + " " +
                                    PersianDateTime.Parse(pastOccasions.Rows[i]["pDateBegin"].ToString()).ToString("MMMM");
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                sDate = PersianDateTime.Parse(pastOccasions.Rows[i]["pDateBegin"].ToString()).ToString("d MMMM");
                            }
                            catch { }
                        }
                      
                        stringFont = new Font("B Mitra", cls_settings.fontSize - 3);
                        backgroundColor = Color.FromArgb(70, cls_settings.nextOccasionsRectColor);
                        DrawTextOnImage(sText, stringFont, lineY, "right", true, foreColor, backgroundColor, cls_settings.hasTextShadow, false, newRectWidth / 20, (3*i)); // newRectWidth / 30  + (10 * i) write occation
                        DrawTextOnImage(sDate, stringFont, lineY, "left", false, foreColor, backgroundColor, cls_settings.hasTextShadow, false, newRectWidth / 20 , (3*i)); //write date                        
                    }
                }
                
                //TODAY OCCASIONS                
                if (todayOccasions != null)
                {
                    lineY -= .4f;                    
                    for (int i = 0; i < todayOccasions.Rows.Count; i++)
                    {
                        sText = todayOccasions.Rows[i]["Occasion"].ToString();
                        string sDate = string.Empty;
                        if (todayOccasions.Rows[i]["pDateEnd"].ToString() != todayOccasions.Rows[i]["pDateBegin"].ToString())
                        {
                            try
                            {
                                sDate = PersianDateTime.Parse(todayOccasions.Rows[i]["pDateBegin"].ToString()).Day.ToString() + " الی " +
                                    PersianDateTime.Parse(todayOccasions.Rows[i]["pDateEnd"].ToString()).Day.ToString() + " " +
                                    PersianDateTime.Parse(todayOccasions.Rows[i]["pDateBegin"].ToString()).ToString("MMMM");
                            }
                            catch { }
                        }

                        stringFont = new Font("B Mitra", cls_settings.fontSize);
                        backgroundColor = Color.FromArgb(70, cls_settings.todayOccasionsRectColor);
                        lineY += 1f;
                        if (sDate != string.Empty)
                        {
                            DrawTextOnImage(sText, stringFont, lineY, "right", true, foreColor, backgroundColor, cls_settings.hasTextShadow, true, newRectWidth / 60, (5 * i));
                            DrawTextOnImage(sDate, stringFont, lineY, "left", false, foreColor, backgroundColor, cls_settings.hasTextShadow, true, newRectWidth / 60, (5 * i));
                        }
                        else
                        {
                            DrawTextOnImage(sText, stringFont, lineY, "center", true, foreColor, backgroundColor, cls_settings.hasTextShadow, true, newRectWidth / 60, (5 * i));
                        }
                    }
                }

                if (todayOccasions.Rows.Count <= 0)
                {
                    // Set up line 2
                    lineY += 1f;
                    sText = "______ مناسبت های بعدی ______";
                    stringFont = new Font("B Nazanin", cls_settings.fontSize - 3);
                    DrawTextOnImage(sText, stringFont, lineY, "center", false, foreColor, Color.Blue, cls_settings.hasTextShadow);
                    lineY -= .4f;
                }
                
                //NEXT OCCASIONS
                if (nextOccasions != null)
                {
                    lineY += .2f;                    
                    for (int i = 0; i < nextOccasions.Rows.Count; i++)
                    {
                        sText = nextOccasions.Rows[i]["Occasion"].ToString();

                        string sDate = string.Empty;
                        string sWeekDay = string.Empty;
                        if (nextOccasions.Rows[i]["pDateEnd"].ToString() != nextOccasions.Rows[i]["pDateBegin"].ToString())
                        {
                            try
                            {
                                sDate = PersianDateTime.Parse(nextOccasions.Rows[i]["pDateBegin"].ToString()).Day.ToString() + " الی " +
                                    PersianDateTime.Parse(nextOccasions.Rows[i]["pDateEnd"].ToString()).Day.ToString() + " " +
                                    PersianDateTime.Parse(nextOccasions.Rows[i]["pDateBegin"].ToString()).ToString("MMMM");
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                sDate = PersianDateTime.Parse(nextOccasions.Rows[i]["pDateBegin"].ToString()).ToString("dddd d MMMM");
                                sWeekDay = PersianDateTime.Parse(nextOccasions.Rows[i]["pDateBegin"].ToString()).ToString("dddd");
                            }
                            catch { }
                        }
                        ////stringFont = new Font("B Mitra", cls_settings.fontSize);
                        ////SizeF textSize = MeasureText(sDate, stringFont);
                        //stringFont = new Font("B Mitra", cls_settings.fontSize );
                        //DrawTextOnImage(sWeekDay, stringFont, lineY, "left", false, foreColor, backgroundColor, cls_settings.hasTextShadow, false, newRectWidth / 30, (newRectHeight / newImageHeight * i), MeasureText(sDate, new Font("B Mitra", cls_settings.fontSize)).Width + MeasureText(sWeekDay, new Font("B Mitra", cls_settings.fontSize)).Width * (newImageWidth)/newRectWidth); //write date
                        lineY += 1f;
                        stringFont = new Font("B Mitra", cls_settings.fontSize);
                        backgroundColor = Color.FromArgb(70, cls_settings.nextOccasionsRectColor);
                        DrawTextOnImage(sText, stringFont, lineY, "right", true, foreColor, backgroundColor, cls_settings.hasTextShadow, false, newRectWidth / 30, (5 * i)); //write occation                                                
                        DrawTextOnImage(sDate, stringFont, lineY, "left", false, foreColor, backgroundColor, cls_settings.hasTextShadow, false, newRectWidth / 30, (5 * i)); //newRectHeight / newImageHeight write date                        
                    }
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in DrawMainText ." + ex.Message);
            }
        }

        public static void DrawMainRectangle()
        {
            try
            {
                //calc
                var imageWidth = image.Width;
                var imageHeight = image.Height;
                scaleWidth = imageWidth / cls_settings.baseWidth;
                scaleHeight = imageHeight / cls_settings.baseHeight;
                newX = cls_settings.baseX * scaleWidth;
                newY = cls_settings.baseY * scaleHeight;
                newImageWidth = cls_settings.baseWidth * scaleWidth;
                newImageHeight = cls_settings.baseHeight * scaleHeight;
                newRectWidth = cls_settings.rectWidth * scaleWidth;
                newRectHeight = cls_settings.rectHeight * scaleHeight;
                cls_settings.padding = 5 * scaleWidth;

                //draw the rectangle
                //Color customColor = Color.FromArgb(55, Color.Blue);
                Color customColor = cls_settings.mainRectColor;
                DrawRectangle(newImageWidth - newRectWidth - newX - cls_settings.padding, //horizontal end of image (makes rect right-to-left)
                                                            newY + cls_settings.padding,
                                                            newRectWidth,
                                                            newRectHeight, true, customColor);

                //draw the border
                customColor = Color.White;
                DrawRectangle(newImageWidth - newRectWidth - newX - cls_settings.padding, //horizontal end of image (makes rect right-to-left)
                                                            newY + cls_settings.padding,
                                                            newRectWidth,
                                                            newRectHeight, false, customColor);
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in draw rectangle. " + ex.Message);
            }
        }

        public static void DrawAdvMainRectangle(bool hasBorder)
        {
            try
            {
                //calc
                var imageWidth = image.Width;
                var imageHeight = image.Height;
                scaleWidthAdv = imageWidth / cls_settings.baseWidthAdv;
                scaleHeightAdv = imageHeight / cls_settings.baseHeightAdv;
                newXadv = cls_settings.baseXadv * scaleWidthAdv;
                newYadv = cls_settings.baseYadv * scaleHeightAdv;
                newImageWidthAdv = cls_settings.baseWidthAdv * scaleWidthAdv;
                newImageHeightAdv = cls_settings.baseHeightAdv * scaleHeightAdv;
                newRectWidthAdv = (cls_settings.rectWidthAdv / 9) * scaleWidthAdv;
                newRectHeightAdv = (cls_settings.rectHeightAdv / 7) * scaleHeightAdv;
                cls_settings.padding = 5 * scaleWidthAdv;

                //draw the rectangle
                //Color customColor = Color.FromArgb(55, Color.Blue);
                Color customColor = cls_settings.advRectColor;
                DrawRectangle(newImageWidthAdv - newRectWidthAdv - newXadv - cls_settings.padding, //horizontal end of image (makes rect right-to-left)
                                                            newYadv + cls_settings.padding,
                                                            newRectWidthAdv,
                                                            newRectHeightAdv, true, customColor);

                if (hasBorder)
                {
                    //draw the border
                    customColor = Color.Black;
                    DrawRectangle(newImageWidthAdv - newRectWidthAdv - newXadv - cls_settings.padding, //horizontal end of image (makes rect right-to-left)
                                                                newYadv + cls_settings.padding,
                                                                newRectWidthAdv,
                                                                newRectHeightAdv, false, customColor, 3);
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in drawing adv rectangle. " + ex.Message);
            }
        }

        public static void InitSettings()
        {

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

            cls_network.ShareAFolder(Environment.MachineName, AppDomain.CurrentDomain.BaseDirectory + @"assets", "assets");
            cls_network.SetFolderPermissions(AppDomain.CurrentDomain.BaseDirectory + @"assets", System.Security.Principal.WellKnownSidType.AuthenticatedUserSid, System.Security.AccessControl.FileSystemRights.ReadAndExecute, System.Security.AccessControl.AccessControlType.Allow);
            //cls_utility.SetFolderPermissions(AppDomain.CurrentDomain.BaseDirectory + @"assets", System.Security.Principal.WellKnownSidType.AuthenticatedUserSid, System.Security.AccessControl.FileSystemRights.Modify, System.Security.AccessControl.AccessControlType.Deny);
            //cls_utility.SetFolderPermissions(AppDomain.CurrentDomain.BaseDirectory + @"assets", System.Security.Principal.WellKnownSidType.AuthenticatedUserSid, System.Security.AccessControl.FileSystemRights.Write, System.Security.AccessControl.AccessControlType.Deny);

            try
            {
                //Reading the settings
                cls_settings.ReadAllSettings();                
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in loading default settings. " + ex.Message);
            }

            if (cls_settings.imagesPlace == null || cls_settings.imagesPlace == string.Empty)
            {
                cls_settings.LoadDefaultValues();
                cls_settings.SaveAllSettings();
                //cls_net.ManipulateGPO(sGPOMainName);
                //cls_utility.ShareAFolder(Environment.MachineName, AppDomain.CurrentDomain.BaseDirectory + @"\assets", "assets");
                cls_utility.Log("No settings where found. defaults will be set.");
            }
        }        

        public static bool DoTheRoutine()
        {            
            // get past occasions
            pastOccasions = db.Select("cal", "pDateBegin < '" + cls_utility.persianDate + "' AND pDateEnd < '" + cls_utility.persianDate + "'", "pDateEnd", "DESC", 1);
            // get today occasions
            todayOccasions = db.Select("cal", "(pDateBegin = '" + cls_utility.persianDate + "') OR (pDateEnd >= '" + cls_utility.persianDate + "' AND pDateBegin <= '" + persianDate + "')", "pDateBegin");
            // get next occations
            nextOccasions = db.Select("cal", "pDateBegin > '" + cls_utility.persianDate + "'", "pDateBegin", "ASC" ,15);
            // get today avds
            advs = db.Select("advs", "(pDateBegin <= '" + cls_utility.persianDate + "' AND pDateEnd >= '" + cls_utility.persianDate + "') AND (visible = true) AND (enabled = true)", "priority, pDateBegin", "ASC");
            // load borders
            borders = db.Select("borders", "", "border_id");

            cls_utility.Log("---------------------------------------------------------");
            cls_utility.Log("Calendar Inventory......Past :" + pastOccasions.Rows.Count.ToString() + "  Today : " + todayOccasions.Rows.Count.ToString() + "  Next : " + nextOccasions.Rows.Count.ToString());
            cls_utility.Log("Advertisements Inventory......Today :" + advs.Rows.Count.ToString());
            
            try
            {
                int nImageIndex = -1;
                int index = 0;
                List<string> allGroups = cls_network.ListAllDomainGroups();
                int allGroupsCount = allGroups.Count;
                string sSecurityFiltering = null;                

                if (cls_settings.advsEnabled)
                {
                    foreach (DataRow adv in advs.Rows)
                    {                        

                        // loading image
                        try
                        {
                            if (cls_settings.imagesPlace != string.Empty)
                            {
                                nImageIndex = GetFilesListAndLoadImage(nImageIndex);
                                cls_utility.Log("Image Loading................... OK");
                            }
                        }
                        catch (Exception ex)
                        {
                            cls_utility.Log("Error in loading main image. " + ex.Message);
                            return false;
                        }

                        sSecurityFiltering = DoSubroutine(adv, index);
                        // gpo
                        cls_network.ManipulateGPO(cls_settings.sGPOMainName + PopulateNumberToString(index), sSecurityFiltering);
                        // remove groups that used for adv
                        string[] sGroups = sSecurityFiltering.Split(new char[] { ';' });
                        foreach (string sGroup in sGroups)
                        {
                            allGroups.Remove(sGroup);
                        }

                        index++;
                    }                    
                }
                cls_utility.Log("Manipulating GPO and produce assets phase #1................... OK");

                // finally get all remaining group and set to them the calendar gpo
                if (allGroups.Count > 0 && allGroups.Count < allGroupsCount && advs.Rows.Count > 0)
                {
                    GetFilesListAndLoadImage(nImageIndex);
                    DoSubroutine(null, index);
                    sSecurityFiltering = allGroups.Count < allGroupsCount ? String.Join(";", allGroups.ToArray()) : "Authenticated Users";
                    // gpo
                    cls_network.ManipulateGPO(cls_settings.sGPOMainName + PopulateNumberToString(index), sSecurityFiltering);
                    index++;
                }
                else if (allGroups.Count > 0 && allGroups.Count == allGroupsCount && advs.Rows.Count <= 0)
                {
                    GetFilesListAndLoadImage(nImageIndex);
                    DoSubroutine(null, index);
                    sSecurityFiltering = allGroups.Count < allGroupsCount ? String.Join(";", allGroups.ToArray()) : "Authenticated Users";
                    // gpo
                    cls_network.ManipulateGPO(cls_settings.sGPOMainName + PopulateNumberToString(index), sSecurityFiltering);
                    index++;
                }
                cls_utility.Log("Manipulating GPO and produce assets phase #2................... OK");

                // delete un used GPOs links
                List<string> GPONames = new List<string>();                
                for (int i = index; i <= cls_settings.nNumberOfGPOSupport; i++)
                {
                    GPONames.Add(cls_settings.sGPOMainName + PopulateNumberToString(i));
                }
                cls_network.DeleteGPOLink(GPONames.ToArray());
                cls_utility.Log("Manipulating GPO and produce assets phase #3................... OK");
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in DoTheRoutine(). " + ex.Message);
                return false;
            }
            
            return true;
        }

        private static string DoSubroutine(DataRow adv, int advIndex)
        {
            //////// draw the calendar///////////////////
            cls_utility.DrawMainRectangle();
            cls_utility.DrawMainText();
            /////////////////////////////////////////////

            string sAdvImagePath = null, sSecurityFiltering = null;
            int border_id = 0;

            if (adv != null)
            {
                sAdvImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Res\advs\" , adv["image"].ToString());
                sSecurityFiltering = adv["securityFiltering"].ToString();
                border_id = Convert.ToInt32(adv["border_id"]);
                int ww = 0, hh = 0;
                bool hasBorder = border_id == 0 ? false : true;

                LoadAdvSettingsFromDB(advIndex);

                // draw the background rectangle
                cls_utility.DrawAdvMainRectangle(hasBorder);

                // draw the adv content
                using (Bitmap overlayImage = (Bitmap)Image.FromFile(sAdvImagePath))
                {
                    DataRow border = cls_utility.FindBorderByID(border_id);
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
                cls_utility.DrawBorder(border_id, ww, hh);
            }

            // save the image
            cls_utility.SaveTheImage(advIndex);                       

            return sSecurityFiltering;
        }

        public static void LoadAdvSettingsFromDB(int nPosition)
        {
            try
            {
                // reading the RTF
                cls_settings.AdvRTF = cls_utility.advs.Rows[nPosition]["adv"].ToString();

                cls_settings.advAlpha = Convert.ToInt32(cls_utility.advs.Rows[nPosition]["alpha"]);

                // Reading AdvRectColor
                cls_settings.advRectColor = Color.FromArgb(cls_settings.advAlpha, ColorTranslator.FromHtml(cls_utility.advs.Rows[nPosition]["backcolor"].ToString()));

                // Reading adv border_id
                cls_settings.advBorderID = Convert.ToInt32(cls_utility.advs.Rows[nPosition]["border_id"].ToString());

                // Reading the adv position
                cls_settings.baseXadv = Convert.ToInt32(cls_utility.advs.Rows[nPosition]["posX"].ToString());
                cls_settings.baseYadv = Convert.ToInt32(cls_utility.advs.Rows[nPosition]["posY"].ToString());
                if (cls_settings.baseXadv == 0 && cls_settings.baseYadv == 0)
                {
                    cls_settings.baseXadv = cls_settings.default_baseXadv;
                    cls_settings.baseYadv = cls_settings.default_baseYadv;
                }
                // Reading the adv size
                cls_settings.rectWidthAdv = Convert.ToInt32(cls_utility.advs.Rows[nPosition]["sizeX"].ToString());
                cls_settings.rectHeightAdv = Convert.ToInt32(cls_utility.advs.Rows[nPosition]["sizeY"].ToString());
                if (cls_settings.rectWidthAdv == 0 && cls_settings.rectHeightAdv == 0)
                {
                    cls_settings.rectWidthAdv = cls_settings.default_rectWidthAdv;
                    cls_settings.rectHeightAdv = cls_settings.default_rectHeightAdv;
                }

                // reading adv security filtering
                cls_settings.SecurityFilteringAdv = cls_utility.advs.Rows[nPosition]["SecurityFiltering"].ToString();

                // reading adv priority
                cls_settings.advPriority = Convert.ToInt32(cls_utility.advs.Rows[nPosition]["priority"]);

                // reading adv enabled
                cls_settings.advEnabled = Convert.ToBoolean(cls_utility.advs.Rows[nPosition]["enabled"]);
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in LoadAdvSettingsFromDB(). " + ex.Message);
            }
        }

        public static int GetFilesListAndLoadImage(int number = -1)
        {
            int randomNumber = 0;
            try
            {
                // Only get JPEG files
                filesList = Directory.GetFiles(cls_settings.imagesPlace, "*.JPG");
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in GetFilesListAndLoadImage() method. " + ex.Message);
            }  

            if (filesList.Length > 0)
            {
                // Loading image randomly
                if (cls_settings.pictureName == "" || cls_settings.pictureName == string.Empty || cls_settings.pictureName == null)
                {
                    try
                    {
                        Random random = new Random();
                        randomNumber = number == -1 ? random.Next(0, filesList.Length - 1) : number;
                        LoadImageFromFile(filesList[randomNumber]);
                        cls_settings.imageFullName = filesList[randomNumber];

                    }catch (Exception ex)
                    {
                        cls_utility.Log("Error in GetFilesListAndLoadImage() method. " + ex.Message);
                    }
                }

                else // load image by picture
                {
                    int pictureIndex = Array.IndexOf(filesList, cls_settings.pictureName);
                    if (pictureIndex >= 0)
                    {
                        cls_settings.imageFullName = filesList[pictureIndex];
                        //cls_settings.pictureName = "";
                        //cls_settings.SaveASetting("pictureName", cls_settings.pictureName);
                        LoadImageFromFile(cls_settings.imageFullName);
                    }
                }
            }

            return randomNumber;
        }

        public static void LoadImageFromFile(string sFilePath)
        {
            // first dispose
            if (cls_utility.image != null)
                cls_utility.image.Dispose();

            cls_utility.image = Image.FromFile(sFilePath);
        }
        
    }
}
