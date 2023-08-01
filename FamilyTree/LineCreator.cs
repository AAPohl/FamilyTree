using Svg;
using Svg.Pathing;
using System;
using System.Drawing;

namespace FamilyTree
{
	public static class LineCreator
	{
		public static SvgElement CreateLine(PointF centre, float startRadius, float endRadius, float angle, Color color)
		{
			PointF p1 = new PointF
			{
				X = centre.X + startRadius * MathF.Sin(angle * MathF.PI / 180.0f),
				Y = centre.Y - startRadius * MathF.Cos(angle * MathF.PI / 180.0f)
			};
			PointF p2 = new PointF
			{
				X = centre.X + endRadius * MathF.Sin(angle * MathF.PI / 180.0f),
				Y = centre.Y - endRadius * MathF.Cos(angle * MathF.PI / 180.0f)
			};

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
