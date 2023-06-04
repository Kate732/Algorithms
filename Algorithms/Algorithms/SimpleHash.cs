namespace Kate.Algorithms
{
	public class SimpleHash
	{
		public static long CalculateSimpleStrHash(string str)
		{
			const int p = 31;
			const long m = 1000000009;
			long hash = 0;
			long k = 1;
			for (int i = 0; i < str.Length; i++)
			{
				hash += ConvertCharToInt(str[i])*k;
				k = p*k%m;
			}
			return hash;
		}

		private static int ConvertCharToInt(char v)
		{
			return v - 'a' + 1;
		}
	}
}
