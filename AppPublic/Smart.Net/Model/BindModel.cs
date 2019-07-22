using System;

namespace Smart.Net45.Model
{
    /// <summary>
    /// 绑定信息模型
    /// </summary>
    public class BindModel
    {
        /// <summary>
        /// 协议
        /// </summary>
        public string Protocol { get; set; } = "Http";

        /// <summary>
        /// 绑定IP
        /// </summary>
        public string BinIp { get; set; } = "*";

        /// <summary>
        /// 绑定端口
        /// </summary>
        public int BinPort { get; set; } = 80;
        /// <summary>
        /// 主机名
        /// </summary>
        public string Host { get; set; }=string.Empty;
        /// <summary>
        /// 绑定信息
        /// </summary>
        public string BindingInformation { get; set; }
    }
}
