using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ServerQueu.Controller
{
    public class TaskSchedulerManagerQueu : TaskScheduler
    {
        protected int MaxSizeQueue=1000;

        protected int SizeQueue = 0;

        public static bool CURRENT_THREAD_RUNNING_ITEMS=false;
        
        protected List<Task> Queue {
            get
            {
                lock (Queue)
                {
                    return Queue;
                }
            }
            private set
            {
                lock (Queue)
                {
                    Queue = value;
                }
            }
        }
        protected LinkedList<Task> InProccessOrRunned
        {
            get
            {
                lock (InProccessOrRunned)
                {
                    return InProccessOrRunned;
                }
            }
            set
            {
                lock (InProccessOrRunned)
                {
                    InProccessOrRunned = value;
                }
            }
        }
        public TaskSchedulerManagerQueu(int maxSizeQueue)
        {
            this.MaxSizeQueue = maxSizeQueue;
            Queue = new List<Task>();
            InProccessOrRunned=new LinkedList<Task>();
        }
        public TaskSchedulerManagerQueu()
        {
            Queue = new List<Task>();
            InProccessOrRunned = new LinkedList<Task>();
        }

        protected override IEnumerable<Task>? GetScheduledTasks()
        {
            return Queue.AsEnumerable<Task>();
        }

        protected override void QueueTask(Task task)
        {
            if (AuthenticationTaskToAdd(task))
            {
                Queue.Add(task);
                NotifyThreadPoolOfPendingWork();
            }
        }
        protected override bool TryDequeue(Task task)
        {

            bool toReturned=Queue.Remove(task);
            if (toReturned)
            {
                InProccessOrRunned.AddLast(task);
            }
            return toReturned;
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (!CURRENT_THREAD_RUNNING_ITEMS)
            {
                return false;
            }

            if (taskWasPreviouslyQueued)
            {
                if (TryDequeue(task))
                {
                     return TryExecuteTask(task);
                }
                else { 
                    return false;
                }
            }

            return TryExecuteTask(task);
        }
        
        protected bool AuthenticationTaskToAdd(Task task)
        {
            if (Queue.Count>=MaxSizeQueue)
            {
                return false;
            }

            return true;
        }
        protected void NotifyThreadPoolOfPendingWork()
        {
            ThreadPool.UnsafeQueueUserWorkItem((a) => {
                if (CURRENT_THREAD_RUNNING_ITEMS)
                {
                    return;
                }
                CURRENT_THREAD_RUNNING_ITEMS = true;
                try
                {
                    while (CURRENT_THREAD_RUNNING_ITEMS)
                    {
                        Task? item=null;
                        item=Queue.FirstOrDefault();

                        if (Queue.Count==0 || item==null)
                        {
                            CURRENT_THREAD_RUNNING_ITEMS = false;
                            break;
                        }
                        TryDequeue(item);

                        TryExecuteTask(item);

                    }
                }
                finally
                {
                    CURRENT_THREAD_RUNNING_ITEMS = false;
                }
            }, null);
        }

    }
}
