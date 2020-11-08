using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPower : MonoBehaviour
{
    public Transform parent;

    private void Awake()
    {
        parent = gameObject.transform;
    }

    public virtual void Execute() { }
}
