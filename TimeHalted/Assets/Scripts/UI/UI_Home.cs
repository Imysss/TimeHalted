using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Home : BaseUI
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
        return UIState.Home;
    }

    public void OnClickStartButton()
    {
        uiManager.OnClickStart();
        FlappyGameManager.Instance.StartGame();
    }
}
