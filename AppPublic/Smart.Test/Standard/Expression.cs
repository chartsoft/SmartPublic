using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Npgsql;
using Smart.Standard;
using Smart.Standard.Enum;
using Smart.Standard.ExpressionVisitor;
using Smart.Standard.Extends;

namespace Smart.Test.Standard
{
    [TestClass]
    public class Expression
    {
        private static readonly IDbConnection DbConnection =
            new NpgsqlConnection(
                "host=192.168.0.110;port=5432;user id=postgres;password=j91920903;database=PFM;");
        [TestMethod]
        public void ToSql()
        {
            //string name = "lijun";
            //List<int> list = new List<int> { 1, 2, 3 };
            //bool flag = list.Contains(1);
            //var item = GetItemByQuery<Student>(c => list.Contains(c.ID) && c.StatesKinds.HasFlag(StatesKinds.DissEnable));
            //Console.WriteLine(item);
            //var item = GetItemByQuery<Student>(c => c.StatesKinds.CastTo<StatesKinds>().HasFlag(StatesKinds.DissEnable));
            //Expression<Func<Student, bool>> condition = c => (c.StatesKinds&StatesKinds.Lock.CastTo<int>())== StatesKinds.Lock.CastTo<int>();
            //Console.WriteLine(condition.ToSql(out object[] paramObjects));
            //Console.WriteLine(paramObjects.PackJson());
            //Console.WriteLine(typeof(int));
            //Console.WriteLine(1.HasFlag(StatesKinds.Enable));
            //StatesKinds statesKinds = StatesKinds.DissEnable | StatesKinds.Enable;
            //Console.WriteLine(statesKinds.CastTo<int>()&4);
            //Console.Write(paObjects.PackJson());

            //Console.WriteLine(new Student
            //{
            //    StatesKinds=StatesKinds.DissEnable
            //}.PackJson().ParseJson<Student>().PackJson());
            var sql =
                "  SELECT COUNT(*) FROM(SELECT *, b.\"StaffName\", c.\"VersionName\"  FROM \"Clique\" a LEFT JOIN \"Staff\" b ON a.\"StaffKey\" = b.\"StaffKey\" LEFT JOIN \"VersionManger\" c ON a.\"VersionCode\" = c.\"VersionCode\" WHERE 1 = 1) dapper_tbl";
           ;
            Console.WriteLine(DbConnection.ExecuteScalarAsync<int>(sql).Result);
        }
        private string GetItemByQuery<T>(Expression<Func<T, bool>> condition)
        {
            // ExpressionSqlWriter writer = new ExpressionSqlWriter();
            //string sql = writer.Translate(condition);
            //Console.WriteLine(DateTime.Now.Millisecond);

            var parameters = new List<object>();
            var writer = new ExpressionParameterizedSqlWriter(parameters, DataBaseType.PostgreSql);
            var sql = writer.Translate(condition);
            return sql;

            //Console.WriteLine($"sql: {sql}");
            //Console.WriteLine(DateTime.Now.Millisecond);

        }
    }

    public class Student
    {
        public int ID { get; set; }

        public Guid Number { get; set; }

        public string RealName { get; set; }

        public int Age { get; set; }

        public char Gender { get; set; }

        public DateTime CreateTime { get; set; }

        public bool DelFlag { get; set; }

        public StatesKinds StatesKinds { get; set; }
    }


}
