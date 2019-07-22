using System;
using System.Threading.Tasks;
using Smart.Standard.ApiClient;
using WebApiClient;

namespace Smart.Standard.Extends
{
    public static class HttpApiClientExtends
    {
        public static T GetSyncServicer<T>(this IHttpApiClient client, Func<Task<T>> action)
        {
            using (client)
            {
                var t = action().GetAwaiter().GetResult();
                return t;
            }
        }

        public static void GetSyncServicer(this IHttpApiClient client, Func<Task> action)
        {
            using (client)
            {
                action().GetAwaiter();
            }
        }
    }
}
