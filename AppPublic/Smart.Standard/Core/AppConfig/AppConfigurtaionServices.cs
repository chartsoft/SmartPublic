using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Smart.Standard.Core.AppConfig
{
    /// <summary>
    /// 读取json配置文件类
    /// </summary>
    public class AppConfigurtaionServices
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public static IConfiguration Configuration { get; set; }
        static AppConfigurtaionServices()
        {                   
            Configuration = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
                .Build();
            
        }
    }
}
