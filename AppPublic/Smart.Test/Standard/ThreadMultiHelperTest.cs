using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smart.Standard.Helper;

namespace Smart.Test.Standard
{
    [TestClass]
    public class ThreadMultiHelperTest
    {

        [TestMethod]
        public void TestMethod1()
        {
            var array=new []{1,2,3,4,5,6,7,8,9,10};
            var t=new ThreadMultiHelper(100,10);
            t.WorkMethod += (taskindex, threadindex) =>
            {
                Console.WriteLine(111);
            };
            t.CompleteEvent += () => { Console.WriteLine("Complete"); };
        }
    }
}
