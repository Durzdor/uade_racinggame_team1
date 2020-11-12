using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public KartPlayer _kartPlayer;
    public AudioSource itemBoxAudio;

    private IAKart _iaKart;
    
    public enum Powers 
    {
        Shieldpower = 1, 
        Missilepower = 2
    }

    private Roulette _roulette;
    private Dictionary<int, int> _dic;
    private float y0;
    private Vector3 tempPos;
    private MeshRenderer _meshRenderer;
    private BoxCollider _boxCollider;
    
    //How high it goes in Y
    [SerializeField] private float amplitude = 0.5f;
    //How fast it goes from the bottom to the top
    [SerializeField] private float speed = 1f;
    //How fast it rotates in X
    [SerializeField] private float xRotationSpeed = 30f;
    //How fast it rotates in Z
    [SerializeField] private float zRotationSpeed = 25f;
    //How long until it respawns
    [SerializeField] private float respawnTimer = 100f;

    private void Awake()
    {
        y0 = transform.position.y;
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _roulette = new Roulette();
        _dic = new Dictionary<int, int>();
        //Adds shield power with its chance to be picked
        _dic.Add((int)Powers.Shieldpower, 40);
        //Adds missile power with its chance to be picked
        _dic.Add((int)Powers.Missilepower, 60);
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
        if (other.gameObject.CompareTag("Car"))
        {
            //Turns off the mesh and collider to simulate destruction
            _meshRenderer.enabled = false;
            _boxCollider.enabled = false;
            //Respawn in the same space
            Invoke(nameof(Respawn), respawnTimer);
            //Runs the roulette to choose the power
            if (other.name == "MainPlayer")
            {
                //Play ItemBox Sound when it enters collision
                itemBoxAudio.Play();
                _kartPlayer.StorePower(_roulette.Run(_dic));
                return;
            }
            if (other.GetComponent<IAKart>() != null)
            {
                _iaKart = other.GetComponent<IAKart>();
                _iaKart.StorePower(_roulette.Run(_dic));
            }
        }
    }
    
    //Function to show the box again
    private void Respawn()
    { 
        _meshRenderer.enabled = true;
        _boxCollider.enabled = true;
    }
}
