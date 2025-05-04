using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterCustomType
{
    Pumkin,
    Dwarf,
    Skeleton,
    Lizard,
    Angel,
}

public class CustomizationManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;

    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        uiManager = GameManager.Instance.UIManager;
    }

    public void ShowCustomizationUI(NpcController npc)
    {
        uiManager.SetNpcCustomization(npc);
        uiManager.ChangeState(UIState.Customization);
    }

    public GameObject GetCharacterCustom(CharacterCustomType type)
    {
        return playerPrefabs[(int)type];
    }
}
