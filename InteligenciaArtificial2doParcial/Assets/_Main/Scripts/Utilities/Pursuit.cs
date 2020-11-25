using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : ISteeringBehaviours
{
    private Transform _target;
    private Transform _npc;
    private Rigidbody _rbTarget;
    private float _timePrediction;
    public Pursuit(Transform npc, Transform target, Rigidbody rbTarget, float timePrediction)
    {
        _timePrediction = timePrediction;
        _rbTarget = rbTarget;
        _npc = npc;
        _target = target;
    }
    public Vector3 GetDir()
    {
        var vel = _rbTarget.velocity.magnitude;
        Vector3 posPrediction = _target.position + _target.forward * vel * _timePrediction;
        Vector3 dir = (posPrediction - _npc.position).normalized;
        return dir;
    }
}
