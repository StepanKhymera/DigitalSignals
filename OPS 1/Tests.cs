using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS_1
{
    [TestFixture]
    class Tests
    {
        [Test]
        public void Zero()
        {
            FunctionBuilder b = new FunctionBuilder();
            Assert.AreEqual(b.Original(0), -1);
        }
        [Test]
        public void P_2()
        {
            FunctionBuilder b = new FunctionBuilder();
            Assert.AreEqual(b.Original(Math.PI/2.0), 1);
        }
        [Test]
        public void M3P_2()
        {
            FunctionBuilder b = new FunctionBuilder();
            Assert.AreEqual(1, b.Original(-3 * Math.PI / 2.0));
        }
        [Test]
        public void MP()
        {
            FunctionBuilder b = new FunctionBuilder();
            Assert.AreEqual(-1, b.Original((-1) * Math.PI));
        }
    }
}
