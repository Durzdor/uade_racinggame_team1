using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : IPower
{
    public GameObject shieldPrefab;
    
    private GameObject cloneShieldPrefab;
    
    [SerializeField] private float shieldDuration = 2f;
    
    public override void Execute()
    {
        //Power
        Debug.Log("test");
        //Spawns shield at the specified location
        cloneShieldPrefab = Instantiate(shieldPrefab,parent.transform.position,Quaternion.identity,parent.transform);
        //Destroys shield after the duration
        Destroy(cloneShieldPrefab, shieldDuration);
    }
}