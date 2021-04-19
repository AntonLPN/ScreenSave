using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsCatchScreen
{
    public partial class CatcherForm : Form
    {


        public CatcherForm()
        {
            InitializeComponent();
        }

        public Image GetScreenshots(int port = 8888)
        {
            var list = new TcpListener(IPAddress.Loopback, port);
          
            list.Start();
            var len =0;
            using (var tcp = list.AcceptTcpClient())//принимаем конект
            using (var stream = tcp.GetStream())//создаем сетевой поток
            using (var br = new BinaryReader(stream)) //создаем BinaryReader
                while (true)//делаем бесконечно
                {
                    //принимаем длину массива
                    try
                    {
                         len = br.ReadInt32();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                 
                    //принимаем массив
                    var arr = br.ReadBytes(len);
                    using (var ms = new MemoryStream(arr))//создаем временный поток для сжатого изображения
                    {
                        ////создаем изображение
                        //yield return Bitmap.FromStream(ms);
                        //создаем изображение
                     
                        
                        return Bitmap.FromStream(ms);
                    }
                }
        }


        private void button1_Click(object sender, EventArgs e)
        {


            ThreadPool.QueueUserWorkItem(
            delegate
            {


                var res = GetScreenshots();
              
                if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                // заносим скриншот в PictureBox
                Bitmap bitmap = new Bitmap(res, pictureBox1.Size);
                pictureBox1.Image = bitmap;

                ////получаем в цикле скриншоты с клиента
                //foreach (var bmp in GetScreenshots())
                //{
                //   // уничтожаем предыдущее изображение
                //    if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                //   // заносим скриншот в PictureBox
                //    Bitmap bitmap = new Bitmap(bmp, pictureBox1.Size);
                //    pictureBox1.Image = bitmap;
                //}
            });
















        }
    }
}
