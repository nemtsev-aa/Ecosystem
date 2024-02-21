using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalkingState : MovementState {
    private Animal _animal;
    private float _hunger => Parameters.Hunger.Value;
    private float _energy => Parameters.Energy.Value;
    private float _grow => Parameters.Grow.Value;
    private float _age => Parameters.Age.Value / Parameters.Age.MaxValue;

    public WalkingState(LivingCreatureStateMachineData data, Animal animal) : base(data, animal) {
        _animal = animal;
    }

    public override void Enter() {
        base.Enter();
        Debug.Log($"WalkingState: Enter");
    }

    public override void Exit() {
        base.Exit();
        Debug.Log($"WalkingState: Exit");
    }

    public override void Update() {
        base.Update();

        Parameters.AddGrowing();
        Parameters.TakeEnergy();
        Parameters.AddHunger();

        if (_grow == 0f)
            StateSwitcher.SwitchState<GrowingState>();

        if (Parameters.IsMoved == true) {
            MoveToTarget();
            return;
        }

        if (_hunger >= 0.5f) {
            MoveToFood();
            return;
        }

        //Репродуктивный возраст
        if (_age > 0.2f && _age < 0.5f) {
            Parameters.AddDesireReproduction();

            if (_energy >= 0.5f) {
                MoveToReproduction();
                return;
            }
        }

        if (_energy <= 0.2f)
            StateSwitcher.SwitchState<IdlingState>();
        else
            SetRandomTargetPosition();
    }

    private void MoveToFood() {
        if (Data.FoodTarget != null) {
            StateSwitcher.SwitchState<EatingState>();
            return;
        }

        FindTarget(Data.Radius, Parameters.FoodLayer, out LivingCreature target);
        if (target == null)
            SetRandomTargetPosition();
        else {
            Data.FoodTarget = target;
            Data.TargetPosition = target.transform.position;
        }
            
    }

    private void MoveToReproduction() {
        if (Data.ReproductionTarget != null) {
            StateSwitcher.SwitchState<ReproductionState>();
            return;
        }

        FindTarget(Data.Radius * 2f, Parameters.ReproductionLayer, out LivingCreature target);
        if (target == null) 
            SetRandomTargetPosition();
        else 
        {
            Data.ReproductionTarget = target;
            Data.TargetPosition = target.transform.position;

            Animal animal2 = (Animal)target;
            animal2.StateMachineData.ReproductionTarget = _animal;
            animal2.StateMachineData.TargetPosition = _animal.transform.position;
        }   
    }

    private void FindTarget(float radius, LayerMask mask, out LivingCreature target) {
        var foods = Physics2D.OverlapCircleAll(Animal.transform.position, radius, mask).ToList();
        Debug.Log($"FindTarget: {foods.Count}");

        if (foods.Count == 0) {
            target = null;
            return;
        } 
           
        if (foods.Count == 1) {
            LivingCreature food = foods[0].attachedRigidbody.GetComponent<LivingCreature>();

            if (GetHealthValue(food) > 0) 
                target = food;
            else 
                target = null;

            return;
        }

        if (foods.Count > 1) {
            if (mask.value == LayerMask.NameToLayer("Plant")) {
                List<Plant> plants = new List<Plant>();

                foreach (var iFood in foods) {
                    Plant plant = (Plant)iFood.attachedRigidbody.GetComponent<LivingCreature>();
                    plants.Add(plant);
                }

                // Сортировка объектов по здоровью, а затем по дистанции
                plants = plants.OrderByDescending(o => o.Parameters.Health.Value)
                               .ThenBy(o => Vector3.Distance(Animal.transform.position, o.transform.position)).ToList();

                // Возврат первого элемента массива (с наибольшим здоровьем и наименьшей дистанцией)
                target = plants[0];
                return;
            }

            if (mask.value == LayerMask.NameToLayer("Plankton")) {
                List<Animal> animals = new List<Animal>();

                foreach (var iFood in foods) {
                    Animal animal = (Animal)iFood.attachedRigidbody.GetComponent<LivingCreature>();
                    animals.Add(animal);
                }

                // Сортировка объектов по здоровью, а затем по дистанции
                animals = animals.OrderByDescending(o => o.Parameters.Health.Value)
                               .ThenBy(o => Vector3.Distance(Animal.transform.position, o.transform.position)).ToList();

                // Возврат первого элемента массива (с наибольшим здоровьем и наименьшей дистанцией)
                target = animals[0];
                return;
            }
        }

        target = null;
    }

    private float GetHealthValue(LivingCreature livingCreature) {

        if (livingCreature is Plant) {
            var plant = (Plant)livingCreature;

            return plant.Parameters.Health.Value;
        }

        if (livingCreature is Animal) {
            var animal = (Animal)livingCreature;

            return animal.Parameters.Health.Value;
        }

        return 0f;
    }

    private void SetRandomTargetPosition() {
        float x = Mathf.Cos(UnityEngine.Random.Range(0, 360)) * Data.Radius;
        float y = Mathf.Sin(UnityEngine.Random.Range(0, 180)) * Data.Radius;
        Vector2 targetPosition = new Vector2(x, y);

        Data.TargetPosition = targetPosition;
        Parameters.IsMoved = true;
    }
}
