using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private CustomizationManager customizationManager;
    public CustomizationManager CustomizationManager { get { return customizationManager; } }

    [SerializeField] private UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }

    //���� ���
    [SerializeField] private GameMode gameMode;
    [SerializeField] public GameMode GameMode { get { return gameMode; } }

    //Flappy Bird Score ����
    [SerializeField] private int flappyScore = 0;

    [SerializeField] private int flappyBestScore = 0;
    [SerializeField] public int FlappyBestScore { get => flappyBestScore; }

    [SerializeField] private const string BestScoreKey = "FlappyBestScore";

    //����, ������ ����� ������
    [SerializeField] HashSet<PlaneType> purchasedPlanes = new HashSet<PlaneType>();
    [SerializeField] PlaneType selectedPlane;
    [SerializeField] public PlaneType SelectedPlane { get { return selectedPlane; } }

    //����, ������ ĳ���� ������
    [SerializeField] HashSet<CharacterCustomType> purchasedCustom = new HashSet<CharacterCustomType>();
    [SerializeField] CharacterCustomType selectedCharacter;
    [SerializeField] public CharacterCustomType SelectedCharacter { get { return selectedCharacter; } }

    //Point ����
    [SerializeField] private int point = 0;
    [SerializeField] public int Point { get { return point; } }

    private bool isFirst;

    public GameObject playerinstance;

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

        dialogue = transform.Find("DialogueManager").GetComponent<DialogueManager>();
        planeShopManager = transform.Find("PlaneShopManager").GetComponent<PlaneShopManager>();
        customizationManager = transform.Find("CustomizationManager").GetComponent<CustomizationManager>();
        uiManager = transform.Find("UIManager").GetComponent<UIManager>();

        isFirst = true;
    }

    private void Start()
    {
        LoadMainGame();

        PurchasePlane(PlaneType.Blue);
        SelectPlane(PlaneType.Blue);
    }

    void OnSceneLoaded(Scene scene)
    {
        foreach (GameObject obj in scene.GetRootGameObjects())
        {
            obj.SetActive(true); // �ֻ��� ������Ʈ��. �ڽ� ������Ʈ�� ���� Ȯ�� �ʿ�
        }
    }

    public void GameStop()
    {
        Time.timeScale = 0.0f;
    }

    public void GameStart()
    {
        Time.timeScale = 1.0f;
    }

    public void ChangeGameMode(GameMode mode)
    {
        gameMode = mode;
    }

    IEnumerator LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        yield return null;
        OnSceneLoaded(SceneManager.GetActiveScene());
        if (sceneName == "FlappyBirdScene")
        {
            GameStop();
            ChangeGameMode(GameMode.FlappyBird);
            InitFlappyGame();
            uiManager.Init();
        }
        else if (sceneName == "MainScene")
        {
            ChangeGameMode(GameMode.Main);
            uiManager.Init();

            if (isFirst)
            {
                PurchaseCustom(CharacterCustomType.Pumkin);
                SelectCustom(CharacterCustomType.Pumkin);
                isFirst = false;
            }
            else
            {
                SelectCustom(selectedCharacter);
            }
           
        }

        CameraController camera = GameObject.Find("Main Camera").GetComponent<CameraController>();
        camera.Init();
    }

    #region Shop
    public bool IsPurchased(PlaneType type)
    {
        return purchasedPlanes.Contains(type);
    }

    public bool IsSelected(PlaneType type)
    {
        return selectedPlane == type;
    }

    public void PurchasePlane(PlaneType type)
    {
        if (!purchasedPlanes.Contains(type))
        {
            uiManager.UpdateMainGameUI();
            purchasedPlanes.Add(type);
        }
    }

    public void SelectPlane(PlaneType type)
    {
        if(IsPurchased(type))
        {
            selectedPlane = type;
        }
    }
    #endregion

    #region Custom
    public bool IsPurchased(CharacterCustomType type)
    {
        return purchasedCustom.Contains(type);
    }

    public bool IsSelected(CharacterCustomType type)
    {
        return selectedCharacter == type;
    }

    public void PurchaseCustom(CharacterCustomType type)
    {
        if (!purchasedCustom.Contains(type))
        {
            purchasedCustom.Add(type);
            uiManager.UpdateMainGameUI();
        }
    }

    public void SelectCustom(CharacterCustomType type)
    {
        if(IsPurchased(type))
        {
            selectedCharacter = type;
            ChangeCustom();
        }
    }

    public void ChangeCustom()
    {
        GameObject playerPrefab = customizationManager.GetCharacterCustom(selectedCharacter);
        if (playerPrefab != null)
        {
            GameObject player = GameObject.Find("Player");
            if (player.GetComponentInChildren<Animator>())
            {
                Destroy(player.GetComponentInChildren<Animator>().gameObject);
            }
            playerinstance = Instantiate(playerPrefab, player.transform);
            playerinstance.transform.localPosition = Vector3.zero;


            StartCoroutine(SetMainSprite(player.GetComponent<PlayerController>()));
        }
    }

    IEnumerator SetMainSprite(PlayerController player)
    {
        yield return null;
        player.SetMainSprite();
    }
    #endregion

    #region Flappy Game
    public void FlappyGameStart()
    {
        StartCoroutine(LoadScene("FlappyBirdScene"));
    }

    public void LoadMainGame()
    {
        StartCoroutine(LoadScene("MainScene"));
    }

    public void InitFlappyGame()
    {
        //�ְ� ���� ��������
        flappyBestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        flappyScore = 0;

        GameObject planePrefab = planeShopManager.GetPlanePrefab(selectedPlane);
        //������ plane ��������
        if (planePrefab != null)
        {
            GameObject plane = GameObject.Find("Plane");
            GameObject instance = Instantiate(planePrefab, plane.transform);
            instance.transform.localPosition = Vector3.zero;
            plane.GetComponent<PlaneController>().Init();
        }
    }

    public void FlappyGameOver()
    {
        UpdateFlappyScore();
        uiManager.ChangeState(UIState.FlappyScore);
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

    public void AddPoint(int point)
    {
        this.point += point;
    }
    #endregion

}
