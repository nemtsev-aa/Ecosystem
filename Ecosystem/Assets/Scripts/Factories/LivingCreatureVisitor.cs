using System.Collections.Generic;
using System.Linq;

public class LivingCreatureVisitor : ILivingCreatureVisitor {
    private List<LivingCreature> _livingCreature = new List<LivingCreature>();

    public LivingCreatureVisitor(List<LivingCreature> livingCreature) {
        _livingCreature = livingCreature;
    }

    public LivingCreature LivingCreature { get; private set; }

    public void Visit(LivingCreatureConfig livingCreature)
        => Visit((dynamic)livingCreature);

    public void Visit(AnimalConfig animal)
        => LivingCreature = _livingCreature.FirstOrDefault(livingCreature => livingCreature is Animal);

    public void Visit(PlantConfig plant)
        => LivingCreature = _livingCreature.FirstOrDefault(livingCreature => livingCreature is Plant);
}
