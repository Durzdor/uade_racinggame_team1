using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    public Transform RocketTarget;
    public Rigidbody RocketRgb;

    public float turnSpeed = 1f;
    public float rocketFlySpeed = 10f;

    private Transform rocketLocalTrans;

    private void Start()
    {
        if (!RocketTarget)
        {
            Debug.Log("Bow1");

            rocketLocalTrans = GetComponent<Transform>();

        }

    }

    private void FixedUpdate()
    {
        if (!RocketRgb)
        
            return;
                RocketRgb.velocity = rocketLocalTrans.forward * rocketFlySpeed;

        var rocketTargetRot = Quaternion.LookRotation(RocketTarget.position - rocketLocalTrans.position);
        RocketRgb.MoveRotation(Quaternion.RotateTowards(rocketLocalTrans.rotation, rocketTargetRot, turnSpeed));
        
    }



}
