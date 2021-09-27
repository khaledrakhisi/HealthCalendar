using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCalendar
{
    [StructLayout(LayoutKind.Sequential)]
    public struct STRUCT_RECT
    {
        public Int32 left;
        public Int32 top;
        public Int32 right;
        public Int32 bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STRUCT_CHARRANGE
    {
        public Int32 cpMin;
        public Int32 cpMax;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STRUCT_FORMATRANGE
    {
        public IntPtr hdc;
        public IntPtr hdcTarget;
        public STRUCT_RECT rc;
        public STRUCT_RECT rcPage;
        public STRUCT_CHARRANGE chrg;
    }
    /// <summary>
    /// Summary description for RtfToPicture2.
    /// </summary>
    public class RtfToPicture2
    {
        public RtfToPicture2()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [DllImport("user32.dll")]
        private static extern Int32 SendMessage(IntPtr hWnd, Int32 msg, Int32
        wParam, IntPtr lParam);

        private const Int32 WM_USER = 0x400;
        private const Int32 EM_FORMATRANGE = WM_USER + 57;


        private int m_nFirstCharOnPage;

        // C#
        /// <summary>
        /// Calculate or render the contents of our RichTextBox for printing
        /// </summary>
        /// <param name="measureOnly">If true, only the calculation is performed,
        /// otherwise the text is rendered as well</param>
        /// <param name="e">The PrintPageEventArgs object from the
        /// PrintPage event</param>
        /// <param name="charFrom">Index of first character to be printed</param>
        /// <param name="charTo">Index of last character to be printed</param>
        /// <returns>(Index of last character that fitted on the
        /// page) + 1</returns>
        //public int FormatRange(bool measureOnly, PrintPageEventArgs e, int charFrom, int charTo)
        public int FormatRange(bool measureOnly, ref RichTextBox RTB, PictureBox pb, int charFrom, int charTo)
        {
            // Specify which characters to print
            STRUCT_CHARRANGE cr;
            cr.cpMin = charFrom;
            cr.cpMax = charTo;

            // Specify the area inside page margins
            STRUCT_RECT rc;
            rc.top = HundredthInchToTwips(0);
            rc.bottom = HundredthInchToTwips(pb.Height);
            rc.left = HundredthInchToTwips(0);
            rc.right = HundredthInchToTwips(pb.Width);

            // Specify the page area
            STRUCT_RECT rcPage;
            rcPage.top = HundredthInchToTwips(0);
            rcPage.bottom = HundredthInchToTwips(pb.Height);
            rcPage.left = HundredthInchToTwips(0);
            rcPage.right = HundredthInchToTwips(pb.Width);

            // Get device context of output device
            Graphics g = pb.CreateGraphics();
            IntPtr hdc = g.GetHdc();

            // Fill in the FORMATRANGE struct
            STRUCT_FORMATRANGE fr;
            fr.chrg = cr;
            fr.hdc = hdc;
            fr.hdcTarget = hdc;
            fr.rc = rc;
            fr.rcPage = rcPage;

            // Non-Zero wParam means render, Zero means measure
            Int32 wParam = (measureOnly ? 0 : 1);

            // Allocate memory for the FORMATRANGE struct and
            // copy the contents of our struct to this memory
            IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr));
            Marshal.StructureToPtr(fr, lParam, false);

            // Send the actual Win32 message
            int res = SendMessage(RTB.Handle, EM_FORMATRANGE, wParam, lParam);

            // Free allocated memory
            Marshal.FreeCoTaskMem(lParam);

            // and release the device context
            g.ReleaseHdc(hdc);

            g.Dispose();

            return res;
        }

        public int FormatRange(bool measureOnly, ref RichTextBox RTB, ref Bitmap b, int charFrom, int charTo)
        {
            // Specify which characters to print
            STRUCT_CHARRANGE cr;
            cr.cpMin = charFrom;
            cr.cpMax = charTo;

            // Specify the area inside page margins
            STRUCT_RECT rc;
            rc.top = HundredthInchToTwips(0);
            rc.bottom = HundredthInchToTwips(b.Height);
            rc.left = HundredthInchToTwips(0);
            rc.right = HundredthInchToTwips(b.Width);

            // Specify the page area
            STRUCT_RECT rcPage;
            rcPage.top = HundredthInchToTwips(0);
            rcPage.bottom = HundredthInchToTwips(b.Height);
            rcPage.left = HundredthInchToTwips(0);
            rcPage.right = HundredthInchToTwips(b.Width);

            // Get device context of output device
            Graphics g = Graphics.FromImage(b);
            IntPtr hdc = g.GetHdc();

            // Fill in the FORMATRANGE struct
            STRUCT_FORMATRANGE fr;
            fr.chrg = cr;
            fr.hdc = hdc;
            fr.hdcTarget = hdc;
            fr.rc = rc;
            fr.rcPage = rcPage;

            // Non-Zero wParam means render, Zero means measure
            Int32 wParam = (measureOnly ? 0 : 1);

            // Allocate memory for the FORMATRANGE struct and
            // copy the contents of our struct to this memory
            IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr));
            Marshal.StructureToPtr(fr, lParam, false);

            // Send the actual Win32 message
            int res = SendMessage(RTB.Handle, EM_FORMATRANGE, wParam, lParam);

            // Free allocated memory
            Marshal.FreeCoTaskMem(lParam);

            // and release the device context
            g.ReleaseHdc(hdc);

            g.Dispose();

            return res;
        }

        // C#
        /// <summary>
        /// Convert between 1/100 inch (unit used by the .NET framework)
        /// and twips (1/1440 inch, used by Win32 API calls)
        /// </summary>
        /// <param name="n">Value in 1/100 inch</param>
        /// <returns>Value in twips</returns>
        private Int32 HundredthInchToTwips(int n)
        {
            return (Int32)(n * 14.4);
        }
        // C#
        /// <summary>
        /// Free cached data from rich edit control after printing
        /// </summary>
        public void FormatRangeDone(ref RichTextBox RTB)
        {
            IntPtr lParam = new IntPtr(0);
            SendMessage(RTB.Handle, EM_FORMATRANGE, 0, lParam);
        }

        public void RTFtoPictureBox(ref RichTextBox RTB, ref PictureBox PB)
        {
            this.m_nFirstCharOnPage = 0;

            do
            {
                this.m_nFirstCharOnPage = this.FormatRange(false, ref RTB, PB,
                this.m_nFirstCharOnPage, RTB.Text.Length);
                if (this.m_nFirstCharOnPage != RTB.Text.Length)
                {
                    PB.Height += 1;
                    this.m_nFirstCharOnPage = 0;
                }
            }
            while (this.m_nFirstCharOnPage < RTB.Text.Length);

            this.FormatRangeDone(ref RTB);

        }

        public void RTFtoBitmap(ref RichTextBox RTB, ref Bitmap b)
        {
            this.m_nFirstCharOnPage = 0;

            do
            {
                this.m_nFirstCharOnPage = this.FormatRange(false, ref RTB, ref b,
                this.m_nFirstCharOnPage, RTB.Text.Length);
                if (this.m_nFirstCharOnPage != RTB.Text.Length)
                {
                    b = new Bitmap(b.Width, b.Height + 1);
                    this.m_nFirstCharOnPage = 0;
                }
            }
            while (this.m_nFirstCharOnPage < RTB.Text.Length);

            this.FormatRangeDone(ref RTB);

        }

    }
}
