using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interop.Exceptions;

namespace Algorithm.Interop
{
    /// <summary>
    /// 文件哈希类
    /// </summary>
    public class Hash
    {
        /// <summary>
        /// DLL 路径
        /// </summary>
        private const string dll_path = "./Core/";

        #region 扩展库函数导入 [DLLImport Hash.dll]

        /// <summary>
        /// 调用扩展库进行 Hash
        /// </summary>
        /// <param name="src">元数据</param>
        /// <returns>哈希后的数据</returns>
        [DllImport($"{dll_path}Hash.dll", EntryPoint = "hash_str")]
        private static extern void hash_str(byte[] src, byte[] output);

        /// <summary>
        /// 调用扩展库进行 Hash 压缩
        /// </summary>
        /// <param name="src">哈希数据</param>
        /// <returns>哈希压缩后的数据</returns>
        [DllImport($"{dll_path}Hash.dll", EntryPoint = "hash_compress_128_str")]
        private static extern void hash_compress_128_str(byte[] src, byte[] output);

        /// <summary>
        /// 调用扩展库进行 Hash 压缩
        /// </summary>
        /// <param name="src">哈希数据</param>
        /// <returns>哈希压缩后的数据</returns>
        [DllImport($"{dll_path}Hash.dll", EntryPoint = "hash_compress_64_str")]
        private static extern void hash_compress_64_str(byte[] src, byte[] output);

        /// <summary>
        /// 调用扩展库进行 Hash 压缩
        /// </summary>
        /// <param name="src">哈希数据</param>
        /// <returns>哈希压缩后的数据</returns>
        [DllImport($"{dll_path}Hash.dll", EntryPoint = "hash_compress_32_str")]
        private static extern void hash_compress_32_str(byte[] src, byte[] output);

        /// <summary>
        /// 调用扩展库进行 Hash 压缩
        /// </summary>
        /// <param name="src">哈希数据</param>
        /// <returns>哈希压缩后的数据</returns>
        [DllImport($"{dll_path}Hash.dll", EntryPoint = "hash_compress_16_str")]
        private static extern void hash_compress_16_str(byte[] src, byte[] output);

        /// <summary>
        /// 调用扩展库进行 Hash 压缩
        /// </summary>
        /// <param name="src">哈希数据</param>
        /// <returns>哈希压缩后的数据</returns>
        [DllImport($"{dll_path}Hash.dll", EntryPoint = "hash_compress_8_str")]
        private static extern void hash_compress_8_str(byte[] src, byte[] output);

        /// <summary>
        /// 调用扩展库进行 Hash 压缩
        /// </summary>
        /// <param name="src">哈希数据</param>
        /// <returns>哈希压缩后的数据</returns>
        [DllImport($"{dll_path}Hash.dll", EntryPoint = "hash_compress_4_str")]
        private static extern void hash_compress_4_str(byte[] src, byte[] output);

        #endregion

        #region 辅助函数

        /// <summary>
        /// 统一调用哈希压缩
        /// </summary>
        /// <param name="mid">源压缩中间哈希值</param>
        /// <param name="rst">哈希压缩值储存地址</param>
        /// <param name="clv">哈希压缩级别</param>
        private static byte[] Compress(ref byte[] mid, CompressLevel clv)
        {
            switch (clv)
            {
                case CompressLevel.x128:
                    byte[] ans128 = new byte[128];
                    hash_compress_128_str(mid, ans128);
                    return ans128;
                case CompressLevel.x64:
                    byte[] ans64 = new byte[64];
                    hash_compress_64_str(mid, ans64);
                    return ans64;
                case CompressLevel.x32:
                    byte[] ans32 = new byte[32];
                    hash_compress_32_str(mid, ans32);
                    return ans32;
                case CompressLevel.x16:
                    byte[] ans16 = new byte[16];
                    hash_compress_16_str(mid, ans16);
                    return ans16;
                case CompressLevel.x8:
                    byte[] ans8 = new byte[8];
                    hash_compress_8_str(mid, ans8);
                    return ans8;
                case CompressLevel.x4:
                    byte[] ans4 = new byte[4];
                    hash_compress_4_str(mid, ans4);
                    return ans4;
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
        /// <returns>哈希后的Byte数组</returns>
        public static byte[] FromString(string str, CompressLevel clv = CompressLevel.x64)
        {
            byte[] array = Encoding.UTF8.GetBytes(str);     //  源字符串转 byte[]
            byte[] mid = new byte[2048];                    //  存储哈希值
            hash_str(array, mid);                           //  哈希运算
            byte[] rst = Compress(ref mid, clv);            //  哈希压缩运算
            return rst;                                     //  返回哈希压缩值
        }

        /// <summary>
        /// 进行字符串哈希(返回十六进制字符串)
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="rmLink">是否移除连字符</param>
        /// <returns>十六进制哈希字符串</returns>
        public static string FromString2Hex(string str, bool rmLink = false,
            CompressLevel clv = CompressLevel.x64)
        {
            byte[] array = Encoding.UTF8.GetBytes(str);     //  源字符串转 byte[]
            byte[] mid = new byte[2048];                    //  存储哈希值
            hash_str(array, mid);                           //  哈希运算
            byte[] rst = Compress(ref mid, clv);            //  哈希压缩运算
            string ans = BitConverter.ToString(rst);        //  哈希压缩运算转十六进制字符串
            return rmLink ? ans.Replace('-', '\0') : ans;   //  返回字符串, 据参数删除连字符
        }

        /// <summary>
        /// 进行字符串哈希
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>哈希后的Byte数组</returns>
        public static byte[] FromString_WithoutCompress(string str)
        {
            byte[] array = Encoding.UTF8.GetBytes(str);     //  源字符串转 byte[]
            byte[] mid = new byte[2048];                    //  存储哈希值
            hash_str(array, mid);                           //  哈希运算
            return mid;                                     //  返回哈希压缩值
        }

        /// <summary>
        /// 进行字符串哈希(返回十六进制字符串, 不压缩)
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="rmLink">是否移除连字符</param>
        /// <returns>十六进制不压缩哈希字符串</returns>
        public static string FromString2Hex_WithoutCompress(string str, bool rmLink = false)
        {
            byte[] array = Encoding.UTF8.GetBytes(str);     //  源字符串转 byte[]
            byte[] mid = new byte[2048];                    //  存储哈希值
            hash_str(array, mid);                           //  哈希运算
            string ans = BitConverter.ToString(mid);        //  哈希压缩运算转十六进制字符串
            return rmLink ? ans.Replace('-', '\0') : ans;   //  返回字符串, 据参数删除连字符
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
        public enum CompressLevel
        {
            x128 = 5, x64 = 4, x32 = 3, x16 = 2, x8 = 1, x4 = 0
        }

        #endregion
    }
}
