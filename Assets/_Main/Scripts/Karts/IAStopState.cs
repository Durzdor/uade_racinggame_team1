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
       // Stop current movement
       _iaKart.Move(Vector3.zero, 0f); 
    }

    public override void Execute()
    {
        // If race is completed
        if (_iaKart.raceCompleted)
        {
            // Save time elapsed
            var totalTime = GameTimer.intance.timePlayingStr;
            // Save last position
            var finishPosition = _iaKart.SavePosition();
            // Send info to the game manager
            GameManager.Instance.AfterRaceStats(finishPosition, totalTime);
            //TP a un podio quizas
            _iaKart.raceCompleted = false;
        }
        // Checks for the start of the race
        else if (_iaKart.onRace)
        {
            // Transitions to driving
            _fsm.Transition(_driveInput);
            _iaKart.onRace = false;
        }
    }
}
