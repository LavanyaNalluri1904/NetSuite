using System;
using System.IO;
using System.Net;
using System.Text;

namespace StandardUtilities
{
    /// <summary>
    /// @Author - Pavan Parmar
    /// </summary>
    public class RestWebservice
    {
        /// <summary>
		/// To perform a HTTP POST Request
		/// </summary>
		/// <param name="url">Url</param>
		/// <param name="jsonContent">JSON Content</param>
		/// <param name="authorizationHeader">Authorization Header</param>
		/// <param name="contentType">Content Type</param>
		/// <returns>Response</returns>
		public static string Post(string url, string jsonContent, string authorizationHeader, string contentType)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                Byte[] byteArray = encoding.GetBytes(jsonContent); //JSON content
                request.ContentType = contentType; 
                request.ContentLength = byteArray.Length;
                request.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
                request.Accept = "application/json";

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);//Generate a data stream
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        string responseString = reader.ReadToEnd();
                        return responseString;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while performing POST operation\nException Details: " + ex.Message);
                return "POST ERROR";
            }
        }

        /// <summary>
        /// To perform a HTTP GET Request
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="authorizationHeader">Authorization Header</param>
        /// <returns>Response</returns>
        public static string Get(string url, string authorizationHeader)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
            request.Accept = "application/json";
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    string responseString = reader.ReadToEnd();
                    return responseString;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while performing GET operation\nException Details: " + ex.Message);
                return "GET ERROR";
            }
        }

        /// <summary>
        /// To perform a HTTP DELETE Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authorizationHeader"></param>
        /// <param name="sessionCookie"></param>
        public static void Delete(string url, string authorizationHeader, string sessionCookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "DELETE";
            request.Timeout = 10000;
            request.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
            request.Headers.Add(HttpRequestHeader.Cookie, sessionCookie);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred during DELETE request.\nException Details: " + ex.Message);
                Console.ReadLine();
            }
        }
    }
}
