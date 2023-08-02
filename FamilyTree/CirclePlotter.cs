using Svg;
using System.Drawing;
using System.Linq;

namespace FamilyTree
{
	public static class CirclePlotter
	{
		public static void PlotCircles(SvgDocument document, PointF centre, int numberOfGenerations)
		{
			var radii = MathHelper.GetCreateRadii(numberOfGenerations).ToArray();
			document.Children.Add(ArcCreator.CreateCircle(centre, radii[0], Color.FromArgb(255, 219, 219, 217)));
			foreach (var radius in radii.Skip(1))
			{
				document.Children.Add(ArcCreator.CreateArcBorder(centre, radius, -Constants.OuterAngle, -Constants.OuterAngle/2.0f, Color.FromArgb(255, 44, 195, 242)));
				document.Children.Add(ArcCreator.CreateArcBorder(centre, radius, -Constants.OuterAngle / 2.0f, 0, Color.FromArgb(255, 116, 158, 56)));
				document.Children.Add(ArcCreator.CreateArcBorder(centre, radius, 0, Constants.OuterAngle / 2.0f, Color.FromArgb(255, 217, 84, 67)));
				document.Children.Add(ArcCreator.CreateArcBorder(centre, radius, Constants.OuterAngle / 2.0f, Constants.OuterAngle, Color.FromArgb(255, 242, 201, 35)));
			}
		}				
	}
}
