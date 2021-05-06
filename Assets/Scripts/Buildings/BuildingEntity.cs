using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEntity : MonoBehaviour
{
    // Describes building
    [SerializeField]
    private string buildingName;
    [SerializeField]
    private string buildingID;
    [SerializeField]
    private double buildingCost;
    [SerializeField]
    private string buildingDescription;         // probably unused

    // Atributes of building
    private Vector2 gridPosition;
    private float scale;
    [SerializeField]
    private double heatStorage;
    private double buildingDurability;          // probably unused, may use later


    public void Setup(Vector2 gridPos, Vector3 screenPos, float scale, double heat)             // Creates building on the grid, saves its main atributes and adds it to table of active buildings
    {
        this.gridPosition = gridPos;
        transform.position = screenPos;
        transform.localScale = new Vector3(scale, scale, 1);
        this.scale = scale;
        this.heatStorage = heat;
        BuildingsTileList.Instance.PlaceBuilding(gridPosition, this.gameObject);
    }

    public void Remove()                                                                        // Destroys building and removes it from table of active buildings
    {
        BuildingsTileList.Instance.RemoveBuilding(gridPosition);
        Destroy(this.gameObject);
    }

    public double ExportHeat(double demand)                                                     // Requests building to give "demand" amount of heat to requster, will return demand or heatStorage if demand is higher than stored heat
    {
        double heatExported = Math.Min(demand, heatStorage);
        this.heatStorage -= heatExported;
        return heatExported;
    }

    private void OnMouseOver()                                                                  // Sells building on right click, returns 80% of its cost
    {
        if (Input.GetMouseButtonDown(1))
        {
            IdleReactov.Instance.power += this.buildingCost * 0.8;
            Remove();
        }
    }

    // Returns of building specific info 
    public string BuildingName()
    {
        return this.buildingName;
    }
    public string BuildingID()
    {
        return this.buildingID;
    }
    public double BuildingCost()
    {
        return this.buildingCost;
    }
    public string BuildingDescription()
    {
        return this.buildingDescription;
    }
    public Vector2 GridPosition()
    {
        return this.gridPosition;
    }
    public float Scale()
    {
        return this.scale;
    }
    public double HeatStorage()
    {
        return this.heatStorage;
    }
    public void HeatStorage(double amount)
    {
        this.heatStorage += amount;
    }
    public double BuildingDurability()
    {
        return this.buildingDurability;
    }
    public void BuildingDurability(double amount)
    {
        this.buildingDurability += amount;
    }
}
