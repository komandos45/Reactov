using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileEntity : MonoBehaviour
{

    private Vector2 GridPosition;
    private float scale;
    private bool TileEmpty = true;

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount > 0)
        {
            TileEmpty = false;
        } else
        {
            TileEmpty = true;
        }
    }

    public void Setup(Vector2 gridPos, Vector3 screenPos, float scale)
    {
        this.GridPosition = gridPos;
        this.scale = scale;
        transform.position = screenPos;
        transform.localScale = new Vector3 (scale, scale, 1);
    }
    private void OnMouseDown()
    {
        Debug.Log("faza 1");
        if (TileEmpty && BuildingsTileList.Instance.BuildingPrefab != null)
        {
            Debug.Log("faza 2");
            if (IdleReactov.Instance.CheckIfYouAreBroke(BuildingsTileList.Instance.PriceOfPrefab))
            {
                GameObject building = Instantiate(BuildingsTileList.Instance.BuildingPrefab);
                building.GetComponent<BuildingEntity>().Setup(GridPosition, transform.position, scale, 0);
                building.transform.parent = this.transform;
                Debug.Log("faza 3");
            }
        }
    }

    public void PlaceSavedBuildings(string ID, double heat)
    {
        BuildingsTileList.Instance.buildingsDictionary.TryGetValue(ID, out GameObject prefab);
        GameObject building = Instantiate(prefab);
        building.GetComponent<BuildingEntity>().Setup(GridPosition, transform.position, scale, heat);
        building.transform.parent = this.transform;
    }

    public Vector2 GetGridPosition()
    {
        return this.GridPosition;
    }
    
}
