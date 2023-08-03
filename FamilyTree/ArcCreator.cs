using Svg;
using Svg.Pathing;
using System;
using System.Drawing;

namespace FamilyTree
{
	public static class ArcCreator
	{
		public static SvgElement CreateCircle(PointF centre, float radius, Color color, float lineWidth)
		{
			var circle = new SvgCircle();
			circle.CenterX = centre.X;
			circle.CenterY = centre.Y;
			circle.Radius = radius;
			circle.Stroke = new SvgColourServer(color);
			circle.StrokeWidth = lineWidth;
			circle.Fill = new SvgColourServer(Color.Empty);

			return circle;
		}
		public static SvgElement CreateArcText(PointF centre, float radius, float startAngle, float endAngle, string text, Color color, float fontSize)
		{
			var path = createArcPath(centre, radius, startAngle, endAngle);
			var arcLength = 2.0f * MathF.PI * radius * (endAngle - startAngle) / (360.0f)  / 2.0f;
			var svgText = new SvgText()
			{
				FontSize = fontSize,
				TextAnchor = SvgTextAnchor.Middle,
				Dy = new SvgUnitCollection() { fontSize / 3},
				X = new SvgUnitCollection() { arcLength},
				Fill = new SvgColourServer(color)
			};

			var guid = Guid.NewGuid().ToString();
			Uri.TryCreate($"#{guid}", UriKind.Relative, out var uri);
			var textPath = new SvgTextPath()
			{
				Text = text,
				ReferencedPath = uri,
				Font = Constants.Font

			};
			path.ID = guid;
			svgText.Children.Add(path);
			svgText.Children.Add(textPath);

			return svgText;
		}

		public static SvgElement CreateArcBorder(PointF centre, float radius, float startAngle, float endAngle, Color color, float lineWidth)
		{
			var path = createArcPath(centre, radius, startAngle, endAngle);
			path.Stroke = new SvgColourServer(color);
			path.StrokeWidth = lineWidth;
			path.Fill = new SvgColourServer(Color.Empty);

			return path;
		}

		private static SvgPath createArcPath(PointF centre, float radius, float startAngle, float endAngle)
		{
			PointF p1 = MathHelper.CreatePoint(centre, radius, startAngle);
			PointF p2 = MathHelper.CreatePoint(centre, radius, endAngle);

			var totalAngle = endAngle - startAngle;
			SvgPathSegmentList ls = new SvgPathSegmentList();
			ls.Add(new SvgMoveToSegment(false, p1));
			ls.Add(new SvgArcSegment(radius, radius, totalAngle, totalAngle < 180.0 ? SvgArcSize.Small : SvgArcSize.Large, SvgArcSweep.Positive, false, p2));

			var path = new SvgPath
			{
				PathData = ls,
			};

			return path;
		}
	}
}
