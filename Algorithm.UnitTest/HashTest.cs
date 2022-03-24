using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Algorithm.Interop;
using System.Collections.Generic;
using System.Threading;

namespace Algorithm.UnitTest
{
    [TestClass]
    public class HashTest
    {
        /// <summary>
        /// 哈希可行性测试
        /// </summary>
        [TestMethod]
        public void FeasibilityTest()
        {
            string[] testData = new string[12]
            {
                "SHVIOSJDifjDKljkJ$*F$W*938r5834r89we9fIOSFJOIS",   // 基础 ASCII 测试
                "SHVIOSJDifjDKljkJ$*F$W*939r5834r89we9fIOSFJOIS",   // 微变更测试 938 -> 939
                "DHSJKfkl5262fads43234LKgjsd#$%$%#$%fjLKSdkfJLD",   // 大变更测试
                "的是抗拒那就客服的撒滤镜打算离开房间啊w8e9832",           // 中文测试
                "的是抗拒那就客服的撒滤镜打算离开房间啊w8e9132",           // 中文微变更测试 9832 -> 9132
                "的dsa是fsd抗f拒s阿f斯是25是34会3卡死了的肌肤",           // 中文大变更测试
                "^$#%#$@T#@$@#$%#@^#$#@^#@%$&$#*$!*()$*@)($*)(#@",  // 纯符号测试
                "^$#%#$@T#@$@#$%#@^#$#@!#@%$&$#*$!*()$*@)($*)(#@",  // 纯符号微变更测试 ^ -> !
                "^$#%#*$(**(&#@(*$#*%(@$*(#@()#@09(()$*!)#(@*(#@",  // 纯符号大变更测试
                "🐦🐡🐣🐱💣",    // Emoji(Unicode) 测试
                "🐦🐡💯🐱💣",    // Emoji(Unicode) 小变更测试 🐣 -> 💯
                "💬💰💮🕷🚩"     // Emoji(Unicode) 大变更测试
            };
            for (int i = 0; i < testData.Length; ++i)
            {
                Console.WriteLine(testData[i]);
                Console.WriteLine(Hash.FromString_ToHex_WithoutCompress(testData[i]));
            }
        }

        /// <summary>
        /// 短长度字符串Hash测试
        /// </summary>
        [TestMethod]
        public void BenchMark_1()
        {
            List<string> testdata = new();
            Random random = new();
            for (int i = 0; i < 50; ++i)
            {
                string data = "";
                for (int j = 0; j < 1024; ++j)
                {
                    data += (char)('a' + random.Next(0, 25));
                }
                testdata.Add(data);
            }
            foreach (string item in testdata)
            {
                Console.WriteLine(item);
                Console.WriteLine(Hash.FromString_ToHex_WithoutCompress(item));
            }
        }

        /// <summary>
        /// 短长度字符串Hash测试
        /// </summary>
        [TestMethod]
        public void BenchMark_1_Native()
        {
            List<string> testdata = new();
            Random random = new();
            for (int i = 0; i < 50; ++i)
            {
                string data = "";
                for (int j = 0; j < 1024; ++j)
                {
                    data += (char)('a' + random.Next(0, 25));
                }
                testdata.Add(data);
            }
            foreach (string item in testdata)
            {
                Console.WriteLine(item);
                Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(item.GetHashCode())));
            }
        }

        /// <summary>
        /// 多线程测试
        /// </summary>
        [TestMethod]
        public void BenchMark_MultiThreads()
        {
            List<Thread> threads = new();
            for(int i = 0; i < 100; ++i)
            {
                threads.Add(new Thread(() =>
                {

                }));
            }
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
        }
    }
}