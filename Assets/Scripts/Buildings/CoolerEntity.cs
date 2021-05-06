using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolerEntity : MonoBehaviour
{
    [SerializeField]
    private double heatPullPerSide;
    [SerializeField]
    private double heatDissapation;
    [SerializeField]
    private double heatCapacity;
    [SerializeField]
    private int tier;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Process", 0, IdleReactov.Instance.tickTime);
    }

    void Process()
    {
        int upgrade = 1 + IdleReactov.Instance.coolingUpgradeLevel[tier];
        List<GameObject> neighbours = BuildingsTileList.Instance.GetNeighboursBasic(this.gameObject.GetComponent<BuildingEntity>().GridPosition());
        foreach (var neighbour in neighbours)
        {
            double heatToPull = Math.Min(heatPullPerSide * IdleReactov.Instance.tickTime * upgrade, (heatCapacity - this.gameObject.GetComponent<BuildingEntity>().HeatStorage())*IdleReactov.Instance.tickTime*upgrade);

            if (neighbour.CompareTag("Reactor"))
            {
                this.gameObject.GetComponent<BuildingEntity>().HeatStorage(neighbour.GetComponent<BuildingEntity>().ExportHeat(heatToPull));
            }
            if (neighbour.CompareTag("Conductor"))
            {
                
            }
        }
        this.gameObject.GetComponent<BuildingEntity>().ExportHeat(heatDissapation*upgrade);        
    }

}
