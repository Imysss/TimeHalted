using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PlaneShop : MonoBehaviour
{
    [SerializeField] private Button redPlaneButton;
    [SerializeField] private Button yellowPlaneButton;
    [SerializeField] private Button greenPlaneButton;

    [SerializeField] private Button exitButton;

    PlaneShopManager planeShopManager;

    private bool isInitialized = false;

    public void Init(PlaneShopManager planeShopManager)
    {
        if (isInitialized) return;

        this.planeShopManager = planeShopManager;

        redPlaneButton = transform.Find("Panel/RedPlane/RedPlaneButton").GetComponent<Button>();
        yellowPlaneButton = transform.Find("Panel/YellowPlane/YellowPlaneButton").GetComponent<Button>();
        greenPlaneButton = transform.Find("Panel/GreenPlane/GreenPlaneButton").GetComponent<Button>();
        exitButton = transform.Find("Panel/ExitButton").GetComponent<Button>();

        redPlaneButton.onClick.AddListener(OnClickRedPlaneButton);
        yellowPlaneButton.onClick.AddListener(OnClickYellowPlaneButton);
        greenPlaneButton.onClick.AddListener(OnClickGreenPlaneButton);
        exitButton.onClick.AddListener(OnClickExitButton);

        isInitialized = true;
    }

    public void OnClickRedPlaneButton()
    {
        planeShopManager.SelectPlane(PlaneType.Red);
        redPlaneButton.interactable = false;
    }

    public void OnClickYellowPlaneButton()
    {
        planeShopManager.SelectPlane(PlaneType.Yellow);
        yellowPlaneButton.interactable = false;
    }

    public void OnClickGreenPlaneButton()
    {
        planeShopManager.SelectPlane(PlaneType.Green);
        greenPlaneButton.interactable = false;
    }

    public void OnClickExitButton()
    {
        gameObject.SetActive(false);
        //강제로 포커스 해제
        EventSystem.current.SetSelectedGameObject(null);
    }
}
