using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Wings.Saas.Service;

namespace Worker
{
    public class ScanWorker
    {

        public string whiteLogin()
        {
            Thread.Sleep(5000);
            var bitmap = this.doCodeAnd("23333032");
            Console.WriteLine("shifou baipin:" + isWhiteImage(bitmap));

            if (isWhiteImage(bitmap))
            {
                relogin();
                Thread.Sleep(2000);
            }
            else
            {
                // this.doCode(i.ToString());
            }
            bitmap.Dispose();
            return "ok";
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string start( int start,  int end)
        {
            Thread.Sleep(5000);
            Console.WriteLine(start + "-" + end);
            if (start <= end)
            {
                for (var i = start; i < end; i++)
                {
                    var bitmap = this.doCodeAnd(i.ToString());
                    if (isWhiteImage(bitmap))
                    {
                        this.relogin();
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        // this.doCode(i.ToString());
                    }
                    bitmap.Dispose();
                }

            }
            return "失败,启动大于结束";
        }

        public string doOne(int code)
        {
            var bitmap = this.doCodeAnd(code.ToString());
            if (isWhiteImage(bitmap))
            {
                relogin();
                Thread.Sleep(2000);
            }
            else
            {
                // this.doCode(i.ToString());
            }
            Thread.Sleep(2000);
            bitmap.Dispose();
            return code.ToString();
        }

        /// <summary>
        /// 重新登录
        /// </summary>
        /// <returns></returns>
        public object relogin()
        {
            Thread.Sleep(5000);
            Robot.moveTo(37, 53);
            Robot.LeftDowm();
            Thread.Sleep(50);
            Robot.leftUp();
            Thread.Sleep(100);
            Robot.moveTo(37, 310);
            Thread.Sleep(1000);
            Robot.LeftDowm();
            Thread.Sleep(20);
            Robot.leftUp();
            Thread.Sleep(10000);
            Robot.moveTo(610, 400);
            Thread.Sleep(5000);
            Robot.LeftDowm();
            Thread.Sleep(50);
            Robot.leftUp();
            Thread.Sleep(5000);

            //Robot.inputString("qq864544407");
            Robot.inputString("704104");
            Thread.Sleep(500);
            Robot.keyEnter();
            Thread.Sleep(5000);

            return true;
        }
        /// <summary>
        /// 处理一条信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Bitmap doCodeAnd( string code)
        {
            Robot.moveAndClick();
            Thread.Sleep(50);
            for (var i = 0; i < 20; i++)
            {
                Robot.backButtonDown();
                Thread.Sleep(50);
            }
            Thread.Sleep(200);
            Robot.inputString(code);
            Thread.Sleep(700);
            var bitmap = Capture.CaptureScreen(IntPtr.Zero);

            System.IO.MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            OSSService.uploadBitmap("dingding1314", code + ".png", bitmap);
            return bitmap;
        }

        /// <summary>
        /// 处理一条信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Bitmap doCode( string code)
        {
            Robot.moveAndClick();
            Thread.Sleep(100);
            Robot.inputString(code);
            // Robot.keyEnter();
            Thread.Sleep(1000);
            var bitmap = Capture.CaptureWindowRectangle(IntPtr.Zero, new System.Drawing.Rectangle { X = 100, Y = 130, Width = 250, Height = 80 });

            System.IO.MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            bitmap.Save("test.png");
            OSSService.uploadBitmap("my-dingding", code + ".png", bitmap);
            return bitmap;
        }
        /// <summary>
        /// 是否白屏
        /// </summary>
        /// <returns></returns>
        public bool isWhite()
        {

            var fs = File.ReadAllBytes("10210571.jpg");
            Bitmap bitmap = new Bitmap(250, 74);
            System.IO.MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            bitmap.Save("test.png");
            var c1 = bitmap.GetPixel(20, 20);
            var c2 = bitmap.GetPixel(40, 40);
            var c3 = bitmap.GetPixel(60, 60);
            if (isZero(c1) && isZero(c2) && isZero(c3))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 是否白屏
        /// </summary>
        /// <returns></returns>
        public static bool isWhiteImage(Bitmap bitmap)
        {

            System.IO.MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            bitmap.Save("test.png");
            var c1 = bitmap.GetPixel(20, 20);
            var c2 = bitmap.GetPixel(40, 40);
            var c3 = bitmap.GetPixel(60, 60);
            if (isZero(c1) && isZero(c2) && isZero(c3))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static bool isZero(System.Drawing.Color c)
        {
            //Console.WriteLine ("A" + c.A + "," + "B" + c.B + "G:" + c.G + "R:" + c.G);
            return c.A == 255 && c.R == 255 && c.G == 255 && c.B == 255;
        }

        /// <summary>
        /// 查询到不存在会员记录
        /// </summary>
        /// <returns></returns>
        public bool CheckNotExsitMemberRecord(int startHeight, int index)
        {
            var bitmap = Capture.CaptureWindowRectangle(IntPtr.Zero, new System.Drawing.Rectangle { X = 445, Y = startHeight + 20, Width = 25, Height = 20 });
            System.IO.MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            File.WriteAllBytes(index + ".png", ms.GetBuffer());
            var minY = startHeight;
            var maxY = startHeight + 15;
            var result = true;
            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 20; y++)
                {

                    if (isZero(bitmap.GetPixel(x, y)) != true)
                    {

                        Console.WriteLine("x:" + x + "  y:" + y);
                        return false;
                    }
                }
            }
            return result;

        }

        public object CheckPage()
        {
            Thread.Sleep(3000);

            var result = new Dictionary<int, string>();
            var startHeight = 265;
            var recordHeight = 76;

            for (var i = 0; i < 10; i++)
            {
                var gult = i;
                if (i < 3)
                {
                    gult = i - 3;
                }
                if (i > 5)
                {
                    gult = i * 2;
                }
                var weijiaru = CheckNotExsitMemberRecord(startHeight + i * recordHeight - gult, i);

                if (weijiaru)
                {
                    Robot.moveTo(197, startHeight + i * recordHeight - gult - 15);
                    Robot.LeftDowm();
                    Thread.Sleep(50);
                    Robot.leftUp();
                    Thread.Sleep(200);
                }
                result.Add(i, CheckNotExsitMemberRecord(startHeight + i * recordHeight - gult, i) ? "未加入" : "已加入");
            }
            return result;

        }
        /// <summary>
        /// 点击下一页
        /// </summary>
        /// <returns></returns>
        public object ClickNext(int num = 1)
        {
            Thread.Sleep(4000);
            for (var i = 0; i < num; i++)
            {
                Robot.moveTo(1400, 762);
                Robot.LeftDowm();
                Thread.Sleep(200);
                Robot.leftUp();
                Thread.Sleep(200);
            }
            return true;

        }

    }

}
