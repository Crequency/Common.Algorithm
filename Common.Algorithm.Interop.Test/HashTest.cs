using System.Diagnostics;

namespace Common.Algorithm.Interop.Test;

[TestClass]
public class HashTest
{
    private readonly string[] TestData = new string[15]
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

    [TestMethod]
    public void Test_Feasibility()
    {
        foreach (var data in TestData)
        {
            Console.WriteLine(data);

            Console.WriteLine(Hash.FromString2Hex_WithoutCompress(data));

            Console.WriteLine(Hash.FromString2Hex(data));
        }
    }

    [TestMethod]
    public void Test_MultiCompressLevel()
    {
        foreach (var item in TestData)
        {
            Console.WriteLine(item);

            foreach (Hash.CompressLevel clv in Enum.GetValues(typeof(Hash.CompressLevel)))
                Console.WriteLine($"" +
                    $"\t{clv}" +
                    $"\t{(clv == Hash.CompressLevel.x128 ? "" : "\t")}" +
                    $"{Hash.FromString2Hex(item, true, clv)}" +
                    $"");
        }
    }

    [TestMethod]
    public void Test_RandomData_MoreThan2048Bytes()
    {
        const int times = 1000;
        var data = new string[times];
        for (var i = 0; i < times; ++i)
        {
            for (var j = 0; j < 2048; ++j)
                data[i] += '0' + j % 9;
            data[i] += '0' + i % 9;
        }
        for (var i = 0; i < times; i++)
        {
            Console.WriteLine($"{i}. {data[i]}\n\t{Hash.FromString2Hex_WithoutCompress(data[i])}");
        }
    }

    [TestMethod]
    public void Test_RandomData_ShortString()
    {
        List<string> testData = new();
        Random random = new();
        for (var i = 0; i < 50; ++i)
        {
            var data = "";
            for (var j = 0; j < 1024; ++j)
            {
                data += (char)('a' + random.Next(0, 25));
            }
            testData.Add(data);
        }
        foreach (var item in testData)
        {
            Console.WriteLine(item);
            Console.WriteLine(Hash.FromString2Hex_WithoutCompress(item));
        }
    }

    [TestMethod]
    public void Test_RandomData_ShortStringWithDefaultHashAlgorithm()
    {
        List<string> testData = new();
        Random random = new();
        for (var i = 0; i < 50; ++i)
        {
            var data = "";
            for (var j = 0; j < 1024; ++j)
            {
                data += (char)('a' + random.Next(0, 25));
            }
            testData.Add(data);
        }
        foreach (var item in testData)
        {
            Console.WriteLine(item);
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(item.GetHashCode())));
        }
    }

    [TestMethod]
    public void BenchMark_MultiThreads()
    {
        var sync = false;
        object locker = new();
        List<Thread> threads = new();
        Stopwatch reg = new();
        TimeSpan sum = new(0);
        int ran = 0, times = 1000;
        if (sync) reg.Start();
        for (var i = (int)1e8; i < (int)1e8 + times; ++i)
        {
            var id = i;
            threads.Add(new Thread(() =>
            {
                var testData = id.ToString();
                Stopwatch st = new();
                if (sync) st.Start();
                var hashCom = Hash.FromString2Hex(testData);
                if (sync) st.Stop();
                var rt = sync ? $"\n\t执行时间: {st.Elapsed}" : "";
                var output = $"{testData}{rt}\n\t{hashCom}\n";
                if (sync)
                    lock (locker)
                    {
                        sum += st.Elapsed;
                    }
                Console.WriteLine(output);
                if (sync) Interlocked.Increment(ref ran);
            }));
        }
        if (sync) reg.Stop();
        if (sync) Console.WriteLine($"注册用时: {reg.Elapsed}");
        foreach (var thread in threads)
        {
            thread.Start();
        }
        if (sync) while (ran != times)
            {
            }

        if (sync) Console.WriteLine($"总用时: {sum}\n平均用时: {sum.TotalSeconds * 1.0 / 1000}s");
    }

    [TestMethod]
    public void BenchMark_MultiProcessors()
    {
        const int times = 10000;
        Stopwatch sw = new();
        sw.Start();
        Parallel.For((int)1e8, (int)1e8 + times, (i, state) =>
        {
            //TODO: 多核并行执行测试
        });
    }

    [TestMethod]
    public void BenchMark_IS_1()
    {
        const string a = "SHVIOSJDifjDKljkJ$*F$W*938r5834r89we9fIOSFJOIS";

        int same = 0, times = 1000;

        const string ans = "97-8F-3C-EB-2E-A3-A8-78-06-03-4D-B1-AF-16-DB-19-42-99-6F-7C-97-5F-71-EA-31-56-DF-37-32-20-4B-72-5B-FD-BB-4E-B3-23-8B-DE-DB-F8-C4-F9-05-29-58-50-20-B5-58-A5-E0-7F-EB-C0-2E-26-CD-98-BC-AF-A5-F8-9D-2A-8D-B8-57-3A-17-25-79-2D-7B-E0-42-56-2D-F9-63-47-DA-3C-BE-08-F2-E2-DB-DF-82-8A-15-3F-CB-D6-2C-41-78-50-43-20-5A-1F-E1-D8-01-5C-5C-C6-5D-A8-01-E3-07-E2-43-4F-F6-BB-E8-17-C5-BF-0D-39-AC-E8-53-E2-12-C2-19-C8-05-40-DD-96-EA-A7-31-2D-B0-CB-23-C2-73-89-B7-1D-F1-E3-82-84-02-66-F3-C1-0D-4D-0E-32-79-3B-8D-C0-6D-00-68-23-64-2F-91-2D-1D-81-17-BB-70-1B-35-44-54-14-83-71-DF-05-08-AE-26-6C-55-CC-E5-28-B5-43-C0-1A-A6-3D-EE-04-CF-85-2B-45-D8-F0-B4-19-C3-65-D6-EF-10-22-3B-63-C0-32-4B-78-F7-61-3E-D8-74-E3-A0-EF-33-6A-D1-2F-EC-04-1A-4B-60-E8-61-E4-95-81-16-AF-D7-1C-30-05-83-7D-7C-5A-11-36-4C-30-28-40-E8-08-6D-80-7F-02-E1-A3-B2-C5-6C-FA-59-38-81-F5-50-2D-67-89-4A-17-51-42-28-09-24-6D-8A-E3-71-88-75-CF-D8-3F-C1-06-C1-39-BE-96-B7-3D-EC-F0-1B-28-A2-74-9B-F4-F3-58-49-66-4B-0C-2E-9A-9E-72-5C-03-B3-5F-CF-B9-49-6E-C2-47-31-2A-B8-CD-BD-9C-33-67-54-97-54-5A-FC-3F-93-19-A0-51-29-02-E7-C2-32-0D-E1-B9-4E-21-1B-2D-7F-63-23-0B-C4-DA-EE-DC-2F-EE-A9-7F-23-3E-20-D8-86-5C-58-B5-EF-10-2A-AB-98-86-F3-58-CD-AB-18-F7-71-A0-01-C2-BA-BA-65-02-51-2F-D2-DA-CA-D5-11-CF-32-7D-84-46-0D-74-2D-AD-95-6A-9C-11-08-B5-D9-F5-B9-FA-8E-53-39-51-A2-DF-63-5E-37-77-38-62-3F-33-DB-28-7B-9D-8C-37-00-72-BC-0A-99-62-03-75-BE-63-7A-AF-47-C8-8D-4F-D3-BC-E6-61-CE-DC-3B-5D-16-0D-8F-C1-77-79-D9-88-3D-6B-E8-C9-CB-C4-85-2F-A3-CD-77-25-89-81-DA-CA-A9-33-32-D7-2A-DE-C8-87-85-88-92-5D-53-8E-89-A2-8D-B6-E6-6B-6A-C9-20-C1-58-ED-CF-DC-D3-C2-95-69-A4-E7-B4-8B-AE-19-66-9C-3C-DA-D0-E9-85-C3-07-C6-3C-A5-6F-1F-19-62-10-4A-98-2F-59-5D-91-64-20-BC-42-D0-DA-4D-AE-5D-C0-B2-01-F9-26-DF-89-9B-DA-AE-09-20-CE-37-AE-D6-06-BB-78-AE-98-96-70-E2-7F-00-AC-8C-E0-82-6D-FC-19-FA-B9-6B-0A-BD-C3-94-4B-53-DF-FC-59-E4-26-75-CC-58-FD-CB-2E-D9-4C-07-87-49-E4-50-B9-00-87-16-69-83-EC-E8-B8-89-59-72-D8-4C-41-D6-70-9B-F1-C0-1A-0E-6C-3B-F4-44-C6-FE-34-AC-9A-0E-8E-D1-3C-64-51-D2-4F-57-8B-EC-DE-52-59-C1-EA-04-BD-7C-9E-90-71-81-EC-0A-BA-15-6C-AC-CB-C8-4B-87-26-EC-E9-80-BC-8E-DA-8C-4A-34-DF-86-55-35-5D-34-22-BF-60-35-06-85-79-16-72-CE-9E-8A-77-F3-0B-4D-BC-34-8B-CF-67-2C-30-77-01-BD-7E-64-64-90-64-B3-3B-58-E2-9D-BB-58-D9-DD-43-CF-B3-31-F4-A6-40-67-2D-00-00-34-95-F6-9E-B8-90-B8-1A-F4-D8-39-E8-D8-A3-C8-AC-56-33-97-9E-70-30-61-D6-71-24-CB-EC-7A-1D-43-7E-5C-4C-4A-4B-14-DD-36-1C-C2-69-5A-C5-73-E6-2B-92-AE-7F-BC-0A-D5-C9-2B-91-D8-F5-25-A4-9A-EC-79-12-C2-A8-25-05-7E-7D-3D-5C-85-88-A1-09-7E-DC-E1-06-D5-67-FA-52-2C-4D-0E-49-03-6A-E8-97-1F-5B-23-DB-28-4A-C1-B2-27-32-E7-BB-C7-BC-BB-52-4C-D3-F3-38-54-28-41-2C-F7-DA-D5-79-C4-66-EF-B6-4D-89-16-7E-58-97-A3-54-0D-56-0B-4C-4B-F8-A3-B8-F9-56-E3-94-77-41-60-42-A2-38-89-91-C9-D2-73-CD-5C-A8-1B-B5-3C-7B-C5-F0-F0-3D-78-00-38-AB-D8-0B-53-C4-8C-84-3F-92-16-51-00-65-89-24-2A-27-BA-16-52-79-F7-AE-6B-32-30-BE-DC-37-54-A9-CB-D4-B1-09-B4-FB-57-65-26-F3-45-A2-11-B6-0B-50-FC-AF-BC-6F-D9-CA-5B-F4-05-11-6D-E0-A4-30-A6-CF-FE-CC-4D-A8-E2-C9-6E-27-FC-C3-1C-ED-06-0F-3A-84-AC-51-C3-0F-25-86-4B-41-E7-C7-63-D1-F0-6E-70-E6-68-BF-7C-B0-5D-3B-89-60-F2-3C-8A-84-61-0D-60-49-8D-E3-B3-3B-D2-00-18-AB-3D-37-6D-C2-8C-EA-B2-82-E6-EC-63-40-1D-2F-F4-DE-DF-5F-4D-13-27-94-E2-77-DB-E4-5F-0F-A0-8B-E3-C5-AE-95-25-64-3A-7E-B8-50-66-D2-FC-0B-EA-60-6E-80-DE-4B-B7-EF-27-2B-DF-10-2B-3E-5D-25-B3-F8-55-82-6B-CB-D3-44-2A-0D-38-8B-69-63-EF-E4-1E-5F-FA-0F-78-9E-AA-57-40-C0-43-CD-40-BD-FE-C4-F1-8A-76-A9-F4-07-7F-1A-6C-32-9A-18-DC-B2-1B-D5-3C-9B-BF-65-A0-70-42-95-C6-42-0B-EE-B9-F2-B3-D2-49-A5-86-8A-4B-0D-4F-6E-E9-E2-44-E8-07-F1-EF-6D-CC-67-F8-32-1D-BC-07-76-66-D8-83-C2-B8-77-1E-7D-B8-39-AB-25-9A-8E-FC-19-1C-25-2A-99-31-8C-F2-70-30-14-05-21-1B-F0-84-08-99-AC-0B-8F-71-CC-F2-AC-D8-A0-24-98-1F-65-1B-66-45-11-2E-B0-1E-C0-17-09-D2-45-2C-9A-14-F0-0A-D6-C0-9B-21-45-13-33-BB-8D-F3-01-DE-32-F2-73-12-80-80-95-7A-4B-3C-61-0D-44-75-62-3A-DE-76-E7-50-11-0B-36-05-37-35-5B-8D-24-14-A0-89-B5-5B-86-60-56-6A-79-D5-53-20-8A-1F-80-D2-9D-3F-A8-79-12-F6-EB-D3-C9-B3-81-B1-09-58-9D-BA-70-C4-06-50-EA-00-D0-6B-30-BC-B7-CE-89-B9-E8-1E-B7-26-C5-1E-D9-A2-88-21-78-A1-85-23-93-3F-9A-72-C6-A4-C9-24-18-62-64-0A-5F-40-32-EB-39-39-8C-B6-36-F1-BC-65-E4-9C-31-BE-47-3A-62-87-69-D3-5C-EB-96-76-39-4F-21-52-A0-5D-A8-75-89-79-74-ED-8E-B7-E6-09-B7-A0-F5-FA-67-7E-50-65-B6-05-74-17-A9-C7-BE-D7-F1-DA-68-8B-59-B6-51-A6-5A-32-5C-CB-3F-14-94-F0-A3-09-51-B7-A2-4C-39-0F-D0-D8-14-29-89-4B-40-EA-5A-FD-5C-72-05-9C-49-36-5D-75-66-25-E9-F5-32-49-36-C0-41-6C-5F-FA-12-A9-CD-03-79-5C-FB-33-5A-FD-3D-CA-7C-31-97-B0-D2-79-5B-F9-88-91-01-7D-E6-ED-7F-83-A9-8B-C0-CF-8E-5C-7C-7A-5B-3F-B8-56-DD-E0-83-2D-72-6D-94-CA-58-D7-5D-68-9E-47-A8-FD-28-12-4B-1C-C6-C3-75-C2-ED-F0-8A-BB-30-13-77-D3-4A-95-A3-2D-02-26-4D-58-D2-B5-A5-8D-7E-72-76-35-98-71-67-8C-DC-99-67-F3-D8-1B-35-DF-F1-AA-65-48-D9-57-41-2E-AD-9F-08-D5-65-87-A5-A5-8E-B4-19-60-EE-C8-97-ED-B2-A0-E5-DF-37-6F-3A-44-22-7C-53-B5-75-B9-9A-8E-48-14-1B-1E-25-00-74-23-0D-25-C3-8D-C5-D6-B0-09-AD-80-F7-57-06-E2-91-73-4A-28-7A-42-6F-66-99-35-9B-25-D1-82-1F-37-E1-DC-E1-A8-E0-69-81-4D-B8-47-6E-AD-4B-D1-29-7F-36-0D-32-9A-8D-CC-4F-CD-53-50-F1-9D-76-F0-7A-2D-E4-9C-9B-1D-D3-C1-D4-9B-E2-B3-03-8E-BA-4C-53-73-A9-E1-A2-6A-A3-43-2A-59-D6-E8-E1-B3-6F-20-92-6E-2A-94-D7-FD-2E-06-5A-60-8C-C2-D4-D7-C2-71-7A-7F-F0-C2-51-71-5F-28-9D-8F-96-EB-BD-B5-62-C3-38-12-4B-FD-7A-47-DE-81-81-3F-58-9B-46-A8-5A-C5-F8-7C-DA-ED-C1-4C-4E-77-75-FA-F8-3A-D6-C0-64-03-F2-E7-97-43-CC-F4-DC-7E-DD-4B-89-9A-09-79-F7-DE-C3-FD-E5-12-12-50-CB-2E-A4-4C-38-4A-1A-99-96-C3-99-16-07-6B-BC-8F-FB-34-9B-11-B1-02-79-34-FC-9B-3F-22-45-BE-A6-EF-5E-DF-39-78-AD-4E-57-ED-47-C1-CD-F1-74-2D-14-A8-02-CB-32-D0-07-D4-3C-40-4A-48-E1-92-A1-A9-A5-F2-14-11-AB-7C-8D-96-58-E5-10-D6-76-EC-2A-25-AB-0D-2B-75-B1-74-16-DD-F1-7A-C6-98-FB-2B-85-A8-34-F8-01-AF-63-22-8A-13-36-9F-5C-11-D9-46-C2-12-B5-56-27-60-53-53-9C-4E-A4-86-C4-01-AD-75-60-54-E7-B1-07-F9-51-C0-41-41-53-96-56-DF-10-C0-A3-1C-E1-F0-C5-C2-5D-80-32-9A-E7-95-30-63-07-A2-A7-A4-62-9C-7C-6E-BC-90-9A-FC-3C-1E-45-9A-87-60-2B-DF-54-D7-8D-6E-EC-ED-0B-70-0D-18-E5-A1-DC-7D-4B-99-AB-CE-E1-CC-3A-7A-CD-E1-6E-DE-1A-74-72-0B-F7-2D-AC-5C-AC-27-FA-71-66-1A-DE-9D-D0-22-30-DD-CA-AB-53-E8-A4-19-C8-35-6F-8C-79-14-A8-24-D8-25-16-C2";

        List<string> list = new();

        for (var i = 0; i < times; ++i)
        {
            var output = Hash.FromString2Hex_WithoutCompress(a);

            if (output == ans) ++same;

            list.Add(output);
        }

        Console.WriteLine($"通过: {same}/{times}, 错误: {times - same}");

        foreach (var item in list)
            Console.WriteLine(item);

        Assert.AreEqual(same, times);
    }
}
