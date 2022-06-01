using NUnit.Framework;
using TresEnRayaApp;

namespace ServerQueuTest
{
    public class SessionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var session = new Session();


            int a = 1;
            Assert.AreEqual(a,1);
        }
    }
}