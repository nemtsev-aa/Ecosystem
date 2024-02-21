using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(LivingCreatureFactory), menuName = "Factories/" + nameof(LivingCreatureFactory))]
public class LivingCreatureFactory : ScriptableObject {
    private List<LivingCreature> _livingCreaturesPrefabs;

    private LivingCreatureVisitor _visitor;

    public void Init(LivingCreaturePrefabs livingCreatures) {
        _livingCreaturesPrefabs = livingCreatures.Prefabs;
    }

    private LivingCreature Companent => _visitor.LivingCreature;

    public T Get<T>(LivingCreatureConfig config, Transform parent) where T : LivingCreature {
        _visitor = new LivingCreatureVisitor(_livingCreaturesPrefabs);
        _visitor.Visit(config);

        var newCompanent = Instantiate(Companent, parent);
        return (T)newCompanent;
    }
}
