using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyGameManager : MonoBehaviour
{
    private static FlappyGameManager instance;
    public static FlappyGameManager Instance { get { return instance; } }

    [SerializeField] private int currentScore = 0;

    [SerializeField] private int flappyBestScore = 0;
    [SerializeField] public int FlappyBestScore { get => flappyBestScore; }

    [SerializeField] private const string BestScoreKey = "FlappyBestScore";

    [SerializeField] GameObject selectedPlanePrefab;
    private void Awake()
    {
        instance = this;
        Time.timeScale = 0.0f;
    }

    private void Start()
    {
        flappyBestScore = PlayerPrefs.GetInt(BestScoreKey, 0);

        //선택한 비행기 가져오기
        selectedPlanePrefab = SelectedPlaneData.selectedPlanePrefab;
        if (selectedPlanePrefab != null)
        {
            GameObject plane = GameObject.Find("Plane");
            GameObject instance = Instantiate(selectedPlanePrefab, plane.transform);
            instance.transform.localPosition = Vector3.zero;
            plane.GetComponent<PlaneController>().Init();
        }
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
        if (flappyBestScore < currentScore)
        {
            flappyBestScore = currentScore;

            PlayerPrefs.SetInt(BestScoreKey, flappyBestScore);
        }

        UIManager.Instance.UpdateScore(currentScore, flappyBestScore);
    }
}
