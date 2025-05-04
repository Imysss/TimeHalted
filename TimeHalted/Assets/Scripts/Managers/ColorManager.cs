using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    White,
    Red,
    Yellow,
    Green,
    Blue,
}
public class ColorManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        uiManager = GameManager.Instance.UIManager;
    }

    public void ShowColorUI(NpcController npc)
    {
        uiManager.SetNpcColor(npc);
        uiManager.ChangeState(UIState.Color);
    }

    public Color GetColor(ColorType type)
    {
        Color color = new Color();
        switch(type)
        {
            case ColorType.White:
                color = Color.white;
                break;
            case ColorType.Red:
                color = Color.red;
                break;
            case ColorType.Yellow:
                color = Color.yellow;
                break;
            case ColorType.Green:
                color = Color.green;
                break;
            case ColorType.Blue:
                color = Color.blue;
                break;
        }

        return color;
    }
}
