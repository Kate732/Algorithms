using Kate.Algorithms;

namespace Kate.TestProject;
[TestClass]
public class SortersTest
{
    [TestMethod]
    public void BubbleSort_5el()
    {
        List<int> origin = new List<int> { 2, 1, -55, 48, 7 };
        List<int> expected = new List<int> { -55, 1, 2, 7, 48 };
        Sorters.BubbleSort(origin);
        CollectionAssert.AreEqual(expected, origin);
    }

    [TestMethod]
    public void BubbleSort_3elSorted()
    {
        List<int> origin = new List<int> { 1, 2, 3 };
        List<int> expected = new List<int> { 1, 2, 3 };
        Sorters.BubbleSort(origin);
        CollectionAssert.AreEqual(expected, origin);
    }

    [TestMethod]
    public void BubbleSort_2el()
    {
        List<int> origin = new List<int> { 2, 1 };
        List<int> expected = new List<int> { 1, 2 };
        Sorters.BubbleSort(origin);
        CollectionAssert.AreEqual(expected, origin);
    }

    [TestMethod]
    public void BubbleSort_1el()
    {
        List<int> origin = new List<int> { 2 };
        List<int> expected = new List<int> { 2 };
        Sorters.BubbleSort(origin);
        CollectionAssert.AreEqual(expected, origin);
    }

    [TestMethod]
    public void BubbleSort_0el()
    {
        List<int> origin = new List<int> { };
        List<int> expected = new List<int> { };
        Sorters.BubbleSort(origin);
        CollectionAssert.AreEqual(expected, origin);
    }
}