using System;
using System.Collections.Generic;
using Microsoft.Web.Administration;
using Smart.Net45.Helper;

namespace Smart.Net45.Model
{
    /// <summary>
    /// 站点信息
    /// </summary>
    public class SiteModel
    {
        /// <summary>
        /// 站点编号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 模式名称
        /// </summary>
        public string SchemaName { get; set; }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 物理路径
        /// </summary>
        public string PhysicalPath { get; set; }
        /// <summary>
        /// 应用程序池
        /// </summary>
        public string ApplicationPoolName { get; set; }
        /// <summary>
        /// 已启用的协议
        /// </summary>
        public string EnabledProtocols { get; set; }
        /// <summary>
        /// 已启用状态
        /// </summary>
        public ObjectState State { get; set; }
        /// <summary>
        /// 是否自动启动
        /// </summary>
        public bool ServerAutoStart { get; set; }
        /// <summary>
        /// 连接超时时间
        /// </summary>
        public TimeSpan ConnectionTimeout { get; set; }
        /// <summary>
        /// 最大连接并发数
        /// </summary>
        public long MaxConnections { get; set; }
        /// <summary>
        /// 最大宽带
        /// </summary>
        public long MaxBandwidth { get; set; }
        /// <summary>
        /// 绑定信息
        /// </summary>
        public List<BindModel> Bindings { get; set; }
    }
}
