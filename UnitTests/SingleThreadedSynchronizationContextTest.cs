using System.Threading;
using NUnit.Framework;

namespace RiskAndPricingSolutions.Threading.UnitTests
{
    [TestFixture]
    public class SingleThreadedSynchronizationContextTest
    {
        [Test]
        public void TestPost()
        {
            var postComplete = false;
            var sync = new SingleThreadedSynchronizationContext();
            var latch = new ManualResetEvent(false);

            sync.Post(state =>
            {
                postComplete = true;
                latch.Set();
            }, null);

            Assert.IsFalse(postComplete);
            latch.WaitOne();
            Assert.IsTrue(postComplete);
        }

        [Test]
        public void TesSend()
        {
            var sendComplete = false;
            var sync = new SingleThreadedSynchronizationContext();
   
            sync.Send(state =>
            {
                sendComplete = true;
            }, null);

            Assert.IsTrue(sendComplete);
        }
    }
}
