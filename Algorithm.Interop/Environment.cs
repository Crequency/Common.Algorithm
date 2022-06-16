using System.Net;

#pragma warning disable SYSLIB0014 // 类型或成员已过时

namespace Algorithm.Interop
{
    public class Environment
    {

        /// <summary>
        /// 核心文件路径
        /// </summary>
        public const string dll_path = "./Core/";

        /// <summary>
        /// 云端 DLL 存储路径
        /// </summary>
        public const string cloudUrl = "https://source.catrol.cn/lib/Algorithm/Core/";

        /// <summary>
        /// 库版本
        /// </summary>
        public const string version = "v1.0";

        /// <summary>
        /// 核心文件文件名
        /// </summary>
        private readonly static List<string> CoreFiles = new()
        {
            "Math.dll",
            "Hash.dll"
        };

        /// <summary>
        /// 检查环境是否就绪, 核心文件是否存在
        /// </summary>
        /// <returns>环境是否就绪</returns>
        public static bool CheckEnvironment()
        {
            foreach (string fn in CoreFiles)
            {
                if (!File.Exists(Path.GetFullPath($"{dll_path}{fn}")))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 安装环境
        /// <paramref name="im">安装方式</paramref>
        /// </summary>
        public static void InstallEnvironment(InstallMethod im = InstallMethod.WebClient)
        {
            if(!Directory.Exists(dll_path))
                Directory.CreateDirectory(dll_path);
            switch (im)
            {
                case InstallMethod.WebClient:
                    WebClient wc = new();
                    foreach (string fn in CoreFiles)
                    {
                        if (!File.Exists($"{dll_path}{fn}"))
                        {
                            wc.DownloadFile(
                                new Uri($"{cloudUrl}{version}/{fn}", UriKind.Absolute),
                                Path.GetFullPath($"{dll_path}{fn}"));
                        }
                    }
                    wc.Dispose();
                    break;
            }
        }

        /// <summary>
        /// 异步安装环境
        /// <paramref name="im">异步安装方式</paramref>
        /// </summary>
        public static async Task InstallEnvironmentAsync(InstallMethodAsync im = InstallMethodAsync.WebClientAsync)
        {
            if (!Directory.Exists(dll_path))
                Directory.CreateDirectory(dll_path);
            switch (im)
            {
                case InstallMethodAsync.WebClientAsync:
                    var wc = new WebClient();
                    foreach (string fn in CoreFiles)
                    {
                        if (!File.Exists($"{dll_path}{fn}"))
                        {
                            await wc.DownloadFileTaskAsync(
                                new Uri($"{cloudUrl}{version}/{fn}", UriKind.Absolute),
                                Path.GetFullPath($"{dll_path}{fn}"));
                        }
                    }
                    wc?.Dispose();
                    break;
                case InstallMethodAsync.Http:

                    break;
            }
        }

        /// <summary>
        /// 安装环境下载方式
        /// </summary>
        public enum InstallMethod
        {
            WebClient
        }

        /// <summary>
        /// 安装环境下载方式
        /// </summary>
        public enum InstallMethodAsync
        {
            WebClientAsync, Http
        }
    }
}

#pragma warning restore SYSLIB0014 // 类型或成员已过时
