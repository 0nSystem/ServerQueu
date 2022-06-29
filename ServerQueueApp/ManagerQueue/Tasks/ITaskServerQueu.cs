using ServerQueu.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ServerQueu.Services
{
    public interface ITaskServerQueu<T> where T : SessionInfo
    {
        Action<TaskServerQueu<T>>? GenerateActionTask();
        bool AuthenticationBeforeGenerateTask(TaskServerQueu<T> taskServerQueu);
        bool AuthenticationAfterGenerateTask(TaskServerQueu<T> taskServerQueu);
    }
}
