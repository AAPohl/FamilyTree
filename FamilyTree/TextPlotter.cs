using Svg;
using System;
using System.Drawing;

namespace FamilyTree
{
	public static class TextPlotter
	{
		public static void PlotText(SvgDocument document, PointF centre, Person model, int numberOfGenerations)
		{
			var topLevel = new PointF(centre.X, centre.Y - Constants.MainFontSize);
			var bottomLevel = new PointF(centre.X, centre.Y + Constants.MainFontSize);
			document.Children.Add(TextCreator.CreateText(topLevel, model.Name, Color.Black, Constants.MainFontSize));
			document.Children.Add(TextCreator.CreateText(centre, model.FamilyName, Color.Black, Constants.MainFontSize));
			document.Children.Add(TextCreator.CreateText(bottomLevel, createYearText(model), Color.FromArgb(255, 139, 139, 142), Constants.MainFontSize));
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
