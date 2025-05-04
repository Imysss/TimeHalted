using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Color : UI_Base
{
    [SerializeField] private Image npcImage;
    [SerializeField] private TextMeshProUGUI npcNameText;

    [SerializeField] private Button whiteEquipButton;
    [SerializeField] private Button redEquipButton;
    [SerializeField] private Button yellowEquipButton;
    [SerializeField] private Button greenEquipButton;
    [SerializeField] private Button blueEquipButton;

    [SerializeField] private Button exitButton;

    GameManager gameManager;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        gameManager = GameManager.Instance;

        npcImage = transform.Find("NPCImage").GetComponent<Image>();
        npcNameText = transform.Find("Panel/NPCName").GetComponent<TextMeshProUGUI>();

        whiteEquipButton = transform.Find("Panel/White/WhiteEquipButton").GetComponent<Button>();
        redEquipButton = transform.Find("Panel/Red/RedEquipButton").GetComponent<Button>();
        yellowEquipButton = transform.Find("Panel/Yellow/YellowEquipButton").GetComponent<Button>();
        greenEquipButton = transform.Find("Panel/Green/GreenEquipButton").GetComponent<Button>();
        blueEquipButton = transform.Find("Panel/Blue/BlueEquipButton").GetComponent<Button>();

        exitButton = transform.Find("Panel/ExitButton").GetComponent<Button>();

        whiteEquipButton.onClick.AddListener(() => OnClickEquipButton(ColorType.White));
        redEquipButton.onClick.AddListener(() => OnClickEquipButton(ColorType.Red));
        yellowEquipButton.onClick.AddListener(() => OnClickEquipButton(ColorType.Yellow));
        greenEquipButton.onClick.AddListener(() => OnClickEquipButton(ColorType.Green));
        blueEquipButton.onClick.AddListener(() => OnClickEquipButton(ColorType.Blue));

        exitButton.onClick.AddListener(OnClickExitButton);

        SetButtonActive();
    }

    public void SetButtonActive()
    {
        //선택 버튼 활성화/비활성화
        if (gameManager.SelectedColor == ColorType.White)
            whiteEquipButton.interactable = false;
        else
            whiteEquipButton.interactable = true;

        if (gameManager.SelectedColor == ColorType.Red)
            redEquipButton.interactable = false;
        else
            redEquipButton.interactable = true;

        if (gameManager.SelectedColor == ColorType.Yellow)
            yellowEquipButton.interactable = false;
        else
            yellowEquipButton.interactable = true;

        if (gameManager.SelectedColor == ColorType.Green)
            greenEquipButton.interactable = false;
        else
            greenEquipButton.interactable = true;

        if (gameManager.SelectedColor == ColorType.Blue)
            blueEquipButton.interactable = false;
        else
            blueEquipButton.interactable = true;
    }

    public void SetNpc(NpcController npc)
    {
        npcImage.sprite = npc.NpcSprite;
        npcNameText.text = npc.NpcName;
    }

    public void OnClickEquipButton(ColorType type)
    {
        gameManager.SelectColor(type);
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
        return UIState.Color;
    }
}
