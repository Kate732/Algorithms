// See https://aka.ms/new-console-template for more information
foreach (Kate.Algorithms.EinsteinQuizSolver.SetUp solution in Kate.Algorithms.EinsteinQuizSolver.Solve())
{
    int i = 1;
    foreach (var house in solution.houses)
    {
        System.Console.WriteLine($"{i} {house.color} {house.drink} {house.cigarettes} {house.pet} {house.nationality}");
        i++;
    }
}

