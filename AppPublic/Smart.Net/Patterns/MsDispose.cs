using System;

namespace Smart.Net45.Patterns
{
    /// <inheritdoc />
    /// <summary>
    /// 在继承关系的基类中使用该模式
    /// </summary>
    public class MsDispose : IDisposable
    {
        private bool _disposed;
        /// <inheritdoc />
        /// <summary>
        /// 手动调用显示终结
        /// </summary>
        public void Dispose()
        {
            Close();
        }
        /// <summary>
        /// 释放非托管资源
        /// 向GC标示托管资源可回收
        /// </summary>
        public void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 系统回收
        /// </summary>
        ~MsDispose()
        {
            Dispose(false);
        }
        /// <summary>
        /// 清理非托管资源
        /// </summary>
        /// <param name="disposing">Should be true when calling from Dispose().</param>
        public void Dispose(bool disposing)
        {
            // 允许多次调用Dispose方法
            //但不多次处理 
            if (_disposed) return;
            lock (this)
            {
                if (disposing)
                {
                    // 这里表示程序正在显示地调用Dispose方法
                    // 可以使用引用了其他对象的域
                    // 因为它们所引用的对象的Finalize方法还没有被调用
                    ReleaseUnManagedResource();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// 在子类中重写
        /// 以释放非托管资源
        /// </summary>
        public virtual void ReleaseUnManagedResource() { }

    }
}
