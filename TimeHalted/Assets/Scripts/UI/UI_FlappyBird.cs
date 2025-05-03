using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum FlappyState
{
    Home,
    Game,
    Score,
}

public class UI_FlappyBird : MonoBehaviour
{
    private FlappyState currentState = FlappyState.Home;

    private UI_FlappyHome flappyHomeUI;
    private UI_FlappyGame flappyGameUI;
    private UI_FlappyScore flappyScoreUI;

    private void Awake()
    {
        flappyHomeUI = GetComponentInChildren<UI_FlappyHome>(true);
        flappyHomeUI.Init(this);

        flappyGameUI = GetComponentInChildren<UI_FlappyGame>(true);
        flappyGameUI.Init(this);

        flappyScoreUI = GetComponentInChildren<UI_FlappyScore>(true);
        flappyScoreUI.Init(this);

        ChangeState(FlappyState.Home);
    }

    public void ChangeState(FlappyState state)
    {
        currentState = state;

        flappyHomeUI?.SetActive(currentState);
        flappyGameUI?.SetActive(currentState);
        flappyScoreUI?.SetActive(currentState);
    }

    public void UpdateScore(int score, int bestScore)
    {
        flappyScoreUI.SetUI(score, bestScore);
    }

    public void StartGame()
    {
        ChangeState(FlappyState.Game);
        FlappyGameManager.Instance.StartGame();
    }

    public void ChangeScore(int score)
    {
        flappyGameUI.UpdateScoreText(score);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
