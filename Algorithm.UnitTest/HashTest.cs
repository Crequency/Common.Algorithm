using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Algorithm.Interop;
using System.Collections.Generic;
using System.Threading;

namespace Algorithm.UnitTest
{
    [TestClass]
    public class 哈希测试
    {
        [TestMethod]
        public void 哈希可行性测试()
        {
            string[] testData = new string[15]
            {
                "SHVIOSJDifjDKljkJ$*F$W*938r5834r89we9fIOSFJOIS",   // 基础 ASCII 测试
                "SHVIOSJDifjDKljkJ$*F$W*939r5834r89we9fIOSFJOIS",   // 微变更测试 938 -> 939
                "DHSJKfkl5262fads43234LKgjsd#$%$%#$%fjLKSdkfJLD",   // 大变更测试
                "的是抗拒那就客服的撒滤镜打算离开房间啊w8e9832",           // 中文测试
                "的是抗拒那就客服的撒滤镜打算离开房间啊w8e9132",           // 中文微变更测试 9832 -> 9132
                "的dsa是fsd抗f拒s阿f斯是25是34会3卡死了的肌肤",           // 中文大变更测试
                "426435314513461434532561234123614325415324",       // 纯数字测试
                "426235314513461434532561234123614325415324",       // 纯数字小变更 4264 -> 4262
                "426435434658956844336135342782895245234324",       // 纯数字大变更
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
                Console.WriteLine(Hash.FromString2Hex_WithoutCompress(testData[i]));
            }
        }

        [TestMethod]
        public void 哈希压缩器可行性测试()
        {
            string[] testData = new string[15]
            {
                "SHVIOSJDifjDKljkJ$*F$W*938r5834r89we9fIOSFJOIS",   // 基础 ASCII 测试
                "SHVIOSJDifjDKljkJ$*F$W*939r5834r89we9fIOSFJOIS",   // 微变更测试 938 -> 939
                "DHSJKfkl5262fads43234LKgjsd#$%$%#$%fjLKSdkfJLD",   // 大变更测试
                "的是抗拒那就客服的撒滤镜打算离开房间啊w8e9832",           // 中文测试
                "的是抗拒那就客服的撒滤镜打算离开房间啊w8e9132",           // 中文微变更测试 9832 -> 9132
                "的dsa是fsd抗f拒s阿f斯是25是34会3卡死了的肌肤",           // 中文大变更测试
                "426435314513461434532561234123614325415324",       // 纯数字测试
                "426235314513461434532561234123614325415324",       // 纯数字小变更 4264 -> 4262
                "426435434658956844336135342782895245234324",       // 纯数字大变更
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
                Console.WriteLine(Hash.FromString2Hex_WithoutCompress(testData[i]));
                Console.WriteLine(Hash.FromString2Hex(testData[i]));
            }
        }

        [TestMethod]
        public void BenchMark_短长度字符串Hash测试()
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
                Console.WriteLine(Hash.FromString2Hex_WithoutCompress(item));
            }
        }

        [TestMethod]
        public void BenchMark_短长度字符串原生Hash测试()
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

        [TestMethod]
        public void BenchMark_多线程测试()
        {
            List<Thread> threads = new();
            for(int i = (int)1e8; i < (int)1e8 + 1000; ++i)
            {
                int id = i;
                threads.Add(new Thread(() =>
                {
                    string testData = id.ToString();
                    string hashCom = Hash.FromString2Hex(testData);
                    string hashSrc = Hash.FromString2Hex_WithoutCompress(testData, true);
                    Console.WriteLine(testData);
                    Console.WriteLine($"\t{hashSrc}");
                    Console.WriteLine($"\t{hashCom}");
                    Console.WriteLine();
                }));
            }
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
        }
    }
}