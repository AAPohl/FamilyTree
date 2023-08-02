using Svg;
using System.Drawing;
using System.Linq;

namespace FamilyTree
{
	public static class RayPlotter
	{
		public static void PlotRays(SvgDocument document, PointF centre, int numberOfGenerations)
		{
			var radii = MathHelper.GetCreateRadii(numberOfGenerations).ToArray();
			for (int j = 1; j < numberOfGenerations; ++j)
			{ 
				var angles = MathHelper.GetCreateAngles(j).ToArray();
				for (int i = 0; i < angles.Length; ++i)
				{
					document.Children.Add(LineCreator.CreateLine(centre, radii[j-1], radii[j], angles[i], Color.FromArgb(255, 219, 219, 217)));
				}
			}
		}
	}
}
