using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    private FSMState<T> _current;
    public void SetInit(FSMState<T> init)
    {
        _current = init;
        _current.Awake();
    }
    public void OnUpdate()
    {
        if (_current != null)
        {
            _current.Execute();
        }
    }
    public void Transition(T input)
    {
        FSMState<T> newState = _current.GetTransition(input);
        if (newState == null) return;
        _current.Sleep();
        _current = newState;
        _current.Awake();
    }
}
