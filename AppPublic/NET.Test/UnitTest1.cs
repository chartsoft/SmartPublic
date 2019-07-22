using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smart.Net45.Attribute;
using Smart.Net45.Enum;
using Smart.Net45.Extends;
using Smart.Net45.Helper;

namespace NET.Test
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    var targetPath = "E:\\临时文件\\压缩测试.zip";
        //    var sourceFolder = "E:\\解压文件\\";

        //    var isOk = ZipHelper.Pack(targetPath, sourceFolder);
        //    Console.WriteLine(isOk);
        //}

        [TestMethod]
        public void TestMethod2()
        {

          
            var t = Math.Round(1.558 * 1000,0);
            //去尾法
            var g = Math.Floor(1.554500 * 1000)/1000;
            //收尾法
            var k = Math.Ceiling(1.554500 * 1000)/1000;
            //Console.WriteLine( 1000.005d.CastTo<decimal>());
            Console.WriteLine("111888".CastTo<int>());

            var i = 4;
            Console.WriteLine(decimal.Parse(1.55555.ToString($"f{i}")) );
            //Console.WriteLine(t / 1000);
            //Console.WriteLine(g / 1000);

        }
    }
    [EnumDescription("www")]
    public enum MyEnum
    {
        [EnumDescription("www333", "2222")]

        Lite = 1,
        [EnumDescription("www222", "33")]
        Jq = 2,
    }
}
