using Svg;
using System.Drawing;

namespace FamilyTree
{
	public static class LineCreator
	{
		public static SvgElement CreateLine(PointF centre, float startRadius, float endRadius, float angle, Color color)
		{
			PointF p1 = MathHelper.CreatePoint(centre, startRadius, angle);
			PointF p2 = MathHelper.CreatePoint(centre, endRadius, angle);

			var line = new SvgLine();
			line.StartX = p1.X;
			line.StartY = p1.Y;
			line.EndX = p2.X;
			line.EndY = p2.Y;
			line.Stroke = new SvgColourServer(color);
			line.StrokeWidth = Constants.StrokeWidth;

			return line;
		}
	}
}
