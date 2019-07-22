using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using Smart.Net45.Patterns;

namespace Smart.Net45.Helper
{
    /// <inheritdoc />
    /// <summary>
    /// WebApi辅助类
    /// </summary>
    public class WebApiHost : MsDispose
    {
        /// <summary>
        /// 托管Server
        /// </summary>
        public HttpSelfHostServer HostServer { get; private set; }

        /// <summary>
        /// 配置自托管
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="configRouteAction"></param>
        /// <param name="maxUrlLength"></param>
        /// <param name="maxBufferSize"></param>
        /// <param name="maxReceivedMessageSize"></param>
        public void SelfHostConfig(Uri uri, Action<HttpSelfHostConfiguration> configRouteAction, int maxUrlLength = 0, int maxBufferSize = 0, int maxReceivedMessageSize = 0)
        {
            var config = new HttpSelfHostConfiguration(uri);
            if (maxUrlLength > 0) config.Properties["maxUrlLength"] = maxUrlLength;
            if (maxBufferSize > 0) config.MaxBufferSize = maxBufferSize;
            if (maxReceivedMessageSize > 0) config.MaxReceivedMessageSize = maxUrlLength;
            config.MessageHandlers.Add(new CrossDomainHandler());
            configRouteAction(config);
            var server = new HttpSelfHostServer(config);
            HostServer = server;
        }
        /// <summary>
        /// 启动自托管
        /// </summary>
        public bool SelfHostStart(out string error)
        {
            error = string.Empty;
            if (HostServer == null)
            {
                error = "请先配置自托管服务";
                return false;
            }
            HostServer.OpenAsync().Wait();
            return true;
        }
        /// <summary>
        /// 停止自托管服务
        /// </summary>
        public bool SelfHostStop(out string error)
        {
            error = string.Empty;
            if (HostServer == null)
            {
                error = "请先配置自托管服务";
                return false;
            }
            HostServer.CloseAsync();
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        /// 释放非托管资源
        /// </summary>
        public override void ReleaseUnManagedResource()
        {
            HostServer.Dispose();
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// 跨域Handler
    /// </summary>
    public class CrossDomainHandler : DelegatingHandler
    {
        /// <summary>
        /// 跨越设置
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = base.SendAsync(request, cancellationToken);
            response.Result.Headers.Add("Access-Control-Allow-Origin", "*");
            //response.Result.Headers.Add("*", "*");
            return response;
        }
    }

}

