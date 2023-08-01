using Svg;
using Svg.Pathing;
using System;
using System.Drawing;

namespace FamilyTree
{
	public static class ArcCreator
	{
		public static SvgElement CreateArc(PointF centre, float radius, float startAngle, float endAngle, Color color)
		{
			PointF p1 = new PointF
			{
				X = centre.X + radius * MathF.Sin(startAngle * MathF.PI / 180.0f),
				Y = centre.Y - radius * MathF.Cos(startAngle * MathF.PI / 180.0f)
			};
			PointF p2 = new PointF
			{
				X = centre.X + radius * MathF.Sin(endAngle * MathF.PI / 180.0f),
				Y = centre.Y - radius * MathF.Cos(endAngle * MathF.PI / 180.0f)
			};

			var totalAngle = endAngle - startAngle;
			SvgPathSegmentList ls = new SvgPathSegmentList();
			ls.Add(new SvgMoveToSegment(false, p1));
			ls.Add(new SvgArcSegment(radius, radius, totalAngle, totalAngle < 180.0 ? SvgArcSize.Small : SvgArcSize.Large, SvgArcSweep.Positive, false, p2));

			var path = new SvgPath
			{
				Stroke = new SvgColourServer(color),
				PathData = ls,
				StrokeWidth = Constants.StrokeWidth,
				Fill = new SvgColourServer(Color.Empty)
			};

			return path;
		}
	}
}
