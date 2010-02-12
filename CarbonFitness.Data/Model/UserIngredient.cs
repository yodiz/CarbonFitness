using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model
{
	public class UserIngredient : Entity
	{
		public virtual User User { get; set; }
		public virtual Ingredient Ingredient { get; set; }
		public virtual int Measure { get; set; }
	}
}