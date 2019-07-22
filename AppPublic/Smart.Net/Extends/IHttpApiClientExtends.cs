using System;
using System.Threading.Tasks;
using Smart.Net45.ApiClient;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// httpClient扩展类
    /// </summary>
    public static class HttpApiClientExtends
    {
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>      
        /// <param name="client"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Call<T>(this IHttpApiClient client, Func<Task<T>> action)
        {        
            //using (client)
            //{
                var t = action().GetAwaiter().GetResult();
                return t;
            //}
        }
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="client"></param>
        /// <param name="action"></param>
        public static void Call(this IHttpApiClient client, Func<Task> action)
        {
            using (client)
            {
                action().GetAwaiter();
            }
        }
    }
}
