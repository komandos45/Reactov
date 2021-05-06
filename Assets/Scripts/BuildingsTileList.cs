using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BuildingsTileList : Singleton<BuildingsTileList>
{
    // Buildings declarations---
 //   [SerializeField]
    private GameObject buildingPrefab;
    private double priceOfPrefab;

    [SerializeField]
    private GameObject meltdownEffect;
    // Buildings "map", contains list of all avaliable to build objects
    public Dictionary<string, GameObject> buildingsDictionary = new Dictionary<string, GameObject>();
    [SerializeField]
    private GameObject[] prefabList;
    // Buildings placed 
    private GameObject[,] buildings = new GameObject[15,15];

    // Buildings reutrns
    public GameObject BuildingPrefab {get{ return buildingPrefab;}}
    public double PriceOfPrefab { get { return priceOfPrefab; }}
    public void SetBuildingPrefab(GameObject prefab) { this.buildingPrefab = prefab; }
    public void SetBuildingPrice(double price) { this.priceOfPrefab = price; }
    // Meltdown return
    public GameObject MeltdownEffect { get { return meltdownEffect; } }


    // Save and load of grid
    public void Start()
    {
        LoadPrefabs();
        Invoke("Load", 0.1F);
        InvokeRepeating("Save", 0.5F, 2.0F);
    }
    private void Save()
    {
        SaveManager.Instance.Save();
    }
    private void Load()
    {
        SaveManager.Instance.Load();
    }




    // Dictionary -------------------------------------------------------------------------------
    public bool Register(GameObject gameObject)
    {
        if (!buildingsDictionary.ContainsKey(gameObject.GetComponent<BuildingEntity>().BuildingID()))
        {
            buildingsDictionary.Add(gameObject.GetComponent<BuildingEntity>().BuildingID(), gameObject);
            return true;
        }
        return false;
    }

    public GameObject GetBuildingByID(string ID)
    {
        GameObject gameObject = null;
        buildingsDictionary.TryGetValue(ID, out gameObject);
        return gameObject;
    }

    public void LoadPrefabs()
    {
        for (int i = 0; i < prefabList.Length; i++)
        {
            if (prefabList[i] != null && prefabList[i].GetComponent<BuildingEntity>() != null)
            {
                // Debug.Log(prefabList[i].GetComponent<BuildingEntity>().BuildingName());
                Register(prefabList[i]);
            }
        }
    }

    // Dictionary -------------------------------------------------------------------------------



    // Buildings placed functions
    public GameObject[,] GetPlacedBuildings()                                                                       //Returns list of all buildings placed on map
    { 
        return buildings;
    }
    public void PlaceBuilding(Vector2 pos, GameObject building)                                                     //Adds building to holding table
    {
            buildings[(int)pos.x, (int)pos.y] = building; 
    }
    public void RemoveBuilding(Vector2 pos)                                                                         //Removes building from holding table
    {
        buildings[(int)pos.x, (int)pos.y] = null;
    }
    public GameObject GetBuildingAtPosition(Vector2 pos)                                                           //Gets building at grid x,y coordinates
    {
        if (pos.x>0 && pos.x<IdleReactov.Instance.x_size && pos.y>0 && pos.y < IdleReactov.Instance.y_size)
        {
            return buildings[(int)pos.x, (int)pos.y];
        }
        return null;
    }
    public List<GameObject> GetNeighboursBasic(Vector2 pos)                                                        //Gets buildings top, bottom, left and right of target
    {
        List<GameObject> neighbours = new List<GameObject>();
        if (GetBuildingAtPosition(pos + new Vector2(1, 0)) != null) { neighbours.Add(GetBuildingAtPosition(pos + new Vector2(1, 0))); }
        if (GetBuildingAtPosition(pos + new Vector2(0, 1)) != null) { neighbours.Add(GetBuildingAtPosition(pos + new Vector2(0, 1))); }
        if (GetBuildingAtPosition(pos + new Vector2(0, -1)) != null) { neighbours.Add(GetBuildingAtPosition(pos + new Vector2(0, -1))); }
        if (GetBuildingAtPosition(pos + new Vector2(-1, 0)) != null) { neighbours.Add(GetBuildingAtPosition(pos + new Vector2(-1, 0))); }
        return neighbours;
    }

    public List<GameObject> GetNeighboursExtended(Vector2 pos)                                                     //Gets building in 3x3 square around target
    {
        List<GameObject> neighbours = new List<GameObject>();
        for (int lx = -1; lx<=1; lx++)
        {
            for (int ly=-1; ly <= 1; ly++)
            {
                if (GetBuildingAtPosition(pos - new Vector2(lx, ly)) != null && !(lx==0 && ly==0)) 
                {
                    neighbours.Add(GetBuildingAtPosition(pos - new Vector2(lx, ly))); 
                }
            }
        }
        return neighbours;
    }
}
