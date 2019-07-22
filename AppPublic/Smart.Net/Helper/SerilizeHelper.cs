using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Smart.Net45.Helper
{
    /// <summary>
    /// XML序列化辅助类
    /// </summary>
    public class SerilizeHelper
    {

        /// <summary>
        /// 解析为JObject
        /// <para>可后续使用JObject进行属性获取等其它操作</para>
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns><see cref="JObject"/>对象</returns>
        public static JObject JObjectParse(string json)
        {
            return JObject.Parse(json);
        }

        /// <summary>
        /// 获得某个json对象下一层指定值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="subJObjectValue"></param>
        /// <returns></returns>
        public static string GetJObjectValue(string json, string subJObjectValue)
        {
            return JObjectParse(json).GetValue(subJObjectValue).ToString();
        }
        
        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T DeserilizeJson<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString)) 
                return default(T);
            var jsonSettings = InitJsonSetting();
            return JsonConvert.DeserializeObject<T>(jsonString,jsonSettings);
        }

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerilizeToJson<T>(T obj)
        {
            var jsonSettings = InitJsonSetting();
            return JsonConvert.SerializeObject(obj,jsonSettings);
        }

        /// <summary>
        /// 初始化Json序列化设置
        /// </summary>
        /// <returns>Json序列化设置</returns>
        private static JsonSerializerSettings InitJsonSetting()
        {
            //设置序列化时间
            var datetimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            var jsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            jsonSettings.Converters.Add(datetimeConverter);
            return jsonSettings;
        }

        /// <summary>
        /// 序列化为Json字符串
        /// </summary>
        public static string SerilizeToJson<T>(IList<T> obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 序列化为内存流MemoryStream
        /// </summary>
        /// <param name="objectToSerilize">要序列化的对象实例</param>
        /// <returns>内存流MemoryStream</returns>
        public static string SerilizeToXml<T>(T objectToSerilize)
        {
            return SerilizeToXml(objectToSerilize, Encoding.UTF8);
        }

        /// <summary>
        /// 序列化为内存流MemoryStream
        /// </summary>
        /// <param name="objectToSerilize">要序列化的对象实例</param>
        /// <param name="encoding">编码</param>
        /// <returns>内存流MemoryStream</returns>
        public static string SerilizeToXml<T>(T objectToSerilize, Encoding encoding)
        {
            ArgumentGuard.ArgumentNotNull("objectToSerilize", objectToSerilize);
            ArgumentGuard.ArgumentNotNull("encoding", encoding);

            var mySerializer = new XmlSerializer(objectToSerilize.GetType());

            var output = new MemoryStream();
            var x = new XmlTextWriter(output, encoding);

            mySerializer.Serialize(x, objectToSerilize);
            var returnString = StreamToString(output, encoding);
            return returnString;
        }

        /// <summary>
        /// MemoryStream反序列化为对象
        /// </summary>
        /// <param name="input">输入流</param>
        /// <returns>对象实例</returns>
        public static T DeserilizeXml<T>(string input)
        {
            return DeserilizeXml<T>(input, Encoding.UTF8);
        }

        /// <summary>
        /// MemoryStream反序列化为对象
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <param name="input">输入流</param>
        /// <returns>对象实例</returns>
        public static T DeserilizeXml<T>(string input, Encoding encoding)
        {
            if (string.IsNullOrEmpty(input))
                return default(T);
            var mySerializer = new XmlSerializer(typeof(T));
            var ms = StringToStream(input, encoding);
            ms.Position = 0;
            var data = (T)mySerializer.Deserialize(ms);
            ms.Close();
            return data;
        }

        /// <summary>
        /// 序列化到XML文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="objectToSerilize">要序列化的对象实例</param>
        /// <param name="fileName">文件名</param>
        public static void SerilizeToFile<T>(T objectToSerilize, string fileName)
        {
            var mySerializer = new XmlSerializer(typeof(T));
            var myWriter = new StreamWriter(fileName);
            mySerializer.Serialize(myWriter, objectToSerilize);
            myWriter.Close();
        }

        /// <summary>
        /// 反序列化文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="fileName">反序列化要读取的文件名</param>
        /// <returns>对象实例</returns>
        public static T DeserilizeFile<T>(string fileName)
        {
            using (var myReader = new StreamReader(fileName))
            {
                var mySerializer = new XmlSerializer(typeof(T));
                return (T)mySerializer.Deserialize(myReader);
            }
        }

        /// <summary>
        /// BinaryFormatter序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="objectToSerilize">要序列化的对象实例</param>
        /// <returns>MemoryStream对象</returns>
        public static string SerilizeToStreamBinary<T>(T objectToSerilize)
        {
            return SerilizeToStreamBinary(objectToSerilize, Encoding.Default);
        }
        /// <summary>
        /// BinaryFormatter序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="objectToSerilize">要序列化的对象实例</param>
        /// <param name="encoding">编码</param>
        /// <returns>MemoryStream对象</returns>
        public static string SerilizeToStreamBinary<T>(T objectToSerilize, Encoding encoding)
        {
            var output = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(output, objectToSerilize);
            output.Position = 0;
            return StreamToString(output, encoding);
        }
        /// <summary>
        /// 序列化为byte数组
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="objectToSerilize">要序列化的对象实例</param>
        /// <returns>序列化后的byte数组</returns>
        public static byte[] SerilizeToBytes<T>(T objectToSerilize)
        {
            var output = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(output, objectToSerilize);
            output.Position = 0;
            return output.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T DeserilizeFromBytes<T>(byte[] input)
        {
            var binaryFormatter = new BinaryFormatter();
            var ms = new MemoryStream(input) {Position = 0};
            var result = (T)binaryFormatter.Deserialize(ms);
            ms.Close();
            return result;
        }
        /// <summary>
        /// BinaryFormatter反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="input">用于反序列化的MemoryStream流</param>
        /// <returns>对象实例</returns>
        public static T DeserilizeStreamBinary<T>(string input)
        {
            return DeserilizeStreamBinary<T>(input, Encoding.Default);
        }

        /// <summary>
        /// BinaryFormatter反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="encoding">编码</param>
        /// <param name="input">用于反序列化的MemoryStream流</param>
        /// <returns>对象实例</returns>
        public static T DeserilizeStreamBinary<T>(string input, Encoding encoding)
        {
            var binaryFormatter = new BinaryFormatter();
            var ms = StringToStream(input, encoding);
            ms.Position = 0;
            var result = (T)binaryFormatter.Deserialize(ms);
            ms.Close();
            return result;
        }


        /// <summary>
        /// MemoryStream转换为String
        /// </summary>
        /// <param name="ms">需要转换的MemoryStream</param>
        /// <param name="encoding">编码</param>
        /// <returns>转换后的String</returns>
        public static string StreamToString(MemoryStream ms, Encoding encoding)
        {
            if (ms == null) return "";
            var bytelist = ms.ToArray();
            var returnString = (encoding ?? Encoding.Default).GetString(bytelist);
            return returnString;
        }

        /// <summary>
        /// String转换为Stream
        /// </summary>
        /// <param name="s">要转换的String</param>
        /// <param name="encoding">编码</param>
        /// <returns>转换后的MemoryStream</returns>
        public static MemoryStream StringToStream(string s, Encoding encoding)
        {
            if (string.IsNullOrEmpty(s)) return null;
            var bytelist = (encoding ?? Encoding.Default).GetBytes(s);
            var ms = new MemoryStream(bytelist);
            return ms;
        }

        /// <summary>
        /// 格式化Json字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatJson(string str)
        {
            TextReader tr = new StringReader(str);
            var serializer = new JsonSerializer();
            var jtr = new JsonTextReader(tr);
            var obj = serializer.Deserialize(jtr);
            if (obj == null) return str;
            var textWriter = new StringWriter();
            var jsonWriter = new JsonTextWriter(textWriter)
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };
            serializer.Serialize(jsonWriter, obj);
            return textWriter.ToString();
        }
        /// <summary>
        /// XML格式化输出
        /// </summary>
        /// <param name="sUnformattedXml"></param>
        /// <returns></returns>
        public static string FormatXml(string sUnformattedXml)
        {
            var xd = new XmlDocument();
            xd.LoadXml(sUnformattedXml);
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw)
                {
                    Formatting = System.Xml.Formatting.Indented,
                    Indentation = 1,
                    IndentChar = '\t'
                };
                xd.WriteTo(xtw);
            }
            finally
            {
                xtw?.Close();
            }
            return sb.ToString();
        }
    }
}
