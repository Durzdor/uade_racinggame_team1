using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAKart : MonoBehaviour
{
    public LayerMask mask;
    public ShieldPower shieldPower;
    public MissilePower missilePower;

    private Rigidbody _rb;
    private CarController _carController;
    private int storedPower = 0;

    [SerializeField] private float range;
    [SerializeField] private float angle;
    [SerializeField] private float gravityFallSpeed = 1f;
    [SerializeField] private float gravMin = 1f;
    [SerializeField] private float gravMax = 1f;
    [SerializeField] private float steerSpeed = 1f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _carController = GetComponent<CarController>();
    }

    private void Start()
    {
        shieldPower.enabled = false;
        missilePower.enabled = false;
    }

    public void Move(Vector3 dir, float speed)
    {
        //Changes the kart rotation
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.1f);
        //Changes the kart velocity 
        Vector3 newVelocity = transform.forward * speed + new Vector3(0, _rb.velocity.y, 0);
        //Changes the velocity in Y axis 
        newVelocity.y -= Time.deltaTime * -Physics.gravity.y * gravityFallSpeed;
        newVelocity.y = Mathf.Clamp(newVelocity.y, gravMin, gravMax);
        _rb.velocity = newVelocity;
    }
    
    //Stores the power gotten from the item box
    public void StorePower(int power)
    {
        if (storedPower != 0) return;
        storedPower = power;
    }

    public bool HasPower()
    {
        // Variable to Return
        bool hasPower;
        // If stored power is different from 0, true
        if (storedPower != 0)
        {
            hasPower = true;
        }
        // If stored power equals 0, false
        else hasPower = false;
        // Return value
        return hasPower;
    }

    public bool MissilePower()
    {
        // Variable to return
        bool missilePowerBool;
        // If stored power equals missile power, true
        if (storedPower == (int) ItemBox.Powers.Missilepower)
        {
            missilePowerBool = true;
        }
        // If not, false
        else missilePowerBool = false;
        // Return value
        return missilePowerBool;
    }

    public bool FirstPosition()
    {
        // Variable to return
        bool firstPosition;
        // If position equals 1st place, true
        if (_carController.GetCarPosition(GameManager.Instance.allCars) == 1)
        {
            firstPosition = true;
        }
        // If position is different from 1st place, false
        else firstPosition = false;
        // Return value
        return firstPosition;
    }

    public bool SecondPosition()
    {
        // Variable to return
        bool secondPosition;
        // If position is 2nd or higher, true
        if (_carController.GetCarPosition(GameManager.Instance.allCars) >= 2)
        {
            secondPosition = true;
        }
        // If position is lower than 2nd, false
        else secondPosition = false;
        // Return value
        return secondPosition;
    }

    public void UseDetector()
    {
        Debug.Log("UseDetector");
    }

    public void StopTree()
    {
        Debug.Log("StopTree");
    }

    public void UsePower()
    {
        // Use the power that is stored
        switch (storedPower)
        {
            case (int) ItemBox.Powers.Shieldpower:
                shieldPower.enabled = true;
                break;
            case (int) ItemBox.Powers.Missilepower:
                missilePower.enabled = true;
                break;
        }
        // Changes the current power to 0/null
        storedPower = 0;
    }
    
    public bool IsInSight(Transform target)
    {
        Vector3 diff = (target.position - transform.position);
        float distance = diff.magnitude;
        if (distance > range) return false;
        float angleToTarget = Vector3.Angle(transform.forward, diff.normalized);
        if (angleToTarget > angle / 2) return false;
        if (Physics.Raycast(transform.position, diff.normalized, distance, mask))
        {
            return false;
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * range);
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);
    }
}