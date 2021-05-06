using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorEntity : MonoBehaviour
{
    [SerializeField]
    private double powerProduction;
    [SerializeField]
    private double powerStorage;
    [SerializeField]
    private double powerCapacity;
    [SerializeField]
    private double heatProduction;
    [SerializeField]
    private double heatDissapation;
    [SerializeField]
    private double meltdownTreshold;
    [SerializeField]
    private int tier;

    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Process", 0, IdleReactov.Instance.tickTime);
        powerStorage = 0;
    }

    void Process()
    {
        int productionMultiplier = ProcessNeighbours();
        double upgrade = 1;
        upgrade += IdleReactov.Instance.productionUpgradeLevel[this.tier];

        
        if (IdleReactov.Instance.useConduits)
        {
            if ((powerCapacity - powerStorage) > powerProduction * IdleReactov.Instance.tickTime * productionMultiplier * upgrade)
            {
                powerStorage += powerProduction * IdleReactov.Instance.tickTime * productionMultiplier * (float)upgrade;
            }
            else
            {
                powerStorage = powerCapacity;
            }
        }
        else
        {
            IdleReactov.Instance.AddPower(powerProduction * IdleReactov.Instance.tickTime * productionMultiplier * upgrade, this.tier);
        }

        if (this.gameObject.GetComponent<BuildingEntity>().HeatStorage() > meltdownTreshold)         // Perform BOOM
        {
            GameObject meltdownEffect = Instantiate(BuildingsTileList.Instance.MeltdownEffect);
            meltdownEffect.GetComponent<MeltdownEntity>().Setup(transform.position, this.gameObject.GetComponent<BuildingEntity>().Scale());
            this.gameObject.GetComponent<BuildingEntity>().Remove();
        }

        this.gameObject.GetComponent<BuildingEntity>().HeatStorage(heatProduction * IdleReactov.Instance.tickTime * productionMultiplier * upgrade- heatDissapation * IdleReactov.Instance.tickTime);
    }

    int ProcessNeighbours()
    {
        int productionMultiplier = 1;

        List<GameObject> neighbours = BuildingsTileList.Instance.GetNeighboursBasic(this.gameObject.GetComponent<BuildingEntity>().GridPosition());
        if (neighbours != null)
        {
            for (int i = 0; i < neighbours.Count; i++)
            {
                if (neighbours[i].CompareTag("Reactor") || neighbours[i].CompareTag("Reflector"))
                {
                    productionMultiplier += 1;
                }
            }
        }
        return productionMultiplier;
    }

    public double MeltdownTreshold()
    {
        return meltdownTreshold;
    }

    public float HowHotAmI()
    {
        return (float)this.gameObject.GetComponent<BuildingEntity>().HeatStorage() / (float)this.meltdownTreshold;
    }
}
