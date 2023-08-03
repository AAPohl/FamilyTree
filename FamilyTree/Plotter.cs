using Svg;
using System.Drawing;
using System.Linq;

namespace FamilyTree
{
	public static class Plotter
	{
		public static SvgDocument Plot(Person model, int numberOfGenerations)
		{
			var outerRadius = MathHelper.GetCreateRadii(numberOfGenerations).Last() + 20;
			var size = new SizeF(2 * outerRadius, 2 * outerRadius);
			var document = new SvgDocument();
			document.Width = size.Width;
			document.Height = size.Height;
			document.Ppi = 96;

			var centre = new PointF(outerRadius, outerRadius);

			RayPlotter.PlotRays(document, centre, numberOfGenerations);
			CirclePlotter.PlotCircles(document, centre, numberOfGenerations);
			TextPlotter.PlotText(document, centre, model, numberOfGenerations);

			return document;
		}
	}
}
