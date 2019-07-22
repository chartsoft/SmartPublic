using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smart.Standard;
using Smart.Standard.Extends;

namespace Smart.Test.Standard
{
    [TestClass]
    public class ListExtend
    {
        [TestMethod]
        public void JoinParse()
        {
            var list = new List<string> { "nihao", "ww" };
           Console.WriteLine( list.Join());   
        }
        
    }
}
