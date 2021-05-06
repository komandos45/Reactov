using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public void Save()
    {
        string saveLocation = Application.dataPath + "/save.txt";

        Vector2[] gridPositions = new Vector2[225];
        string[] buildingIDs = new string[225];
        double[] storedHeats = new double[225];
        int buildingsOnBoard = 0;

        for (int x = 0; x<15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                Vector2 pos = new Vector2(x, y);
                if (BuildingsTileList.Instance.GetBuildingAtPosition(pos) != null)
                {
                    gridPositions[buildingsOnBoard] = BuildingsTileList.Instance.GetBuildingAtPosition(pos).GetComponent<BuildingEntity>().GridPosition();
                    buildingIDs[buildingsOnBoard] = BuildingsTileList.Instance.GetBuildingAtPosition(pos).GetComponent<BuildingEntity>().BuildingID();
                    storedHeats[buildingsOnBoard] = BuildingsTileList.Instance.GetBuildingAtPosition(pos).GetComponent<BuildingEntity>().HeatStorage();
                    buildingsOnBoard++;
                    //Debug.Log(objects[objects.Count-1].Item1 + " : " + objects[objects.Count-1].Item2 + " : " + objects[objects.Count-1].Item3);
                }
            }
        }
        SaveObject save = new SaveObject
        {
            gridPosition = gridPositions, 
            buildingID = buildingIDs,
            storedHeat = storedHeats,
            buildingsPlaced = buildingsOnBoard
        };
        string json = JsonUtility.ToJson(save);
        File.WriteAllText(saveLocation, json);
    }

    public void Load()
    {
        string saveLocation = Application.dataPath + "/save.txt";
        if (File.Exists(saveLocation))
        {
            SaveObject save = JsonUtility.FromJson<SaveObject>(File.ReadAllText(saveLocation));
            if (save.gridPosition != null)
            {
                for (int i = 0; i < save.buildingsPlaced; i++)
                {
                    GridManager.Instance.grid[save.gridPosition[i]].GetComponent<TileEntity>().PlaceSavedBuildings(save.buildingID[i], save.storedHeat[i]);
                }
            }
        }
    }


    [Serializable]
    public class SaveObject
    {
        public Vector2[] gridPosition;
        public string[] buildingID;
        public double[] storedHeat;
        public int buildingsPlaced;
    }
}
