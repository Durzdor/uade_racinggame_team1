using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IADriveState<T> : FSMState<T>
{
    private FSM<T> _fsm;
    private IAKart _iaKart;
    private T _stunInput;
    private T _stopInput;

    // Waypoint min distance before the next
    [SerializeField] private float distance = 35f;

    // Waypoint offset radius
    [SerializeField] private float radius = 75f;

    // Kart Speed for testing
    [SerializeField] private float speed = 260f;

    // Offset for the waypoints
    private Vector3 offset;

    // Next waypoint in the list
    private int _nextWaypoint;

    // Next waypoint value
    private int _waypointModifier = 1;

    public IADriveState(FSM<T> fsm, T stunInput, T stopInput, IAKart iaKart)
    {
        _fsm = fsm;
        _iaKart = iaKart;
        _stunInput = stunInput;
        _stopInput = stopInput;
    }

    public override void Awake()
    {
        base.Awake();
    }

    //Generates randomness in the route through an offset
    public void Offset()
    {
        // Lock the offset to the current waypoint
        if (offset != Vector3.zero) return;
        offset = Random.insideUnitSphere * radius;
        offset.y = 0;
    }

    public override void Execute()
    {
        //Generates Offset to the waypoint
        Offset();
        // Next waypoint
        Transform point = GameManager.Instance.waypoints[_nextWaypoint];
        // Next waypoint position
        Vector3 pointPosition = point.position + offset;
        pointPosition.y = _iaKart.transform.position.y;
        // Next waypoint direction
        Vector3 dir = pointPosition - _iaKart.transform.position;
        // Next waypoint magnitude
        if (dir.magnitude < distance)
        {
            // What is the next waypoint 
            if (_nextWaypoint + _waypointModifier >= GameManager.Instance.waypoints.Count || _nextWaypoint + _waypointModifier < 0)
            {
                // If the next waypoint is out of the array sets the next waypoint to the first
                _nextWaypoint = -1;
            }

            // Update to the next waypoint
            _nextWaypoint += _waypointModifier;
            // Reset the offset for the next waypoint
            offset = Vector3.zero;
        }

        // Movement call
        _iaKart.Move(dir.normalized, speed);
    }
}