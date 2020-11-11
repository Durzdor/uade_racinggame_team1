using System;
using UnityEngine;
using UnityEngine.UI;

public class KartPlayer : MonoBehaviour
{
    private Rigidbody _rb;
    private CarController _carController;
    
    public ShieldPower shieldPower;
    public MissilePower missilePower;
    public Text myPosition;
    public Text powerName;

    
    private int storedPower = 0;
    
    [SerializeField] private float gravityFallSpeed = 1f;
    [SerializeField] private float gravMin = 1f;
    [SerializeField] private float gravMax = 1f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _carController = GetComponent<CarController>();
    }

    private void Start()
    {
        shieldPower.enabled = false;
        missilePower.enabled = false;
    }

    public void Move(float steering, float speed)
    {
        //Changes the kart rotation
        transform.Rotate(0, steering, 0);
        //Changes the kart velocity 
        Vector3 newVelocity = transform.forward * speed + new Vector3(0, _rb.velocity.y, 0);
        //Changes the velocity in Y axis 
        newVelocity.y -= Time.deltaTime * -Physics.gravity.y * gravityFallSpeed;
        newVelocity.y = Mathf.Clamp(newVelocity.y, gravMin,gravMax);
        _rb.velocity = newVelocity;
    }

    private void Update()
    {
        Power();
        PositionDisplay();
        DisplayPower();
    }

    //Power use
    private void Power()
    {
        //Checks to see if a power is available
        if (storedPower == 0) return;
        //Activates the stored power
        if (Input.GetKeyDown(KeyCode.V))
        {
            // Use the power that is stored
            switch (storedPower)
            {
                case (int) ItemBox.Powers.Shieldpower:
                    shieldPower.enabled = true;
                    break;
                case (int) ItemBox.Powers.Missilepower:
                    missilePower.enabled = true;
                    break;
            }
            //Changes the current power to 0/null
            storedPower = 0;
        }
    }

    //Stores the power gotten from the item box
    public void StorePower(int power)
    {
        if (storedPower != 0) return;
        storedPower = power;
    }
    
    private void DisplayPower()
    {
        powerName.text = Enum.GetName(typeof(ItemBox.Powers),storedPower);
    }
    
    // Display the current position
    private void PositionDisplay()
    {
        var positionDisplay = _carController.GetCarPosition(GameManager.Instance.allCars);
        string positionString = positionDisplay.ToString();
        myPosition.text = positionString;
    }
}