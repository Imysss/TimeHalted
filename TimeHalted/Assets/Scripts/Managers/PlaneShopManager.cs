using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlaneType
{
    Red,
    Yellow,
    Green,
}
public class PlaneShopManager : MonoBehaviour
{
    public GameObject[] planePrefabs;

    public void SelectPlane(PlaneType type)
    {
        Debug.Log("select plane");
        SelectedPlaneData.selectedPlanePrefab = planePrefabs[(int)type];
    }
}
