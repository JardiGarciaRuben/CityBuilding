using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MonoBehaviour
{ 
    
    public int money;
    public int day;
    public int curPopulation;
    public int curJobs;
    public int curFood;
    public int maxPopulation;
    public int maxJobs;
    public int incomePerJob;
    public TextMeshProUGUI statsText;

    private List<BuildingPreset> buildings = new List<BuildingPreset>();

public static City inst;

    void Awake()
    {
        inst = this;
    }

// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPlaceBuilding(BuildingPreset building)
    {
        maxPopulation += building.population;
        maxJobs += building.jobs;
        buildings.Add(building);
    }

    void CalculateMoney()
    {
        money += curJobs * incomePerJob;

        foreach (BuildingPreset building in buildings)
            money -= building.costPerTurn;
    }

    void CalculatePopulation()
    {
        maxPopulation = 0;

        foreach (BuildingPreset building in buildings)
            maxPopulation += building.population;

        if (curFood >= curPopulation && curPopulation < maxPopulation)
        {
            curFood -= curPopulation / 4;
            curPopulation = Mathf.Min(curPopulation + (curFood / 4), maxPopulation);
        }
        else if (curFood < curPopulation)
        {
            curPopulation = curFood;
        }
    }

    void CalculateJobs()
    {
        curJobs = 0;
        maxJobs = 0;

        foreach (BuildingPreset building in buildings)
            maxJobs += building.jobs;

        curJobs = Mathf.Min(curPopulation, maxJobs);
    }
    void CalculateFood()
    {
        curFood = 0;

        foreach (BuildingPreset building in buildings)
            curFood += building.food;
    }

    public void EndTurn()
    {
        day++;
        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();

        statsText.text = string.Format("Day: {0}   Money: ${1}   Pop: {2} / {3}   Jobs: {4} / {5}   Food: {6}", new object[7] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFood });
    }
}