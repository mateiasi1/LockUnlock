using AKSoftware.WebApi.Client;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Documents_Editor
{
    public class DocumentManager
    {
        private static readonly HttpClient client = new HttpClient();
        public List<BaseDocument> baseDocumentsManager = new List<BaseDocument>();
        private static readonly HttpClient _Client = new HttpClient();
        private static JavaScriptSerializer _Serializer = new JavaScriptSerializer();
        static string lockRoute = "http://localhost:5000/LockUnlock/lock";
        static string unlockRoute = "http://localhost:5000/LockUnlock/unlock";
        static string getAll = "http://localhost:5000/LockUnlock";
        static RequestObject requestObject = new RequestObject();
        public DocumentManager(List<BaseDocument> baseDocuments)
        {
            baseDocumentsManager = baseDocuments;
        }
        public void Write(BaseDocument baseDocument)
        {
            IAsyncResult ar = LockAsync(baseDocument);
            if (!ar.IsCompleted)
            {
                ar.AsyncWaitHandle.WaitOne()
;
            }
        }
        public void FinishWrite(BaseDocument baseDocument)
        {
            IAsyncResult ar = UnlockAsync(baseDocument);
            if (!ar.IsCompleted)
            {
                ar.AsyncWaitHandle.WaitOne()
;
            }
        }

        public static async Task LockAsync(BaseDocument baseDocument)
        {
            requestObject.ActionType = ActionType.Lock;
            requestObject.LockedBy = 1;
            requestObject.RowId = baseDocument.Id;

            var json = JsonConvert.SerializeObject(requestObject);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            //client.Timeout = TimeSpan.FromMinutes(5);
            var response = await client.PostAsync(lockRoute, data);
            bool result = Convert.ToBoolean(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine(result);
        }
        static async Task UnlockAsync(BaseDocument baseDocument)
        {
            requestObject.ActionType = ActionType.Unlock;
            requestObject.LockedBy = 1;
            requestObject.RowId = baseDocument.Id;


            var json = JsonConvert.SerializeObject(requestObject);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            //client.Timeout = TimeSpan.FromMinutes(5);
            var response = await client.PostAsync(unlockRoute, data);
            bool result = Convert.ToBoolean(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine(result);
        }


        public void GetAll()
        {
            List<RequestObject> lockedObjects = new List<RequestObject>();
            string responseString = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getAll);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                responseString = reader.ReadToEnd();
                JsonSerializer serializer = new JsonSerializer();
                RequestObject requestObject = new RequestObject();
                var list = JsonConvert.DeserializeObject<List<RequestObject>>(responseString);
                foreach (var item in list)
                {
                    Console.WriteLine("Item ID: " + item.Id + " LockedBy: " + item.LockedBy + " RowId: " + item.RowId + " StartDate: " + item.StartDate + " EndDate: " + item.EndDate);
                }
            }

        }

    }
}
