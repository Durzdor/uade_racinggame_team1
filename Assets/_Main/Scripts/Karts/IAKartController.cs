using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class IAKartController : MonoBehaviour
{
    private IAKart _ia;

    // Waypoint min distance before the next
    [SerializeField] private float distance = 0.1f;
    // Waypoint offset radius
    [SerializeField] private float radius = 75f;
    // Kart Speed for testing
    [SerializeField] private float speed = 75f;

    private Vector3 offset;
    private int _nextWaypoint;
    private int _waypointModifier = 1;

    private void Awake()
    {
        _ia = GetComponent<IAKart>();
    }

    public void Update()
    {
        Offset();
        RaceWaypoints();
    }

    //Generates randomness in the route through an offset
    private void Offset()
    {
        // Lock the offset to the current waypoint
        if (offset != Vector3.zero) return;
        offset = Random.insideUnitSphere * radius;
        offset.y = 0;
    }

    private void RaceWaypoints()
    {
        // Next waypoint
        Transform point = GameManager.Instance.waypoints[_nextWaypoint];
        // Next waypoint position
        Vector3 pointPosition = point.position + offset;
        pointPosition.y = _ia.transform.position.y;
        // Next waypoint direction
        Vector3 dir = pointPosition - _ia.transform.position;
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
        _ia.Move(dir.normalized,speed);
    }
}