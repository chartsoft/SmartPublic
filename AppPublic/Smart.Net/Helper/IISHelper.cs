using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Web.Administration;
using Smart.Net45.Enum;
using Smart.Net45.Extends;
using Smart.Net45.Model;

namespace Smart.Net45.Helper
{
    /// <summary>
    /// iis操作类
    /// </summary>
    public class IisHelper
    {
        /// <summary>
        /// 远程iisIP
        /// </summary>
        public static string RemoteServerIp { get; set; } = "127.0.0.1";
        /// <summary>
        /// 获取站点信息
        /// </summary>
        /// <returns></returns>
        public static List<SiteModel> GetiisSiteList()
        {
            var serverManager = new ServerManager();

            return serverManager.Sites.Select(c => new SiteModel
            {
                ApplicationPoolName = c.Applications["/"].ApplicationPoolName,
                Bindings = c.Bindings.Select(x => new BindModel
                {
                    BinIp = x.EndPoint?.Address.ToString() ?? "*",
                    BinPort = x.EndPoint?.Port ?? 0,
                    Protocol = x.Protocol,
                    Host = x.Host,
                    BindingInformation = x.BindingInformation
                }).ToList(),
                ConnectionTimeout = c.Limits.ConnectionTimeout,
                EnabledProtocols = c.Applications["/"].EnabledProtocols,
                Id = c.Id,
                MaxBandwidth = c.Limits.MaxBandwidth,
                MaxConnections = c.Limits.MaxConnections,
                Name = c.Name,
                PhysicalPath = c.Applications["/"].VirtualDirectories["/"].PhysicalPath,
                SchemaName = c.Schema.Name,
                ServerAutoStart = c.ServerAutoStart,
                State = c.State,

            }).ToList();
        }


        /// <summary>
        /// 获取进程池
        /// </summary>
        /// <returns></returns>
        public static List<ApplicationPoolModel> GetApplicationPoolList()
        {
            var serverManager = new ServerManager();
            return serverManager.ApplicationPools.Select(c => new ApplicationPoolModel()
            {
                ManagedPipelineMode = c.ManagedPipelineMode,
                State = c.State,
                Name = c.Name,
                ManagedRuntimeVersion = c.ManagedRuntimeVersion,
                PingingEnabled = c.ProcessModel.PingingEnabled,
                QueueLength = c.QueueLength,
                MaxProcessespe = c.ProcessModel.MaxProcesses,
                WorkerProcessesCount = c.WorkerProcesses.Count,
                Enable32BitAppOnWin64 = c.Enable32BitAppOnWin64

            }).ToList();
        }

        /// <summary>
        /// 添加应用程序
        /// </summary>
        public static void AddApplication(string siteName, string aliasName, string path)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                var website = st.Sites.FirstOrDefault(c => c.Name == siteName);
                var app = website?.Applications.Add($"/{aliasName}", path);
                if (app != null) app.ApplicationPoolName = website.Applications[0].ApplicationPoolName;
                st.CommitChanges();
            }
        }
        /// <summary>
        /// 移除应用程序
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="aliasName"></param>
        public static void RemoveApplication(string siteName, string aliasName)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                var website = st.Sites.FirstOrDefault(c => c.Name == siteName);
                website?.Applications.Remove(website.Applications.FirstOrDefault(c => c.Path == $"/{aliasName}"));
                st.CommitChanges();
            }
        } /// <summary>
          /// 移除所有应用程序
          /// </summary>
          /// <param name="siteName"></param>
        public static void RemoveApplication(string siteName)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                var website = st.Sites.FirstOrDefault(c => c.Name == siteName);
                var list = website?.Applications.Where(c => !c.Path.Replace("/", "").IsNullOrEmpty()).ToList();
                if (list != null)
                    foreach (var item in list)
                    {
                        website.Applications.Remove(item);
                    }

                st.CommitChanges();
            }
        }

        /// <summary>
        /// 获取所有应用程序名
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static List<ApplicationModel> GetApplication(string siteName)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                var website = st.Sites.FirstOrDefault(c => c.Name == siteName);
                var list = website?.Applications.Select(c => new ApplicationModel
                {
                    ApplicationName = c.Path.Replace("/", ""),
                    ApplicationPoolName = c.ApplicationPoolName,
                    Path = c.Path
                }).ToList();
                list?.Remove(list.FirstOrDefault(c => c.ApplicationName.IsNullOrEmpty()));
                return list;
            }
        }

        /// <summary>
        /// 创建应用程池
        /// </summary>
        /// <returns></returns>
        public static ApplicationPool CreateApplicationPool(string applicationPoolName, bool enable32BitAppOnWin64 = false, Netkinds netVersion = Netkinds.Net4, ManagedPipelineMode managedPipelineMode = ManagedPipelineMode.Integrated)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                var t = st.ApplicationPools.Add(applicationPoolName);
                st.ApplicationPools[applicationPoolName].ManagedPipelineMode = managedPipelineMode;
                st.ApplicationPools[applicationPoolName].ManagedRuntimeVersion = netVersion.GetEnumDisplayText();
                st.ApplicationPools[applicationPoolName].Enable32BitAppOnWin64 = enable32BitAppOnWin64;
                st.CommitChanges();
                return t;
            }
        }
        /// <summary>
        /// 移除应用程池
        /// </summary>
        /// <param name="applicationPoolName"></param>
        /// <returns></returns>
        public static bool RemoveApplicationPool(string applicationPoolName)
        {

            var site = GetiisSiteList().FirstOrDefault(c => c.ApplicationPoolName == applicationPoolName);
            if (site != null) throw new Exception($"站点{ site.Name },已经使用该应用程池");
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                var pool = st.ApplicationPools.FirstOrDefault(c => c.Name == applicationPoolName);
                if (pool == null) throw new Exception($"应用程池{applicationPoolName}不存在");
                st.ApplicationPools.Remove(pool);
                st.CommitChanges();
                return true;
            }
        }
        /// <summary>
        /// 创建站点
        /// </summary>
        /// <param name="siteName">站点名称</param>
        /// <param name="applicationPoolName">网站名称</param>
        /// <param name="path">物理路径</param>
        /// <param name="bindings">绑定信息</param>
        /// <returns></returns>
        public static Site CreateSite(string siteName, string applicationPoolName, string path, IEnumerable<BindModel> bindings)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                var bindModels = bindings as BindModel[] ?? bindings.ToArray();
                var site = st.Sites.Add(siteName, path, bindModels.ToArray()[0].BinPort);
                st.Sites[siteName].Applications[0].ApplicationPoolName = applicationPoolName;
                st.Sites[siteName].Bindings.Clear();
                foreach (var item in bindModels)
                {
                    st.Sites[siteName].Bindings.Add($"{item.BinIp}:{item.BinPort}:{item.Host}", item.Protocol);
                }

                st.CommitChanges();
                return site;
            }

        }
        /// <summary>
        /// 移除站点
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static bool RemoveSite(string siteName)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                var stmove = st.Sites.FirstOrDefault(c => c.Name == siteName);
                if (stmove == null) throw new Exception("站点不存在");
                st.Sites.Remove(stmove);
                st.CommitChanges();
                return true;
            }
        }
        /// <summary>
        /// 停止网站
        /// </summary>
        /// <returns></returns>
        public static void StopSite(string siteName)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                st.Sites[siteName].Stop();
                st.CommitChanges();
            }
        }
        /// <summary>
        ///开启网站
        /// </summary>
        /// <param name="siteName"></param>
        public static void StartSite(string siteName)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                st.Sites[siteName].Start();
                st.CommitChanges();
            }
        }
        /// <summary>
        /// 重启网站
        /// </summary>
        /// <param name="siteName"></param>
        public static void Restart(string siteName)
        {
            StopSite(siteName);
            StartSite(siteName);
        }
        /// <summary>
        /// 运行应用程池
        /// </summary>
        /// <param name="applicationPoolName"></param>
        public static void ApplicationPoolStart(string applicationPoolName)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                st.ApplicationPools[applicationPoolName].Start();
                st.CommitChanges();
            }
        }
        /// <summary>
        /// 关闭运用程池
        /// </summary>
        /// <param name="applicationPoolName"></param>
        public static void ApplicationPoolStop(string applicationPoolName)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                st.ApplicationPools[applicationPoolName].Stop();
                st.CommitChanges();
            }
        }
        /// <summary>
        /// 重启运用程池
        /// </summary>
        /// <param name="applicationPoolName"></param>
        public static void ApplicationPoolRestart(string applicationPoolName)
        {
            ApplicationPoolStop(applicationPoolName);
            ApplicationPoolStart(applicationPoolName);
        }
        /// <summary>
        /// 站点是否存在
        /// </summary>
        /// <returns></returns>
        public static bool ExistSite(string siteName)
        {
            return GetiisSiteList().Any(c => c.Name == siteName);
        }
        /// <summary>
        /// 进程是否存在
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public static bool ExistApplication(string siteName, string applicationName)
        {
            return GetApplication(siteName).Exists(c => c.ApplicationName == applicationName);
        }
        /// <summary>
        /// 应用程池是否存在
        /// </summary>
        /// <param name="applicationPoolName"></param>
        /// <returns></returns>
        public static bool ExistApplicationPool(string applicationPoolName)
        {
            return GetApplicationPoolList().Any(c => c.Name == applicationPoolName);
        }
        /// <summary>
        /// 域名绑定
        /// </summary>
        /// <returns></returns>
        public static bool BinDomainName(string siteName, IEnumerable<BindModel> bindings)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                var bindModels = bindings as BindModel[] ?? bindings.ToArray();
                foreach (var item in bindModels)
                {
                    st.Sites[siteName].Bindings.Add($"{item.BinIp}:{item.BinPort}:{item.Host}", item.Protocol);
                }
                st.CommitChanges();
                return true;
            }
        }
        /// <summary>
        /// 移除所有绑定信息
        /// </summary>
        /// <returns></returns>
        public static void ClearAllBins(string siteName)
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                st.Sites[siteName].Bindings.Clear();
                st.CommitChanges();
            }
        }

        /// <summary>
        /// 添加默认文档
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="defaultDocName"></param>
        /// <param name="index"></param>
        public static void AddDefaultDocument(string siteName, string defaultDocName, int index = 0)
        {
            using (var mgr = new ServerManager())
            {
                var cfg = mgr.GetWebConfiguration(siteName);
                var defaultDocumentSection = cfg.GetSection("system.webServer/defaultDocument");
                var filesElement = defaultDocumentSection.GetChildElement("files");
                var filesCollection = filesElement.GetCollection();
                foreach (var elt in filesCollection)
                {
                    if (elt.Attributes["value"].Value.ToString() == defaultDocName)
                    {
                        return;
                    }
                }
                try
                {
                    var docElement = filesCollection.CreateElement();
                    docElement.SetAttributeValue("value", defaultDocName);
                    filesCollection.AddAt(index, docElement);
                }
                catch (Exception)
                {
                    // ignored
                } //this will fail if existing 

                mgr.CommitChanges();
            }
        }
        /// <summary>
        /// 修改网站名称
        /// </summary>
        public static void EditSiteAppName(string sourceName, string targetName="",string applicationPoolName = "")
        {
            using (var st = ServerManager.OpenRemote(RemoteServerIp))
            {
                if (!applicationPoolName.IsNullOrEmpty())
                {
                    st.ApplicationPools[sourceName].Name = applicationPoolName;
                    st.Sites[sourceName].Applications[0].ApplicationPoolName = applicationPoolName;
                }
                if(!targetName.IsNullOrEmpty())
                st.Sites[sourceName].Name = targetName;
                
                st.CommitChanges();
            }
        }
    }

}
