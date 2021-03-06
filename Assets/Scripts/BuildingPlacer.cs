using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    private bool currentlyPlacing;
    private BuildingPreset curBuildingPreset;

    private float placementIndicatorUpdateRate = 0.05f;
    private float lastUpdateTime;
    private float rotateSpeed = 500f;
    private Vector3 curPlacementPos;
    private GameObject placementIndicator;
    public GameObject placementIndicatorFarm;
    public GameObject placementIndicatorHouse;
    public GameObject placementIndicatorRoad;
    public GameObject placementIndicatorFactory;
    public static BuildingPlacer inst;


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
        if (Input.GetKeyDown(KeyCode.Escape)){

            CancelBuildingPlacement();
        }
        if (Input.GetKeyDown(KeyCode.R)){
            transform.rotation *= Quaternion.Euler(0, 90 * rotateSpeed * 0.002f, 0);
        }       
        if (Time.time - lastUpdateTime > placementIndicatorUpdateRate && currentlyPlacing)
        {
            lastUpdateTime = Time.time;

            curPlacementPos = Selector.inst.GetCurTilePosition();
            placementIndicator.transform.position = curPlacementPos;
        }

        if (currentlyPlacing && Input.GetMouseButtonDown(0))
        {
            PlaceBuilding();
        }
    }

    public void BeginNewBuildingPlacement(BuildingPreset buildingPreset)
    {
        if (City.inst.money < buildingPreset.cost){
            return;
        }
        transform.rotation = new Quaternion(0, 0, 0, 0);
        switch (buildingPreset.displayName)
        {
            case "Farm":
                placementIndicator = placementIndicatorFarm;
                break;
            case "House":
                placementIndicator = placementIndicatorHouse;
                break;
            case "Road":
                placementIndicator = placementIndicatorRoad;
                break;
            case "Factory":
                placementIndicator = placementIndicatorFactory;
                break;
        }    
        
        curBuildingPreset = buildingPreset;
        currentlyPlacing = true;
        placementIndicator.SetActive(true);
    }
    
public void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
    }
    
void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curPlacementPos, transform.rotation);
        City.inst.OnPlaceBuilding(curBuildingPreset);

        CancelBuildingPlacement();
    }
}
