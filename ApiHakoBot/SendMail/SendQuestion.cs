using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ApiHakoBot.SendMail
{
    public class SendQuestion
    {
        [HttpPost]
        public  string PostRequest(string question, string url)
        {

            //Data parameter Example
            //string data = "name=" + value

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.ContentLength = question.Length;

            var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
            streamWriter.Write(question);
            streamWriter.Close();

            var message = httpRequest.GetResponse().ToString();

            return message;
        }

        //public static HttpWebResponse SendGetRequest(string url)
        //{

        //    HttpWebRequest httpRequest = HttpWebRequest.Create(url);
        //    httpRequest.Method = "GET";

        //    return httpRequest.GetResponse();
        //}
    }
}