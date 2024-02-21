public interface IState {
    public IStateSwitcher StateSwitcher { get; set; }
    void Enter();
    void Exit();
    void Update();
}
