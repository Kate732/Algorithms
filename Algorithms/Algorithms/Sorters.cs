using System.Threading.Channels;

namespace Kate.Algorithms;

public static class Sorters
{
    public static void BubbleSort(List<int> list)
    {
        bool IsSorted = false;
		int leftAdge = 0;
		int rightAdge = list.Count - 1;
        bool DirectionToTheEnd = true;
		while (!IsSorted)
        {
            IsSorted = true;
            if (DirectionToTheEnd)
            {
                for(int i = leftAdge; i < rightAdge; i++)
                {
                    bool changed = CompareAndSwitch(i, list);
                    IsSorted &= !changed;
                }
            }
            else
            {
                for(int i = rightAdge; i > leftAdge; i--)
                {
                    int leftElementIndex = i - 1;
					bool changed = CompareAndSwitch(leftElementIndex, list);
					IsSorted &= !changed;
				}
            }
            if(DirectionToTheEnd)
            {
				rightAdge--;
			}
            else
            {
                leftAdge++;
			}
            DirectionToTheEnd = !DirectionToTheEnd;
        }
    }

	private static bool CompareAndSwitch(int leftIndexElement, List<int> list)
	{
        int rightIndexElement = leftIndexElement + 1;
		if (list[leftIndexElement] <= list[rightIndexElement])
        {
            return false;
        }
		int biggerNum = list[leftIndexElement];
		list[leftIndexElement] = list[rightIndexElement];
		list[rightIndexElement] = biggerNum;
		return true;
	}
}