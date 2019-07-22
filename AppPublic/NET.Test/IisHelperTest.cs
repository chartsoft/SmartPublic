using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smart.Net45.Extends;
using Smart.Net45.Helper;
using Smart.Net45.Model;

namespace NET.Test
{
    [TestClass]
    public class IisHelperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //IisHelper iisHelper=new IisHelper();
     //IisHelper.GetList();

        }
        [TestMethod]
        public void GetApplication()
        {
            
            Console.WriteLine(IisHelper.GetApplication("8119").Select(c=>c.ApplicationName).Join());
        }
        [TestMethod]
        public void AddApplication()
        {
           //IisHelper.AddApplication("www9828", "sst", @"E:\iframe");
           // IisHelper.AddApplication("www9828", "sst2", @"E:\smms");
            IisHelper.AddApplication("www9828", Guid.NewGuid().ToString(), @"E:\smms2");
             IisHelper.AddApplication("www9828", Guid.NewGuid().ToString(), @"E:\smms2");
        }
        [TestMethod]
        public void RemoveApplication()
        {
            IisHelper.RemoveApplication("www9828","sst");
        }
        [TestMethod]
        public void Get()
        {
            IisHelper.AddDefaultDocument("127.0.0.1", "zy18899.aspx");


        }
        [TestMethod]
        public void ExistApplication()
        {
            Console.WriteLine(IisHelper.ExistApplication("www9828", "sst"));
            Console.WriteLine(IisHelper.ExistApplication("www9828", "sst2"));
        }
        [TestMethod]
        public void GetiisSiteList()
        {
            Console.Write(IisHelper.GetiisSiteList().PackJson());
        }

        [TestMethod]
        public void EditWebsiteName()
        {
           IisHelper.EditSiteAppName("www25", "www", "www");
        }
    }
}
