public interface ILivingCreatureVisitor {
    void Visit(LivingCreatureConfig companent);
    void Visit(AnimalConfig animal);
    void Visit(PlantConfig plant);
}
