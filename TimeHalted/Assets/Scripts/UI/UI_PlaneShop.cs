using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PlaneShop : UI_Base
{
    [SerializeField] private Image npcImage;
    [SerializeField] private TextMeshProUGUI npcNameText;

    [SerializeField] private Button redPlanePurchaseButton;
    [SerializeField] private Button yellowPlanePurchaseButton;
    [SerializeField] private Button greenPlanePurchaseButton;

    [SerializeField] private Button redPlaneEquipButton;
    [SerializeField] private Button yellowPlaneEquipButton;
    [SerializeField] private Button greenPlaneEquipButton;

    [SerializeField] private Button exitButton;

    private GameManager gameManager;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        gameManager = GameManager.Instance;

        npcImage = transform.Find("NPCImage").GetComponent<Image>();
        npcNameText = transform.Find("Panel/NPCName").GetComponent<TextMeshProUGUI>();

        redPlanePurchaseButton = transform.Find("Panel/RedPlane/RedPlanePurchaseButton").GetComponent<Button>();
        yellowPlanePurchaseButton = transform.Find("Panel/YellowPlane/YellowPlanePurchaseButton").GetComponent<Button>();
        greenPlanePurchaseButton = transform.Find("Panel/GreenPlane/GreenPlanePurchaseButton").GetComponent<Button>();

        redPlaneEquipButton = transform.Find("Panel/RedPlane/RedPlaneEquipButton").GetComponent<Button>();
        yellowPlaneEquipButton = transform.Find("Panel/YellowPlane/YellowPlaneEquipButton").GetComponent<Button>();
        greenPlaneEquipButton = transform.Find("Panel/GreenPlane/GreenPlaneEquipButton").GetComponent<Button>();

        exitButton = transform.Find("Panel/ExitButton").GetComponent<Button>();


        redPlanePurchaseButton.onClick.AddListener(() => OnClickPurchaseButton(PlaneType.Red));
        yellowPlanePurchaseButton.onClick.AddListener(() => OnClickPurchaseButton(PlaneType.Yellow));
        greenPlanePurchaseButton.onClick.AddListener(() => OnClickPurchaseButton(PlaneType.Green));

        redPlaneEquipButton.onClick.AddListener(() => OnClickEquipButton(PlaneType.Red));
        yellowPlaneEquipButton.onClick.AddListener(() => OnClickEquipButton(PlaneType.Yellow));
        greenPlaneEquipButton.onClick.AddListener(() => OnClickEquipButton(PlaneType.Green));
        exitButton.onClick.AddListener(OnClickExitButton);

        SetButtonActive();
    }

    public void SetNpc(NpcController npc)
    {
        npcImage.sprite = npc.NpcSprite;
        npcNameText.text = npc.NpcName;
    }

    public void SetButtonActive()
    {
        //구매 버튼 비활성화
        if (gameManager.IsPurchased(PlaneType.Red))
            redPlanePurchaseButton.gameObject.SetActive(false);
        if (gameManager.IsPurchased(PlaneType.Yellow))
            yellowPlanePurchaseButton.gameObject.SetActive(false);
        if (gameManager.IsPurchased(PlaneType.Green))
            greenPlanePurchaseButton.gameObject.SetActive(false);

        //선택 버튼 활성화/비활성화
        if (gameManager.SelectedPlane == PlaneType.Red)
            redPlaneEquipButton.interactable = false;
        else
            redPlaneEquipButton.interactable = true;

        if (gameManager.SelectedPlane == PlaneType.Yellow)
            yellowPlaneEquipButton.interactable = false;
        else
            yellowPlaneEquipButton.interactable = true;

        if (gameManager.SelectedPlane == PlaneType.Green)
            greenPlaneEquipButton.interactable = false;
        else
            greenPlaneEquipButton.interactable = true;
    }

    public void OnClickPurchaseButton(PlaneType type)
    {
        int needPoint = 9999;
        switch(type)
        {
            case PlaneType.Red:
                needPoint = 50;
                break;
            case PlaneType.Yellow:
                needPoint = 100;
                break;
            case PlaneType.Green:
                needPoint = 200;
                break;
        }
        if (needPoint <= gameManager.Point)
        {
            gameManager.AddPoint(-needPoint);
            gameManager.PurchasePlane(type);
            SetButtonActive();
        }
        else
            Debug.Log("포인트 부족!");
    }

    public void OnClickEquipButton(PlaneType type)
    {
        gameManager.SelectPlane(type);
        SetButtonActive();
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
