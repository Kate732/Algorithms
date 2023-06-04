using Kate.Algorithms;

namespace Kate.TestProject;

[TestClass]
public class SimpleHashTest
{
	[TestMethod]
	public void Calculate_TheSameInput_TheSameOutput()
	{
		Assert.AreEqual(SimpleHash.CalculateSimpleStrHash("kate"), SimpleHash.CalculateSimpleStrHash("kate"));
	}

	[TestMethod]
	public void Calculate_DifferentInput_DifferentOutput()
	{
		Assert.AreNotEqual(SimpleHash.CalculateSimpleStrHash("ira"), SimpleHash.CalculateSimpleStrHash("kate"));
	}

	[TestMethod]
	public void Calculate_ZeroInput_ZeroOutput()
	{
		Assert.AreEqual(0, SimpleHash.CalculateSimpleStrHash(""));
	}

	[TestMethod]
	public void Calculate_abString()
	{
		Assert.AreEqual(63, SimpleHash.CalculateSimpleStrHash("ab"));
	}
}
