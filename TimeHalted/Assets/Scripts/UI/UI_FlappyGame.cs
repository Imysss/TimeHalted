using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_FlappyGame : BaseUI
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public override void Init(UI_FlappyBird flappyBird)
    {
        base.Init(flappyBird);

        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    protected override FlappyState GetUIState()
    {
        return FlappyState.Game;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
