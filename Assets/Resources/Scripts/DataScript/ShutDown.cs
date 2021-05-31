using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;

namespace Resources.Scripts.DataScript
{
    public class ShutDown
    {
        private string url { get; }

        public ShutDown(string shutdownApi, int port)
        {
            this.url = $"http://127.0.0.1:{port}/{shutdownApi}";
        }

        public void ShutDownOldServer()
        {
            var httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            HttpWebResponse httpResponse;
            try
            {
                httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
            }
            catch (WebException we)
            {
                httpResponse = (HttpWebResponse) we.Response;
            }
            Debug.Log($"Try shutting down the server, received response code {httpResponse.StatusCode}");
        }
    }
}