using System.Drawing;

namespace Kate.Algorithms
{
	// 3 geometric figure in the row. Red is on the left of green, square is red,
	// triangle is not next to the circle. Where is the triangle?
	public static class SimpleEinshtainQuizSolver
	{
		public static IEnumerable<SetUp> Solve()
		{
			foreach (SetUp possibleSolution in GenerateAllPossibleSolutions())
			{
				if (IsSolution(possibleSolution))
				{
					yield return possibleSolution;
				}
			}
		}
		private static IEnumerable<T> GetAllCharacteristics<T>()
		{
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

		private static IEnumerable<SetUp> GenerateAllPossibleSolutions()
		{
			foreach (Color color1 in GetAllCharacteristics<Color>())
			{
				foreach (Shape shape1 in GetAllCharacteristics<Shape>())
				{
					foreach (Color color2 in GetAllCharacteristics<Color>())
					{
						if (color1 == color2) continue;
						foreach (Shape shape2 in GetAllCharacteristics<Shape>())
						{
							if (shape1 == shape2) continue;
							foreach (Color color3 in GetAllCharacteristics<Color>())
							{
								if (color2 == color3 || color1 == color3) continue;
								foreach (Shape shape3 in GetAllCharacteristics<Shape>())
								{
									if (shape2 == shape3 || shape1 == shape3) continue;
									yield return new SetUp()
									{
										shapes = new List<GeometricShape>
										{
											new GeometricShape {color = color1, shape = shape1},
											new GeometricShape {color = color2,  shape = shape2},
											new GeometricShape {color = color3,  shape = shape3 }
										}
									};
								}
							}
						}
					}
				}
			}
		}

		private static bool IsSolution(SetUp item)
		{
			return (item.shapes[0].shape == Shape.Triangle
				&& item.shapes[1].shape == Shape.Sqare
				&& item.shapes[2].color == Color.Green
				&& TriangleIsNotNextToCircle(item) 
				&& SquareIsRed(item));
		}

		private static bool SquareIsRed(SetUp item)
		{
			return item.shapes.Single(s => s.shape == Shape.Sqare).color == Color.Red;
		}

		private static bool TriangleIsNotNextToCircle(SetUp item)
		{
			int circleIndex = -1;
			int triangleIndex = -1;
			for (int i = 0; i < item.shapes.Count; i++)
			{
				if (item.shapes[i].shape == Shape.Triangle)
				{
					triangleIndex = i;
				}
				else if (item.shapes[i].shape == Shape.Circle)
				{
					circleIndex = i;
				}
			}
			if(circleIndex == -1 || triangleIndex == -1)
			{
				throw new Exception("No circle or triangle in set up");
			}
			return Math.Abs(circleIndex - triangleIndex) != 1;
		}
	}

	public class SetUp
	{
		public List<GeometricShape> shapes { get; set; }
	}

	public class GeometricShape
	{
		public Color color { get; set; }
		public Shape shape { get; set; }
	}
	public enum Color
	{
		Red, Green, Yellow
	}
	public enum Shape
	{
		Circle, Sqare, Triangle
	}
}
