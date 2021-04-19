using System;
using System.Runtime.InteropServices ;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSave
{
   public class ScreenCapture
    {
       

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        private static extern IntPtr GetDesktopWindow();

        public ScreenCapture()
        {
            
        }


        /// <summary>  
        /// Capture Screen  
        /// </summary>  
        /// <returns></returns>  
        public Bitmap Capture()
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            Bitmap screenBmp = new Bitmap(screenWidth, screenHeight);
            Graphics g = Graphics.FromImage(screenBmp);

            IntPtr dc1 = GetDC(GetDesktopWindow());
            IntPtr dc2 = g.GetHdc();

            BitBlt(dc2, 0, 0, screenWidth, screenHeight, dc1, 0, 0, 13369376);

            ReleaseDC(GetDesktopWindow(), dc1);
            g.ReleaseHdc(dc2);
            g.Dispose();






            return screenBmp;
        }





    }
}
