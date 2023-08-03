using Svg;
using System;
using System.Drawing;
using System.Linq;

namespace FamilyTree
{
	public static class TextPlotter
	{
		public static void PlotText(SvgDocument document, PointF centre, Person model, int numberOfGenerations)
		{
			var radii = MathHelper.GetCreateRadii(numberOfGenerations).Intersect().Compute(i => (i.start + i.end) / 2.0f).ToArray();

			// Main Person
			plotMainPerson(document, centre, model, 12);

			// Generation 1
			var angleSets_1 = MathHelper.GetCreateAngles(2).Intersect().ToArray();
			var personSet_1 = MathHelper.GetPersonsOfLevel(model, 2).ToArray();
			plotInnerPersons(document, centre, radii[0], angleSets_1, personSet_1, 12);

			// Generation 2
			var angleSets_2 = MathHelper.GetCreateAngles(3).Intersect().ToArray();
			var personSet_2 = MathHelper.GetPersonsOfLevel(model, 3).ToArray();
			plotInnerPersons(document, centre, radii[1], angleSets_2, personSet_2, 12);

			// Generation 3 .. n
			for (int i = 3; i < numberOfGenerations; ++i)
			{
				var angleSet_N = MathHelper.GetCreateAngles(i+1).Intersect().Compute(i => (i.start + i.end) / 2.0f).ToArray();
				var personSet_N = MathHelper.GetPersonsOfLevel(model, i+1).ToArray();
				plotOuterPersons(document, centre, radii[i-1], angleSet_N, personSet_N, i, getFontForLevel(i));
			}
		}

		private static float getFontForLevel(int currentGeneration)
		{
			if(currentGeneration < 8)
				return 12.0f;
			else if (currentGeneration == 8)
				return 9.0f;
			else if (currentGeneration == 9)
				return 6.0f;
			return 3.0f;
		}

		private static void plotOuterPersons(SvgDocument document, PointF centre, float radius, float[] angleSets, Person[] personSet, int currentGeneration, float fontSize)
		{
			for (int i = 0; i < angleSets.Length; ++i)
				plotOuterPerson(document, centre, radius, angleSets[i], personSet[i], currentGeneration, fontSize);

		}

		private static void plotOuterPerson(SvgDocument document, PointF centre, float radius, float angle, Person person, int currentGeneration, float fontSize)
		{
			// x / ( 2 * pi * r) = deltaAngle / 360
			var deltaAngle = fontSize / (2.0f * MathF.PI * radius) * 360.0f;
			if (angle < 0)
				deltaAngle = -deltaAngle;

			if (currentGeneration < 6)
			{
				document.Children.Add(RotatedLineCreator.CreateRotatedText(
																centre,
																radius,
																angle - deltaAngle,
																person.Name,
																Color.Black,
																fontSize));
				document.Children.Add(RotatedLineCreator.CreateRotatedText(
																centre,
																radius,
																angle,
																person.FamilyName,
																Color.Black,
																fontSize));
				document.Children.Add(RotatedLineCreator.CreateRotatedText(
																centre,
																radius,
																angle + deltaAngle,
																createYearText(person),
																Color.FromArgb(255, 139, 139, 142),
																fontSize));
			}
			else if (currentGeneration < 7)
			{
				document.Children.Add(RotatedLineCreator.CreateRotatedText(
																centre,
																radius,
																angle - deltaAngle /2.0f,
																person.Name,
																Color.Black,
																fontSize));
				document.Children.Add(RotatedLineCreator.CreateRotatedText(
																centre,
																radius,
																angle + deltaAngle / 2.0f,
																person.FamilyName,
																Color.Black,
																fontSize));
			}
			else
			{
				var name = $"{person.Name.Remove(1,person.Name.Length - 1)}.{person.FamilyName}";
				document.Children.Add(RotatedLineCreator.CreateRotatedText(
																centre,
																radius,
																angle,
																name,
																Color.Black,
																fontSize));
			}
		}

		private static void plotInnerPersons(SvgDocument document, PointF centre, float radius, (float start, float end)[] angleSets, Person[] personSet, float fontSize)
		{
			for(int i = 0; i <angleSets.Length; ++i)			
				plotInnerPerson(document, centre, radius, angleSets[i], personSet[i], fontSize);
			
		}

		private static void plotInnerPerson(SvgDocument document, PointF centre, float radius, (float innerAngle, float outerAngle) angle, Person person, float fontSize)
		{
			document.Children.Add(ArcCreator.CreateArcText(	centre, 
															radius + fontSize,
															angle.innerAngle,
															angle.outerAngle, 
															person.Name, 
															Color.Black, 
															fontSize));
			document.Children.Add(ArcCreator.CreateArcText(	centre, 
															radius,
															angle.innerAngle,
															angle.outerAngle, 
															person.FamilyName, 
															Color.Black,
															fontSize));
			document.Children.Add(ArcCreator.CreateArcText(	centre, 
															radius - fontSize,
															angle.innerAngle,
															angle.outerAngle, 
															createYearText(person), 
															Color.FromArgb(255, 139, 139, 142),
															fontSize));
		}

		private static void plotMainPerson(SvgDocument document, PointF centre, Person person, float fontSize)
		{
			document.Children.Add(TextCreator.CreateHorizontalText(new PointF(centre.X, centre.Y - fontSize), 
																	person.Name, 
																	Color.Black,
																	fontSize));
			document.Children.Add(TextCreator.CreateHorizontalText(	centre, 
																	person.FamilyName,	
																	Color.Black,
																	fontSize));
			document.Children.Add(TextCreator.CreateHorizontalText(	new PointF(centre.X, centre.Y + fontSize), 
																	createYearText(person), 
																	Color.FromArgb(255, 139, 139, 142),
																	fontSize));
		}

		private static string createYearText(Person person)
		{
			if(person.IsAlive)
				return $"{person.BirthYear} - Lebend";
			else
				return $"{person.BirthYear} - {person.DeathYear}";
		}
	}
}
