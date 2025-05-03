using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FlappyHome : BaseUI
{
    [SerializeField] private Button startButton;

    public override void Init(UI_FlappyBird uiFlappyBird)
    {
        base.Init(uiFlappyBird);

        startButton = transform.Find("Panel/StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(OnClickStartButton);
    }

    protected override FlappyState GetUIState()
    {
        return FlappyState.Home;
    }

    public void OnClickStartButton()
    {
        uiFlappyBird.StartGame();
    }
}
