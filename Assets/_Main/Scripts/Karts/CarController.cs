using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public int currentWaypoint;
    public int currentLap;
    public Transform lastWaypoint;
    public int nbWaypoint; //Set the amount of Waypoints
    
    private int cpt_waypoint = 0;

    private void Start()
    {
        Initialize();
    }

    // Use this for initialization
    private void Initialize()
    {
        currentWaypoint = 0;
        currentLap = 0;
        cpt_waypoint = 0;
        lastWaypoint = GameManager.Instance.lastCheckpointInLap;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemBox")) return;
        string otherTag = other.gameObject.tag;
        currentWaypoint = System.Convert.ToInt32(otherTag);
        if (currentWaypoint == 1 && cpt_waypoint == nbWaypoint)
        {
            // completed a lap, so increase currentLap;
            currentLap++;
            cpt_waypoint = 0;
        }

        if (cpt_waypoint == currentWaypoint - 1)
        {
            lastWaypoint = other.transform;
            cpt_waypoint++;
        }
    }

    private float GetDistance()
    {
        return (transform.position - lastWaypoint.position).magnitude;
    }

    public int GetCarPosition(CarController[] allCars)
    {
        float distance = GetDistance();
        int position = 1;
        int lap = currentLap;
        int waypoint = currentWaypoint;
        foreach (CarController car in allCars)
        {
            if (car.currentLap > lap)
            {
                position++;
                continue;
            }
            if (car.currentWaypoint > waypoint && car.currentLap == lap)
            {
                position++;
                continue;
            }
            if (car.GetDistance() > distance && car.currentWaypoint == waypoint && car.currentLap == lap)
            {
                position++;
            }
        }
        return position;
    }
}