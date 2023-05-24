namespace TestProject;
[TestClass]
public class CalcuratorTest
{
    [TestMethod]
    public void Add_TwoPositiveInt()
    {
        Assert.AreEqual(5, Kate.Algorithms.Calculator.Add(2, 3));
    }

    [TestMethod]
    public void Add_TwoNegativeInt()
    {
        Assert.AreEqual(-5, Kate.Algorithms.Calculator.Add(-2, -3));

    }
}