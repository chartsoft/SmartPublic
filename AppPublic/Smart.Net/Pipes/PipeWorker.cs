﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Smart.Net45.Pipes
{
    internal class PipeWorker
    {
        private readonly TaskScheduler _callbackThread;

        private static TaskScheduler CurrentTaskScheduler =>
            (SynchronizationContext.Current != null
            ? TaskScheduler.FromCurrentSynchronizationContext()
            : TaskScheduler.Default);

        public event WorkerSucceededEventHandler Succeeded;
        public event WorkerExceptionEventHandler Error;

        public PipeWorker() : this(CurrentTaskScheduler) { }

        public PipeWorker(TaskScheduler callbackThread)
        {
            _callbackThread = callbackThread;
        }

        public void DoWork(Action action)
        {
            new Task(DoWorkImpl, action, CancellationToken.None, TaskCreationOptions.LongRunning).Start();
        }

        private void DoWorkImpl(object oAction)
        {
            var action = (Action)oAction;
            try
            {
                action();
                Callback(Succeed);
            }
            catch (Exception e)
            {
                Callback(() => Fail(e));
            }
        }

        private void Succeed()
        {
            Succeeded?.Invoke();
        }

        private void Fail(Exception exception)
        {
            Error?.Invoke(exception);
        }

        private void Callback(Action action)
        {
            Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, _callbackThread);
        }
    }

    internal delegate void WorkerSucceededEventHandler();
    internal delegate void WorkerExceptionEventHandler(Exception exception);
}
