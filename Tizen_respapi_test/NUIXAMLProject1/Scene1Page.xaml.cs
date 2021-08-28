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
        static string filePath = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/tizen.png";

        static string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
        static string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
        static string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";

        byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
        byte[] endboundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
        byte[] buffer = new byte[4096];
        
        public Scene1Page()
        {
            InitializeComponent();
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

                string resdata;
                try
                {

                    Console.WriteLine(">>> 1");
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.ServicePoint.Expect100Continue = false;
                    request.Method = "POST";
                    //                request.CookieContainer = cookie;
                    request.ContentType = "multipart/form-data; boundary=" + boundary;

                    using (Stream requestStream = request.GetRequestStream())
                    {
/*
                        foreach (KeyValuePair<String, String> param in parameters) 
                        { 
                            requestStream.Write(boundarybytes, 0, boundarybytes.Length); 
                            string formItem = string.Format(formdataTemplate, param.Key, param.Value); 
                            byte[] formItemByte = System.Text.Encoding.UTF8.GetBytes(formItem); 
                            requestStream.Write(formItemByte, 0, formItemByte.Length); 
                        }
  */                      
                        requestStream.Write(boundarybytes, 0, boundarybytes.Length); 
                        //octet-stream 으로 변경 처리 진행 
                        //string header = string.Format(headerTemplate, "outfaxfile", Path.GetFileName(filePath), "multipart/form-data");
                        string header = string.Format(headerTemplate, "outfaxfile", System.IO.Path.GetFileName(filePath), "octet-stream"); 
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
            }

            return false;
        }
    }
}
