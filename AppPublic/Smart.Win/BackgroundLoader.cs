using System;
using System.ComponentModel;

namespace Smart.Win
{
    /// <summary>
    /// 后台加载
    /// </summary>
    public class BackgroundLoader
    {
        private readonly Action _action;
        private readonly Action _completedWork;
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="doWork">加载动作内容</param>
        /// <param name="completedWork">完成后内容</param>
        public BackgroundLoader(Action doWork,Action completedWork)
        {
            _action = doWork;
            _completedWork = completedWork;
            _worker.DoWork += bw_DoWork;
            _worker.RunWorkerCompleted += bw_RunWorkerCompleted;
            _worker.RunWorkerAsync();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _completedWork?.Invoke();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            _action?.Invoke();
        }
    }
}
