using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Collections.Generic;


namespace NUIXAMLProject1
{

    public partial class Scene1Page : View
    {
        const string url = "http://211.250.175.87:5100/predict";
        static string filePath = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/maksssksksss48.png";

        public Scene1Page()
        {
            InitializeComponent();
        }

        public object lockObj = new object();
        public string FileUploadRequestPost(string url, string filePath)
        {
            lock(lockObj)
            {
                string resdata = "";

                try
                {
                    string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                    string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";

                    byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                    byte[] endboundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                    byte[] buffer = new byte[4096];

                    Console.WriteLine(">>> 1");
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.ServicePoint.Expect100Continue = false;
                    request.Method = "POST";
                    request.ContentType = "multipart/form-data; boundary=" + boundary;

                    using (Stream requestStream = request.GetRequestStream())
                    {

                        requestStream.Write(boundarybytes, 0, boundarybytes.Length);
                        string header = string.Format(headerTemplate, "file", System.IO.Path.GetFileName(filePath), "multipart/form-data");
                        byte[] headerByte = System.Text.Encoding.UTF8.GetBytes(header);
                        requestStream.Write(headerByte, 0, headerByte.Length);

                        int bytesRead = 0;
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                requestStream.Write(buffer, 0, bytesRead);
                            }
                        }
                        requestStream.Write(endboundarybytes, 0, endboundarybytes.Length);

                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {
                            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8, true);
                            resdata = sr.ReadToEnd();
                        }

                        Console.WriteLine(">>> E " + resdata);
                    }



                }
                catch (Exception excepton)
                {

                }
                finally
                {

                }

                return resdata;
            }
        }

        private bool ImageView_TouchEvent(object source, TouchEventArgs e)
        {
            View touchedView = source as View;
            if(e.Touch.GetState(0) == PointStateType.Down)
            {
                touchedView.BackgroundColor = Color.Red;

                Animation ani = new Animation(1000);
                ani.Looping = true;
                ani.AnimateTo(touchedView, "Scale", new Vector3(2.0f, 2.0f, 1.0f));
                ani.Play();

                string res = FileUploadRequestPost(url, filePath);
            }

            return false;
        }
    }
}
