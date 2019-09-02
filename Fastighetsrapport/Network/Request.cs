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
        private static CredentialCache GetCredentials(Uri uri)
        {
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(uri, "Basic", new NetworkCredential(Creds.UserName, Creds.Password));
            return credentialCache;
        }

        /// <summary>
        /// Make HTTP GET Request given to URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>JSON-data</returns>
        public static string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            var credentials = GetCredentials(new Uri(url));
            if (credentials != null)
            {
                request.Credentials = credentials;
                request.PreAuthenticate = true;
            }

            request.Accept = "application/xml";
            request.Method = "GET";
            request.ContentType = "application/xml; charset=utf-8";

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