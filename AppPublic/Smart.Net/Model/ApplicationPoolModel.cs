using Microsoft.Web.Administration;

namespace Smart.Net45.Model
{
    /// <summary>
    /// 应用程池模型
    /// </summary>
    public class ApplicationPoolModel
    {
        /// <summary>
        /// .Net版本
        /// </summary>
        public string ManagedRuntimeVersion { get; set; }
        /// <summary>
        /// 应用程池名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 管道模式
        /// </summary>
        public ManagedPipelineMode ManagedPipelineMode { get; set; }
        /// <summary>
        /// 应用程池状态
        /// </summary>
        public ObjectState State { get; set; }
        /// <summary>
        /// 队列长度
        /// </summary>
        public long QueueLength { get; set; }
        /// <summary>
        /// 最大工作进程数
        /// </summary>
        public long MaxProcessespe { get; set; }
        /// <summary>
        /// 当前工作进程数
        /// </summary>
        public long WorkerProcessesCount { get; set; }
        /// <summary>
        /// 允许Ping
        /// </summary>
        public bool PingingEnabled { get; set; }
        /// <summary>
        /// 启用32位应用程序
        /// </summary>
        public bool Enable32BitAppOnWin64 { get; set; }
    }
}
