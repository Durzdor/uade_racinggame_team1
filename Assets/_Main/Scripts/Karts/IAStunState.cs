using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAStunState<T> : FSMState<T> 
{
    private FSM<T> _fsm;
    private IAKart _iaKart;
    private T _driveInput;

    public IAStunState(FSM<T> fsm, T driveInput, IAKart iaKart)
    {
        _fsm = fsm;
        _iaKart = iaKart;
        _driveInput = driveInput;
    }

    public override void Awake()
    {
        _iaKart.Move(Vector3.zero, 0f);
    }

    public override void Execute()
    {
        while (_iaKart.stunDuration > 0f)
        {
            if (_iaKart.stunDuration == 0f) break;
            _iaKart.stunDuration -= Time.deltaTime;
        }
        _fsm.Transition(_driveInput);
    }
}
