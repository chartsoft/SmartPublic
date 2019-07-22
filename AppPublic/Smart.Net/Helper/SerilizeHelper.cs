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
    /// XML���л�������
    /// </summary>
    public class SerilizeHelper
    {

        /// <summary>
        /// ����ΪJObject
        /// <para>�ɺ���ʹ��JObject�������Ի�ȡ����������</para>
        /// </summary>
        /// <param name="json">json�ַ���</param>
        /// <returns><see cref="JObject"/>����</returns>
        public static JObject JObjectParse(string json)
        {
            return JObject.Parse(json);
        }

        /// <summary>
        /// ���ĳ��json������һ��ָ��ֵ
        /// </summary>
        /// <param name="json"></param>
        /// <param name="subJObjectValue"></param>
        /// <returns></returns>
        public static string GetJObjectValue(string json, string subJObjectValue)
        {
            return JObjectParse(json).GetValue(subJObjectValue).ToString();
        }
        
        /// <summary>
        /// ת��Ϊʵ��
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
        /// ת��Ϊʵ��
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerilizeToJson<T>(T obj)
        {
            var jsonSettings = InitJsonSetting();
            return JsonConvert.SerializeObject(obj,jsonSettings);
        }

        /// <summary>
        /// ��ʼ��Json���л�����
        /// </summary>
        /// <returns>Json���л�����</returns>
        private static JsonSerializerSettings InitJsonSetting()
        {
            //�������л�ʱ��
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
        /// ���л�ΪJson�ַ���
        /// </summary>
        public static string SerilizeToJson<T>(IList<T> obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// ���л�Ϊ�ڴ���MemoryStream
        /// </summary>
        /// <param name="objectToSerilize">Ҫ���л��Ķ���ʵ��</param>
        /// <returns>�ڴ���MemoryStream</returns>
        public static string SerilizeToXml<T>(T objectToSerilize)
        {
            return SerilizeToXml(objectToSerilize, Encoding.UTF8);
        }

        /// <summary>
        /// ���л�Ϊ�ڴ���MemoryStream
        /// </summary>
        /// <param name="objectToSerilize">Ҫ���л��Ķ���ʵ��</param>
        /// <param name="encoding">����</param>
        /// <returns>�ڴ���MemoryStream</returns>
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
        /// MemoryStream�����л�Ϊ����
        /// </summary>
        /// <param name="input">������</param>
        /// <returns>����ʵ��</returns>
        public static T DeserilizeXml<T>(string input)
        {
            return DeserilizeXml<T>(input, Encoding.UTF8);
        }

        /// <summary>
        /// MemoryStream�����л�Ϊ����
        /// </summary>
        /// <param name="encoding">����</param>
        /// <param name="input">������</param>
        /// <returns>����ʵ��</returns>
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
        /// ���л���XML�ļ�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="objectToSerilize">Ҫ���л��Ķ���ʵ��</param>
        /// <param name="fileName">�ļ���</param>
        public static void SerilizeToFile<T>(T objectToSerilize, string fileName)
        {
            var mySerializer = new XmlSerializer(typeof(T));
            var myWriter = new StreamWriter(fileName);
            mySerializer.Serialize(myWriter, objectToSerilize);
            myWriter.Close();
        }

        /// <summary>
        /// �����л��ļ�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="fileName">�����л�Ҫ��ȡ���ļ���</param>
        /// <returns>����ʵ��</returns>
        public static T DeserilizeFile<T>(string fileName)
        {
            using (var myReader = new StreamReader(fileName))
            {
                var mySerializer = new XmlSerializer(typeof(T));
                return (T)mySerializer.Deserialize(myReader);
            }
        }

        /// <summary>
        /// BinaryFormatter���л�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="objectToSerilize">Ҫ���л��Ķ���ʵ��</param>
        /// <returns>MemoryStream����</returns>
        public static string SerilizeToStreamBinary<T>(T objectToSerilize)
        {
            return SerilizeToStreamBinary(objectToSerilize, Encoding.Default);
        }
        /// <summary>
        /// BinaryFormatter���л�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="objectToSerilize">Ҫ���л��Ķ���ʵ��</param>
        /// <param name="encoding">����</param>
        /// <returns>MemoryStream����</returns>
        public static string SerilizeToStreamBinary<T>(T objectToSerilize, Encoding encoding)
        {
            var output = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(output, objectToSerilize);
            output.Position = 0;
            return StreamToString(output, encoding);
        }
        /// <summary>
        /// ���л�Ϊbyte����
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="objectToSerilize">Ҫ���л��Ķ���ʵ��</param>
        /// <returns>���л����byte����</returns>
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
        /// BinaryFormatter�����л�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="input">���ڷ����л���MemoryStream��</param>
        /// <returns>����ʵ��</returns>
        public static T DeserilizeStreamBinary<T>(string input)
        {
            return DeserilizeStreamBinary<T>(input, Encoding.Default);
        }

        /// <summary>
        /// BinaryFormatter�����л�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="encoding">����</param>
        /// <param name="input">���ڷ����л���MemoryStream��</param>
        /// <returns>����ʵ��</returns>
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
        /// MemoryStreamת��ΪString
        /// </summary>
        /// <param name="ms">��Ҫת����MemoryStream</param>
        /// <param name="encoding">����</param>
        /// <returns>ת�����String</returns>
        public static string StreamToString(MemoryStream ms, Encoding encoding)
        {
            if (ms == null) return "";
            var bytelist = ms.ToArray();
            var returnString = (encoding ?? Encoding.Default).GetString(bytelist);
            return returnString;
        }

        /// <summary>
        /// Stringת��ΪStream
        /// </summary>
        /// <param name="s">Ҫת����String</param>
        /// <param name="encoding">����</param>
        /// <returns>ת�����MemoryStream</returns>
        public static MemoryStream StringToStream(string s, Encoding encoding)
        {
            if (string.IsNullOrEmpty(s)) return null;
            var bytelist = (encoding ?? Encoding.Default).GetBytes(s);
            var ms = new MemoryStream(bytelist);
            return ms;
        }

        /// <summary>
        /// ��ʽ��Json�ַ���
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
        /// XML��ʽ�����
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
