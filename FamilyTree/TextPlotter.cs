using Svg;
using System.Drawing;

namespace FamilyTree
{
	public static class TextPlotter
	{
		public static void PlotText(SvgDocument document, PointF centre, Person model, int numberOfGenerations)
		{
			// Main Person
			plotMainPerson(document, centre, model);

			// Generation 1
			plotInnerPerson(document, centre, 75, -Constants.OuterAngle, 0, model.Father);
			plotInnerPerson(document, centre, 75, 0, Constants.OuterAngle, model.Mother);

			// Generation 2
			plotInnerPerson(document, centre, 125, -Constants.OuterAngle, -Constants.OuterAngle / 2.0f, model.Father.Father);
			plotInnerPerson(document, centre, 125, -Constants.OuterAngle / 2.0f, 0, model.Father.Mother);
			plotInnerPerson(document, centre, 125, 0, Constants.OuterAngle / 2.0f, model.Mother.Father);
			plotInnerPerson(document, centre, 125, Constants.OuterAngle /2.0f, Constants.OuterAngle, model.Mother.Mother);
		}

		private static void plotInnerPerson(SvgDocument document, PointF centre, float radius, float innerAngle, float outerAngle, Person person)
		{
			document.Children.Add(ArcCreator.CreateArcText(	centre, 
															radius + Constants.MainFontSize,
															innerAngle,
															outerAngle, 
															person.Name, 
															Color.Black, 
															Constants.MainFontSize));
			document.Children.Add(ArcCreator.CreateArcText(	centre, 
															radius,
															innerAngle,
															outerAngle, 
															person.FamilyName, 
															Color.Black, 
															Constants.MainFontSize));
			document.Children.Add(ArcCreator.CreateArcText(	centre, 
															radius - Constants.MainFontSize,
															innerAngle,
															outerAngle, 
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
