using System;
using System.Net;
using System.IO;
using System.Text;
using System.Xml.Linq;

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
      request.Headers.Add("Authorization", "Bearer " + "StringToken");

      using (WebResponse response = request.GetResponse())
      {
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
          return reader.ReadToEnd();
        }
      }
    }
    public static string Post(string url, XElement requestXml)
    {
      HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
      byte[] requestInFormOfBytes = Encoding.UTF8.GetBytes(requestXml.ToString());
      req.Method = "POST";
      req.Accept = "application/xml";
      req.ContentType = "application/xml; charset=utf-8";
      req.ContentLength = requestInFormOfBytes.Length;
      req.Headers.Add("Authorization", "Bearer " + "StringToken");

      Stream requestStream = req.GetRequestStream();
      requestStream.Write(requestInFormOfBytes, 0, requestInFormOfBytes.Length);
      requestStream.Close();

      HttpWebResponse response = (HttpWebResponse)req.GetResponse();
      StreamReader respStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
      string receivedResponse = respStream.ReadToEnd();
      respStream.Close();
      response.Close();
      return receivedResponse;
    }
  }
}