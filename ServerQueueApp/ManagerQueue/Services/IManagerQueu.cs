﻿using ServerQueu.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerQueue.Services
{
    public interface IManagerQueu<T> where T : SessionInfo
    {
        void Run();
    }
}
