using TresEnRayaApp;
using ServerQueu.Sessions;
using ManagerQueue.Tasks;

namespace ManagerQueue.Controller
{
    public class ControllerSession<T>:IControllerSession<T> where T : SessionInfo
    {
        public delegate TaskServerQueu<T> FactoryTaskServerQueu(Session<T> session);
        public delegate Action TaskActionServerQueu(TaskServerQueu<T> taskServerQueu);

        protected readonly FactoryTaskServerQueu FactoryTaskServerQueueAction;
        protected readonly TaskActionServerQueu TaskActionServerQueuAction;

                
        public ControllerSession(FactoryTaskServerQueu factoryTaskServerQueueAction,TaskActionServerQueu taskActionServerQueu)
        {
            FactoryTaskServerQueueAction = factoryTaskServerQueueAction;
            TaskActionServerQueuAction = taskActionServerQueu;

        }

        public Action? MakeTaskSession(Session<T> session)
        {
            if (FactoryTaskServerQueueAction == null || TaskActionServerQueuAction == null)
            {
                return null;
            }
            TaskServerQueu<T>? taskToExecuteProccesClients = FactoryTaskServerQueueAction.Invoke(session);
            if (taskToExecuteProccesClients == null)
            {
                return null;
            }
            Action taskGenerated=TaskActionServerQueuAction.Invoke(taskToExecuteProccesClients);
            
            return taskGenerated;
        }
    }
}
