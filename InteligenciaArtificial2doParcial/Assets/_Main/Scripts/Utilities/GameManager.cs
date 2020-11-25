using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool dontDestroyOnLoad;
    public static GameManager Instance;
    
    public List<Transform> waypoints;
    public Transform lastCheckpointInLap;
    public CarController[] allCars;
    public CarController[] carOrder;
    public int maxLaps = 3;
    
    public float startupCountdown = 5f;
    public AudioSource startSound;

    public Text[] endRaceStats;
    public Text[] currentPositions;
    public GameObject endGameStatDisplay;
    public GameObject playerUI;

    private Dictionary<int, string> finalPositionTable;
    private List<string> kartNames;
    private bool playing;
    private bool raceOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        Cursor.visible = false;
        finalPositionTable = new Dictionary<int, string>();
        kartNames = new List<string>();
        endGameStatDisplay.SetActive(false);
        playerUI.SetActive(true);
    }

    private void Start()
    {
        //Sets up the car objects
        carOrder = new CarController[allCars.Length];
        InvokeRepeating(nameof(ManualUpdate), 0f, 0.1f);
        InvokeRepeating(nameof(DisplayPositions), 3f, 0.1f);
    }

    private void Update()
    {
        StartRaceTimer();
    }

    private void ManualUpdate()
    {
        foreach (CarController car in allCars)
        {
            carOrder[car.GetCarPosition(allCars) - 1] = car;
        }
    }

    // Display current positions
    private void DisplayPositions()
    {
        for (int i = 0; i < 6; i++)
        {
            foreach (var car in carOrder)
            {
                currentPositions[i].text = carOrder[i].name;
            }
        }
    }

    // Save the values of the race
    public void AfterRaceStats(int pos, string time, string name)
    {
        // Add each value to a dictionary
        finalPositionTable.Add(pos,time);
        kartNames.Insert(pos-1,name);
        // When the dictionary saves every car stats
        if (finalPositionTable.ContainsKey(allCars.Length))
        {
            // Update all endgame 
            AfterRaceDisplayStats();
        }
    }

    private void AfterRaceDisplayStats()
    {
        // Hide the player UI
        playerUI.SetActive(false);
        // Show end game stats
        endGameStatDisplay.SetActive(true);
        // Show the cursor
        Cursor.visible = true;
        // Stop the timer
        GameTimer.intance.EndTimer();
        // Check the dictionary for the values to display
        foreach (var stat in finalPositionTable)
        {
            endRaceStats[stat.Key - 1].text = $"{stat.Key}: {kartNames[stat.Key - 1]} - {stat.Value}";
        }
    }

    // Timer to start the race
    private void StartRaceTimer()
    {
        if (playing) return;
        if (raceOver) return;
        if (!startSound.isPlaying)
        {
            startSound.Play();
        }
        // If it is higher than 0 subtract
        if (startupCountdown > 0)
        {
            startupCountdown -= Time.deltaTime;
        }
        // If it reaches 0 or lower start the race
        if (startupCountdown <= 0)
        {
            // Check every car and let them start
            foreach (var car in allCars)
            {
                var ia = car.GetComponent<IAKart>();
                var player = car.GetComponent<KartPlayer>();
                // Check ia cars
                if (ia != null)
                {
                    ia.onRace = true;
                }
                // Check player car
                else if (player != null)
                {
                    player.blockInput = false;
                }
            }
            // Start the global timer
            GameTimer.intance.BeginTimer();
            playing = true;
        }
    }
    
    // Go back to main menu
    public void EndRace()
    {
        SceneManager.LoadScene("MainMenu");
    }
}