using System;
using System.IO;
using System.Net;
using System.Text;

namespace Smart.Standard.Helper
{
    /// <summary>
    /// Http请求类
    /// </summary>
    public class HttpRequestHelper
    {
        private static readonly CookieContainer MCookie = new CookieContainer();

        /// <summary>
        /// Http请求提交(提交数据使用UTF-8传送)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <param name="isCatch">是否捕获到异常；true:捕获到异常</param>
        /// <param name="saveCookie">是否用一个容器来保存cookies默认不保存</param>
        /// <param name="requestType"></param>
        /// <param name="encodingName"></param>
        /// <param name="timeout">超时时间，以毫秒为单位</param>
        /// <param name="contentType"></param>
        /// <param name="referer"></param>
        /// <returns></returns>
        public static string HttpRequest(string url, string value, out bool isCatch, bool saveCookie = false, bool requestType = true, string encodingName = "Utf-8", int timeout = 120000, string contentType = "application/x-www-form-urlencoded", string referer = null)
        {
            string returnString = null;
            isCatch = false;
            try
            {
                //if (value == null) { value = string.Empty; }
                //if (!requestType && !string.IsNullOrWhiteSpace(value))
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var currentUrl = new Uri(url);

                    if (currentUrl.Query != "")
                    {
                        url += "&" + value;
                    }
                    else
                    {
                        url += "?" + value;
                    }
                }
                #region 请求
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, chainsslPolicyErrors) => true;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 120000;
                var method = requestType ? "POST" : "GET";
                request.Method = method;
                request.ContentType = contentType;
                request.Proxy = WebRequest.DefaultWebProxy;
                if (saveCookie)
                {
                    //创建一个容器来保存cookie
                    request.CookieContainer = MCookie;
                }

                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                if (referer != null)
                    request.Referer = referer;
                if (requestType)
                {
                    var encoding = new UTF8Encoding();
                    var data = encoding.GetBytes(value ?? string.Empty);
                    request.ContentLength = data.Length;
                    request.ServicePoint.Expect100Continue = false;
                    var sw = request.GetRequestStream();
                    sw.Write(data, 0, data.Length);
                    sw.Close();
                }

                #endregion

                #region 获取响应消息
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var resStream = response.GetResponseStream();
                        if (resStream != null)
                        {
                            var streamReader = new StreamReader(resStream, Encoding.GetEncoding(encodingName));
                            returnString = streamReader.ReadToEnd();
                            streamReader.Close();
                        }
                        resStream?.Close();
                        //TODO Info

                        return returnString;
                    }
                    return response.StatusCode + "--" + response.StatusDescription;
                }
                #endregion
            }
            catch (Exception ex)
            {
                var msg = $"网络请求异常：{ex.Message}";
                //TODO Error
                isCatch = true;
                return msg;
            }
        }

        /// <summary>
        /// 将实体转换成Post格式字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="kbm"></param>
        /// <param name="keyNameJoinStr">属性名与属性值连接字符串</param>
        /// <param name="splitStr">每组值的分割符</param>
        /// <returns></returns>
        public static string ConvertEntityToKvString<T>(T kbm, string keyNameJoinStr = "=", string splitStr = "&")
        {
            var sb = new StringBuilder();
            var t = typeof(T);
            var pros = t.GetProperties();
            for (var i = 0; i < pros.Length; i++)
            {
                if (i != 0)
                {
                    sb.Append(splitStr);
                }
                var pro = pros[i];
                sb.Append(pro.Name + keyNameJoinStr + pro.GetValue(kbm, null));
            }
            return sb.ToString();
        }
    }
}
