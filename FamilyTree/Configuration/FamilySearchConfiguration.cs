using System.Collections.Generic;
using System.Drawing;

namespace FamilyTree.Configuration
{
	internal class FamilySearchConfiguration : IConfiguration
	{
		private readonly int numberOfGenerations;

		public FamilySearchConfiguration(int numberOfGenerations)
		{
			this.numberOfGenerations = numberOfGenerations;
		}

		public IEnumerable<float> GetCreateRadii()
		{
			var radius = 1.0f * Constants.ScalingFactor;
			yield return radius;

			for (int i = 1; i < numberOfGenerations; ++i)
			{
				if (i < 3)
					radius = radius + 1.0f * Constants.ScalingFactor;
				else
					radius = radius + 2.0f * Constants.ScalingFactor;
				
				yield return radius;
			}
		}

		public int GetNumberOfGenerations() => numberOfGenerations;

		public IEnumerable<TextInfo> GetTextInfo(Person person, int currentGeneration)
		{
			if (currentGeneration < 6)
			{
				yield return new TextInfo(person.Name, Color.Black, 12);
				yield return new TextInfo(person.FamilyName, Color.Black, 12);
				yield return new TextInfo(createYearText(person), Color.FromArgb(255, 139, 139, 142), 12);
			}
			else if (currentGeneration < 7)
			{
				yield return new TextInfo(person.Name, Color.Black, 12);
				yield return new TextInfo(person.FamilyName, Color.Black, 12);
			}
			else
			{
				yield return new TextInfo($"{person.Name} {person.FamilyName}", Color.Black, 12);
			}
		}

		private static string createYearText(Person person)
		{
			if (person.IsAlive)
			{
				return person.BirthYear != null ? $"{person.BirthYear} - Lebend" : "Lebend";
			}
			else
			{
				if (person.DeathYear == null)
					return person.BirthYear != null ? $"{person.BirthYear} - Verstorben" : "Verstorben";
				else
					return person.BirthYear != null ? $"{person.BirthYear} - {person.DeathYear}" : $"Unbekannt - {person.DeathYear}";

			}
		}
	}
}
