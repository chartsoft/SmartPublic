using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Smart.Standard.Extends
{
    public static class JsonHelper
    {

        private static JsonSerializerSettings InitSetting()
        {
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
        /// 将指定的对象序列化成 JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns></returns>
        public static string PackJson(this object obj)
        {
            var jsonSettings = InitSetting();
            try
            {
                return JsonConvert.SerializeObject(obj, Formatting.None, jsonSettings);
            }
            catch
            {
                return null;
            }
      
        }

        /// <summary>
        /// 将指定的 JSON 数据反序列化成指定对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="json">JSON 数据。</param>
        /// <returns></returns>
        public static T ParseJson<T>(this string json)
        {        
            var jsonSettings = InitSetting();
            try
            {
                return JsonConvert.DeserializeObject<T>(json, jsonSettings);
            }
            catch
            {
                return default(T);
            }
           
        }

        public static JObject JObjectParse(this string json)
        {
            return JObject.Parse(json);
        }

        /// <summary>
        /// 获得某个json对象下一层指定值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="subJObjectValue"></param>
        /// <returns></returns>
        public static string GetJObjectValue(this string json, string subJObjectValue)
        {
            return JObjectParse(json).GetValue(subJObjectValue).ToString();
        }

   
    }
}
