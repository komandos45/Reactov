using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuildingButton : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private double basePrice;

    private double CalculateFinalPrice()
    {
        return basePrice * 1;           // TUTAJ WSTAW SWOJE CHORE POMYSŁY NA SKALOWANIE KOSZTÓW BUDYNKÓW
    }

    public void OnMouseDown()
    {
        BuildingsTileList.Instance.SetBuildingPrefab(prefab);
        this.basePrice = prefab.GetComponent<BuildingEntity>().BuildingCost();
        BuildingsTileList.Instance.SetBuildingPrice(CalculateFinalPrice());
    }


}
