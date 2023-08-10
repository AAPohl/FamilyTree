using System.Drawing;

namespace FamilyTree.Configuration
{
	public class TextInfo
	{
		public string Text { get; }

		public TextInfo(string text, Color color, float fontSize)
		{
			Text = text;
			Color = color;
			FontSize = fontSize;
		}

		public Color Color { get; }
		public float FontSize { get; }
	}
}
