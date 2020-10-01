using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using PInvoke;
using static PInvoke.User32;
using System.Drawing.Imaging;

namespace Worker
{
    /// <summary>
    ///    工具类
    /// </summary>
    public class Robot
    {
        private const int MOUSEEVENTF_MOVE = 0x0001;
        public static byte MOUSEEVENTF_LEFTDOWN { get; } = 0x0002;
        public static byte MOUSEEVENTF_LEFTUP { get; } = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        /// <summary>
        /// 移动到某个点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void moveTo(int x, int y)
        {

            User32.mouse_event(User32.mouse_eventFlags.MOUSEEVENTF_MOVE, -3000, -3000, 0, IntPtr.Zero);
            Thread.Sleep(50);
            User32.mouse_event(User32.mouse_eventFlags.MOUSEEVENTF_MOVE, -3000, -3000, 0, IntPtr.Zero);
            Thread.Sleep(50);

            User32.mouse_event(User32.mouse_eventFlags.MOUSEEVENTF_MOVE, x / 2, y / 2, 0, IntPtr.Zero);
            Thread.Sleep(20);
            // User32.mouse_event(mouse_eventFlags.MOUSEEVENTF_MOVE, x, y, 0, IntPtr.Zero);
        }
        // }
        // public static void keyup()
        // {
        //     // KeyDown "BackSpace", 1
        //     //     Delay 50
        //     //     KeyUp "BackSpace", 1
        //     // 102102102

        /// <summary>
        ///  鼠标左键点击
        /// 记得释放
        /// </summary>
        public static void LeftDowm()
        {
            mouse_event(User32.mouse_eventFlags.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero);
        }
        /// <summary>
        /// 鼠标左键释放
        /// </summary>
        public static void leftUp()
        {
            mouse_event(User32.mouse_eventFlags.MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero);
        }
        /// <summary>
        /// 鼠标右键按下
        /// </summary>
        public static void rightDown()
        {
            mouse_event(User32.mouse_eventFlags.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero);
        }
        /// <summary>
        /// 鼠标右键释放
        /// </summary>
        public static void rightUp()
        {
            mouse_event(User32.mouse_eventFlags.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 后退按钮
        /// </summary>
        public static void backButtonDown()
        {
            keybd_event((byte)VirtualKeyShort.BACK, 64, User32.KEYEVENTF.KEYEVENTF_UNICODE, IntPtr.Zero);
            // 12345678
        }
        /// <summary>
        /// 回车
        /// </summary>
        public static void keyEnter()
        {
            keybd_event((byte)VirtualKeyShort.RETURN, 64, User32.KEYEVENTF.KEYEVENTF_UNICODE, IntPtr.Zero);
        }

        // public static void ScreenCapture()
        // {
        //     {
        //         Rectangle bounds = Screen.GetBounds(System.Drawing.Point.Empty);
        //         using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
        //         {
        //             using (Graphics g = Graphics.FromImage(bitmap))
        //             {
        //                 g.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, bounds.Size);
        //             }
        //             string fullName = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
        //             bitmap.Save(fullName, ImageFormat.Jpeg);
        //         }
        //     }
        // }

        /// <summary>
        /// 数字键
        /// </summary>
        /// <param name="num"></param>
        public static void keyNum(char num)
        {
            var code = (byte)VirtualKeyShort.NUMPAD0;
            switch (num)
            {
                case '0':
                    code = (byte)VirtualKeyShort.NUMPAD0;
                    break;
                case '1':
                    code = (byte)VirtualKeyShort.NUMPAD1;
                    break;
                case '2':
                    code = (byte)VirtualKeyShort.NUMPAD2;
                    break;
                case '3':
                    code = (byte)VirtualKeyShort.NUMPAD3;
                    break;
                case '4':
                    code = (byte)VirtualKeyShort.NUMPAD4;
                    break;
                case '5':
                    code = (byte)VirtualKeyShort.NUMPAD5;
                    break;
                case '6':
                    code = (byte)VirtualKeyShort.NUMPAD6;
                    break;
                case '7':
                    code = (byte)VirtualKeyShort.NUMPAD7;
                    break;
                case '8':
                    code = (byte)VirtualKeyShort.NUMPAD8;
                    break;
                case '9':
                    code = (byte)VirtualKeyShort.NUMPAD9;
                    break;
                case 'q':
                    code = (byte)VirtualKeyShort.KEY_Q;
                    break;

            }
            keybd_event(code, 64, User32.KEYEVENTF.KEYEVENTF_UNICODE, IntPtr.Zero);
        }
        /// <summary>
        /// 移动并点击
        /// </summary>
        public static void moveAndClick()
        {
            Robot.backButtonDown();
            Thread.Sleep(10);

            Robot.moveTo(180, 43);
            Thread.Sleep(60);
            Robot.LeftDowm();
            Thread.Sleep(60);
            Robot.leftUp();
        }
        /// <summary>
        /// 输入
        /// </summary>
        /// <param name="code"></param>
        public static void inputString(string code)
        {
            foreach (var c in code)
            {
                Robot.keyNum(c);
                Thread.Sleep(20);
            }
        }
        // Function dropCardLeave(d)
        //     LeftDown 1
        //     Delay 1
        //     LeftDown 1
        //     MoveTo 626, 138
        //     Delay 50
        //     LeftUp 1
        //     Delay 50
        // End Function
        /// <summary>
        /// 移动
        /// </summary>
        public static void dropCardLeave()
        {
            LeftDowm();
            Thread.Sleep(100);
            leftUp();
            Thread.Sleep(100);
            moveTo(626, 138);

        }


        /// <summary>
        /// 点
        /// </summary>
        public class Point
        {
            /// <summary>
            ///  输入点
            /// </summary>
            /// <returns></returns>
            public static Point inputPoint = new Point(138, 43);
            /// <summary>
            /// x
            /// </summary>
            /// <value></value>
            public int x { get; set; }
            /// <summary>
            /// y
            /// </summary>
            /// <value></value>
            public int y { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }

}
