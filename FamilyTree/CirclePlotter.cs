using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FamilyTree
{
	public static class CirclePlotter
	{
		public static void PlotCircles(SvgDocument document, PointF centre, int numberOfGenerations)
		{
			var radii = getCreateRadii(numberOfGenerations).ToArray();
			document.Children.Add(ArcCreator.CreateCircle(centre, radii[0], Color.Black));
			foreach (var radius in radii.Skip(1))
			{
				document.Children.Add(ArcCreator.CreateArcBorder(centre, radius, -Constants.OuterAngle, Constants.OuterAngle, Color.Black));
			}
		}

		private static IEnumerable<float> getCreateRadii(int numberOfGenerations)
		{
			var radius = 1.0f * Constants.ScalingFactor;
			yield return radius;

			for(int i = 1; i < numberOfGenerations + 1; ++i)
			{
				radius = radius + (i > 2 ? 2.0f : 1.0f) * Constants.ScalingFactor;
				yield return radius;
			}
		}
	}
}
