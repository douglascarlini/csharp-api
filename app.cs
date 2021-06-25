using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;

namespace CSharp_Shell
{
    public static class Program
    {

        public static HttpListener listener;

        public static void Main()
        {

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:80/");
            listener.Start();

            while (listener.IsListening)
            {

                HttpListenerContext context = listener.GetContext();

                try {

                    // Client IP (with proxy)
                    string[] ip = context.Request.Headers.GetValues("X-Real-IP");

                    // Client IP (no proxy)
                    //string ip = context.Request.RemoteEndPoint.ToString();

                    // Prepare Response
                    string type = context.Request.HttpMethod;
                    string resp = type + " from " + ip[0];
                    string url = context.Request.RawUrl;
                    resp += " on " + url;

                    // Response Headers
                    context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(resp);
                    context.Response.AddHeader("Content-Type", "application/json");
                    context.Response.StatusCode = (int)HttpStatusCode.OK;

                    using (Stream stream = context.Response.OutputStream)
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {

                            // Send Response
                            writer.Write(resp);

                        }
                    }

                } catch (Exception e) {

                    Console.WriteLine("Error: {0}", e);

                }

            }

        }

    }
}
