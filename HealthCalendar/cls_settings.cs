using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.ModifyRegistry;

namespace HealthCalendar
{
    static class cls_settings
    {
        public static int nNumberOfGPOSupport = 5;
        public static string sOutputImageFileName = "000_def_hcalendar";
        public static string sGPOMainName = "August-HCalendar";

        /// <summary>
        /// 
        /// </summary>
        public static string imagesPlace { get { return _imagesPlace; } set { _imagesPlace = value; _isSaved = false; } }
        private static string _imagesPlace;

        /// <summary>
        /// background picture name
        /// </summary>
        public static string pictureName { get { return _pictureName; } set { _pictureName = value; _isSaved = false; } }
        public static string default_pictureName { get { return ""; } }
        private static string _pictureName;
        
        /// <summary>
        /// 
        /// </summary>
        public static string scheduleTime { get { return _scheduleTime; } set { _scheduleTime = value; _isSaved = false; } }
        public static string default_scheduleTime { get { return "00:01"; } }
        private static string _scheduleTime;

        private static string _securityFiltering;
        /// <summary>
        /// 
        /// </summary>
        public static string SecurityFiltering { get { return _securityFiltering; } set { _securityFiltering = value; _isSaved = false; } }
        public static string default_securityFiltering { get { return "Authenticated Users"; } }
                
        /// <summary>
        /// 
        /// </summary>
        public static string imageFullName { get { return _imageFullName; } set { _imageFullName = value; _isSaved = false; } }
        private static string _imageFullName;

        private static bool _isSaved;
        /// <summary>
        /// 
        /// </summary>
        public static bool isSaved
        {
            get { return _isSaved; }
        }       

        //Settings
        private static int _baseX;
        /// <summary>
        /// 
        /// </summary>
        public static int baseX { get { return _baseX; } set { _baseX = value; _isSaved = false; } }
        public static int default_baseX { get { return 3; } }
        
        private static int _baseY;
        /// <summary>
        /// 
        /// </summary>
        public static int baseY { get { return _baseY; } set { _baseY = value; _isSaved = false; } }
        public static int default_baseY { get { return 5; } }

        private static int _baseXadv;
        /// <summary>
        /// 
        /// </summary>
        public static int baseXadv { get { return _baseXadv; } set { _baseXadv = value; _isSaved = false; } }
        public static int default_baseXadv { get { return -1; } }

        private static int _baseYadv;
        /// <summary>
        /// 
        /// </summary>
        public static int baseYadv { get { return _baseYadv; } set { _baseYadv = value; _isSaved = false; } }
        public static int default_baseYadv { get { return default_baseY + default_rectHeight + default_padding + 2; } }        
        
        /// <summary>
        /// 
        /// </summary>
        public static int baseWidth = 200, baseHeight = 140;
        public static int baseWidthAdv = 200, baseHeightAdv = 140;

        private static int _padding;
        /// <summary>
        /// 
        /// </summary>
        public static int padding { get { return _padding; } set { _padding = value; _isSaved = false; } }
        public static int default_padding { get { return 5; } }

        public static int _rectWidth;
        /// <summary>
        /// 
        /// </summary>        
        public static int rectWidth { get { return _rectWidth; } set { _rectWidth = value; _isSaved = false; } }
        public static int default_rectWidth { get { return 70; } }


        public static int _rectHeight;
        /// <summary>
        /// 
        /// </summary>        
        public static int rectHeight { get { return _rectHeight; } set { _rectHeight = value; _isSaved = false; } }
        public static int default_rectHeight { get { return 50; } }

        private static int _rectWidthAdv;
        /// <summary>
        /// 
        /// </summary>        
        public static int rectWidthAdv { get { return _rectWidthAdv; } set { _rectWidthAdv = value; _isSaved = false; } }
        public static int default_rectWidthAdv { get { return 740; } }//{ get { return 70; } }


        private static int _rectHeightAdv;
        /// <summary>
        /// 
        /// </summary>        
        public static int rectHeightAdv { get { return _rectHeightAdv; } set { _rectHeightAdv = value; _isSaved = false; } }
        public static int default_rectHeightAdv { get { return 570; } }//{ get { return 60; } }

        // Appreance Settings     
   
        private static int _nAlpha;
        /// <summary>
        /// 
        /// </summary>
        public static int nAlpha { get { return _nAlpha; } set { _nAlpha = value; _isSaved = false; } }
        public static int default_nAlpha { get { return 55;} }

        
        private static int _fontSize;
        /// <summary>
        /// 
        /// </summary>
        public static int fontSize { get { return _fontSize; } set { _fontSize = value; _isSaved = false; } }
        public static int default_fontSize { get { return 15; } }
        
        private static Color _mainRectColor;
        /// <summary>
        /// 
        /// </summary>
        public static Color mainRectColor { get { return _mainRectColor; } set { _mainRectColor = value; _isSaved = false; } }
        public static Color default_mainRectColor { get { return Color.FromArgb(55, Color.Blue); } }
        
        private static Color _todayOccasionsRectColor;
        /// <summary>
        /// 
        /// </summary>
        public static Color todayOccasionsRectColor { get { return _todayOccasionsRectColor; } set { _todayOccasionsRectColor = value; _isSaved = false; } }
        public static Color default_todayOccasionsRectColor { get { return Color.FromArgb(70, Color.Green); } }
        
        private static Color _nextOccasionsRectColor;
        /// <summary>
        /// 
        /// </summary>
        public static Color nextOccasionsRectColor { get { return _nextOccasionsRectColor; } set { _nextOccasionsRectColor = value; _isSaved = false; } }
        public static Color default_nextOccasionsRectColor { get { return Color.FromArgb(70, Color.Cyan); } }

        private static Color _foreColor;
        /// <summary>
        /// 
        /// </summary>
        public static Color foreColor { get { return _foreColor; } set { _foreColor = value; _isSaved = false; } }
        public static Color default_foreColor { get { return Color.White; } }

        private static bool _hasTextShadow;
        /// <summary>
        /// 
        /// </summary>
        public static bool hasTextShadow { get { return _hasTextShadow; } set { _hasTextShadow = value; _isSaved = false; } }
        public static bool default_hasTextShadow { get { return true; } }
        
        /// <summary>
        /// 
        /// </summary>
        public static bool advsEnabled { get { return _advsEnabled; } set { _advsEnabled = value; _isSaved = false; } }
        public static bool default_advsEnabled { get { return true; } }
        private static bool _advsEnabled;

        /// <summary>
        /// 
        /// </summary>
        public static bool advEnabled { get { return _advEnabled; } set { _advEnabled = value; _isSaved = false; } }
        public static bool default_advEnabled { get { return true; } }
        private static bool _advEnabled;

        private static int _advAlpha;
        /// <summary>
        /// this will be stored in the DB
        /// </summary>
        public static int advAlpha { get { return _advAlpha; } set { _advAlpha = value; _isSaved = false; } }
        public static int default_AdvAlpha { get { return 200; } }

        private static int _advBorderID;
        /// <summary>
        /// this will be stored in the DB
        /// </summary>
        public static int advBorderID { get { return _advBorderID; } set { _advBorderID = value; _isSaved = false; } }
        public static int default_advBorderID { get { return 1; } }//1 == SIMPLE BORDER}


        private static Color _advRectColor;
        /// <summary>
        /// this will be stored in the DB
        /// </summary>
        public static Color advRectColor { get { return _advRectColor; } set { _advRectColor = value; _isSaved = false; } }
        public static Color default_advRectColor { get { return Color.FromArgb(200, Color.White); } }
        
        /// <summary>
        /// this will be stored in the DB
        /// </summary>
        public static string SecurityFilteringAdv { get { return _securityFilteringAdv; } set { _securityFilteringAdv = value; _isSaved = false; } }
        public static string default_securityFilteringAdv { get { return "Authenticated Users"; } }
        private static string _securityFilteringAdv;

        /// <summary>
        /// this will be stored in the DB
        /// </summary>
        public static string AdvRTF { get { return _AdvRTF; } set { _AdvRTF = value; _isSaved = false; } }
        public static string default_AdvRTF { get { return ""; } }
        private static string _AdvRTF;
        
        /// <summary>
        /// this will be stored in the DB
        /// </summary>
        public static int advPriority { get { return _advPriority; } set { _advPriority = value; _isSaved = false; } }
        public static int default_advPriority { get { return 100; } }//100 == default hightest priority}
        private static int _advPriority;


        // Themes
        private static int _themeID;
        /// <summary>
        /// 
        /// </summary>
        public static int themeID { get { return _themeID; } set { _themeID = value; _isSaved = false; } }
        public static int default_themeID { get { return 0; } } // 0 == Automatic Theme

        public static cls_theme[] themes = {
                                    new cls_theme(1, "SEA",Color.FromArgb(77, Color.Blue), Color.FromArgb(70, Color.Green), Color.FromArgb(70, Color.Cyan), Color.White, true),
                                    new cls_theme(2, "MARS", Color.FromArgb(77, Color.Red), Color.FromArgb(70, Color.Orange),Color.FromArgb(70, Color.Wheat), Color.White, true),
                                    new cls_theme(3, "VEGETABLES", Color.FromArgb(66, Color.Green),Color.FromArgb(70, Color.Blue), Color.FromArgb(70, Color.GreenYellow),  Color.White, true),
                                    new cls_theme(4, "DARKNESS", Color.FromArgb(77, Color.Black), Color.FromArgb(70, Color.DarkGray), Color.FromArgb(70, Color.LightGray), Color.White, true),
                                    new cls_theme(5, "SNOW WHITE", Color.FromArgb(88, Color.White), Color.FromArgb(70, Color.Blue), Color.FromArgb(70, Color.CornflowerBlue), Color.Blue, false),
                                    new cls_theme(6, "SUNNY", Color.FromArgb(88, Color.Yellow), Color.FromArgb(70, Color.Orange), Color.FromArgb(70, Color.Wheat), Color.Black, false),
                                    new cls_theme(7, "MONEY", Color.FromArgb(88, Color.DarkCyan), Color.FromArgb(70, Color.LightBlue), Color.FromArgb(70, Color.LightSeaGreen), Color.White, true),
                                    new cls_theme(8, "CANDY", Color.FromArgb(88, Color.Purple), Color.FromArgb(70, Color.MediumPurple), Color.FromArgb(70, Color.OrangeRed), Color.White, true),
                                    new cls_theme(9, "AUTUMN", Color.FromArgb(88, Color.Orange), Color.FromArgb(70, Color.Yellow), Color.FromArgb(70, Color.OrangeRed), Color.White, true)                                    
                                 };         

        private static ModifyRegistry myRegistry = new ModifyRegistry();

        static cls_settings()
        {
            _isSaved = true;
            try
            {
                myRegistry.BaseRegistryKey = Registry.LocalMachine;
                //myRegistry.BaseRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                myRegistry.SubKey = "SOFTWARE\\August\\HealthCalendar";
                
                cls_utility.Log("Registry Path is : " + myRegistry.SubKey);
            }
            catch (Exception ex)
            {
                cls_utility.Log("Cannot read registry " + myRegistry.SubKey + "--" + ex.Message);
            }
                           
        }

        public static void SaveAllSettings()
        {
            try
            {
                // Save Image Place
                myRegistry.Write("images", imagesPlace);

                // Save background picture name
                myRegistry.Write("pictureName", pictureName);

                // Save schedule time
                myRegistry.Write("scheduleTime", scheduleTime);

                //Save Security Filtering
                myRegistry.Write("securityFiltering", SecurityFiltering);                

                //Save MainRectColor
                myRegistry.Write("MainRectColorR", mainRectColor.R);
                myRegistry.Write("MainRectColorG", mainRectColor.G);
                myRegistry.Write("MainRectColorB", mainRectColor.B);

                // save alpha
                myRegistry.Write("MainRectColorA", nAlpha.ToString());                

                //Save TodayOccasionsRectColor
                myRegistry.Write("TodayOccasionsRectColorR", todayOccasionsRectColor.R);
                myRegistry.Write("TodayOccasionsRectColorG", todayOccasionsRectColor.G);
                myRegistry.Write("TodayOccasionsRectColorB", todayOccasionsRectColor.B);
                //myRegistry.Write("MainRectColorA", nAlpha.ToString());

                //Save NextOccasionsRectColor
                myRegistry.Write("NextOccasionsRectColorR", nextOccasionsRectColor.R);
                myRegistry.Write("NextOccasionsRectColorG", nextOccasionsRectColor.G);
                myRegistry.Write("NextOccasionsRectColorB", nextOccasionsRectColor.B);
                //myRegistry.Write("MainRectColorA", nAlpha.ToString());

                //Save NextOccasionsRectColor
                myRegistry.Write("ForeColorR", foreColor.R);
                myRegistry.Write("ForeColorG", foreColor.G);
                myRegistry.Write("ForeColorB", foreColor.B);
                //myRegistry.Write("MainRectColorA", nAlpha.ToString());

                myRegistry.Write("HasTextShadow", hasTextShadow.ToString());

                // Save Advs settings
                myRegistry.Write("AdvsEnabled", advsEnabled.ToString());

                //myRegistry.Write("AdvRectColorR", advRectColor.R);
                //myRegistry.Write("AdvRectColorG", advRectColor.G);
                //myRegistry.Write("AdvRectColorB", advRectColor.B);
                //// save adv alpha
                //myRegistry.Write("AdvRectColorA", advAlpha.ToString());      
                
                //myRegistry.Write("AdvAlpha", advAlpha.ToString());     

                //myRegistry.Write("AdvPositionX", baseXadv.ToString());
                //myRegistry.Write("AdvPositionY", baseYadv.ToString());
                //myRegistry.Write("AdvSizeX", rectWidthAdv.ToString());
                //myRegistry.Write("AdvSizeY", rectHeightAdv.ToString());

                myRegistry.Write("ThemeID", themeID.ToString());

                // save font size
                myRegistry.Write("FontSize", fontSize.ToString());

                myRegistry.Write("CalendarPositionX", baseX.ToString());
                myRegistry.Write("CalendarPositionY", baseY.ToString());
                myRegistry.Write("CalendarSizeX", rectWidth.ToString());
                myRegistry.Write("CalendarSizeY", rectHeight.ToString());                
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in Saving Settings. "+ ex.Message);
            }
            finally
            {
                _isSaved = true;
            }            
        }

        public static void ReadAllSettings()
        {
            try
            {
                _imagesPlace = cls_settings.ReadASetting("images");

                _pictureName = cls_settings.ReadASetting("pictureName");

                _scheduleTime = cls_settings.ReadASetting("scheduleTime");

                // Reading Security Filtering
                _securityFiltering = cls_settings.ReadASetting("securityFiltering");
                if (SecurityFiltering == null || SecurityFiltering == string.Empty)
                {
                    SecurityFiltering = default_securityFiltering;
                }                

                // Reading MainRectColor
                cls_settings.mainRectColor = Color.FromArgb(Convert.ToInt32(cls_settings.ReadASetting("MainRectColorA")),
                                                                Convert.ToInt32(cls_settings.ReadASetting("MainRectColorR")),
                                                                Convert.ToInt32(cls_settings.ReadASetting("MainRectColorG")),
                                                                Convert.ToInt32(cls_settings.ReadASetting("MainRectColorB")));
                if (cls_settings.mainRectColor.R == 0 && cls_settings.mainRectColor.G == 0 && cls_settings.mainRectColor.B == 0 && cls_settings.mainRectColor.A == 0)
                {
                    // Default Color
                    cls_settings.mainRectColor = cls_settings.default_mainRectColor;
                }
                cls_settings.nAlpha = cls_settings.mainRectColor.A;                

                // Reading todayOccasionsRectColor
                cls_settings.todayOccasionsRectColor = Color.FromArgb(Convert.ToInt32(cls_settings.ReadASetting("todayOccasionsRectColorR")),
                                                                        Convert.ToInt32(cls_settings.ReadASetting("todayOccasionsRectColorG")),
                                                                        Convert.ToInt32(cls_settings.ReadASetting("todayOccasionsRectColorB")));
                if (cls_settings.todayOccasionsRectColor.R == 0 && cls_settings.todayOccasionsRectColor.G == 0 && cls_settings.todayOccasionsRectColor.B == 0)
                {
                    // Default Color
                    cls_settings.todayOccasionsRectColor = cls_settings.default_todayOccasionsRectColor;
                }                

                // Reading nextOccasionsRectColor
                cls_settings.nextOccasionsRectColor = Color.FromArgb(Convert.ToInt32(cls_settings.ReadASetting("NextOccasionsRectColorR")),
                                                                        Convert.ToInt32(cls_settings.ReadASetting("NextOccasionsRectColorG")),
                                                                        Convert.ToInt32(cls_settings.ReadASetting("NextOccasionsRectColorB")));
                if (cls_settings.nextOccasionsRectColor.R == 0 && cls_settings.nextOccasionsRectColor.G == 0 && cls_settings.nextOccasionsRectColor.B == 0)
                {
                    // Default Color
                    cls_settings.nextOccasionsRectColor = cls_settings.default_nextOccasionsRectColor;
                }

                // Reading foreColor
                cls_settings.foreColor = Color.FromArgb(Convert.ToInt32(cls_settings.ReadASetting("ForeColorR")),
                                                                        Convert.ToInt32(cls_settings.ReadASetting("ForeColorG")),
                                                                        Convert.ToInt32(cls_settings.ReadASetting("ForeColorB")));
                if (cls_settings.foreColor.R == 0 && cls_settings.foreColor.G == 0 && cls_settings.foreColor.B == 0)
                {
                    // Default Color
                    cls_settings.foreColor = cls_settings.default_foreColor;
                }

                cls_settings.hasTextShadow = Convert.ToBoolean(cls_settings.ReadASetting("HasTextShadow"));


                //// Reading AdvRectColor
                //cls_settings.advRectColor = Color.FromArgb(Convert.ToInt32(cls_settings.ReadASetting("AdvRectColorA")),
                //                                                Convert.ToInt32(cls_settings.ReadASetting("AdvRectColorR")),
                //                                                Convert.ToInt32(cls_settings.ReadASetting("AdvRectColorG")),
                //                                                Convert.ToInt32(cls_settings.ReadASetting("AdvRectColorB")));
                //if (cls_settings.advRectColor.R == 0 && cls_settings.advRectColor.G == 0 && cls_settings.advRectColor.B == 0 && cls_settings.advRectColor.A == 0)
                //{
                //    // Default Color
                //    cls_settings.advRectColor = cls_settings.default_advRectColor;
                //}
                cls_settings.advsEnabled = Convert.ToBoolean(cls_settings.ReadASetting("AdvsEnabled"));
                //cls_settings.advAlpha = Convert.ToInt32(cls_settings.ReadASetting("AdvAlpha"));

                cls_settings.themeID = Convert.ToInt32(cls_settings.ReadASetting("ThemeID"));

                cls_settings.fontSize = Convert.ToInt32(cls_settings.ReadASetting("FontSize"));

                // Reading the calendar position
                cls_settings.baseX = Convert.ToInt32(cls_settings.ReadASetting("CalendarPositionX"));
                cls_settings.baseY = Convert.ToInt32(cls_settings.ReadASetting("CalendarPositionY"));
                if (cls_settings.baseX == 0 && cls_settings.baseY == 0)
                {
                    cls_settings.baseX = cls_settings.default_baseX;
                    cls_settings.baseY = cls_settings.default_baseY;
                }
                // Reading the calendar size
                cls_settings.rectWidth = Convert.ToInt32(cls_settings.ReadASetting("CalendarSizeX"));
                cls_settings.rectHeight = Convert.ToInt32(cls_settings.ReadASetting("CalendarSizeY"));
                if (cls_settings.rectWidth == 0 && cls_settings.rectHeight == 0)
                {
                    cls_settings.rectWidth = cls_settings.default_rectWidth;
                    cls_settings.rectHeight = cls_settings.default_rectHeight;
                }

                //// Reading the adv position
                //cls_settings.baseXadv = Convert.ToInt32(cls_settings.ReadASetting("AdvPositionX"));
                //cls_settings.baseYadv = Convert.ToInt32(cls_settings.ReadASetting("AdvPositionY"));
                //if (cls_settings.baseXadv == 0 && cls_settings.baseYadv == 0)
                //{
                //    cls_settings.baseXadv = cls_settings.default_baseXadv;
                //    cls_settings.baseYadv = cls_settings.default_baseYadv;
                //}
                //// Reading the adv size
                //cls_settings.rectWidthAdv = Convert.ToInt32(cls_settings.ReadASetting("AdvSizeX"));
                //cls_settings.rectHeightAdv = Convert.ToInt32(cls_settings.ReadASetting("AdvSizeY"));
                //if (cls_settings.rectWidthAdv == 0 && cls_settings.rectHeightAdv == 0)
                //{
                //    cls_settings.rectWidthAdv = cls_settings.default_rectWidthAdv;
                //    cls_settings.rectHeightAdv = cls_settings.default_rectHeightAdv;
                //}
                
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in reading registry settings. " + ex.Message);
            }
            finally
            {
                _isSaved = true;
            }
        }

        public static void LoadDefaultValues(bool exceptImagesPlace = false)
        {
            if(!exceptImagesPlace)
                imagesPlace = AppDomain.CurrentDomain.BaseDirectory + @"Res\pix\";

            pictureName = default_pictureName;
            scheduleTime = default_scheduleTime;
            SecurityFiltering = default_securityFiltering;

            mainRectColor = default_mainRectColor;
            advRectColor = default_advRectColor;

            todayOccasionsRectColor = default_todayOccasionsRectColor;
            nextOccasionsRectColor = default_nextOccasionsRectColor;
            foreColor = default_foreColor;

            hasTextShadow = default_hasTextShadow;
            advsEnabled = default_advsEnabled;

            nAlpha = default_nAlpha;
            advAlpha = default_AdvAlpha;

            baseX = default_baseX;
            baseY = default_baseY;
            rectWidth = default_rectWidth;
            rectHeight = default_rectHeight;

            baseXadv = default_baseXadv;
            baseYadv = default_baseY + default_rectHeight + default_padding;
            //rectWidthAdv = default_rectWidthAdv;
            //rectHeightAdv = default_rectHeightAdv;
            advBorderID = default_advBorderID;

            themeID = default_themeID;
            fontSize = default_fontSize;
        }

        public static void SaveASetting(string sName, string sValue){
            myRegistry.Write(sName, sValue);
        }
        public static string ReadASetting(string sName)
        {
            // Reading The Registery
            string s = myRegistry.Read(sName);

            return s;
        }

        public static void ApplyTheme(cls_theme theme)
        {
            _mainRectColor = theme.color1;
            _nAlpha = theme.color1.A;
            _todayOccasionsRectColor = theme.color2;
            _nextOccasionsRectColor = theme.color3;
            _foreColor = theme.foreColor;
            _hasTextShadow = theme.hasTextShadow;
        }        
    }
}
