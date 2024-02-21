using UnityEngine;

public abstract class MovementState : IState {
    private const float TargetPositionOffset = 0.03f;

    public IStateSwitcher StateSwitcher { get; set; }

    protected readonly LivingCreatureStateMachineData Data;
    protected readonly Animal Animal;
    protected AnimalParameters Parameters;
    
    public MovementState(LivingCreatureStateMachineData data, Animal animal) {
        Data = data;
        Animal = animal;
        Parameters = animal.Parameters;
    }

    protected AnimalView View => Animal.View;

    public virtual void Enter() {
        Animal.IsMoved = true;
    }

    public virtual void Exit() {
        Animal.IsMoved = false;
    }

    public virtual void Update() {
        Parameters.IsMoved = !Check—oming();
    }

    protected void MoveToTarget() {
        MoveTowards();
        LookAt();
    }

    private bool Check—oming() {
        if (Data.TargetPosition == Vector2.zero)
            return true;

        float distantionToTarget = Vector3.Distance(Animal.transform.position, Data.TargetPosition);
        bool checkResult = (distantionToTarget < TargetPositionOffset) ? true : false;

        return checkResult;
    }

    protected void MoveTowards() {
        Animal.transform.position = Vector2.MoveTowards(Animal.transform.position, Data.TargetPosition, Data.Speed);
    }

    protected void LookAt() {
        float angle = AngleBetweenPoints(Animal.transform.position, Data.TargetPosition);
        var targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        Animal.transform.rotation = Quaternion.Slerp(Animal.transform.rotation, targetRotation, Data.Speed * 10f);
    }

    private float AngleBetweenPoints(Vector2 a, Vector2 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}
