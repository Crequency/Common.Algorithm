using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
        [DllImport($"{dll_path}Hash.dll", EntryPoint = "hash_compress_str")]
        private static extern void hash_compress_str(byte[] src, byte[] output);

        /// <summary>
        /// 进行字符串哈希
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>哈希后的Byte数组</returns>
        public static byte[] FromString(string str)
        {
            byte[] array = Encoding.UTF8.GetBytes(str); //  源字符串转 byte[]
            byte[] mid = new byte[2048];                //  存储哈希值
            byte[] rst = new byte[64];                  //  存储哈希压缩值
            hash_str(array, mid);                       //  哈希运算
            hash_compress_str(mid, rst);                //  哈希压缩运算
            return rst;                                 //  返回哈希压缩值
        }

        /// <summary>
        /// 进行字符串哈希(返回十六进制字符串)
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="rmLink">是否移除连字符</param>
        /// <returns>十六进制哈希字符串</returns>
        public static string FromString_ToHex(string str, bool rmLink)
        {
            byte[] array = Encoding.UTF8.GetBytes(str);     //  源字符串转 byte[]
            byte[] mid = new byte[2048];                    //  存储哈希值
            byte[] rst = new byte[64];                      //  存储哈希压缩值
            hash_str(array, mid);                           //  哈希运算
            hash_compress_str(mid, rst);                    //  哈希压缩运算
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
            byte[] array = Encoding.UTF8.GetBytes(str); //  源字符串转 byte[]
            byte[] mid = new byte[2048];                //  存储哈希值
            hash_str(array, mid);                       //  哈希运算
            return mid;                                 //  返回哈希压缩值
        }

        /// <summary>
        /// 进行字符串哈希(返回十六进制字符串, 不压缩)
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="rmLink">是否移除连字符</param>
        /// <returns>十六进制不压缩哈希字符串</returns>
        public static string FromString_ToHex_WithoutCompress(string str, bool rmLink = false)
        {
            byte[] array = Encoding.UTF8.GetBytes(str);     //  源字符串转 byte[]
            byte[] mid = new byte[2048];                    //  存储哈希值
            hash_str(array, mid);                           //  哈希运算
            string ans = BitConverter.ToString(mid);        //  哈希压缩运算转十六进制字符串
            return rmLink ? ans.Replace('-', '\0') : ans;   //  返回字符串, 据参数删除连字符
        }

        /// <summary>
        /// 哈希模式
        /// </summary>
        /// StringHash  ->  字符串哈希
        /// FileHash    ->  文件哈希
        public enum HashMode
        {
            StringHash = 0, FileHash = 1
        }
    }
}
