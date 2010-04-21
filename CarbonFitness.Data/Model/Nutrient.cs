using System;
using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
    public class Nutrient : Entity {
        public virtual string Name { get; set; }
        public virtual decimal RDI { get; set; }
    }

    public enum NutrientEntity {
        FibresInG,
		EnergyInKJ,
		EnergyInKcal,
		CarbonHydrateInG,
		FatInG,
		DVitaminIn�G,
		AscorbicAcidInmG,
		IronInmG,
		CalciumInmG,
		NatriumInmG,
		ProteinInG,
		AlcoholInG,
		SaturatedFatInG,
		MonoUnSaturatedFatInG,
		PolyUnSaturatedFatInG,
		FatAcidC4C10InG,
		FatAcidC12InG,
		FatAcidC14InG,
		FatAcidC16InG,
		FatAcidC18InG,
		FatAcidC20InG,
		FatAcidC16_1InG,
		FatAcidC18_1InG,
		FatAcidC18_2InG,
		FatAcidC18_3InG,
		FatAcidC20_4InG,
		FatAcidC20_5InG,
		FatAcidC22_5InG,
		FatAcidC22_6InG,
		MonosaccharidesInG,
		DisaccharidesInG,
		SucroseInG,
		RetinolEquivalentIn�G,
		RetinolIn�G,
		EVitaminInmG,
		TokopherolInmG,
		CaroteneIn�G,
		ThiamineInmG,
		RiboflavinInmG,
		NiacinEquivalentInmG,
		NiacinInmG,
		VitaminB6InmG,
		VitaminB12In�G,
		FolicAcidIn�G,
		PhosphorusInmG,
		PotassiumInmG,
		MagnesiumInmG,
		SeleniumIn�G,
		ZincInmG,
		CholesteroleInmG,
		AshInG,
		AquaInG,
		WasteInPercent
    }
}