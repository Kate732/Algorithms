namespace Kate.Algorithms;

public static class Sorters
{
    public static void BubbleSort<T>(List<T> list) where T : IComparable<T>
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
                    bool changed = CompareAndSwitch<T>(i, list);
                    IsSorted &= !changed;
                }
            }
            else
            {
                for(int i = rightAdge; i > leftAdge; i--)
                {
                    int leftElementIndex = i - 1;
					bool changed = CompareAndSwitch<T>(leftElementIndex, list);
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

	private static bool CompareAndSwitch<T1>(int leftIndexElement, List<T1> list) where T1 : IComparable<T1>
	{
        int rightIndexElement = leftIndexElement + 1;
        int compared = list[leftIndexElement].CompareTo(list[rightIndexElement]);
		if (compared <= 0 )
        {
            return false;
        }
		T1 biggerNum = list[leftIndexElement];
		list[leftIndexElement] = list[rightIndexElement];
		list[rightIndexElement] = biggerNum;
		return true;
	}
}