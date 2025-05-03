using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PlaneShop : UI_Base
{
    [SerializeField] private Button redPlanePerchaseButton;
    [SerializeField] private Button yellowPlanePerchaseButton;
    [SerializeField] private Button greenPlanePerchaseButton;

    [SerializeField] private Button redPlaneEquipButton;
    [SerializeField] private Button yellowPlaneEquipButton;
    [SerializeField] private Button greenPlaneEquipButton;

    [SerializeField] private Button exitButton;


    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        redPlanePerchaseButton = transform.Find("Panel/RedPlane/RedPlanePerchaseButton").GetComponent<Button>();
        yellowPlanePerchaseButton = transform.Find("Panel/YellowPlane/YellowPlanePerchaseButton").GetComponent<Button>();
        greenPlanePerchaseButton = transform.Find("Panel/GreenPlane/GreenPlanePerchaseButton").GetComponent<Button>();

        redPlaneEquipButton = transform.Find("Panel/RedPlane/RedPlaneEquipButton").GetComponent<Button>();
        yellowPlaneEquipButton = transform.Find("Panel/YellowPlane/YellowPlaneEquipButton").GetComponent<Button>();
        greenPlaneEquipButton = transform.Find("Panel/GreenPlane/GreenPlaneEquipButton").GetComponent<Button>();

        exitButton = transform.Find("Panel/ExitButton").GetComponent<Button>();

        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void SetPerchaseButton()
    {

    }

    public void SetEquipButton()
    {

    }

    public void OnClickExitButton()
    {
        gameObject.SetActive(false);
        //강제로 포커스 해제
        EventSystem.current.SetSelectedGameObject(null);
    }

    protected override UIState GetUIState()
    {
        return UIState.PlaneShop;
    }
}
