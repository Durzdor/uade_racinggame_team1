using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAStopState<T> : FSMState<T>
{
    private FSM<T> _fsm;
    private IAKart _iaKart;
    private T _driveInput;

    public IAStopState(FSM<T> fsm, T driveInput, IAKart iaKart)
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
        _fsm.Transition(_driveInput);
    }
}
