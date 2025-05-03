using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_MainGame : UI_Base
{
    private TextMeshProUGUI pointText;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        pointText = transform.Find("PointText").GetComponent<TextMeshProUGUI>();
    }

    protected override UIState GetUIState()
    {
        return UIState.MainGame;
    }

    public void SetPoint(int point)
    {
        pointText.text = point.ToString();
    }
}
