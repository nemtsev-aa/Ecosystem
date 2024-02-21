using System;
using System.Collections.Generic;
using System.Linq;

public class LivingCreatureStateMachine : IStateSwitcher {
    public event Action<IState> StateChanged;
    
    private List<IState> _states;
    private IState _currentState;

    public IReadOnlyList<IState> States => _states;

    public LivingCreatureStateMachine(LivingCreature livingCreature, List<IState> states) {
        SetStates(states);

        _currentState = _states[0];
        _currentState.Enter();
        StateChanged?.Invoke(_currentState);
    }

    public void SwitchState<T>() where T : IState {
        IState state = _states.FirstOrDefault(state => state is T);

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();

        StateChanged?.Invoke(_currentState);
    }

    public void Update() {
        _currentState.Update();
    } 

    private void SetStates(List<IState> states) {
        _states = states;

        foreach (var iState in _states) {
            iState.StateSwitcher = this;
        }
    }
}
