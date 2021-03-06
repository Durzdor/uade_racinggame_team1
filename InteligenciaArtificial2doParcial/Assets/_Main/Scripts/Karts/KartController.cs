﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class KartController : MonoBehaviour
{
    public GameObject rearviewCamera;
    public AudioSource kartSound;
    
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private bool isBraking;
    private bool isPaused;
    private bool isRearview;
    private float currentSpeed;
    private float steeringAngle;
    private bool lockStats;
    
    //How fast it turns
    [SerializeField] private float steeringSpeed = 45f;
    //How fast it accelerates
    [SerializeField] private float baseSpeed = 1f;
    //Max speed going forward
    [SerializeField] private float maxForwardSpeed = 200f;
    //Max speed going in reverse
    [SerializeField] private float maxReverseSpeed = -100f;
    //How fast it brakes
    [SerializeField] [Range(0.01f, 1f)] private float brakingSpeed = 0.5f;
    //How fast it decelerates
    [SerializeField] [Range(0.01f, 1f)] private float decelerationSpeed = 0.01f;

    private KartPlayer _kartPlayer;

    //Gets references
    private void Awake()
    {
        _kartPlayer = GetComponent<KartPlayer>();
    }
    
    //Functions update
    private void Update()
    {
        Pause();
        if (LockInput() != false) return;
        GetInput();
        RearviewCamera();
        SteeringFormula();
        BrakingFormula();
        VelocityFormula();
        if (lockStats != false) return;
        IsPlayerFinished();
    }
    
    //Input detections
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(Horizontal);
        verticalInput = Input.GetAxis(Vertical);
        isBraking = Input.GetKey(KeyCode.Space);
        isRearview = Input.GetKey(KeyCode.R);
    }
    
    //Pause
    private void Pause()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        isPaused = !isPaused;
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    
    //Rearview camera
    private void RearviewCamera()
    {
        if (isRearview)
        {
            rearviewCamera.SetActive(true);
        }
        else
        {
            rearviewCamera.SetActive(false);
        }
    }
    
    //Calculates the steering for the kart
    private void SteeringFormula()
    {
        if (verticalInput != 0 || currentSpeed != 0)
        {
            steeringAngle = horizontalInput * steeringSpeed * Time.deltaTime;
        }
        else
        {
            steeringAngle = 0;
        }
    }

    //Calculates the braking speed
    private void BrakingFormula()
    {
        if (isBraking)
        {
            currentSpeed += brakingSpeed * (Mathf.Sign(currentSpeed) * -1f);
        }
    }

    //Calculates the velocity for the kart
    private void VelocityFormula()
    {
        //Goes forward
        if (verticalInput > 0)
        {
            if (!kartSound.isPlaying)
            {
                kartSound.Play();
            }
            currentSpeed += baseSpeed;
        }
        //Goes in reverse
        else if (verticalInput < 0)
        {
            if (!kartSound.isPlaying)
            {
                kartSound.Play();
            }
            currentSpeed -= baseSpeed;
        }
        //Deceleration
        else
        {
            currentSpeed -= currentSpeed * decelerationSpeed;
            //Condition to stop drifting
            if (currentSpeed < 25f)
            {
                currentSpeed = 0;
                kartSound.Stop();
            }
        }

        //Sets maximum speeds for forward/reverse
        currentSpeed = Mathf.Clamp(currentSpeed, maxReverseSpeed, maxForwardSpeed);
    }
    
    // Checks if the player is input locked
    private bool LockInput()
    {
        var input = _kartPlayer.blockInput;
        return input;
    }

    private void IsPlayerFinished()
    {
        if (_kartPlayer.PlayerCurrentLap() == GameManager.Instance.maxLaps)
        {
            _kartPlayer.PlayerEndStats();
            lockStats = true;
        }
    }
    
    //Resolves unity physics
    private void FixedUpdate()
    {
        //Calls the function to move with the corresponding values
        _kartPlayer.Move(steeringAngle, currentSpeed);
    }
}