using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormScreenSave
{
    public partial class ScreenSave : Form
    {
        public ScreenSave()
        {
            InitializeComponent();
        }


        public void Start(string host = "localhost", int port = 8888)
        {
            //размеры экрана
            var rect = Screen.PrimaryScreen.Bounds;

            //конкетимся к серверу, получаем поток
            using (var tcp = new TcpClient(host, port)) //создаем TcpClient
            using (var stream = tcp.GetStream()) //получаем сетевой поток
            using (var bw = new BinaryWriter(stream)) //создаем BinaryWriter
            using (var bmp = new Bitmap(rect.Width, rect.Height)) //создаем битмап для отправки
            using (var gr = Graphics.FromImage(bmp)) //создаем канву
            using (var ms = new MemoryStream()) //создаем временный поток для сжатого изображения
                while (true) //делаем бесконечно
                {
                    //захватываем изображение экрана
                    gr.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
                    //конвертируем изображение в массив байт в формате jpeg
                    ms.Position = 0;
                    bmp.Save(ms, ImageFormat.Jpeg);
                    var arr = ms.ToArray(); //получаем массив байт
                                            //отправляем длину массива данных
                    bw.Write(arr.Length);
                    //отправляем массив
                    bw.Write(arr);
                    //точно, отправялем
                    bw.Flush();
                }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {

            //размеры экрана
            var rect = Screen.PrimaryScreen.Bounds;

            //конкетимся к серверу, получаем поток
            string host = "localhost"; int port = 8888;
            using (var tcp = new TcpClient(host, port)) //создаем TcpClient
            using (var stream = tcp.GetStream()) //получаем сетевой поток
            using (var bw = new BinaryWriter(stream)) //создаем BinaryWriter
            using (var bmp = new Bitmap(rect.Width, rect.Height)) //создаем битмап для отправки
            using (var gr = Graphics.FromImage(bmp)) //создаем канву
            using (var ms = new MemoryStream()) //создаем временный поток для сжатого изображения
            {
                //захватываем изображение экрана
                gr.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
                //конвертируем изображение в массив байт в формате jpeg
                ms.Position = 0;
                bmp.Save(ms, ImageFormat.Jpeg);
                Bitmap bitmap = new Bitmap(bmp, pictureBox1.Size);
                pictureBox1.Image = bitmap;


                var arr = ms.ToArray(); //получаем массив байт
                                        //отправляем длину массива данных
                bw.Write(arr.Length);
                //отправляем массив
                bw.Write(arr);
                //точно, отправялем
                bw.Flush();
            }   
               







        }
    }
}
