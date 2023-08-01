using System;
using System.Drawing;

namespace FamilyTree
{
	public static class MathHelper
	{
		public static PointF CreatePoint(PointF centre, float radius, float angle)
		{
			return new PointF
			{
				X = centre.X + radius * MathF.Sin(angle * MathF.PI / 180.0f),
				Y = centre.Y - radius * MathF.Cos(angle * MathF.PI / 180.0f)
			};
		}
	}
}
