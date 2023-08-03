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
			var radii = MathHelper.GetCreateRadii(numberOfGenerations).ToArray();

			// Main Person
			plotMainPerson(document, centre, model);

			// Generation 1
			var angleSets_1 = MathHelper.Intersect(MathHelper.GetCreateAngles(2)).ToArray();
			var personSet_1 = MathHelper.GetPersonsOfLevel(model, 2).ToArray();
			plotInnerPersons(document, centre, (radii[1] + radii[0]) / 2.0f, angleSets_1, personSet_1);

			// Generation 2
			var angleSets_2 = MathHelper.Intersect(MathHelper.GetCreateAngles(3)).ToArray();
			var personSet_2 = MathHelper.GetPersonsOfLevel(model, 3).ToArray();
			plotInnerPersons(document, centre, (radii[2] + radii[1]) / 2.0f, angleSets_2, personSet_2);

		}

		private static void plotInnerPersons(SvgDocument document, PointF centre, float radius, (float start, float end)[] angleSets, Person[] personSet)
		{
			for(int i = 0; i <angleSets.Length; ++i)			
				plotInnerPerson(document, centre, radius, angleSets[i], personSet[i]);
			
		}

		private static void plotInnerPerson(SvgDocument document, PointF centre, float radius, (float innerAngle, float outerAngle) angle, Person person)
		{
			document.Children.Add(ArcCreator.CreateArcText(	centre, 
															radius + Constants.MainFontSize,
															angle.innerAngle,
															angle.outerAngle, 
															person.Name, 
															Color.Black, 
															Constants.MainFontSize));
			document.Children.Add(ArcCreator.CreateArcText(	centre, 
															radius,
															angle.innerAngle,
															angle.outerAngle, 
															person.FamilyName, 
															Color.Black, 
															Constants.MainFontSize));
			document.Children.Add(ArcCreator.CreateArcText(	centre, 
															radius - Constants.MainFontSize,
															angle.innerAngle,
															angle.outerAngle, 
															createYearText(person), 
															Color.FromArgb(255, 139, 139, 142), 
															Constants.MainFontSize));
		}

		private static void plotMainPerson(SvgDocument document, PointF centre, Person person)
		{
			document.Children.Add(TextCreator.CreateHorizontalText(new PointF(centre.X, centre.Y - Constants.MainFontSize), 
																	person.Name, 
																	Color.Black, 
																	Constants.MainFontSize));
			document.Children.Add(TextCreator.CreateHorizontalText(	centre, 
																	person.FamilyName,	
																	Color.Black, 
																	Constants.MainFontSize));
			document.Children.Add(TextCreator.CreateHorizontalText(	new PointF(centre.X, centre.Y + Constants.MainFontSize), 
																	createYearText(person), 
																	Color.FromArgb(255, 139, 139, 142), 
																	Constants.MainFontSize));
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
