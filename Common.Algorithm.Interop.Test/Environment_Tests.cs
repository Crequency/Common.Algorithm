namespace Common.Algorithm.Interop;

[TestClass]
public class Environment_Tests
{
    [TestMethod]
    public async Task Test_Install()
    {
        Console.WriteLine(Environment.Check());

        Console.WriteLine(Path.GetFullPath("./"));

        if (!Environment.Check())
            await Environment.InstallAsync();

        Thread.Sleep(3000);

        Assert.IsTrue(Environment.Check());
    }
}
