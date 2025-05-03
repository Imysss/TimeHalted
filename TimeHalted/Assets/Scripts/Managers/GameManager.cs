using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode
{
    Main,
    FlappyBird,
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private DialogueManager dialogue;
    public DialogueManager DialogueManager { get { return dialogue; } }

    private PlaneShopManager planeShopManager;
    public PlaneShopManager PlaneShopManager { get { return planeShopManager; } }

    private UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }

    //게임 모드
    [SerializeField] private GameMode gameMode;
    [SerializeField] public GameMode GameMode { get { return gameMode; } }

    //Flappy Bird Score 저장
    [SerializeField] private int flappyScore = 0;

    [SerializeField] private int flappyBestScore = 0;
    [SerializeField] public int FlappyBestScore { get => flappyBestScore; }

    [SerializeField] private const string BestScoreKey = "FlappyBestScore";

    //구매, 선택한 비행기 프리팹
    [SerializeField] GameObject[] purchasedPlanePrefab;
    [SerializeField] GameObject selectedPlanePrefab;

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        dialogue = transform.Find("DialogueManager").GetComponent<DialogueManager>();
        planeShopManager = transform.Find("PlaneShopManager").GetComponent<PlaneShopManager>();
        uiManager = transform.Find("UIManager").GetComponent<UIManager>();

        ChangeGameMode(GameMode.Main);
    }

    public void GameStop()
    {
        Time.timeScale = 0.0f;
    }

    public void GameStart()
    {
        Time.timeScale = 1.0f;
    }

    public void FlappyGameStart()
    {
        StartCoroutine(LoadScene("FlappyBirdScene"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        yield return null;
        GameStop();
        if (sceneName == "FlappyBirdScene")
            ChangeGameMode(GameMode.FlappyBird);
        else if (sceneName == "MainScene")
            ChangeGameMode(GameMode.Main);
    }

    public void InitFlappyGame()
    {
        //최고 점수 가져오기
        flappyBestScore = PlayerPrefs.GetInt(BestScoreKey, 0);

        //선택한 비행기 가져오기
        if (selectedPlanePrefab != null)
        {
            GameObject plane = GameObject.Find("Plane");
            GameObject instance = Instantiate(selectedPlanePrefab, plane.transform);
            instance.transform.localPosition = Vector3.zero;
            plane.GetComponent<PlaneController>().Init();
        }
    }

    public void FlappyGameOver()
    {
        UpdateFlappyScore();
        uiManager.ChangeState(UIState.FlappyGame);
    }

    public void AddFlappyScore(int score)
    {
        flappyScore += score;
        uiManager.ChangeFlappyScore(flappyScore);
    }

    public void UpdateFlappyScore()
    {
        if (flappyBestScore < flappyScore)
        {
            flappyBestScore = flappyScore;

            PlayerPrefs.SetInt(BestScoreKey, flappyBestScore);
        }

        uiManager.UpdateScoreUI(flappyScore, flappyBestScore);
    }

    public void ChangeGameMode(GameMode mode)
    {
        gameMode = mode;
        uiManager.Init();
    }


}
