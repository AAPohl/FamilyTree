using System.Collections.Generic;

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

	}
}
