using FamilyTree.Configuration;
using Svg;
using System.Drawing;
using System.Linq;

namespace FamilyTree
{
	public static class RayPlotter
	{
		public static void PlotRays(SvgDocument document, PointF centre, IConfiguration configuration)
		{
			var radii = configuration.GetCreateRadii().ToArray();
			for (int j = 1; j < configuration.GetNumberOfGenerations(); ++j)
			{ 
				var angles = MathHelper.GetCreateAngles(j+1).ToArray();
				for (int i = 0; i < angles.Length; ++i)
				{
					document.Children.Add(RotatedLineCreator.CreateRotatedLine(centre, radii[j-1], radii[j], angles[i], Color.FromArgb(255, 219, 219, 217),  getLineWidth(j)));
				}
			}
		}

		private static float getLineWidth(int currentGeneration)
		{
			if (currentGeneration < 8)
				return 3.0f;
			else if (currentGeneration == 8)
				return 2.0f;
			else if (currentGeneration == 9)
				return 1.0f;
			return 0.5f;
		}
	}
}
