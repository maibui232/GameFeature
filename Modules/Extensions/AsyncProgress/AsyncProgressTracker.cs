namespace Modules.Extensions.AsyncProgress
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Cysharp.Threading.Tasks;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class AsyncProgressTracker
    {
        public static T CreateHandler<T>() where T : IAsyncProgressHandler
        {
            return Activator.CreateInstance<T>();
        }

        public static IAsyncProgressHandler TrackTask(params Task[] tasks)
        {
            return new TaskProgressHandler(tasks);
        }

        public static IAsyncProgressHandler TrackTask(IEnumerable<Task> tasks)
        {
            return new TaskProgressHandler(tasks);
        }

        public static IAsyncProgressHandler TrackUniTask(params UniTask[] uniTasks)
        {
            return new UniTaskProgressHandler(uniTasks);
        }

        public static IAsyncProgressHandler TrackUniTask(IEnumerable<UniTask> uniTasks)
        {
            return new UniTaskProgressHandler(uniTasks);
        }

        public static IAsyncProgressHandler TrackAsyncOperationHandle(params AsyncOperationHandle[] handles)
        {
            return new AsyncOperationHandleProgressHandler(handles);
        }

        public static IAsyncProgressHandler TrackAsyncOperationHandle(IEnumerable<AsyncOperationHandle> handles)
        {
            return new AsyncOperationHandleProgressHandler(handles);
        }
    }
}