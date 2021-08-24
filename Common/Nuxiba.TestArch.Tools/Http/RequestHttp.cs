using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Nuxiba.TestArch.Tools.Http
{
    public static class RequestHttp
    {
        public static T Get<T>(string url, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            try
            {
                result = GetWithError<T>(url, headers);
            }
            catch (WebException)
            {

            }
            catch (Exception)
            {

            }

            return result;
        }

        public static T GetWithError<T>(string url, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Timeout = 300000;
            httpWebRequest.ReadWriteTimeout = 600000;

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    httpWebRequest.Headers[header.Key] = header.Value;
                }
            }

            using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (var stream = httpWebResponse.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        var @string = streamReader.ReadToEnd();

                        if (typeof(T) == typeof(string))
                        {
                            result = (T)(object)@string;
                        }
                        else
                        {
                            result = JsonConvert.DeserializeObject<T>(@string);
                        }

                        streamReader.Close();
                    }

                    stream.Flush();
                    stream.Close();
                }

                httpWebResponse.Close();
            }

            return result;
        }

        public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            try
            {
                result = await GetWithErrorAsync<T>(url, headers);
            }
            catch (WebException)
            {

            }
            catch (Exception)
            {

            }

            return result;
        }

        public static async Task<T> GetWithErrorAsync<T>(string url, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Timeout = 300000;
            httpWebRequest.ReadWriteTimeout = 600000;

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    httpWebRequest.Headers[header.Key] = header.Value;
                }
            }

            using (var httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync())
            {
                using (var stream = httpWebResponse.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        var @string = await streamReader.ReadToEndAsync();

                        if (typeof(T) == typeof(string))
                        {
                            result = (T)(object)@string;
                        }
                        else
                        {
                            result = JsonConvert.DeserializeObject<T>(@string);
                        }

                        streamReader.Close();
                    }

                    await stream.FlushAsync();
                    stream.Close();
                }

                httpWebResponse.Close();
            }

            return result;
        }

        public static async Task<T> GetWithErrorFromBodyAsync<T>(string url, Dictionary<string, string> headers = null, object objectSendFromBody = null)
        {
            var result = default(T);

            string model = string.Empty;
            HttpClient client = new HttpClient();

            if (objectSendFromBody != null)
            {
                model = typeof(string) == objectSendFromBody.GetType() ? (string)objectSendFromBody : JsonConvert.SerializeObject(objectSendFromBody);
            }

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Content = new StringContent(model, Encoding.UTF8, "application/json"),
            };

            var response = await client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var @string = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (typeof(T) == typeof(string))
            {
                result = (T)(object)@string;
            }
            else
            {
                result = JsonConvert.DeserializeObject<T>(@string);
            }

            return result;
        }

        public static T PostJson<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            try
            {
                result = PostJsonWithError<T>(url, objectToSend, headers);
            }
            catch (WebException)
            {

            }
            catch (Exception)
            {

            }

            return result;
        }

        public static T PostJsonWithError<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Timeout = 300000;
            httpWebRequest.ReadWriteTimeout = 600000;

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    httpWebRequest.Headers[header.Key] = header.Value;
                }
            }

            if (objectToSend != null)
            {
                var json = typeof(string) == objectToSend.GetType() ? (string)objectToSend : JsonConvert.SerializeObject(objectToSend);
                var dataToSend = Encoding.UTF8.GetBytes(json);

                httpWebRequest.ContentLength = dataToSend.Length;

                using (var stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(dataToSend, 0, dataToSend.Length);
                    stream.Flush();
                    stream.Close();
                }
            }

            if (url.Trim().ToLower().StartsWith("https"))
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            }

            using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var @string = streamReader.ReadToEnd();

                    if (typeof(T) == typeof(string))
                    {
                        result = (T)(object)@string;
                    }
                    else
                    {
                        result = JsonConvert.DeserializeObject<T>(@string);
                    }

                    streamReader.Close();
                }

                httpResponse.Close();
            }

            return result;
        }

        static bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static async Task<T> PostJsonAsync<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            try
            {
                result = await PostJsonWithErrorAsync<T>(url, objectToSend, headers);
            }
            catch (WebException)// webException)
            {
                //var response = (string)null;
                //var errorMessage = string.Empty;
                //try
                //{
                //    var res = (System.Net.HttpWebResponse)webException.Response;
                //    var t = res.StatusCode;
                //    var t1 = (int)res.StatusCode;
                //    return webException.Response == null ? null : new System.IO.StreamReader(webException.Response.GetResponseStream()).ReadToEnd();

                //}
                //catch (Exception)
                //{

                //}
            }
            catch (Exception)
            {

            }

            return result;
        }

        public static async Task<T> PostJsonWithErrorAsync<T>(string url, object objectToSend, Dictionary<string, string> headers = null)
        {
            var result = default(T);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Timeout = 300000;
            httpWebRequest.ReadWriteTimeout = 600000;

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    httpWebRequest.Headers[header.Key] = header.Value;
                }
            }

            if (objectToSend != null)
            {
                var json = typeof(string) == objectToSend.GetType() ? (string)objectToSend : JsonConvert.SerializeObject(objectToSend);
                var dataToSend = Encoding.UTF8.GetBytes(json);

                httpWebRequest.ContentLength = dataToSend.Length;

                using (var stream = await httpWebRequest.GetRequestStreamAsync())
                {
                    await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                    await stream.FlushAsync();
                    stream.Close();
                }
            }

            if (url.Trim().ToLower().StartsWith("https"))
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            }

            using (var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync())
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var @string = await streamReader.ReadToEndAsync();

                    if (typeof(T) == typeof(string))
                    {
                        result = (T)(object)@string;
                    }
                    else
                    {
                        result = JsonConvert.DeserializeObject<T>(@string);
                    }

                    streamReader.Close();
                }

                httpResponse.Close();
            }

            return result;
        }
    }
}
