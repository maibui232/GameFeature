namespace Modules.Quest.Core.Runtime.Model.Progress
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Services.Message;

    public interface IQuestProgress
    {
        int  CurrentProgress { get; }
        int  MaxProgress     { get; }
        Type HandlerType     { get; }

        public interface IHandler<in T> where T : IQuestProgress
        {
            void AddTrackProgress(params    IQuestProgress[] progresses);
            void RemoveTrackProgress(params IQuestProgress[] progresses);
        }

        public abstract class BaseQuestProgressHandler<T> : IQuestProgress.IHandler<T> where T : IQuestProgress
        {
#region Inject

            protected readonly IMessageService MessageService;

#endregion

            protected BaseQuestProgressHandler(IMessageService messageService)
            {
                this.MessageService = messageService;
            }

            protected readonly HashSet<T> TrackProgresses = new();

            public void AddTrackProgress(params IQuestProgress[] progresses)
            {
                foreach (var progress in progresses)
                {
                    if (progress is T questProgress)
                    {
                        this.TrackProgresses.Remove(questProgress);
                    }
                }
            }

            public void RemoveTrackProgress(params IQuestProgress[] progresses)
            {
                foreach (var progress in progresses)
                {
                    if (progress is T questProgress)
                    {
                        this.TrackProgresses.Remove(questProgress);
                    }
                }
            }
        }
    }
}