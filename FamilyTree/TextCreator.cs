﻿using Svg;
using System.Drawing;

namespace FamilyTree
{
	class TextCreator
	{
		public static SvgElement CreateHorizontalText(PointF centre, string text, Color color, float fontSize)
		{
			var svgText = new SvgText()
			{
				FontSize = fontSize,
				TextAnchor = SvgTextAnchor.Middle,
				X = new SvgUnitCollection() { centre.X },
				Y = new SvgUnitCollection() { centre.Y },
				Fill = new SvgColourServer(color),
				Text = text,
				Font = Constants.Font
			};

			return svgText;
		}
	}
}
