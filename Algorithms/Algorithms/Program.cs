using Kate.Algorithms;

//CallEinsteinQuizSolver();
CallTextStatistics();

void CallTextStatistics()
{
	foreach(var pair in TextStatistics.CalculateDuplicatedWords("Fairytale.txt", 15)){
		Console.WriteLine($"{pair.Key} - {pair.Value}");
	}
}

static void CallEinsteinQuizSolver()
{
	foreach (EinsteinQuizSolver.SetUp solution in EinsteinQuizSolver.Solve())
	{
		int i = 1;
		foreach (var house in solution.houses)
		{
			System.Console.WriteLine($"{i} {house.color} {house.drink} {house.cigarettes} {house.pet} {house.nationality}");
			i++;
		}
	}
}

