﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

		internal static IEnumerable<Person> GetPersonsOfLevel(Person person, int level)
		{
			if (level == 1)
				yield return person;
			else
			{
				if (person.Father == null)
				{
					foreach (var p in GetPersonsOfLevel(Person.NoPerson, level - 1))
						yield return p;
				}
				else
				{
					foreach (var p in GetPersonsOfLevel(person.Father, level - 1))
						yield return p;
				}

				if (person.Mother == null)
				{
					foreach (var p in GetPersonsOfLevel(Person.NoPerson, level - 1))
						yield return p;
				}
				else
				{

					foreach (var p in GetPersonsOfLevel(person.Mother, level - 1))
						yield return p;
				}
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

		public static IEnumerable<(T start, T end)> Intersect<T>(this IEnumerable<T> values)
		{
			var x = values.ToArray();
			for(int i = 1; i < x.Count(); ++i)
			{
				yield return (x[i - 1], x[i]);
			}
		}

		public static IEnumerable<T> Compute<T>(this IEnumerable<(T start, T end)> values, Func<(T start, T end), T> operation)
		{
			var x = values.ToArray();
			for (int i = 0; i < x.Length; ++i)
			{
				yield return operation(x[i]);
			}
		}
	}
}
