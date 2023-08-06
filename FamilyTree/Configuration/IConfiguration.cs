using System.Collections.Generic;

namespace FamilyTree.Configuration
{
	public interface IConfiguration
	{
		int GetNumberOfGenerations();
		IEnumerable<float> GetCreateRadii();

	}
}
