﻿using Common.Algorithm.Interop.Exceptions;
using System.Runtime.InteropServices;
using System.Text;

namespace Common.Algorithm.Interop;

/// <summary>
/// 文件哈希类
/// </summary>
public static class Hash
{
    /// <summary>
    /// DLL 路径
    /// </summary>
    private const string DllPath = Environment.DllPath;

    #region 扩展库函数导入 [DLLImport Hash.dll]

    /// <summary>
    /// 调用扩展库进行 Hash
    /// </summary>
    /// <param name="src">元数据</param>
    /// <param name="output">输出</param>
    /// <param name="length">长度</param>
    /// <returns>哈希后的数据</returns>
    [DllImport($"{DllPath}Hash.dll", EntryPoint = "hash_str")]
    private static extern void hash_str(byte[] src, byte[] output, int length);

    /// <summary>
    /// 调用扩展库进行 Hash 压缩
    /// </summary>
    /// <param name="src">哈希数据</param>
    /// <param name="output">输出</param>
    /// <returns>哈希压缩后的数据</returns>
    [DllImport($"{DllPath}Hash.dll", EntryPoint = "hash_compress_128_str")]
    private static extern void hash_compress_128_str(byte[] src, byte[] output);

    /// <summary>
    /// 调用扩展库进行 Hash 压缩
    /// </summary>
    /// <param name="src">哈希数据</param>
    /// <param name="output">输出</param>
    /// <returns>哈希压缩后的数据</returns>
    [DllImport($"{DllPath}Hash.dll", EntryPoint = "hash_compress_64_str")]
    private static extern void hash_compress_64_str(byte[] src, byte[] output);

    /// <summary>
    /// 调用扩展库进行 Hash 压缩
    /// </summary>
    /// <param name="src">哈希数据</param>
    /// <param name="output">输出</param>
    /// <returns>哈希压缩后的数据</returns>
    [DllImport($"{DllPath}Hash.dll", EntryPoint = "hash_compress_32_str")]
    private static extern void hash_compress_32_str(byte[] src, byte[] output);

    /// <summary>
    /// 调用扩展库进行 Hash 压缩
    /// </summary>
    /// <param name="src">哈希数据</param>
    /// <param name="output">输出</param>
    /// <returns>哈希压缩后的数据</returns>
    [DllImport($"{DllPath}Hash.dll", EntryPoint = "hash_compress_16_str")]
    private static extern void hash_compress_16_str(byte[] src, byte[] output);

    /// <summary>
    /// 调用扩展库进行 Hash 压缩
    /// </summary>
    /// <param name="src">哈希数据</param>
    /// <param name="output">输出</param>
    /// <returns>哈希压缩后的数据</returns>
    [DllImport($"{DllPath}Hash.dll", EntryPoint = "hash_compress_8_str")]
    private static extern void hash_compress_8_str(byte[] src, byte[] output);

    /// <summary>
    /// 调用扩展库进行 Hash 压缩
    /// </summary>
    /// <param name="src">哈希数据</param>
    /// <param name="output">输出</param>
    /// <returns>哈希压缩后的数据</returns>
    [DllImport($"{DllPath}Hash.dll", EntryPoint = "hash_compress_4_str")]
    private static extern void hash_compress_4_str(byte[] src, byte[] output);

    #endregion

    #region 辅助函数

    /// <summary>
    /// 哈希压缩统一调用
    /// </summary>
    /// <param name="mid">源压缩中间哈希值</param>
    /// <param name="clv">哈希压缩级别</param>
    private static byte[] Compress(ref byte[] mid, CompressLevel clv)
    {
        var ans = new byte[(int)clv];

        switch (clv)
        {
            case CompressLevel.x128:
                hash_compress_128_str(mid, ans);
                return ans;
            case CompressLevel.x64:
                hash_compress_64_str(mid, ans);
                return ans;
            case CompressLevel.x32:
                hash_compress_32_str(mid, ans);
                return ans;
            case CompressLevel.x16:
                hash_compress_16_str(mid, ans);
                return ans;
            case CompressLevel.x8:
                hash_compress_8_str(mid, ans);
                return ans;
            case CompressLevel.x4:
                hash_compress_4_str(mid, ans);
                return ans;
            default: return Array.Empty<byte>();
        }
    }

    /// <summary>
    /// 推断哈希采用的压缩级别, 并检查是否合法
    /// </summary>
    /// <param name="compressed">哈希压缩值</param>
    /// <param name="src">原文</param>
    /// <param name="lnk">是否拥有连字符</param>
    /// <returns>压缩级别</returns>
    /// <exception cref="HashException">不合法判断异常, 级别未找到异常</exception>
    public static CompressLevel HashCompressLevelParse(string compressed,
        string src, bool lnk = true)
    {
        foreach (CompressLevel item in Enum.GetValues(typeof(CompressLevel)))
            if (FromString2Hex(src, !lnk, item).Equals(compressed))
                return item;
        throw new HashException(HashException.ErrorType.UndefinedCompressLevel);
    }

    #endregion

    #region 字符串哈希算法

    /// <summary>
    /// 进行字符串哈希
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="clv">压缩级别</param>
    /// <returns>哈希后的Byte数组</returns>
    public static byte[] FromString(string str,
        CompressLevel clv = CompressLevel.x64)
    {
        var array = Encoding.UTF8.GetBytes(str);     //  源字符串转 byte[]
        var mid = new byte[2048];                    //  存储哈希值

        hash_str(array, mid, array.Length);             //  哈希运算

        var rst = Compress(ref mid, clv);            //  哈希压缩运算

        return rst;                                     //  返回哈希压缩值
    }

    /// <summary>
    /// 进行字符串哈希 (返回十六进制字符串)
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="rmLink">是否移除连字符</param>
    /// <param name="clv">压缩级别</param>
    /// <returns>十六进制哈希字符串</returns>
    public static string FromString2Hex(string str, bool rmLink = false,
        CompressLevel clv = CompressLevel.x64)
    {
        var array = Encoding.UTF8.GetBytes(str);     //  源字符串转 byte[]
        var mid = new byte[2048];                    //  存储哈希值

        hash_str(array, mid, array.Length);             //  哈希运算

        var rst = Compress(ref mid, clv);            //  哈希压缩运算
        var ans = BitConverter.ToString(rst);        //  哈希压缩运算转十六进制字符串

        return rmLink ? ans.Replace("-", "") : ans;     //  返回字符串, 据参数删除连字符
    }

    /// <summary>
    /// 进行字符串哈希
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>哈希后的Byte数组</returns>
    public static byte[] FromString_WithoutCompress(string str)
    {
        var array = Encoding.UTF8.GetBytes(str);     //  源字符串转 byte[]
        var mid = new byte[2048];                    //  存储哈希值

        hash_str(array, mid, array.Length);             //  哈希运算

        return mid;                                     //  返回哈希压缩值
    }

    /// <summary>
    /// 进行字符串哈希 (返回十六进制字符串, 不压缩)
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="rmLink">是否移除连字符</param>
    /// <returns>十六进制不压缩哈希字符串</returns>
    public static string FromString2Hex_WithoutCompress(string str,
        bool rmLink = false)
    {
        var array = Encoding.UTF8.GetBytes(str);     //  源字符串转 byte[]
        var mid = new byte[2048];                    //  存储哈希值

        hash_str(array, mid, array.Length);             //  哈希运算

        var ans = BitConverter.ToString(mid);        //  哈希压缩运算转十六进制字符串

        return rmLink ? ans.Replace("-", "") : ans;     //  返回字符串, 据参数删除连字符
    }

    #endregion

    #region 辅助枚举值定义

    /// <summary>
    /// 哈希模式
    /// </summary>
    /// StringHash  ->  字符串哈希
    /// FileHash    ->  文件哈希
    public enum HashMode
    {
        StringHash = 0, FileHash = 1
    }

    /// <summary>
    /// 哈希压缩级别
    /// </summary>
    public enum CompressLevel : int
    {
        // ReSharper disable InconsistentNaming

        x128 = 128, x64 = 64, x32 = 32, x16 = 16, x8 = 8, x4 = 4

        // ReSharper restore InconsistentNaming
    }

    #endregion
}
