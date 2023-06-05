using System.Diagnostics.Tracing;

namespace Kate.Algorithms;

public class TextStatistics
{
	public static Dictionary<string, int> CalculateDuplicatedWords(string fileName, int numOfTopHashes)
	{
		string fileText = FileToString(fileName);
		string[] lowerCaseWords = FilterText(fileText);
		string[] words = DeleteShortWords(lowerCaseWords);
		List<long> hashes = HashWords(words);
		Dictionary<long, string> hashToWord = GetHashToWord(words, hashes);
		Dictionary<long, int> hashRepeatings = CountRepeatings(hashes);
		Dictionary<long, int> topHashes = GetTopHashes(hashRepeatings, numOfTopHashes);
		return GetTopWords(topHashes, hashToWord);
	}

	private static Dictionary<string, int> GetTopWords(Dictionary<long, int> topHashes, Dictionary<long, string> hashToWord)
	{
		return topHashes.ToDictionary(pair => hashToWord[pair.Key], pair => pair.Value);
	}

	private static Dictionary<long, string> GetHashToWord(string[] words, List<long> hashes)
	{
		Dictionary<long, string> result = new Dictionary<long, string>();
		foreach (var pair in words.Zip(hashes))
		{
			if (!result.ContainsKey(pair.Second))
			{
				result.Add(pair.Second, pair.First);
			}
		}
		return result;
	}

	private static Dictionary<long, int> GetTopHashes(Dictionary<long, int> hashRepeatings, int numOfTopHashes)
	{
		return hashRepeatings.OrderByDescending(pair => pair.Value).Take(numOfTopHashes).ToDictionary(p => p.Key, p => p.Value);
	}

	private static Dictionary<long, int> CountRepeatings(List<long> hashes)
	{
		Sorters.BubbleSort(hashes);
		long oldHash = -2;
		Dictionary<long, int> hashRepeatings = new Dictionary<long, int>();
		foreach(long currentHash in hashes)
		{
			if(oldHash != currentHash)
			{
				hashRepeatings.Add(currentHash, 1);
				oldHash = currentHash;
			}
			else
			{
				hashRepeatings[currentHash]++;
			}
		}
		return hashRepeatings;
	}

	private static List<long> HashWords(string[] words)
	{
		return words.Select(SimpleHash.CalculateSimpleStrHash).ToList();
	}

	private static string[] DeleteShortWords(string[] words)
	{
		return words.Where(w => w.Length > 4).ToArray();
	}

	private static string[] FilterText(string fileText)
	{
		fileText = fileText.ToLower();
		List<char> charsToSplit = new List<char>();
		foreach (char c in fileText)
		{
			if (c < 'a' || c > 'z') 
			{
				charsToSplit.Add(c);
			}
		}
		return fileText.Split(charsToSplit.ToArray());
	}

	private static string FileToString(string fileName)
	{
		string fileText = File.ReadAllText(fileName);
		return fileText;
	}
}

