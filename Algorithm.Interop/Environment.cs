using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (!File.Exists($"{dll_path}{fn}"))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 安装环境
        /// </summary>
        public static void InstallEnvironment()
        {
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
        }
    }
}

#pragma warning restore SYSLIB0014 // 类型或成员已过时
