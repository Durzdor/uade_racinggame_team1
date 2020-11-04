using UnityEngine;

public class KartPlayer : MonoBehaviour
{
    private Rigidbody _rb;
    public IPower storedPower;
   
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(float steering, float speed)
    {
        //Changes the kart rotation
        transform.Rotate(0, steering, 0);
        //Changes the kart velocity
        _rb.velocity = transform.forward * speed + new Vector3(0, _rb.velocity.y, 0);
    }

    private void Update()
    {
        Power();
    }
    
    //Power
    private void Power()
    {
        //Checks to see if a power is available
        if (storedPower == null) return;
        //Activates the stored power
        if (Input.GetKeyDown(KeyCode.V))
        {
            storedPower.Execute();
            storedPower = null;
        }
    }
    
    //Stores the power gotten from the item box
     public void StorePower(IPower power)
     {
         if (storedPower != null) return;
         storedPower = Instantiate(power);
         Debug.Log($"{storedPower}");
     }
}