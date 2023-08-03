using Svg;
using Svg.Pathing;
using Svg.Transforms;
using System.Drawing;

namespace FamilyTree
{
	public static class RotatedLineCreator
	{
		public static SvgElement CreateRotatedText(PointF centre, float radius, float angle, string text, Color color, float fontSize)
		{			
			PointF textCentre = MathHelper.CreatePoint(centre, radius, angle);

			var svgText = new SvgText()
			{
				FontSize = fontSize,
				TextAnchor = SvgTextAnchor.Middle,
				X = new SvgUnitCollection() { 0 },
				Y = new SvgUnitCollection() { 0 },
				Fill = new SvgColourServer(color),
				Text = text,
				Font = Constants.Font,
				Transforms = new SvgTransformCollection() {new SvgTranslate(textCentre.X, textCentre.Y), new SvgRotate(adjustAngle(angle)) }
			};

			return svgText;
		}

		public static SvgElement CreateRotatedLine(PointF centre, float startRadius, float endRadius, float angle, Color color)
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

		private static float adjustAngle(float angle)
		{
			if (angle < 0)
				return angle + 90;
			else
				return angle - 90;
		}
	}
}
