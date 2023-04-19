namespace Common.Algorithm.Interop;

public static class Environment
{
    /// <summary>
    /// 云端 DLL 存储路径
    /// </summary>
    private static string _cloudUrl = "https://source.catrol.cn/lib/Common.Algorithm.Core";

    /// <summary>
    /// 库版本
    /// </summary>
    private static string _version = "v2.0";

    /// <summary>
    /// 库架构
    /// </summary>
    private static string _arch
#if DEBUG
        = "win-x64-debug";
#else
        = "win-x64";
#endif

    /// <summary>
    /// 核心文件文件名
    /// </summary>
    private static readonly List<string> CoreFiles = new()
    {
        "Common.Algorithm.Core.dll",
    };

    /// <summary>
    /// 更新云端存储链接
    /// </summary>
    /// <param name="url"></param>
    public static void UpdateCloudStorageUrl(string url) => _cloudUrl = url;

    /// <summary>
    /// 更新云端存储版本
    /// </summary>
    /// <param name="ver"></param>
    public static void UpdateCloudStorageVersion(string ver) => _version = ver;

    /// <summary>
    /// 更新云端存储架构
    /// </summary>
    /// <param name="arch">架构</param>
    public static void UpdateCloudStorageArchitecture(string arch) => _arch = arch;

    /// <summary>
    /// 检查环境是否就绪, 核心文件是否存在
    /// </summary>
    /// <returns>环境是否就绪</returns>
    public static bool Check()
    {
        foreach (var fn in CoreFiles)
        {
            if (!File.Exists(Path.GetFullPath($"./{fn}")))
                return false;
        }
        return true;
    }

    /// <summary>
    /// 安装环境
    /// <paramref name="im">安装方式</paramref>
    /// </summary>
    public static async Task InstallAsync(InstallMethod im = InstallMethod.HttpClient)
    {
        switch (im)
        {
            case InstallMethod.HttpClient:

                {
                    using var client = new HttpClient();

                    foreach (var fn in CoreFiles)
                    {
                        var downloadUrl = $"{_cloudUrl}/{_arch}/{_version}/{fn}";
                        var savePath = Path.GetFullPath($"./{fn}");

                        if (!File.Exists(savePath))
                        {
                            using var stream = await client.GetStreamAsync(downloadUrl);

                            using var fileStream = File.Create(savePath);

                            await stream.CopyToAsync(fileStream);

                            await fileStream.FlushAsync();
                        }
                    }
                }

                break;
        }
    }

    /// <summary>
    /// 安装环境下载方式
    /// </summary>
    public enum InstallMethod
    {
        HttpClient = 1,
    }
}
