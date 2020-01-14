using System;
using System.Net;
using System.IO;

namespace Fastighetsrapport.Network
{
    public class Request
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        
        /// <summary>
        /// Make HTTP GET Request given to URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>JSON-data</returns>
        public static string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            
            request.Accept = "application/xml";
            request.Method = "GET";
            request.ContentType = "application/xml; charset=utf-8";
            request.Headers.Add("Authorization", "Bearer " + StringToken);

      using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}