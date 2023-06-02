namespace Kate.Algorithms
{
    public static class EinsteinQuizSolver
    {
        public static IEnumerable<SetUp> Solve()
        {
            long step = 1;
            foreach (SetUp possibleSolution in GenerateAllPossibleSolutions())
            {
                step++;
                if (step % 50_000_000 == 0)
                {
                    Console.WriteLine($"Step = {step}, {DateTime.Now}");
                }
                if (IsSolution(possibleSolution))
                {
                    yield return possibleSolution;
                }
            }
        }
        private static List<T> GetAllCharacteristics<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        private static IEnumerable<House> GenerateAllPossibleHouses(
            IEnumerable<Color> possibleColors,
            IEnumerable<Nationality> possibleNationalities,
            IEnumerable<Pet> possiblePets,
            IEnumerable<Drink> possibleDrinks,
            IEnumerable<Сigarettes> possibleСigarettes)
        {
            foreach (Color color in possibleColors)
            {
                foreach (Nationality nationality in possibleNationalities)
                {
                    foreach (Pet pet in possiblePets)
                    {
                        foreach (Drink drink in possibleDrinks)
                        {
                            foreach (Сigarettes cigarettes in possibleСigarettes)
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
            var phc1 = GetAllCharacteristics<Color>();
            var phn1 = new List<Nationality> { Nationality.Norwegian };
            var php1 = GetAllCharacteristics<Pet>();
            var phd1 = GetAllCharacteristics<Drink>().Where(x => x != Drink.Milk).ToList();
            var phcg1 = GetAllCharacteristics<Сigarettes>();
            foreach (House house1 in GenerateAllPossibleHouses(phc1, phn1, php1, phd1, phcg1))
            {
                var phc2 = phc1.Where(x => x != house1.color).ToList();
                var phn2 = GetAllCharacteristics<Nationality>().Where(x => x != house1.nationality).ToList();
                var php2 = php1.Where(x => x != house1.pet).ToList();
                var phd2 = phd1.Where(x => x != house1.drink).ToList();
                var phcg2 = phcg1.Where(x => x != house1.cigarettes).ToList();
                foreach (House house2 in GenerateAllPossibleHouses(phc2, phn2, php2, phd2, phcg2))
                {
                    var phc3 = phc2.Where(x => x != house2.color).ToList();
                    var phn3 = phn2.Where(x => x != house2.nationality).ToList();
                    var php3 = php2.Where(x => x != house2.pet).ToList();
                    var phd3 = new List<Drink> { Drink.Milk };
                    var phcg3 = phcg2.Where(x => x != house2.cigarettes).ToList();
                    foreach (House house3 in GenerateAllPossibleHouses(phc3, phn3, php3, phd3, phcg3))
                    {
                        var phc4 = phc3.Where(x => x != house3.color).ToList();
                        var phn4 = phn3.Where(x => x != house3.nationality).ToList();
                        var php4 = php3.Where(x => x != house3.pet).ToList();
                        var phd4 = phd2.Where(x => x != house2.drink).ToList();
                        var phcg4 = phcg3.Where(x => x != house3.cigarettes).ToList();
                        foreach (House house4 in GenerateAllPossibleHouses(phc4, phn4, php4, phd4, phcg4))
                        {
                            var phc5 = phc4.Where(x => x != house4.color).ToList();
                            var phn5 = phn4.Where(x => x != house4.nationality).ToList();
                            var php5 = php4.Where(x => x != house4.pet).ToList();
                            var phd5 = phd4.Where(x => x != house4.drink).ToList();
                            var phcg5 = phcg4.Where(x => x != house4.cigarettes).ToList();
                            foreach (House house5 in GenerateAllPossibleHouses(phc5, phn5, php5, phd5, phcg5))
                            {
                                SetUp possibleSolution = new SetUp()
                                {
                                    houses = new List<House> { house1, house2, house3, house4, house5 }
                                };
                                yield return possibleSolution;
                            }
                        }
                    }
                }
            }
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
                //&& MilkIsDrunkInTheMiddleHouse(item)
                //&& NorwegianLivesInTheFirstHouse(item)
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
            return greenIndex - ivoryIndex == 1;
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
                if (item.houses[i].pet == Pet.Fox)
                {
                    indexOfManWithTheFox = i;
                }
            }
            if (indexOfManWithTheFox == -1 || indexOfManWhoSmokesChesterfields == -1)
            {
                throw new Exception("No house in which man who smokes Chesterfields or has fox lives in set up");
            }
            return Math.Abs(indexOfManWithTheFox - indexOfManWhoSmokesChesterfields) == 1;
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
                if (item.houses[i].cigarettes == Сigarettes.Kools)
                {
                    indexOfHouseWhereKoolsAreSmoked = i;
                }
            }
            if (indexOfHouseWithTheHorse == -1 || indexOfHouseWhereKoolsAreSmoked == -1)
            {
                throw new Exception("No house in which man who smokes Kools or has horse lives in set up");
            }

            return Math.Abs(indexOfHouseWithTheHorse - indexOfHouseWhereKoolsAreSmoked) == 1;
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
                if (item.houses[i].nationality == Nationality.Norwegian)
                {
                    indexOfHouseOfNorwegian = i;
                }
            }
            if (indexOfHouseOfNorwegian == -1 || indexOfHouseOfNorwegian == -1)
            {
                throw new Exception("No house in which Norwegian lives or blue house in set up");
            }

            return Math.Abs(indexOfHouseOfNorwegian - indexOfHouseOfNorwegian) == 1;
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
