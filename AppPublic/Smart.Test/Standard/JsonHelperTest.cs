using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smart.Standard;
using Smart.Standard.Consts;
using Smart.Standard.Extends;

namespace Smart.Test.Standard
{
    [TestClass]
    public class JsonHelperTest
    {
        [TestMethod]
        public void PackJson()
        {
            var list = new List<string> { "1", "2", "3" };
            Test test = new Test()
            {
                Time=DateTime.Now
            };
            Console.WriteLine(test.PackJson());
            Console.WriteLine(list.PackJson());
            Console.WriteLine(list.PackXml());
       
             
            Assert.IsNotNull(list.PackJson());
        }

        [TestMethod]
        public void ParseJson()
        {
            var list = new Test { Name = "1234", Pwd = "456", Time = DateTime.Now };
            //Console.WriteLine(list.PackJson());
            var res = list.PackJson().ParseJson<Test>();
           
            Assert.AreEqual("1234", res.Name);
        }

        [TestMethod]
        public void FormatJson()
        {
            const string str = "{\"glossary\": {\"title\": \"example glossary\"," +
                               "\"GlossDiv\": {\"title\": \"S\", \"GlossList\": " +
                               "{ \"GlossEntry\": { \"ID\": \"SGML\",\"SortAs\": \"SGML\",\"GlossTerm\": " +
                               "\"Standard Generalized Markup Language\",\"Acronym\": \"SGML\",\"Abbrev\":" +
                               " \"ISO 8879:1986\", \"GlossDef\":" +
                               " {\"para\": \"A meta-markup language, used to create markup languages such as" +
                               " DocBook.\", \"GlossSeeAlso\": [\"GML\", \"XML\"] },\"GlossSee\": \"markup\"}}}}}";
            Console.Write(str.FormatJson());
        }
        [TestMethod]
        public void FormatXml()
        {
            const string str =
                "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><note><to>Tove</to><from>Jani</from><heading>Reminder</heading><body> Don't forget me this weekend!</body></note>";
           Console.WriteLine(str.FormatXml());
        }
    }


    public class Test
    {
        public string Name { get; set; }

        public string Pwd { get; set; }

        public DateTime Time { get; set; }

    }
}
