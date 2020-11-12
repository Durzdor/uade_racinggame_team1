using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : Power
{
    private GameObject cloneShieldPrefab;
    
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private float shieldDuration = 2f;

    public override void Awake()
    {
        base.Awake();
        audioSource = shieldPrefab.GetComponent<AudioSource>();
    }

    public override void Execute()
    {
        // Spawns shield at the specified location
        cloneShieldPrefab = Instantiate(shieldPrefab,parent.transform.position,Quaternion.Euler(-90,0,0 ),parent.transform);
        // Sound effect
        if (name == "MainPlayer")
        {
            audioSource.Play();
        }
        // Destroys shield after the duration
        Object.Destroy(cloneShieldPrefab, shieldDuration);
        // Turns off the object
        this.enabled = false;
    }

    private void Update()
    {
        //If not enabled stops
        if (!this.enabled) return;
        Execute();
    }
}