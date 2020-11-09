using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointTest : MonoBehaviour
{
    [SerializeField] private float radius = 75f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
