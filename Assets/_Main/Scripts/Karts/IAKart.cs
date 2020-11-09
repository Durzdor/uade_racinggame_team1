using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAKart : MonoBehaviour
{
    public LayerMask mask;

    private Rigidbody _rb;

    [SerializeField] private float range;
    [SerializeField] private float angle;
    [SerializeField] private float gravityFallSpeed = 1f;
    [SerializeField] private float gravMin = 1f;
    [SerializeField] private float gravMax = 1f;
    [SerializeField] private float steerSpeed = 1f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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