using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_FlappyGame : UI_Base
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    protected override UIState GetUIState()
    {
        return UIState.FlappyGame;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
