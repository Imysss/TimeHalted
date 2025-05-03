using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FlappyHome : UI_Base
{
    [SerializeField] private Button startButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        startButton = transform.Find("Panel/StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(OnClickStartButton);
    }

    protected override UIState GetUIState()
    {
        return UIState.FlappyHome;
    }

    public void OnClickStartButton()
    {
        uiManager.StartFlappyGame();
    }
}
