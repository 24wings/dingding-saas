using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using PInvoke;
using static PInvoke.User32;

namespace Worker
{

    /// <summary>
    /// 截屏
    /// </summary>
    public class Capture
    {

        [DllImport("gdi32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int GetPixel(int hdc, int x, int y);
        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static Bitmap CaptureWindow(IntPtr handle)
        {

            var windowRect = new RECT();
            GetWindowRect(GetForegroundWindow(), out windowRect);
            Rectangle tScreenRect = new Rectangle(windowRect.left, windowRect.top, windowRect.right - windowRect.left, windowRect.bottom - windowRect.top);
            Bitmap tSrcBmp = new Bitmap(windowRect.right - windowRect.left, windowRect.bottom - windowRect.top); // 用于屏幕原始图片保存
            Graphics gp = Graphics.FromImage(tSrcBmp);
            gp.CopyFromScreen(tScreenRect.Left + 5, tScreenRect.Top + 5, 0, 0, tScreenRect.Size);
            gp.DrawImage(tSrcBmp, tScreenRect.Left + 5, tScreenRect.Top + 5, tScreenRect, GraphicsUnit.Pixel);

            return tSrcBmp;


        }
        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Bitmap CaptureWindowRectangle(IntPtr handle, Rectangle rectangle)
        {
            var windowRect = new RECT();
            GetWindowRect(handle, out windowRect);

            Bitmap tSrcBmp = new Bitmap(rectangle.Width, rectangle.Height); // 用于屏幕原始图片保存
            Graphics gp = Graphics.FromImage(tSrcBmp);
            gp.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, rectangle.Size);
            gp.DrawImage(tSrcBmp, 0, 0, rectangle, GraphicsUnit.Pixel);

            return tSrcBmp;

        }



        /// <summary>
        /// 截屏,全部区域
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static Bitmap CaptureScreen(IntPtr handle)
        {
            handle = GetDesktopWindow();
            var windowRect = new RECT();
            GetWindowRect(handle, out windowRect);
            Rectangle tScreenRect = new Rectangle(windowRect.left, windowRect.top, windowRect.right - windowRect.left, windowRect.bottom - windowRect.top);
            Bitmap tSrcBmp = new Bitmap(windowRect.right - windowRect.left, windowRect.bottom - windowRect.top); // 用于屏幕原始图片保存
            Graphics gp = Graphics.FromImage(tSrcBmp);
            gp.CopyFromScreen(0, 0, 0, 0, tScreenRect.Size);
            gp.DrawImage(tSrcBmp, 0, 0, tScreenRect, GraphicsUnit.Pixel);

            return tSrcBmp;
        }

        /**
         *TODO:
         *- 截取部分屏幕
         *- [x]123
         */
        public static Bitmap CaptureScreenRect(IntPtr handle, RECT rect)
        {
            return null;
        }

    }
}
