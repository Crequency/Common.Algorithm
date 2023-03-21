namespace Common.Algorithm.Interop;

public static class Environment
{

    /// <summary>
    /// 核心文件路径
    /// </summary>
    public const string DllPath = "./Core/";

    /// <summary>
    /// 云端 DLL 存储路径
    /// </summary>
    private static string _cloudUrl = "https://source.catrol.cn/lib/Algorithm/Core/";

    /// <summary>
    /// 库版本
    /// </summary>
    private static string _version = "v1.0";

    /// <summary>
    /// 核心文件文件名
    /// </summary>
    private static readonly List<string> CoreFiles = new()
    {
        "Math.dll",
        "Hash.dll",
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
    /// 检查环境是否就绪, 核心文件是否存在
    /// </summary>
    /// <returns>环境是否就绪</returns>
    public static bool Check()
    {
        foreach (string fn in CoreFiles)
        {
            if (!File.Exists(Path.GetFullPath($"{DllPath}{fn}")))
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
        if (!Directory.Exists(DllPath))
            Directory.CreateDirectory(DllPath);

        switch (im)
        {
            case InstallMethod.HttpClient:

                {
                    using var client = new HttpClient();

                    foreach (var fn in CoreFiles)
                    {
                        var downloadUrl = $"{_cloudUrl}{_version}/{fn}";
                        var savePath = Path.GetFullPath($"{DllPath}{fn}");

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
