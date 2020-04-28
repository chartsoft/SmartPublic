using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using Smart.Standard.Attribute;
using Smart.Standard.Dapper;
using Smart.Standard.Enum;
using Smart.Standard.Extends;
using Smart.Standard.FastReflection;
using Smart.Standard.Helper;
using Smart.Test.Dotnet;

namespace Smart.Test.Standard
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetPropertiesCache()
        {
            //typeof(test).GetAttribute<DbTableAttribute>();
            Console.WriteLine(typeof(TestPg).GetAttribute<DbTableAttribute>().PackJson());

        }

        private static readonly IDbConnection DbConnection =
            new NpgsqlConnection(
                "host=192.168.31.173;port=5432;user id=postgres;password=j91920903;database=postgres;");
        [TestMethod]
        public void InsertPgSql()
        {
            var res = DbConnection.Insert(new TestPg { TestValue = "InsertPgSql248" }, DataBaseType.PostgreSql);
            Console.WriteLine();
            // Assert.IsTrue(Convert.ToInt32(res) > 0);
        }

        [TestMethod]
        public void Sqlhelper()
        {
            var sql = new SqlHelper();
            //select  *   from   usr  where   UsrId='1'
            sql.Select("*")
                .From("usr")
                .Where("userid='2'");
            //sql.Select("bulid,ddd").From(111, 3, 55, 66, 99)
            //    .And("11")
            //    .And("2222")
            //    .And("opp")
            //    .LeftJoin("111")
            //    .On("1==9");
            Console.WriteLine(sql.ToString());
        }

        [TestMethod]
        public void TestPakckJson()
        {
            var list = new[] { "222", "2222", "dgh" };
            Console.WriteLine(list.PackJson());
            Console.WriteLine(list.PackXml());
            Console.WriteLine(list.PackXml().ToHash(HashAlgorithmKinds.Md5));
            Console.WriteLine(list.PackXml().ToHash(HashAlgorithmKinds.Sha256));
            Console.WriteLine(list.PackXml().DesEncrypt("123456"));
            Console.WriteLine(list.PackJson().DesEncrypt("123456").DesDecrypt("123456"));
            Console.WriteLine(DateTime.Now.GetLunarDateTime());
        }
        [TestMethod]
        public void TestEnum()
        {

            Console.WriteLine(DateTime.Now.ToUnixTimeSeconds());
            Console.WriteLine(DateTime.Now.ToUnixTimestamp());
            Console.WriteLine(DateTimeHelper.UnixSecondsToDateTime(DateTime.Now.ToUnixTimeSeconds()).ToDateTimeStringSecond());
        }
        private void Test()
        {
            Console.WriteLine(DateTime.Now.ToUnixTimeSeconds());
            Console.WriteLine(DateTime.Now.ToUnixTimestamp());
            Console.WriteLine(DateTimeHelper.UnixSecondsToDateTime(DateTime.Now.ToUnixTimeSeconds()).ToDateTimeStringSecond());
        }
        [TestMethod]

        public void CommonTest()
        {
            //var list = new[] { "111", "2222" }.ToList();
            //Expression<Func<TestPg, object>> func = c => c.TestId == 2;
            //Console.WriteLine(func.GetPropertyName());
            //var methods = GetType().GetMethods();
            //获取所有成员 


            //IDateTimeProvider provider = new UtcDateTimeProvider();
            //var now = provider.GetNow();
            //var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // or use JwtValidator.UnixEpoch
            //var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);

            //iss： jwt签发者
            //sub：jwt所面向的用户
            //aud：接收jwt的一方
            //exp：jwt的过期时间，这个过期时间必须要大于签发时间
            //nbf：定义在什么时间之前，该jwt都是不可用的.
            //iat ：jwt的签发时间
            //jti ：jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击。

            //var payload = new Dictionary<string, object>
            //{
            //    { "name", "MrBug" },
            //    {"exp",DateTime.Now.ToUnixTimeSeconds()+100 },
            //    {"jti","luozhipeng" },
            //    {"nbf", DateTime.Now.ToUnixTimeSeconds()+20}
            //};
            //IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            //IJsonSerializer serializer = new JsonNetSerializer();
            //IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            //IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            //var token = encoder.Encode(payload, "1111");
            //Console.WriteLine(token);
            //CryptoHelper.TokenClaim("1111",new Tuple<ClaimKinds, object>(ClaimKinds.Exp,DateTime.Now.ToUnixTimeSeconds()))

            //var token =
            //    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJuYW1lIjoiTXJCdWciLCJleHAiOjE1MjQ2Mzk4MDksImp0aSI6Imx1b3poaXBlbmciLCJuYmYiOjE1MjQ2Mzk3Mjl9.mIC9ufDq_MeQm5t1_7bHZN-2Q1AbHgewMXTBf2gHi00";
            //try
            //{
            //    IJsonSerializer _serializer = new JsonNetSerializer();
            //    IDateTimeProvider provider = new UtcDateTimeProvider();
            //    IJwtValidator validator = new JwtValidator(_serializer, provider);
            //    IBase64UrlEncoder _urlEncoder = new JwtBase64UrlEncoder();
            //    IJwtDecoder decoder = new JwtDecoder(_serializer, validator, _urlEncoder);
            //    var json = decoder.Decode(token, "1111", true);//token为之前生成的字符串
            //    Console.WriteLine(json);
            //}
            //catch (TokenExpiredException)
            //{
            //    Console.WriteLine("Token has expired");
            //}
            //catch (SignatureVerificationException)
            //{
            //    Console.WriteLine("Token has invalid signature");
            //}
            var claims =
                new Dictionary<ClaimKinds, object>
                {
                    {ClaimKinds.Exp, DateTime.Now.ToUnixTimeSeconds() + 100},
                    {ClaimKinds.Jti,7},
                    {ClaimKinds.CusClaim,"888" }
                };
            var token = CryptoHelper.TokenClaim("1111", claims);
        
            Console.WriteLine(CryptoHelper.GetTokenClaim<string>(token, "1111",ClaimKinds.Jti));
            Console.WriteLine(CryptoHelper.GetTokenClaim<string>(token, "1111", ClaimKinds.Jti));
            Console.WriteLine(CryptoHelper.GetTokenClaim<string>(token, "1111", ClaimKinds.Jti));
            Console.WriteLine(CryptoHelper.GetTokenClaim<string>(token, "1111", ClaimKinds.Jti));
            Console.WriteLine(CryptoHelper.GetTokenClaim<string>(token, "1111", ClaimKinds.Jti));
            Console.WriteLine(CryptoHelper.GetTokenClaim<string>(token, "1111", ClaimKinds.Jti));
            Console.WriteLine(CryptoHelper.GetTokenClaim<string>(token, "1111", ClaimKinds.Jti));
            Console.WriteLine(CryptoHelper.GetTokenClaim<string>(token, "1111", ClaimKinds.Jti));
        }

        /// <summary>
        /// 打包压缩文件夹测试
        /// </summary>
        [TestMethod]
        public void PackTest()
        {
            var targetPath = "E:\\临时文件\\压缩测试.zip";
            var sourceFolder = "E:\\解压文件\\";

            var isOk = ZipHelper.Pack(targetPath, sourceFolder);
            Console.WriteLine(isOk);
        }
        /// <summary>
        /// 解压缩文件测试
        /// </summary>
        [TestMethod]
        public void UnpackTest()
        {
            //指定一个文件给解压
            var sourcePath = "d:\\解压文件\\压缩测试.zip";
            var targetFolder = "d:\\临时文件\\";
            var isOk = ZipHelper.Unpack(sourcePath, targetFolder);
            Console.WriteLine(isOk);
        }

        /// <summary>
        /// 压缩文件带密码
        /// </summary>
        [TestMethod]
        public void ZipFileWithPasswordTest()
        {
            var sourcePath = "d:\\解压文件\\压缩测试.zip";
            var targetPath = "d:\\解压文件\\压缩测试加密.zip";

            var isOk = ZipHelper.ZipFileWithPassword(sourcePath, targetPath, "111111");
            Assert.IsTrue(isOk);
        }

        /// <summary>
        /// 解压文件带密码
        /// </summary>
        [TestMethod]
        public void UnZipFileWithPasswordTest()
        {
            var sourcePath = "d:\\解压文件\\压缩测试加密.zip";
            var targetPath = "d:\\解压文件\\";

            var isOk = ZipHelper.UnZipFileWithPassword(sourcePath, targetPath, "111111");
            Assert.IsTrue(isOk);
        }
        [TestMethod]
        public void EnumTest()
        {
            var kinds = StatesKinds.DissEnable;
            Console.WriteLine(kinds.GetEnumDisplayText());
          Console.WriteLine(EnumDescription.GetFlagShow(StatesKinds.DissEnable|StatesKinds.Enable, ","));

        }
        [TestMethod]
        public void TestMethod2()
        {
            Console.WriteLine(EnumDescription.GetFieldInfos(typeof(MyEnum)).PackJson());
            Console.WriteLine(EnumDescription.GetFieldInfos(typeof(MyEnum)).PackJson());
        }

        [TestMethod]
        public void Enum()
        {
         
            Console.WriteLine(StatesToSql("a.\"States\"", StatesKinds.Enable|StatesKinds.Lock));
        }
        [TestMethod]
        public void TestList()
        {
            //var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //Console.WriteLine(list.Join());
           
            var list = new List<StudentInfo>
            {
                new StudentInfo {Id = 1, Name = "李一"},
                new StudentInfo {Id = 2, Name = "李二"},
                new StudentInfo {Id = 3, Name = "李三"},
                new StudentInfo {Id = 4, Name = "李四"},
                new StudentInfo {Id = 5, Name = "李五"},
                new StudentInfo {Id = 6, Name = "李六"},
                new StudentInfo {Id = 7, Name = "李七"}
            };
            Console.WriteLine(list.PackJson());
            var model = list.FirstOrDefault();
            var model1 = model;
            list.MoveToEnd(c => c == model1);
            Console.WriteLine(list.PackJson());
            model = list.FirstOrDefault();
            var model2 = model;
            list.MoveToEnd(c => c == model2);
            Console.WriteLine(list.PackJson());
            model = list.FirstOrDefault();
            list.MoveToEnd(c => c == model);
            Console.WriteLine(list.PackJson());
            list.Remove(model);
            Console.WriteLine(list.PackJson());
        }
        private string StatesToSql(string clounms, StatesKinds states)
        {
            //var st = StatesKinds.Remove | StatesKinds.Enable;
            var sql = string.Empty;
            var list = EnumDescription.GetFieldInfos(typeof(StatesKinds))
                .Select(c => c.EnumValue.CastTo<StatesKinds>());
            sql = list.Where(item => states.HasFlag(item)).Aggregate(sql, (current, item) => current + $" AND {clounms}|{item.CastTo<int>()}={item.CastTo<int>()}");
            return sql;
        }
        [TestMethod]
        public void DbTest()
        {
          var t=  typeof(StudentInfo).GetAttribute<DbTableAttribute>();
          Console.WriteLine(t.PackJson());
        }

        [TestMethod]
        public void DateTimeTest()
        {
            Console.WriteLine(DateTime.Now.LastMonday().Date.ToDateTimeStringSecond());
            Console.WriteLine(DateTime.Now.LastMonthStart().Date.ToDateTimeStringSecond());
            Console.WriteLine(DateTime.Now.DayOfWeekTime(DayOfWeek.Sunday).Date);
            Console.WriteLine(DateTime.Now.DayOfWeekTime(DayOfWeek.Monday).Date);
            Console.WriteLine(DateTime.Now.DayOfWeekTime(DayOfWeek.Tuesday).Date);
            Console.WriteLine(DateTime.Now.DayOfWeekTime(DayOfWeek.Wednesday).Date);
            Console.WriteLine(DateTime.Now.DayOfWeekTime(DayOfWeek.Thursday).Date);
            Console.WriteLine(DateTime.Now.DayOfWeekTime(DayOfWeek.Friday).Date);
            Console.WriteLine(DateTime.Now.DayOfWeekTime(DayOfWeek.Saturday).Date);
            Console.WriteLine(DateTime.Now.DayOfWeekTime(DayOfWeek.Sunday).Date);
        }
    }


    [EnumDescription("www")]
    enum MyEnum
    {
        [EnumDescription("www333")]

        Lite = 1,
        [EnumDescription("www222")]
        Jq = 2,
    }

 
}
[DbTable(TableName = "1111")]
public class StudentInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
}
