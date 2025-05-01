using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Home,
    Game,
    Score,
}

public class UIManager : MonoBehaviour
{
    private static UIManager instance;   
    public static UIManager Instance {  get { return instance; } }

    private UIState currentState = UIState.Home;

    private UI_Home UIHome;
    private UI_Game UIGame;
    private UI_Score UIScore;

    private void Awake()
    {
        instance = this;

        UIHome = GetComponentInChildren<UI_Home>(true);
        UIHome.Init(this);

        UIGame = GetComponentInChildren<UI_Game>(true);
        UIGame.Init(this);

        UIScore = GetComponentInChildren<UI_Score>(true);
        UIScore.Init(this);

        ChangeState(UIState.Home);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;

        UIHome?.SetActive(currentState);
        UIGame?.SetActive(currentState);
        UIScore?.SetActive(currentState);
    }

    public void UpdateScore(int score, int bestScore)
    {
        UIScore.SetUI(score, bestScore);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.Game);
    }

    public void ChangeScore(int score)
    {
        UIGame.UpdateScoreText(score);
    }
}
