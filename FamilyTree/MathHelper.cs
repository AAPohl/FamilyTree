using System;
using System.Collections.Generic;
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

		public static IEnumerable<float> GetCreateRadii(int numberOfGenerations)
		{
			var radius = 1.0f * Constants.ScalingFactor;
			yield return radius;

			for (int i = 1; i < numberOfGenerations; ++i)
			{
				radius = radius + (i > 2 ? 2.0f : 1.0f) * Constants.ScalingFactor;
				yield return radius;
			}
		}

		public static IEnumerable<float> GetCreateAngles(int numberOfGenerations)
		{
			var numberOfAngles = (int)Math.Pow(2, numberOfGenerations - 1) + 1;
			var deltaAngle = Constants.OuterAngle * 2.0f / (numberOfAngles - 1);
			for(int i = 0; i < numberOfAngles; ++i)
			{
				yield return -Constants.OuterAngle + i * deltaAngle;
			}

		}
	}
}
