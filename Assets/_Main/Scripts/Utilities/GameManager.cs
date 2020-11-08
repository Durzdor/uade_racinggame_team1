using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool dontDestroyOnLoad;
    public static GameManager Instance;
    
    public Transform lastCheckpointInLap;
    public CarController[] allCars;
    public CarController[] carOrder;
    
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

        LoadStats();
    }

    private void LoadStats()
    {
        Debug.Log("Gamemanager test");
        
    }

    private void Start()
    {
        //Sets up the car objects
        carOrder = new CarController[allCars.Length];
        InvokeRepeating(nameof(ManualUpdate), 0.5f, 0.5f);
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
        Debug.Log($"{carOrder[0]} esta primero");
    }
}