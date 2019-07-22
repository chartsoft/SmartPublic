using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using Smart.Standard.Attribute;
using Smart.Standard.Dapper;
using Smart.Standard.Enum;
using Smart.Standard.Extends;


namespace Smart.Test.Standard
{
    [TestClass]
    public class DapperExtend
    {
        private static readonly IDbConnection DbConnection =
            new NpgsqlConnection(
                "host=192.168.31.173;port=5432;user id=postgres;password=j91920903;database=postgres;");

        private static TestPg _test1;
        private static TestPg _test2;

        /// <summary>
        /// 初始化dapper预热
        /// </summary>
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            var key1 = (int)DbConnection.Insert(new TestPg { TestValue = "1" }, DataBaseType.PostgreSql);
            var key2 = (int)DbConnection.Insert(new TestPg { TestValue = "2" }, DataBaseType.PostgreSql);
            _test1 = DbConnection.Query<TestPg>(c => c.TestId == key1, DataBaseType.PostgreSql).FirstOrDefault();
            _test2 = DbConnection.Query<TestPg>(c => c.TestId == key2, DataBaseType.PostgreSql).FirstOrDefault();
        }
        [ClassCleanup]
        public static void ClassClean()
        {
            DbConnection.DeleteByWhere<TestPg>(c => c.TestId > 0, DataBaseType.PostgreSql);
        }
        [TestMethod]
        public void InsertPgSql()
        {
            var res = DbConnection.Insert(new TestPg { TestValue = "InsertPgSql248" }, DataBaseType.PostgreSql);
            Assert.IsTrue(Convert.ToInt32(res) > 0);
        }
        [TestMethod]
        public void Extcute()
        {
            var res = DbConnection.ExecuteScalar($"SELECT * FROM {"Test".AddDoubleQuotes()}");
            Console.WriteLine(res);
        }
        [TestMethod]
        public void BullkInsert()
        {
            var list = new List<TestPg> { new TestPg { TestValue = "cs1" }, new TestPg { TestValue = "cs256" } };
            var res = DbConnection.BulkInsert(list, DataBaseType.PostgreSql);
            Assert.IsTrue(res > 0);
        }
        [TestMethod]
        public void Query()
        {
            var list = DbConnection.Query<TestPg>(c => c.TestId > 0, DataBaseType.PostgreSql);
            Assert.IsTrue(list.Count() >= 2);
        }
        [TestMethod]
        public void Update()
        {
            _test1.TestValue = "2333";
            var i = DbConnection.Update(_test1, new[] { nameof(TestPg.TestValue) }, DataBaseType.PostgreSql);
            Assert.IsTrue(i == 1);
        }
        [TestMethod]
        public void Update_T()
        {
            _test1.TestValue = "23335";
            var i = DbConnection.Update(_test2, DataBaseType.PostgreSql);
            Assert.IsTrue(i == 1);
        }
        [TestMethod]
        public void BullUpdate()
        {
            _test1.TestValue = "2548258";
            _test2.TestValue = "365889";
            var res = DbConnection.BulkUpdate(new List<TestPg> { _test1, _test2 }, DataBaseType.PostgreSql);
            Assert.IsTrue(res == 2);
        }
        [TestMethod]
        public void UpdateWhere()
        {
            _test1.TestValue = "lijunyun";
            var res = DbConnection.UpdateWhere(_test1, c => c.TestId > 0, DataBaseType.PostgreSql);
            //Console.WriteLine(res);
            Assert.IsTrue(res >= 2);
        }
        [TestMethod]
        public void BullUpdate_T()
        {
            _test1.TestValue = "25482258";
            _test2.TestValue = "3658289";
            var res = DbConnection.BulkUpdate(new List<TestPg> { _test1, _test2 }, new[] { "TestValue" }, DataBaseType.PostgreSql);
            Assert.IsTrue(res == 2);
        }
        [TestMethod]
        public void DeleteByKey()
        {
            var test = new TestPg
            {
                TestValue = "ertop"
            };
            var key = DbConnection.Insert(test, DataBaseType.PostgreSql);
            var res = DbConnection.DeleteByKey<TestPg>(key, DataBaseType.PostgreSql);
            Assert.IsTrue(res == 1);

        }
        [TestMethod]
        public void DeleteByWhere()
        {
            var test = new TestPg
            {
                TestValue = "ertop"
            };
            var key = DbConnection.Insert(test, DataBaseType.PostgreSql);
            var res = DbConnection.DeleteByWhere<TestPg>(c => c.TestId == (int)key, DataBaseType.PostgreSql);
            Assert.IsTrue(res == 1);
        }
        [TestMethod]
        public void BullkDeleByKeys()
        {
            var test = new TestPg
            {
                TestValue = "ertop"
            };
            var key1 = DbConnection.Insert(test, DataBaseType.PostgreSql);
            var key2 = DbConnection.Insert(test, DataBaseType.PostgreSql);
            var key3 = DbConnection.Insert(test, DataBaseType.PostgreSql);
            var res = DbConnection.BulkDeleTeByKeys<TestPg>(new[] { key1, key2, key3 }, DataBaseType.PostgreSql);
            Assert.IsTrue(res == 3);

        }
        [TestMethod]
        public void DeleteByModel()
        {
            var test = new TestPg
            {
                TestValue = "ertop"
            };
            var key1 = DbConnection.Insert(test, DataBaseType.PostgreSql);
            test.TestId = (int)key1;
            var res = DbConnection.DeleteByModel(test, DataBaseType.PostgreSql);
            Assert.IsTrue(res == 1);

        }
        [TestMethod]
        public void DeleteByModels()
        {
            var test = new TestPg
            {
                TestValue = "ertop"
            };
            var test2 = new TestPg
            {
                TestValue = "ertop"
            };
            var key1 = DbConnection.Insert(test, DataBaseType.PostgreSql);
            var key2 = DbConnection.Insert(test2, DataBaseType.PostgreSql);
            test.TestId = (int)key1;
            test2.TestId = (int)key2;
            var list = new List<TestPg>(new List<TestPg>() { test, test2 });
            var res = DbConnection.DeleteByModels(list, DataBaseType.PostgreSql);
            Assert.IsTrue(res == 2);
        }
        [TestMethod]
        public void GetModelByKey()
        {
            var res = DbConnection.GetModelByKey<TestPg>(_test1.TestId, DataBaseType.PostgreSql);
            Assert.IsTrue(res.TestId == _test1.TestId);
        }
        [TestMethod]
        public void GetModelWhere()
        {
            var res = DbConnection.GetModelWhere<TestPg>(c => c.TestId == _test1.TestId, DataBaseType.PostgreSql);
            Assert.IsTrue(res.TestId == _test1.TestId);
        }
        [TestMethod]
        public void GetCount()
        {
            var res = DbConnection.GetCount<TestPg>(c => c.TestId > 0, DataBaseType.PostgreSql);
            Assert.IsTrue(res >= 2);
        }
        [TestMethod]
        public void ExitKey()
        {
            var res = DbConnection.ExitKey<TestPg>(_test1.TestId, DataBaseType.PostgreSql);
            Assert.IsTrue(res);
        }

        public void ExitWhere()
        {
            var res = DbConnection.ExitWhere<TestPg>(c => c.TestId > 0, DataBaseType.PostgreSql);
            Assert.IsTrue(res);


        }

      
    }
    #region 测试Model
    [DbTable(PrimaryKey = "TestId", TableName = "Test", AutoIncrement = true)]
    //[DbTable( TableName = "test")]
    public class TestPg
    {

        public int TestId { get; set; } = 1;

        public string TestValue { get; set; } = "assss";
    }

    #endregion
}
