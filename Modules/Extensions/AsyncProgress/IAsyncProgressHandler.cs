namespace Modules.Extensions.AsyncProgress
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cysharp.Threading.Tasks;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public interface IAsyncProgressHandler
    {
        public int                 TotalTask     { get; }
        public int                 CompletedTask { get; }
        public float               Progress      { get; }
        public event Action<float> ProgressChangeEvent;
        public event Action        CompletedEvent;
    }

    public abstract class AsyncProgressHandler : IAsyncProgressHandler
    {
        public int                 TotalTask     { get; protected set; }
        public int                 CompletedTask { get; protected set; }
        public float               Progress      => (float)this.CompletedTask / this.TotalTask;
        public event Action<float> ProgressChangeEvent;
        public event Action        CompletedEvent;

        protected void OnProgressChange()
        {
            this.ProgressChangeEvent?.Invoke(this.Progress);
        }

        protected void OnCompleted()
        {
            this.CompletedEvent?.Invoke();
        }
    }

    public class UniTaskProgressHandler : AsyncProgressHandler
    {
        public UniTaskProgressHandler()
        {
        }

        public UniTaskProgressHandler(params UniTask[] tasks)
        {
            this.InternalTrackUniTask(tasks);
        }

        public UniTaskProgressHandler(IEnumerable<UniTask> tasks)
        {
            this.InternalTrackUniTask(tasks);
        }

        public void TrackUniTask(params UniTask[] tasks)
        {
            this.InternalTrackUniTask(tasks);
        }

        public void TrackUniTask(IEnumerable<UniTask> tasks)
        {
            this.InternalTrackUniTask(tasks);
        }

        private async void InternalTrackUniTask(IEnumerable<UniTask> tasks)
        {
            this.TotalTask += tasks.Count();
            foreach (var task in tasks)
            {
                await task;
                this.CompletedTask++;
                this.OnProgressChange();
            }
        }
    }

    public class TaskProgressHandler : AsyncProgressHandler
    {
        public TaskProgressHandler()
        {
        }

        public TaskProgressHandler(params Task[] tasks)
        {
            this.InternalTrackTask(tasks);
        }

        public TaskProgressHandler(IEnumerable<Task> tasks)
        {
            this.InternalTrackTask(tasks);
        }

        public void TrackTask(params Task[] tasks)
        {
            this.InternalTrackTask(tasks);
        }

        public void TrackTask(IEnumerable<Task> tasks)
        {
            this.InternalTrackTask(tasks);
        }

        private async void InternalTrackTask(IEnumerable<Task> tasks)
        {
            this.TotalTask += tasks.Count();
            foreach (var task in tasks)
            {
                await task;
                this.CompletedTask++;
                this.OnProgressChange();
            }
        }
    }

    public class AsyncOperationHandleProgressHandler : AsyncProgressHandler
    {
        public AsyncOperationHandleProgressHandler()
        {
        }

        public AsyncOperationHandleProgressHandler(params AsyncOperationHandle[] handles)
        {
            this.InternalTrackAsyncOperationHandle(handles);
        }

        public AsyncOperationHandleProgressHandler(IEnumerable<AsyncOperationHandle> handles)
        {
            this.InternalTrackAsyncOperationHandle(handles);
        }

        public void TrackAsyncOperationHandle(params AsyncOperationHandle[] handles)
        {
            this.InternalTrackAsyncOperationHandle(handles);
        }

        public void TrackAsyncOperationHandle(IEnumerable<AsyncOperationHandle> handles)
        {
            this.InternalTrackAsyncOperationHandle(handles);
        }

        private async void InternalTrackAsyncOperationHandle(IEnumerable<AsyncOperationHandle> handles)
        {
            this.TotalTask += handles.Count();
            foreach (var handle in handles)
            {
                await handle;
                this.CompletedTask++;
                this.OnProgressChange();
            }
        }
    }
}