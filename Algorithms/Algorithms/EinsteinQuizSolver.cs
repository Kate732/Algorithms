using System.Security.Authentication;
using static Kate.Algorithms.EinsteinQuizSolver;

namespace Kate.Algorithms
{
	public static class EinsteinQuizSolver
	{
		public static IEnumerable<SetUp> Solve()
		{
			foreach (SetUp houses in GenerateAllPossibleSolutions())
			{
				if (IsSolution(houses))
				{
					yield return houses;
				}
				
			}
		}
		private static IEnumerable<T> GetAllCharacteristics<T>()
		{
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

		private static IEnumerable<House> GenerateAllPossibleHouses()
		{
			foreach (Color color in GetAllCharacteristics<Color>())
			{
				foreach (Nationality nationality in GetAllCharacteristics<Nationality>())
				{
					foreach (Pet pet in GetAllCharacteristics<Pet>())
					{
						foreach (Drink drink in GetAllCharacteristics<Drink>())
						{
							foreach (Сigarettes cigarettes in GetAllCharacteristics<Сigarettes>())
							{
								yield return new House { color = color, nationality = nationality, pet = pet, drink = drink, cigarettes = cigarettes };
							}
						}
					}
				}
			}
		}


		public static IEnumerable<SetUp> GenerateAllPossibleSolutions()
		{
			foreach (House house1 in GenerateAllPossibleHouses())
			{
				foreach (House house2 in GenerateAllPossibleHouses())
				{
					if (HousesHaveSameChar(new List<House> { house1, house2 })) continue;
					foreach (House house3 in GenerateAllPossibleHouses())
					{
						if (HousesHaveSameChar(new List<House> { house1, house2, house3 })) continue;
						foreach (House house4 in GenerateAllPossibleHouses())
						{
							if (HousesHaveSameChar(new List<House> { house1, house2, house3, house4 }))
								continue;
							foreach (House house5 in GenerateAllPossibleHouses())
							{
								if (HousesHaveSameChar(new List<House> { house1, house2, house3, house4, house5 }))
									continue;

								SetUp possibleSolution = new SetUp();
								possibleSolution.houses = new List<House> { house1, house2, house3, house4, house5 };
								if (!IsSolution(possibleSolution))
								{
									continue;
								}
								yield return possibleSolution;
							}
						}
					}
				}
			}
		}


		private static bool TwoHousesHaveSameChar(House h1, House h2)
		{
			if (h1.color == h2.color
				|| h1.pet == h2.pet
				|| h1.nationality == h2.nationality
				|| h1.cigarettes == h2.cigarettes
				|| h1.drink == h2.drink) return true;
			return false;
		}

		private static bool HousesHaveSameChar(List<House> houses)
		{
			for (int i = 0; i < houses.Count; i++)
			{
				foreach (House house in houses)
				{
					if (house == houses[i]) continue;
					if (TwoHousesHaveSameChar(house, houses[i]))
					{
						return true;
					}
				}
			}
			return false;
		}

		private static bool IsSolution(SetUp item)
		{
			if (TheEnglishmanLivesInTheRedHouse(item)
				&& TheSpaniardOwnsTheDog(item)
				&& CoffeeIsDrunkInTheGreenHouse(item)
				&& TheUkrainianDrinksTea(item)
				&& GreenHouseIsToTheRightOfIvoryHouse(item)
				&& TheOldGoldSmokerOwnsSnails(item)
				&& KoolsAreSmokedInTheYellowHouse(item)
				&& MilkIsDrunkInTheMiddleHouse(item)
				&& NorwegianLivesInTheFirstHouse(item)
				&& ManWhoSmokesChesterfieldsLivesNextToTheManWithTheFox(item)
				&& KoolsAreSmokedNextToTheHouseWithTheHorse(item)
				&& LuckyStrikeSmokerDrinksOrangeJuice(item)
				&& JapaneseSmokesParliaments(item)
				&& NorwegianLivesNextToTheBlueHouse(item))
			{
				return true;
			}
			return false;
		}

		private static bool TheEnglishmanLivesInTheRedHouse(SetUp item)
		{
			return item.houses.Single(h => h.color == Color.Red).nationality == Nationality.Englishman;
		}

		private static bool TheSpaniardOwnsTheDog(SetUp item)
		{
			return item.houses.Single(n => n.nationality == Nationality.Spaniard).pet == Pet.Dog;
		}
		private static bool CoffeeIsDrunkInTheGreenHouse(SetUp item)
		{
			return item.houses.Single(d => d.drink == Drink.Coffee).color == Color.Green;
		}
		private static bool TheUkrainianDrinksTea(SetUp item)
		{
			return item.houses.Single(d => d.drink == Drink.Tea).nationality == Nationality.Ukrainian;
		}
		private static bool GreenHouseIsToTheRightOfIvoryHouse(SetUp item)
		{
			int ivoryIndex = -1;
			int greenIndex = -1;
			for (int i = 0; i < item.houses.Count; i++)
			{
				if (item.houses[i].color == Color.Green)
				{
					greenIndex = i;
				}
				else if (item.houses[i].color == Color.Ivory)
				{
					ivoryIndex = i;
				}
			}
			if (ivoryIndex == -1 || greenIndex == -1)
			{
				throw new Exception("No ivory or green house in set up");
			}
			return greenIndex - ivoryIndex != 1;
		}

		private static bool TheOldGoldSmokerOwnsSnails(SetUp item)
		{
			return item.houses.Single(c => c.cigarettes == Сigarettes.OldGold).pet == Pet.Snails;
		}

		private static bool KoolsAreSmokedInTheYellowHouse(SetUp item)
		{
			return item.houses.Single(c => c.cigarettes == Сigarettes.Kools).color == Color.Yellow;
		}
		private static bool MilkIsDrunkInTheMiddleHouse(SetUp item)
		{
			int indexOfMilkHouse = -1;
			for (int i = 0; i < item.houses.Count; i++)
			{
				if (item.houses[i].drink == Drink.Milk)
				{
					indexOfMilkHouse = i;
					break;
				}
			}
			if (indexOfMilkHouse == -1)
			{
				throw new Exception("No house in which milk is drunk in set up");
			}
			return indexOfMilkHouse == 2;
		}
		private static bool NorwegianLivesInTheFirstHouse(SetUp item)
		{
			int indexOfNorwegianHouse = -1;
			for (int i = 0; i < item.houses.Count; i++)
			{
				if (item.houses[i].nationality == Nationality.Norwegian)
				{
					indexOfNorwegianHouse = i;
					break;
				}
			}
			if (indexOfNorwegianHouse == -1)
			{
				throw new Exception("No house in which Norwegian lives in set up");
			}
			return indexOfNorwegianHouse == 0;
		}
		private static bool ManWhoSmokesChesterfieldsLivesNextToTheManWithTheFox(SetUp item)
		{
			int indexOfManWhoSmokesChesterfields = -1;
			int indexOfManWithTheFox = -1;
			for (int i = 0; i < item.houses.Count; i++)
			{
				if (item.houses[i].cigarettes == Сigarettes.Chesterfields)
				{
					indexOfManWhoSmokesChesterfields = i;
				}
				else if (item.houses[i].pet == Pet.Fox)
				{
					indexOfManWithTheFox = i;
				}
			}
			if (indexOfManWithTheFox == -1 || indexOfManWhoSmokesChesterfields == -1)
			{
				throw new Exception("No house in which man who smokes Chesterfields or has fox lives in set up");
			}
			return Math.Abs(indexOfManWithTheFox - indexOfManWhoSmokesChesterfields) != 1;
		}
		private static bool KoolsAreSmokedNextToTheHouseWithTheHorse(SetUp item)
		{
			int indexOfHouseWhereKoolsAreSmoked = -1;
			int indexOfHouseWithTheHorse = -1;
			for (int i = 0; i < item.houses.Count; i++)
			{
				if (item.houses[i].pet == Pet.Horse)
				{
					indexOfHouseWithTheHorse = i;
				}
				else if (item.houses[i].cigarettes == Сigarettes.Kools)
				{
					indexOfHouseWhereKoolsAreSmoked = i;
				}
			}
			if (indexOfHouseWithTheHorse == -1 || indexOfHouseWhereKoolsAreSmoked == -1)
			{
				throw new Exception("No house in which man who smokes Kools or has horse lives in set up");
			}

			return Math.Abs(indexOfHouseWithTheHorse - indexOfHouseWhereKoolsAreSmoked) != 1;
		}
		private static bool LuckyStrikeSmokerDrinksOrangeJuice(SetUp item)
		{
			return item.houses.Single(c => c.cigarettes == Сigarettes.LuckyStrike).drink == Drink.OrangeJuice;
		}

		private static bool JapaneseSmokesParliaments(SetUp item)
		{
			return item.houses.Single(c => c.cigarettes == Сigarettes.Parliaments).nationality == Nationality.Japanese;
		}

		private static bool NorwegianLivesNextToTheBlueHouse(SetUp item)
		{
			int indexOfHouseOfNorwegian = -1;
			int indexOfTheBlueHouse = -1;
			for (int i = 0; i < item.houses.Count; i++)
			{
				if (item.houses[i].color == Color.Blue)
				{
					indexOfTheBlueHouse = i;
				}
				else if (item.houses[i].nationality == Nationality.Norwegian)
				{
					indexOfHouseOfNorwegian = i;
				}
			}
			if (indexOfHouseOfNorwegian == -1 || indexOfHouseOfNorwegian == -1)
			{
				throw new Exception("No house in which Norwegian lives or blue house in set up");
			}

			return Math.Abs(indexOfHouseOfNorwegian - indexOfHouseOfNorwegian) != 1;
		}

		public class SetUp
		{
			public List<House> houses { get; set; }
		}

		public class House
		{
			public Color color { get; set; }
			public Nationality nationality { get; set; }
			public Pet pet { get; set; }
			public Drink drink { get; set; }
			public Сigarettes cigarettes { get; set; }
		}

		public enum Color
		{
			Red, Green, Ivory, Yellow, Blue
		}
		public enum Nationality
		{
			Englishman, Spaniard, Ukrainian, Norwegian, Japanese
		}
		public enum Pet
		{
			Dog, Snails, Fox, Horse, Zebra
		}
		public enum Drink
		{
			Coffee, Tea, Milk, OrangeJuice, Water
		}
		public enum Сigarettes
		{
			OldGold, Chesterfields, Kools, LuckyStrike, Parliaments
		}
	}
}
