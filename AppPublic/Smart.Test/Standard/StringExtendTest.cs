using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smart.Standard.Enum;
using Smart.Standard.Extends;

namespace Smart.Test.Standard
{
    [TestClass]
    public class StringExtendTest
    {
        [TestMethod]
        public void IsRegex()
        {
            Assert.IsTrue("127.0.0.1".IsRegex(RegexKinds.Ip));
            Assert.IsFalse("222.35.287.98".IsRegex(RegexKinds.Ip));
            Assert.IsTrue("622154".IsRegex(RegexKinds.ChinaPost));
            Assert.IsFalse("TTTTs".IsRegex(RegexKinds.ChinaPost));
            Assert.IsTrue("http://www.baidu.com/1.zip".IsRegex(RegexKinds.Url));
            Assert.IsFalse("http2://www.baidu.com/1.zip".IsRegex(RegexKinds.Url));


        }
        [TestMethod]
        public void ToHash()
        {
            Console.WriteLine("123456789".ToHash(HashAlgorithmKinds.Sha1).ToLower());
            Console.WriteLine("women".ToHexString().ToLower());
        }
    }
}
