using FamilyTree.Configuration;
using Svg;
using System;
using System.Drawing;
using System.Linq;

namespace FamilyTree
{
	public static class TextPlotter
	{
		public static void PlotText(SvgDocument document, PointF centre, Person model, IConfiguration configuration)
		{
			var radii = configuration.GetCreateRadii().Intersect().Compute(i => (i.start + i.end) / 2.0f).ToArray();

			// Main Person (Generation 1)
			plotMainPerson(document, centre, model, configuration);

			// Generation 2 .. 3
			for (int i = 2; i < 4; ++i)
			{
				var angleSets_N = MathHelper.GetCreateAngles(i).Intersect().ToArray();
				var personSet_N = MathHelper.GetPersonsOfLevel(model, i).ToArray();
				for (int j = 0; j < angleSets_N.Length; ++j)
					plotInnerPerson(document, centre, radii[i-2], angleSets_N[j], personSet_N[j], i, configuration);
			}

			// Generation 4 .. n
			for (int i = 3; i < configuration.GetNumberOfGenerations(); ++i)
			{
				var angleSet_N = MathHelper.GetCreateAngles(i+1).Intersect().Compute(i => (i.start + i.end) / 2.0f).ToArray();
				var personSet_N = MathHelper.GetPersonsOfLevel(model, i+1).ToArray();
				for (int j = 0; j < angleSet_N.Length; ++j)
					plotOuterPerson(document, centre, radii[i - 1], angleSet_N[j], personSet_N[j], i, configuration);
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

		private static void plotOuterPerson(SvgDocument document, PointF centre, float radius, float angle, Person person, int currentGeneration, IConfiguration configuration)
		{
			if (person == Person.NoPerson)
				return;

			var angles = Array.Empty<float>();
			var textInfos = configuration.GetTextInfo(person, currentGeneration).ToArray();

			switch (textInfos.Length)
			{
				case 1:
					angles = new[] { angle };
					break;
				case 2:
					var deltaAngle = (textInfos[0].FontSize + textInfos[1].FontSize) / (4.0f * MathF.PI * radius) * 360.0f;
					if (angle < 0)
						deltaAngle = -deltaAngle;
					angles = new[] { angle - deltaAngle / 2.0f, angle + deltaAngle / 2.0f };
					break;
				case 3:
					var deltaAngle_1 = (textInfos[0].FontSize + textInfos[1].FontSize) / (4.0f * MathF.PI * radius) * 360.0f;
					var deltaAngle_2 = (textInfos[1].FontSize + textInfos[2].FontSize) / (4.0f * MathF.PI * radius) * 360.0f;
					if (angle < 0)
					{
						deltaAngle_1 = -deltaAngle_1;
						deltaAngle_2 = -deltaAngle_2;
					}
					angles = new[] { angle - deltaAngle_1, angle, angle + deltaAngle_2};
					break;
				default:
					throw new NotImplementedException();
			}

			foreach (var textLine in angles.Select((a, i) => (angle: a, textInfo: textInfos[i])))
			{
				document.Children.Add(RotatedLineCreator.CreateRotatedText(
														centre,
														radius,
														textLine.angle,
														textLine.textInfo.Text,
														textLine.textInfo.Color,
														textLine.textInfo.FontSize));
			}
			
		}

		private static void plotInnerPerson(SvgDocument document, PointF centre, float radius, (float innerAngle, float outerAngle) angle, Person person, int currentGeneration, IConfiguration configuration)
		{
			if (person == Person.NoPerson)
				return;

			var textInfos = configuration.GetTextInfo(person, currentGeneration).ToArray();
			if (textInfos.Length != 3)
				throw new NotImplementedException();

			var radii = new[] { radius + textInfos[0].FontSize / 2.0f + textInfos[1].FontSize / 2.0f,
								radius,
								radius - textInfos[1].FontSize / 2.0f - textInfos[2].FontSize / 2.0f};

			foreach (var textLine in radii.Select((r, i) => (radius: r, textInfo: textInfos[i])))
			{
				document.Children.Add(ArcCreator.CreateArcText(centre,
															textLine.radius,
															angle.innerAngle,
															angle.outerAngle,
															textLine.textInfo.Text,
															textLine.textInfo.Color,
															textLine.textInfo.FontSize));
			}
		}

		private static void plotMainPerson(SvgDocument document, PointF centre, Person person, IConfiguration configuration)
		{
			if (person == Person.NoPerson)
				return;

			var textInfos = configuration.GetTextInfo(person, 1).ToArray();
			if (textInfos.Length != 3)
				throw new NotImplementedException();

			var points = new[] { PointF.Subtract(centre, new SizeF(0, textInfos[0].FontSize / 2.0f + textInfos[1].FontSize / 2.0f)),
								 centre,
								 PointF.Add(centre, new SizeF(0, textInfos[1].FontSize / 2.0f + textInfos[2].FontSize / 2.0f))};

			foreach(var textLine in points.Select((p, i) => (point: p, textInfo: textInfos[i])))
			{
				document.Children.Add(TextCreator.CreateHorizontalText(textLine.point,
																	textLine.textInfo.Text,
																	textLine.textInfo.Color,
																	textLine.textInfo.FontSize));
			}
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
