using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public abstract class LivingCreatureView : MonoBehaviour {
    [SerializeField] protected Canvas _canvas;
    [SerializeField] protected TextMeshProUGUI _stateNameLabel;
    [SerializeField] protected ParameterView _ageView;
    [SerializeField] protected ParameterView _growView;
    [SerializeField] protected ParameterView _healthView;
    [SerializeField] protected ParameterView _reproductionView;

    protected Animator _animator;
    protected Dictionary<IState, string> _states;
    protected IState _currentState;

    public virtual void Init(LivingCreatureStateMachine stateMachine, LivingCreatureParameters parameters) {
        _animator ??= GetComponent<Animator>();

        stateMachine.StateChanged += SwitchView;

        IReadOnlyList<IState> states = stateMachine.States;
        CreateStatesDictionary(states);

        InitParameterViews(parameters);
    }

    public void ShowCanvas(bool status) {
        _canvas.enabled = status;
    }

    public void SwitchView(IState state) {
        if (_currentState != null)
            ShowView(_currentState, false);

        _currentState = state;
        ShowView(_currentState, true);

        _stateNameLabel.text = _currentState.ToString();
    }

    public abstract void CreateStatesDictionary(IReadOnlyList<IState> states);
    public abstract void InitParameterViews(LivingCreatureParameters parameters);
   
    protected IState FindStateByType<T>(IReadOnlyList<IState> states) {
        return states.FirstOrDefault(state => state is T);
    }

    protected void ShowView(IState state, bool status) {
        if (_states.TryGetValue(state, out string value))
            _animator.SetBool(value, status);
        else
            Debug.LogError($"State {state} the key for switching animations is not assigned");
    }
}
