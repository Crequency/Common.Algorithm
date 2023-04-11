namespace Common.Algorithm.Interop;

[TestClass]
public class Environment_Tests
{
    [TestMethod]
    public async Task Test_Install()
    {
        Console.WriteLine(Environment.Check());

        var dir = Path.GetFullPath($"{Environment.DllPath}");

        Console.WriteLine(dir);

        if (Directory.Exists(dir))
            Directory.Delete(Path.GetFullPath($"{Environment.DllPath}"), true);

        if (!Environment.Check())
            await Environment.InstallAsync();

        Thread.Sleep(3000);

        Assert.IsTrue(Environment.Check());
    }
}
