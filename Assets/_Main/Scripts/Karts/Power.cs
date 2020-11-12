using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public Transform parent;
    public AudioSource audioSource;
    public string name;

    public virtual void Awake()
    {
        parent = gameObject.transform;
        name = parent.name;
    }

    public virtual void Execute() { }
}
