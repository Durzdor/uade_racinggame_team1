using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class IAKartController : MonoBehaviour
{
    private IAKart _iaKart;
    private FSM<string> _fsm;
    private INode _init;

    private void Awake()
    {
        _iaKart = GetComponent<IAKart>();
    }

    private void Start()
    {
        FiniteStateMachineInitialization();
    }
    
    // Finite state machine initialization
    private void FiniteStateMachineInitialization()
    {
        // Loads the finite state machine
        _fsm = new FSM<string>();
        // Adds each state to the finite state machine
        IADriveState<string> iaDrive = new IADriveState<string>(_fsm,"Stun","Stop",_iaKart);
        IAStunState<string> iaStun = new IAStunState<string>(_fsm,"Drive",_iaKart);
        IAStopState<string> iaStop = new IAStopState<string>(_fsm,"Drive",_iaKart);
        // Adds each transition between states of the finite state machine
        iaDrive.AddTransition("Stun", iaStun);
        iaDrive.AddTransition("Stop", iaStop);
        iaStun.AddTransition("Drive",iaDrive);
        iaStop.AddTransition("Drive", iaDrive);
        // Sets the initial state of the finite state machine
        _fsm.SetInit(iaStop); 
    }
    
    public void Update()
    {
        // Updates the finite state machine
        _fsm.OnUpdate();
        // Updates the decision tree
        DecisionTreeUpdate();
        _init.Execute();
    }

    private void DecisionTreeUpdate()
    {
        ActionNode usePower = new ActionNode(_iaKart.UsePower);
        ActionNode stopTree = new ActionNode(_iaKart.StopTree);
        ActionNode useDetector = new ActionNode(_iaKart.UseDetector);
        QuestionNode secondPosition = new QuestionNode(_iaKart.SecondPosition, useDetector, usePower);
        QuestionNode firstPosition = new QuestionNode(_iaKart.FirstPosition, stopTree, usePower);
        QuestionNode missilePower = new QuestionNode(_iaKart.MissilePower, firstPosition, secondPosition);
        QuestionNode hasPower = new QuestionNode(_iaKart.HasPower, missilePower, stopTree);

        // Selects the initial node
        _init = hasPower;
    }
}