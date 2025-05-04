using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlaneType
{
    Blue,
    Red,
    Yellow,
    Green,
}
public class PlaneShopManager : MonoBehaviour
{
    public GameObject[] planePrefabs;
    //public GameObject 

    private UIManager uiManager;

    private void Start()
    {
        uiManager = GameManager.Instance.UIManager;
    }

    public void ShowPlaneShopUI(NpcController npc)
    {
        uiManager.SetNpcShop(npc);
        uiManager.ChangeState(UIState.PlaneShop);
    }

    public GameObject GetPlanePrefab(PlaneType type)
    {
        return planePrefabs[(int)type];
    }
}
