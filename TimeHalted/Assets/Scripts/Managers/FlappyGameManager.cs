using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyGameManager : MonoBehaviour
{
    private static FlappyGameManager instance;
    public static FlappyGameManager Instance { get { return instance; } }

    [SerializeField] private int currentScore = 0;

    [SerializeField] private int bestScore = 0;
    [SerializeField] public int BestScore { get => bestScore; }

    [SerializeField] private const string BestScoreKey = "FlappyBestScore";

    private void Awake()
    {
        instance = this;
        Time.timeScale = 0.0f;
    }

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        UpdateScore();
        UIManager.Instance.ChangeState(UIState.Score);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        UIManager.Instance.ChangeScore(currentScore);
    }

    public void UpdateScore()
    {
        if (bestScore < currentScore)
        {
            bestScore = currentScore;

            PlayerPrefs.SetInt(BestScoreKey, bestScore);
        }

        UIManager.Instance.UpdateScore(currentScore, bestScore);
    }
}
