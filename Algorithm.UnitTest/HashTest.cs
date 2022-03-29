using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Algorithm.Interop;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

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
        public void IS_1_压力测试()
        {
            const string a = "SHVIOSJDifjDKljkJ$*F$W*938r5834r89we9fIOSFJOIS";
            int same = 0, times = 1000;
            string ans = "86-31-F0-48-DD-8B-DD-A9-4B-D5-D3-92-AD-B0-BF-8A-73-9B-7A-2A-55-DB-D9-CC-FC-A6-BD-97-8C-7B-AE-8F-D9-FE-84-7E-CD-68-74-FB-88-72-C8-D8-4D-7D-AE-A6-BF-62-6E-E6-50-80-E1-97-40-B4-EF-CB-23-63-EA-90-B0-5E-93-A7-79-21-AA-9C-76-B4-2C-8B-88-08-A7-82-93-72-DD-90-99-86-85-C9-B2-B2-79-E5-5F-DB-9E-74-01-8E-E8-6D-BD-44-8B-C9-91-51-B2-1A-8E-8F-0F-89-C8-E8-0B-7B-B6-9E-F9-CB-74-FB-C0-91-E1-5C-9E-6E-94-8C-B8-44-AE-7A-06-09-97-C1-CD-D2-48-94-8A-56-6A-A3-F3-90-56-48-AF-85-D4-9E-8A-2E-68-4E-D9-67-67-AD-93-7D-73-AD-B9-5A-F9-E9-A8-6D-7C-0A-8A-A4-4E-92-8C-93-6D-00-39-95-B3-CD-58-7A-B7-77-A5-8D-9A-AF-BD-F4-9C-AC-C6-7A-BF-5F-CE-68-37-99-76-12-EC-A2-B3-95-7A-A8-BC-89-FB-A5-73-EE-EA-C7-1E-2F-F4-E2-E8-D5-CF-6A-F0-68-38-C7-53-C3-92-48-B5-C6-4A-E5-0B-B7-36-86-D4-9A-E1-71-AB-E6-F9-AD-AD-A1-CC-9D-B4-DE-4A-65-5D-0C-42-97-29-72-8E-71-8F-CE-BF-58-3B-70-C4-2B-43-62-29-04-82-80-16-C8-96-DF-AE-F5-FE-8D-8F-23-B1-12-AD-A1-88-92-8F-96-27-5A-A5-9F-B9-3D-71-97-5B-A9-CA-03-D5-7C-E0-BA-4E-DC-E3-AF-81-BB-98-B7-A8-91-C6-B1-88-8F-BD-7B-D6-D2-E2-0E-E3-A2-AC-57-80-CC-4C-8E-74-D2-6A-D4-22-1D-5C-CC-D3-A3-9F-18-70-16-7C-9D-FD-0D-F4-77-18-CA-EF-30-BA-AE-3B-5B-63-07-D8-46-E9-E9-5C-1B-76-94-3D-D8-CD-E6-CF-90-E3-B9-E4-FD-D7-A7-E8-49-AF-82-88-7E-C0-BE-B4-F7-9B-ED-EE-D6-9C-AA-BC-8C-8D-CB-09-2E-F1-E1-1C-4F-9B-C1-72-1C-71-97-1D-5C-13-D9-E7-04-67-48-CC-CE-DE-BE-68-98-AC-DB-EB-00-FF-FD-C3-D9-BA-92-82-86-72-9C-A6-55-03-E3-66-B0-B0-AC-8D-F5-84-8A-10-A0-77-BE-56-9D-BE-04-8D-D3-91-63-7E-06-79-86-AE-83-54-6E-E8-7F-FA-14-A4-D0-83-BB-94-A1-91-93-79-1B-C8-54-80-9C-94-EB-79-78-46-76-DE-B7-DB-B9-CA-BB-35-BA-0F-CB-65-A3-86-B9-40-66-71-6F-10-A7-EF-B8-DF-08-B0-8F-5B-78-8C-C4-D5-A0-61-94-B9-BE-A1-4B-5B-7D-A0-62-96-7C-2B-EA-DB-B6-59-3B-FB-A6-B5-F9-F9-27-F2-CD-D6-07-62-B5-D1-C5-70-5C-26-63-D2-F7-AE-A4-61-BD-78-7E-D7-0F-AC-8E-2A-2D-DB-76-C3-19-93-8E-D6-CA-D6-74-9F-9F-D4-AB-44-B4-27-A2-AB-CF-7E-37-F0-3D-C4-29-44-BF-4F-66-D2-C5-8D-D8-B6-D2-E7-5C-7F-73-E9-A3-A8-BB-CA-A7-9E-68-01-AF-AF-E5-85-C7-38-74-65-06-D1-8F-F7-2B-D3-84-E3-D9-9F-8D-34-90-92-91-89-93-8D-49-9C-FC-54-B3-8A-77-F0-C8-D1-BF-36-F3-AF-DB-26-84-03-81-8D-4A-F6-D1-E4-D8-D7-DC-45-F2-D2-F0-12-6C-6B-94-95-C2-0B-12-44-57-9B-24-6F-9E-18-6F-47-81-A6-D7-0E-31-B5-8A-56-65-FF-CF-EA-75-A6-C4-A0-D2-E0-68-35-A6-69-6E-60-DC-0D-5C-31-5D-04-BF-43-D0-0F-AB-45-69-8E-5F-F2-54-80-CB-D9-67-F3-1E-86-BF-E7-80-EC-0B-1C-4D-74-29-73-D1-10-B5-4D-25-A1-54-B5-4F-DD-C5-0B-4C-40-C1-19-15-AD-59-66-20-EE-4A-1B-34-67-1D-70-E8-B6-4C-92-60-79-97-13-77-17-16-C8-5A-8C-07-91-FB-61-F1-2E-7D-38-5F-5F-70-5B-39-77-6B-85-8A-67-8F-7C-AF-C1-D6-6D-A8-19-23-18-DF-4F-7B-61-B9-9A-A7-5D-FE-3B-86-7D-CB-6D-70-1F-7F-28-A2-4D-9B-13-41-55-80-34-1E-80-11-53-53-B2-6D-C7-65-CB-59-7A-E2-36-AB-09-9F-25-72-1F-3F-6E-90-08-4A-09-9D-30-D1-8A-23-63-C6-70-89-2A-AE-37-E5-0B-8D-61-98-39-DA-3A-96-56-25-62-27-BC-50-A4-22-1A-36-2C-51-2A-D2-28-83-B2-2F-F4-68-99-D7-D5-0A-79-4B-F0-96-3C-3C-01-7E-83-0C-B6-73-C0-43-92-3C-55-5B-C9-E0-CC-35-75-4B-89-30-9D-2F-9C-EE-09-EA-20-A8-7A-4A-1C-7F-03-BE-35-B7-1A-E5-57-93-30-01-28-C9-0D-90-1B-B6-22-48-1F-FE-2D-88-2B-4D-2E-49-94-7A-9F-A3-35-F3-59-D3-4C-2E-3B-9A-20-CB-D5-28-A8-08-4C-14-42-09-AB-4A-17-81-08-9A-37-E3-31-89-2C-14-31-B2-27-6F-91-2F-8B-40-CD-62-EE-2F-6A-6E-C5-86-02-0D-39-E4-EF-1D-77-4A-36-C0-68-BC-28-B9-0E-A6-14-98-33-7C-2B-84-0A-E0-2E-2A-3D-E3-7A-B1-6D-4A-0A-76-20-99-35-32-36-6C-14-15-1F-A2-19-97-F4-DF-23-97-93-91-02-9E-4A-88-57-08-53-31-DB-6D-A8-80-2A-24-88-06-78-36-B0-60-96-3A-C5-0D-CD-53-16-12-D9-47-A1-61-70-D7-2D-CB-2A-D7-5A-92-31-7E-DD-54-D8-53-85-0F-C3-61-B8-1B-6B-0E-FC-19-B8-6F-E8-32-EE-6A-19-86-7D-99-3D-A1-54-B2-E6-44-6F-20-1D-30-33-04-0C-9D-04-9D-06-CA-63-5B-83-6C-EA-3E-95-14-A8-C0-0D-86-1E-AA-6D-C5-1B-FB-41-7D-0F-AA-37-4B-21-4A-21-28-07-A0-17-B0-39-C5-06-6D-6E-C7-3B-EE-29-42-5B-70-34-91-1D-B5-A9-EE-4C-70-2C-F5-66-6D-A6-71-C4-55-75-86-08-A7-68-55-A2-1B-EC-02-3A-1A-8A-A2-AF-09-C3-B1-3E-56-50-76-33-02-6E-8B-28-C6-ED-27-78-9E-3D-00-5A-6C-A8-48-DB-1A-15-37-83-3E-39-DA-57-2D-0D-34-C6-4D-C5-39-BE-56-0B-B4-9E-14-E1-64-F7-0F-30-35-9B-21-9E-18-0E-33-48-18-1D-D7-7B-C4-33-C3-14-AA-A9-2D-9E-0E-53-DB-06-8A-0F-41-0B-A5-09-A9-42-2A-92-22-90-24-98-E0-86-31-F5-01-46-55-E4-34-84-BE-DA-8B-73-1D-30-9D-1F-F5-17-D5-36-58-34-A2-3D-13-3C-75-63-9B-20-1F-38-AE-34-E8-06-CD-3D-1A-9A-0B-E8-06-A5-1B-AE-0F-13-B5-69-74-2C-3C-65-C4-0A-24-69-E5-06-EB-1A-7A-B1-36-38-68-EC-0D-26-1D-D4-83-40-9B-01-ED-5E-6D-6B-75-3D-AD-C8-36-0A-36-7D-0E-6D-30-B6-AA-07-AC-3D-D0-C8-0D-27-97-3F-ED-0C-29-7F-7D-5F-36-C3-1C-77-94-37-79-03-E0-62-1C-DC-19-C3-2F-7C-20-7C-D7-61-97-D9-36-89-1D-39-DF-28-8F-0B-54-21-89-C4-17-C6-EA-25-E3-03-7E-05-48-01-54-B2-07-E7-24-37-01-1F-99-01-AF-07-27-11-AF-0E-07-22-55-1F-67-6A-5F-0F-A9-CF-08-D5-76-AB-35-AF-58-C0-3B-CF-F4-18-CC-3B-B7-3C-A2-07-D4-62-55-97-01-72-1A-33-82-B5-BC-66-EC-2D-DB-93-31-A3-1D-CA-CF-8C-25-D2-2C-94-F9-17-13-29-B3-37-83-29-07-1D-83-25-A2-0A-8E-86-24-F6-56-CF-01-69-60-EC-35-93-24-BC-66-08-A6-39-72-80-2E-B0-05-B5-27-AE-17-BC-28-21-00-19-CF-62-83-22-BD-14-A9-06-8B-0C-81-29-6D-09-7B-1B-DE-2F-EC-01-C2-35-86-40-AA-35-9F-3D-83-20-68-31-9A-3A-31-8A-5B-EE-23-80-3C-23-0E-AC-78-13-32-B4-80-B1-25-63-F4-63-68-39-82-29-E3-0B-A8-38-B3-31-85-2C-CA-1E-7B-68-28-AD-2F-79-17-BC-4C-C0-2D-AD-73-C1-58-07-B2-22-B3-3E-41-0A-85-BF-36-F7-06-AE-19-FD-36-85-AE-3D-D2-1A-E9-05-73-34-BD-2A-49-18-B3-EC-4F-7D-34-93-21-F6-2E-D6-3D-78-3C-22-7C-C8-6C-76-07-D3-2F-8E-2D-A7-E3-28-A2-3F-B6-40-F0-3E-D6-01-72-2B-6E-01-F8-63-9D-19-41-08-BD-25-C8-01-4B-7F-3D-7C-0F-87-48-C5-7F-88-05-AA-20-C8-0B-56-0C-89-2E-76-04-8C-22-2D-CE-0C-86-3A-BA-1B-68-31-68-3D-8B-A7-19-E9-4A-85-2B-EB-3C-5F-30-37-25-7E-01-23-05-A1-03-0D-30-23-E3-27-C5-0E-DE-C4-80-14-88-13-10-19-EF-06-B0-34-06-06-FD-09-B0-19-B4-37-F6-38-71-20-C0-1A-D5-13-67-BB-1D-6E-17-40-14-00-1C-11-24-D1-2B-E9-31-A1-10-2B-36-C3-3A-A8-20-F9-16-0B-0B-92-92-08-CD-1D-81-14-97-1C-BF-82-A6-22-72-56-27-8F-75-C2-16-F5-87-15-C9-9A-27-E5-37-2F-1C-F6-28-84-3C-A0-BF-6F-8A-39-7D-21-BE-FC-25-A5-78-0A-D9-29-B3-7D-22-B3-3B-EE-21-E4-96-22-B1-39-B1-29-67-3E-C5-22-0C-25-EA-35-92-5B-19-9D-25-BF-29-71-38-C4-31-57-AB-2E-B3-AB-23-99-16-54-19-8F-7F-3F-D6-16-25-2A-69-13-0E-A2-29-6F-E7-1C-88-3B-68-18-CE-10-23-91-21-7D-DC-29-7A-1A-D3-1F-75-04-AF-2A-68-2D-5F-27-53-13-E1-6A-3F-B2-37-A4-39-72-09-C5-82-01-64-0F-3A";
            List<string> list = new();
            for (int i = 0; i < times; ++i)
            {
                string output = Hash.FromString2Hex_WithoutCompress(a);
                if (output == ans) ++same;
                list.Add(output);
            }
            Console.WriteLine($"通过: {same}/{times}, 错误: {times - same}");
            foreach (var item in list)
            {
                Console.WriteLine(item);
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
            bool sync = false;
            object locker = new();
            List<Thread> threads = new();
            Stopwatch reg = new();
            TimeSpan sum = new(0);
            int runned = 0, times = 10000;
            if(sync) reg.Start();
            for (int i = (int)1e8; i < (int)1e8 + times; ++i)
            {
                int id = i;
                threads.Add(new Thread(() =>
                {
                    string testData = id.ToString();
                    Stopwatch st = new();
                    if (sync) st.Start();
                    string hashCom = Hash.FromString2Hex(testData);
                    if (sync) st.Stop();
                    string rt = sync ? $"\n\t执行时间: { st.Elapsed}" : "";
                    string output = $"{testData}{rt}\n\t{hashCom}\n";
                    if (sync) lock (locker)
                    {
                        sum += st.Elapsed;
                    }
                    Console.WriteLine(output);
                    if (sync) Interlocked.Increment(ref runned);
                }));
            }
            if(sync) reg.Stop();
            if (sync) Console.WriteLine($"注册用时: {reg.Elapsed}");
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            if (sync) while (runned != times) ;
            if (sync) Console.WriteLine($"总用时: {sum}\n平均用时: {sum.TotalSeconds * 1.0 / 1000}s");
        }

        [TestMethod]
        public void CPU多核并行执行测试()
        {
            const int times = 10000;
            Stopwatch sw = new();
            sw.Start();
            Parallel.For((int)1e8, (int)1e8 + times, (i, state) =>
            {

            });
        }
    }
}