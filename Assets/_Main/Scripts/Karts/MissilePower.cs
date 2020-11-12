using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePower : Power
{
    private GameObject cloneMissilePrefab;
    
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float missileDuration = 10f;

    public override void Awake()
    {
        base.Awake();
        audioSource = missilePrefab.GetComponent<AudioSource>();
    }

    public override void Execute()
    {
        //Spawns shield at the specified location
        cloneMissilePrefab = Instantiate(missilePrefab,parent.transform.position,Quaternion.Euler(90,0,0));
        // Sound effect
        if (name == "MainPlayer")
        {
            audioSource.Play();
        }
        //Destroys shield after the duration
        Object.Destroy(cloneMissilePrefab, missileDuration);
        //Turns off the object
        this.enabled = false;
    }
    private void Update()
    {
        //If not enabled stops
        if (!this.enabled) return;
        Execute();
    }
}
