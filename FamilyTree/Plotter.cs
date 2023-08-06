using FamilyTree.Configuration;
using Svg;
using System.Drawing;
using System.Linq;

namespace FamilyTree
{
	public static class Plotter
	{
		public static SvgDocument Plot(Person model, IConfiguration configuration)
		{
			var outerRadius = configuration.GetCreateRadii().Last() + 20;
			var size = new SizeF(2 * outerRadius, 2 * outerRadius);
			var document = new SvgDocument();
			document.Width = size.Width;
			document.Height = size.Height;
			document.Ppi = 96;

			var centre = new PointF(outerRadius, outerRadius);

			RayPlotter.PlotRays(document, centre, configuration);
			CirclePlotter.PlotCircles(document, centre, 3.0f, configuration);
			TextPlotter.PlotText(document, centre, model, configuration);

			return document;
		}
	}
}
