using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : ISteeringBehaviours
{
    private Transform _target;
    private Transform _npc;
    private float _radius;
    private LayerMask _mask;
    private float _avoidWeight;

    public ObstacleAvoidance(Transform target, Transform npc, float radius, LayerMask mask, float avoidWeight)
    {
        _target = target;
        _npc = npc;
        _radius = radius;
        _mask = mask;
        _avoidWeight = avoidWeight;
    }
    public Vector3 GetDir()
    {
        var obstacles = Physics.OverlapSphere(_npc.position,_radius,_mask);
        Transform obsSaved = null;
        var count = obstacles.Length;
        for (int i = 0; i < count; i++)
        {
            var currentObs = obstacles[i].transform;
            if (obsSaved == null)
            {
                obsSaved = currentObs;
            }
            else if (Vector3.Distance(_npc.position,obsSaved.position) > Vector3.Distance(_npc.position, currentObs.position))
            {
                obsSaved = currentObs;
            }
        }
        var dirToTarget=(_target.position - _npc.position).normalized;
        if (obsSaved != null)
        {
            var dirObsToNpc = (_npc.position - obsSaved.position).normalized * _avoidWeight;
            dirToTarget += dirObsToNpc;
        }
        return dirToTarget.normalized;
    }
}
