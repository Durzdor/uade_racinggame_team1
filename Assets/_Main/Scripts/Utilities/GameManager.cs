using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool dontDestroyOnLoad;
    public static GameManager Instance;
    
    public List<Transform> waypoints;

    public Transform lastCheckpointInLap;
    public CarController[] allCars;
    public CarController[] carOrder;

    public Text firstPlace;
    public Text secondPlace;
    public Text thirdPlace;
    public Text fourthPlace;
    public Text fifthPlace;
    public Text sixthPlace;

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
    }

    private void Start()
    {
        //Sets up the car objects
        carOrder = new CarController[allCars.Length];
        InvokeRepeating(nameof(ManualUpdate), 0f, 0.1f);
        InvokeRepeating(nameof(DisplayPositions), 3f, 0.1f);
    }
    
    private void ManualUpdate()
    {
        foreach (CarController car in allCars)
        {
            carOrder[car.GetCarPosition(allCars) - 1] = car;
        }
    }

    private void Update()
    {
        
    }

    private void DisplayPositions()
    {
        var firPlace = carOrder[0].name;
        var secPlace = carOrder[1].name;
        var thrPlace = carOrder[2].name;
        var forPlace = carOrder[3].name;
        var fifPlace = carOrder[4].name;
        var sixPlace = carOrder[5].name;

        firstPlace.text = firPlace;
        secondPlace.text = secPlace;
        thirdPlace.text = thrPlace;
        fourthPlace.text = forPlace;
        fifthPlace.text = fifPlace;
        sixthPlace.text = sixPlace;
    }
}