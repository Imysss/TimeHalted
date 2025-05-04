using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Customization : UI_Base
{
    [SerializeField] private Image npcImage;
    [SerializeField] private TextMeshProUGUI npcNameText;

    [SerializeField] private Button pumkinPurchaseButton;
    [SerializeField] private Button dwarfPurchaseButton;
    [SerializeField] private Button skeletonPurchaseButton;
    [SerializeField] private Button lizardPurchaseButton;
    [SerializeField] private Button angelPurchaseButton;

    [SerializeField] private Button pumkinEquipButton;
    [SerializeField] private Button dwarfEquipButton;
    [SerializeField] private Button skeletonEquipButton;
    [SerializeField] private Button lizardEquipButton;
    [SerializeField] private Button angelEquipButton;

    [SerializeField] private Button exitButton;

    private GameManager gameManager;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        gameManager = GameManager.Instance;

        npcImage = transform.Find("NPCImage").GetComponent<Image>();
        npcNameText = transform.Find("Panel/NPCName").GetComponent<TextMeshProUGUI>();

        pumkinPurchaseButton = transform.Find("Panel/Pumkin/PumkinPurchaseButton").GetComponent<Button>();
        dwarfPurchaseButton = transform.Find("Panel/Dwarf/DwarfPurchaseButton").GetComponent<Button>();
        skeletonPurchaseButton = transform.Find("Panel/Skeleton/SkeletonPurchaseButton").GetComponent<Button>();
        lizardPurchaseButton = transform.Find("Panel/Lizard/LizardPurchaseButton").GetComponent<Button>();
        angelPurchaseButton = transform.Find("Panel/Angel/AngelPurchaseButton").GetComponent<Button>();

        pumkinEquipButton = transform.Find("Panel/Pumkin/PumkinEquipButton").GetComponent<Button>();
        dwarfEquipButton = transform.Find("Panel/Dwarf/DwarfEquipButton").GetComponent<Button>();
        skeletonEquipButton = transform.Find("Panel/Skeleton/SkeletonEquipButton").GetComponent<Button>();
        lizardEquipButton = transform.Find("Panel/Lizard/LizardEquipButton").GetComponent<Button>();
        angelEquipButton = transform.Find("Panel/Angel/AngelEquipButton").GetComponent<Button>();

        exitButton = transform.Find("Panel/ExitButton").GetComponent<Button>();

        //이벤트 연결
        pumkinPurchaseButton.onClick.AddListener(() => OnClickPurchaseButton(CharacterCustomType.Pumkin));
        dwarfPurchaseButton.onClick.AddListener(() => OnClickPurchaseButton(CharacterCustomType.Dwarf));
        skeletonPurchaseButton.onClick.AddListener(() => OnClickPurchaseButton(CharacterCustomType.Skeleton));
        lizardPurchaseButton.onClick.AddListener(() => OnClickPurchaseButton(CharacterCustomType.Lizard));
        angelPurchaseButton.onClick.AddListener(() => OnClickPurchaseButton(CharacterCustomType.Angel));

        pumkinEquipButton.onClick.AddListener(() => OnClickEquipButton(CharacterCustomType.Pumkin));
        dwarfEquipButton.onClick.AddListener(() => OnClickEquipButton(CharacterCustomType.Dwarf));
        skeletonEquipButton.onClick.AddListener(() => OnClickEquipButton(CharacterCustomType.Skeleton));
        lizardEquipButton.onClick.AddListener(() => OnClickEquipButton(CharacterCustomType.Lizard));
        angelEquipButton.onClick.AddListener(() => OnClickEquipButton(CharacterCustomType.Angel));

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
        if (gameManager.IsPurchased(CharacterCustomType.Pumkin))
            pumkinPurchaseButton.gameObject.SetActive(false);
        if (gameManager.IsPurchased(CharacterCustomType.Dwarf))
            dwarfPurchaseButton.gameObject.SetActive(false);
        if (gameManager.IsPurchased(CharacterCustomType.Skeleton))
            skeletonPurchaseButton.gameObject.SetActive(false);
        if (gameManager.IsPurchased(CharacterCustomType.Lizard))
            lizardPurchaseButton.gameObject.SetActive(false);
        if (gameManager.IsPurchased(CharacterCustomType.Angel))
            angelPurchaseButton.gameObject.SetActive(false);

        //선택 버튼 활성화/비활성화
        if (gameManager.SelectedCharacter == CharacterCustomType.Pumkin)
            pumkinEquipButton.interactable = false;
        else
            pumkinEquipButton.interactable = true;

        if (gameManager.SelectedCharacter == CharacterCustomType.Dwarf)
            dwarfEquipButton.interactable = false;
        else
            dwarfEquipButton.interactable = true;

        if (gameManager.SelectedCharacter == CharacterCustomType.Skeleton)
            skeletonEquipButton.interactable = false;
        else
            skeletonEquipButton.interactable = true;

        if (gameManager.SelectedCharacter == CharacterCustomType.Lizard)
            lizardEquipButton.interactable = false;
        else
            lizardEquipButton.interactable = true;

        if (gameManager.SelectedCharacter == CharacterCustomType.Angel)
            angelEquipButton.interactable = false;
        else
            angelEquipButton.interactable = true;
    }
    
    public void OnClickPurchaseButton(CharacterCustomType type)
    {
        int needPoint = 9999;
        switch(type)
        {
            case CharacterCustomType.Pumkin:
                needPoint = 0;
                break;
            case CharacterCustomType.Dwarf:
                needPoint = 50;
                break;
            case CharacterCustomType.Skeleton:
                needPoint = 100;
                break;
            case CharacterCustomType.Lizard:
                needPoint = 150;
                break;
            case CharacterCustomType.Angel:
                needPoint = 200;
                break;
        }

        if (needPoint <= gameManager.Point)
        {
            gameManager.AddPoint(-needPoint);
            gameManager.PurchaseCustom(type);
            SetButtonActive();
        }
        else
        {
            Debug.Log("포인트 부족");
        }
    }

    public void OnClickEquipButton(CharacterCustomType type)
    {
        gameManager.SelectCustom(type);
        SetButtonActive();
    }

    public void OnClickExitButton()
    {
        gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    protected override UIState GetUIState()
    {
        return UIState.Customization;
    }


}
