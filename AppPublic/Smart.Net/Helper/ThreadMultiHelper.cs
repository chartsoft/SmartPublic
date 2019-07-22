using System.Threading;

namespace Smart.Net45.Helper
{
    /// <summary>
    /// 多线程帮助类
    /// </summary>
    public class ThreadMultiHelper
    {
        /// <summary>
        /// 声明线程完成委托
        /// </summary>
        public delegate void DelegateComplete();
        /// <summary>
        /// 声明线程执行的任务委托
        /// </summary>
        public delegate void DelegateWork(int taskindex, int threadindex);
        /// <summary>
        /// 线程完成委托
        /// </summary>
        public DelegateComplete CompleteEvent;
        /// <summary>
        /// 线程执行的任务委托
        /// </summary>
        public DelegateWork WorkMethod;


        private ManualResetEvent[] _resets;
        private readonly int _taskCount;
        private readonly int _threadCount = 5;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskcount">任务数量</param>
        public ThreadMultiHelper(int taskcount)
        {
            _taskCount = taskcount;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskcount">任务数量</param>
        /// <param name="threadCount">执行线程数</param>
        public ThreadMultiHelper(int taskcount, int threadCount)
        {
            _taskCount = taskcount;
            _threadCount = threadCount;
        }
        /// <summary>
        /// 开始执行线程任务
        /// </summary>
        public void Start()
        {
            if (_taskCount < _threadCount)
            {
                //任务数小于线程数的
                _resets = new ManualResetEvent[_taskCount];
                for (var j = 0; j < _taskCount; j++)
                {
                    _resets[j] = new ManualResetEvent(false);
                    ThreadPool.QueueUserWorkItem(Work, new object[] { j, j });
                }
            }
            else
            {
                _resets = new ManualResetEvent[_threadCount];
                //任务数大于线程数 先把线程数的任务启动
                for (var i = 0; i < _threadCount; i++)
                {
                    _resets[i] = new ManualResetEvent(false);
                    ThreadPool.QueueUserWorkItem(Work, new object[] { i, i });
                }
                //完成一个任务后在利用完成的那个线程执行下一个任务
                var receivereset = WaitHandle.WaitAny(_resets);
                for (var l = _threadCount; l < _taskCount; l++)
                {
                    _resets[receivereset].Reset();
                    ThreadPool.QueueUserWorkItem(Work, new object[] { l, receivereset });
                    receivereset = WaitHandle.WaitAny(_resets);
                }
            }

            foreach (var v in _resets)
            {
                v.WaitOne();
            }
            //WaitHandle.WaitAll(_resets);
            CompleteEvent?.Invoke();
        }
        private void Work(object arg)
        {
            var taskindex = int.Parse(((object[])arg)[0].ToString());
            var resetindex = int.Parse(((object[])arg)[1].ToString());
            WorkMethod?.Invoke(taskindex + 1, resetindex + 1);
            _resets[resetindex].Set();
        }
    }
}
