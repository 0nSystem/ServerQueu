﻿using ManagerQueue.Controller;
using ManagerQueue.Handlers;
using ManagerQueue.Tasks;
using NUnit.Framework;
using ServerQueu.Sessions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TresEnRayaApp;

namespace ManagerQueue.Test.Handlers
{
    [TestFixture]
    public class HandlersManageQueueTest
    {
        private ConcurrentQueue<Session<SessionInfo>> _sessions;
        private ControllerSession<SessionInfo>.FactoryTaskServerQueu _actionfactoryTask = (session) => new TaskServerQueu<SessionInfo>(session);
        private ControllerSession<SessionInfo>.TaskActionServerQueu _actionTask = (a) => () => { };
        private TaskFactory _taskfactory;
        private ControllerSession<SessionInfo> _controllerSession;
        private HandlerManagerQueu<SessionInfo> _handlerManagerQueu;
        



        [SetUp]
        public void SetUp()
        {
            _sessions = new ConcurrentQueue<Session<SessionInfo>>();
            _sessions.Enqueue(new Session<SessionInfo>(0));
            _taskfactory=new TaskFactory();
        }

        [Test]
        public void BasicFuntion()
        {
            string exceptedMsg = "hola";
            string returnedMsg = "";
            _actionTask = (a) => () => {
                returnedMsg = exceptedMsg;
            };
            _controllerSession = new ControllerSession<SessionInfo>(_actionfactoryTask, _actionTask);
            _handlerManagerQueu = new HandlerManagerQueu<SessionInfo>(_sessions, _controllerSession, _taskfactory);

            int countSessionIsOne = _sessions.Count;
            bool canRunElement = _handlerManagerQueu.CanRunElement();
            bool isRunningElement = _handlerManagerQueu.RunElement();
            int countSessionIsZero = _sessions.Count;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, countSessionIsOne);
                Assert.IsTrue(canRunElement);
                Assert.IsTrue(isRunningElement);
                Assert.AreEqual(0, countSessionIsZero);
                Assert.AreEqual(exceptedMsg, returnedMsg);
            });
        }
    }
}
