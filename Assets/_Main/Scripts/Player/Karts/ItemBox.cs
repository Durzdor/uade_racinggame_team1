using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public KartPlayer _kartPlayer;
    public IPower _iPower;
    public ShieldPower shieldPower;
    public MissilePower missilePower;
    public MaxSpeedPower maxSpeedPower;
    
    private Roulette _roulette;
    private Dictionary<IPower, int> _dic;
    private float y0;
    private Vector3 tempPos;
    
    //How high it goes in Y
    [SerializeField] private float amplitude = 0.5f;
    //How fast it goes from the bottom to the top
    [SerializeField] private float speed = 1f;
    //How fast it rotates in X
    [SerializeField] private float xRotationSpeed = 30f;
    //How fast it rotates in Z
    [SerializeField] private float zRotationSpeed = 25f;

    private void Awake()
    {
        _iPower = GetComponent<IPower>();
        shieldPower = GetComponent<ShieldPower>();
        missilePower = GetComponent<MissilePower>();
        maxSpeedPower = GetComponent<MaxSpeedPower>();
        y0 = transform.position.y;
    }

    private void Start()
    {
        _roulette = new Roulette();
        _dic = new Dictionary<IPower, int>();
        //Adds shield power with its chance to be picked
        _dic.Add(shieldPower, 400);
        //Adds missile power with its chance to be picked
        _dic.Add(missilePower, 60);
        //Adds maxspeed power with its chance to be picked
        _dic.Add(maxSpeedPower, 33);
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //Floating up and down
        tempPos = transform.position;
        tempPos.y = y0 + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;
        //Rotating in X and Z
        transform.Rotate(xRotationSpeed * Time.deltaTime,0,zRotationSpeed* Time.deltaTime);
    }

    //If this object collides with another
    private void OnTriggerEnter(Collider other)
    {
        //Checks if it has the corresponding tag
        if (other.gameObject.CompareTag("Player"))
        {
            //Runs the roulette to choose the power
            _kartPlayer.StorePower(_roulette.Run(_dic));
            _iPower.parent = other.gameObject.transform;
            Destroy(this);
        }
    }
}
