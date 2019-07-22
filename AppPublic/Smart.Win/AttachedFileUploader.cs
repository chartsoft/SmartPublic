using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Smart.Win
{
    /// <summary>
    /// 附件上传类
    /// </summary>
    public class AttachedFileUploader
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fullName"></param>
        /// <param name="attachedFileType"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// Perform the equivalent of posting a form with a fullName and two files, in HTML:
        /// <form action="{url}" method="post" enctype="multipart/form-data">
        ///     <input type="text" name="fullName" />
        ///     <input type="file" name="file1" />
        /// </form>
        /// </code>
        /// </example>
        public string Upload(string url, string fullName, int attachedFileType, string cookies)
        {
            using (var fileStream = File.OpenRead(fullName))
            {
                // Convert each of the three inputs into HttpContent objects

                var filename = Path.GetFileName(fullName);
                // examples of converting both Stream and byte [] to HttpContent objects
                // representing input type file
                HttpContent fileStreamContent = new StreamContent(fileStream);

                var clientHandler = new HttpClientHandler
                {
                    AllowAutoRedirect = true,
                    UseCookies = true,
                    CookieContainer = new CookieContainer()
                };
                // Submit the form using HttpClient and 
                // create form data as Multipart (enctype="multipart/form-data")

                using (var client = new HttpClient(clientHandler))
                using (var formData = new MultipartFormDataContent())
                {
                    var uri = new Uri(url);

                    if (!string.IsNullOrWhiteSpace(cookies))
                        clientHandler.CookieContainer.SetCookies(uri, cookies);

                    // Add the HttpContent objects to the form data

                    // <input type="text" name="fullName" />
                    formData.Add(new StringContent(filename), "FileName");
                    formData.Add(new StringContent(attachedFileType.ToString()), "AttachedFileType");
                    // <input type="file" name="file1" />
                    formData.Add(fileStreamContent, "Filedata", filename);

                    // Actually invoke the request to the server

                    // equivalent to (action="{url}" method="post")
                    HttpResponseMessage response;
                    try
                    {
                        response = client.PostAsync(url, formData).Result;
                    }
                    catch (Exception)//服务器连接不成功
                    {
                        return null;
                    }
                    return !response.IsSuccessStatusCode ? null : response.Content.ReadAsStringAsync().Result;
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public string Delete(string url, string cookies)
        {
            var clientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };

            using (var client = new HttpClient(clientHandler))
            {
                var uri = new Uri(url);
                if (!string.IsNullOrWhiteSpace(cookies))
                    clientHandler.CookieContainer.SetCookies(uri, cookies);
                HttpResponseMessage response;
                try
                {
                    response = client.DeleteAsync(url).Result;
                }
                catch (Exception)//服务器连接不成功
                {
                    return null;
                }
                return !response.IsSuccessStatusCode ? null : response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
