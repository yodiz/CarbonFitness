using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
	public class Ingredient : Entity {
		public virtual string Name { get; set; }

		public virtual decimal WeightInG { get; set; }
		public virtual decimal FibresInG { get; set; }

		public virtual decimal EnergyInKJ { get; set; }

		public virtual decimal EnergyInKcal { get; set; }

		public virtual decimal CarbonHydrateInG { get; set; }

		public virtual decimal FatInG { get; set; }

		public virtual decimal DVitaminInµG { get; set; }

		public virtual decimal AscorbicAcidInmG { get; set; }

		public virtual decimal IronInmG { get; set; }

		public virtual decimal CalciumInmG { get; set; }

		public virtual decimal NatriumInmG { get; set; }

		public virtual decimal ProteinInG { get; set; }

		public virtual decimal AlcoholInG { get; set; }

		public virtual decimal SaturatedFatInG { get; set; }

		public virtual decimal MonoUnSaturatedFatInG { get; set; }

		public virtual decimal PolyUnSaturatedFatInG { get; set; }

		public virtual decimal FatAcidC4C10InG { get; set; }

		public virtual decimal FatAcidC12InG { get; set; }

		public virtual decimal FatAcidC14InG { get; set; }

		public virtual decimal FatAcidC16InG { get; set; }

		public virtual decimal FatAcidC18InG { get; set; }

		public virtual decimal FatAcidC20InG { get; set; }

		public virtual decimal FatAcidC16_1InG { get; set; }

		public virtual decimal FatAcidC18_1InG { get; set; }

		public virtual decimal FatAcidC18_2InG { get; set; }

		public virtual decimal FatAcidC18_3InG { get; set; }

		public virtual decimal FatAcidC20_4InG { get; set; }

		public virtual decimal FatAcidC20_5InG { get; set; }

		public virtual decimal FatAcidC22_5InG { get; set; }

		public virtual decimal FatAcidC22_6InG { get; set; }

		public virtual decimal MonosaccharidesInG { get; set; }

		public virtual decimal DisaccharidesInG { get; set; }

		public virtual decimal SucroseInG { get; set; }

		public virtual decimal RetinolEquivalentInµG { get; set; }

		public virtual decimal RetinolInµG { get; set; }

		public virtual decimal EVitaminInmG { get; set; }

		public virtual decimal TokopherolInmG { get; set; }

		public virtual decimal CaroteneInµG { get; set; }

		public virtual decimal ThiamineInmG { get; set; }

		public virtual decimal RiboflavinInmG { get; set; }

		public virtual decimal NiacinEquivalentInmG { get; set; }

		public virtual decimal NiacinInmG { get; set; }

		public virtual decimal VitaminB6InmG { get; set; }

		public virtual decimal VitaminB12InµG { get; set; }

		public virtual decimal FolicAcidInµG { get; set; }

		public virtual decimal PhosphorusInmG { get; set; }

		public virtual decimal PotassiumInmG { get; set; }

		public virtual decimal MagnesiumInmG { get; set; }

		public virtual decimal SeleniumInµG { get; set; }

		public virtual decimal ZincInmG { get; set; }

		public virtual decimal CholesteroleInmG { get; set; }

		public virtual decimal AshInG { get; set; }

		public virtual decimal AquaInG { get; set; }

		public virtual decimal WasteInPercent { get; set; }
	}
}