using System.Collections.Generic;
using UnityEditor;
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
    public int maxLaps;

    public Text firstPlace;
    public Text secondPlace;
    public Text thirdPlace;
    public Text fourthPlace;
    public Text fifthPlace;
    public Text sixthPlace;

    private Dictionary<int, string> finalPositionTable;

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

    public void AfterRaceStats(int pos, string time)
    {
        finalPositionTable.Add(pos,time);
        var text = finalPositionTable[1];
        var text2 = finalPositionTable[2];
        var text3 = finalPositionTable[3];
        var text4 = finalPositionTable[4];
        var text5 = finalPositionTable[5];
        var text6 = finalPositionTable[6];
        AfterRaceDisplayStats(text,text2,text3,text4,text5,text6);
    }

    private void AfterRaceDisplayStats(string one,string two,string three,string four,string five,string six)
    {
        
    }

    private void StartRaceTimer()
    {
        foreach (var car in allCars)
        {
            
        }
    }
}