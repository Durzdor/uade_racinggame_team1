using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 dir)
    {
        dir.y = 0;
        _rb.velocity = dir * speed;
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.9f);
    }
}
