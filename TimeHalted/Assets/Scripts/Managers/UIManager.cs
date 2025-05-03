using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIState
{
    None,
    FlappyHome,
    FlappyGame,
    FlappyScore,
    Dialogue,
    PlaneShop,
    PressSpace,
    MainGame,
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIState currentState = UIState.None;

    [SerializeField] private UI_FlappyHome flappyHomeUI;
    [SerializeField] private UI_FlappyGame flappyGameUI;
    [SerializeField] private UI_FlappyScore flappyScoreUI;

    [SerializeField] private UI_MainGame mainGameUI;
    [SerializeField] private UI_Dialogue dialogueUI;
    [SerializeField] private UI_PlaneShop planeShopUI;
    [SerializeField] private UI_PressSpace pressSpaceUI;

    GameMode gameMode;

    public void Init()
    {
        Debug.Log("init uimanger");
        gameMode = GameManager.Instance.GameMode;

        // 씬 전환마다 유효하지 않은 참조가 남아있지 않도록 초기화
        flappyHomeUI = null;
        flappyGameUI = null;
        flappyScoreUI = null;

        mainGameUI = null;
        dialogueUI = null;
        planeShopUI = null;
        pressSpaceUI = null;

        if (gameMode == GameMode.Main)
        {
            mainGameUI = GameObject.Find("UI_MainGame").GetComponent<UI_MainGame>();
            mainGameUI?.Init(this);
            UpdateMainGameUI();

            dialogueUI = GameObject.Find("UI_Dialogue").GetComponent<UI_Dialogue>();
            dialogueUI?.Init(this);

            planeShopUI = GameObject.Find("UI_PlaneShop").GetComponent<UI_PlaneShop>();
            planeShopUI?.Init(this);

            pressSpaceUI = GameObject.Find("UI_PressSpace").GetComponent<UI_PressSpace>();
            pressSpaceUI?.Init(this);

            ChangeState(UIState.None);
        }
        else if (gameMode == GameMode.FlappyBird)
        {
            flappyHomeUI = GameObject.Find("UI_FlappyHome").GetComponent<UI_FlappyHome>();
            flappyHomeUI?.Init(this);

            flappyGameUI = GameObject.Find("UI_FlappyGame").GetComponent<UI_FlappyGame>();
            flappyGameUI?.Init(this);

            flappyScoreUI = GameObject.Find("UI_FlappyScore").GetComponent<UI_FlappyScore>();
            flappyScoreUI?.Init(this);

            ChangeState(UIState.FlappyHome);
        }
    }

    public void ChangeState(UIState state)
    {
        currentState = state;

        if (currentState == UIState.PlaneShop)
            planeShopUI.SetButtonActive();

        flappyHomeUI?.SetActive(currentState);
        flappyGameUI?.SetActive(currentState);
        flappyScoreUI?.SetActive(currentState);

        dialogueUI?.SetActive(currentState);
        planeShopUI?.SetActive(currentState);
        pressSpaceUI?.SetActive(currentState);
    }

    public void UpdateScoreUI(int score, int bestScore)
    {
        flappyScoreUI.SetUI(score, bestScore);
    }

    #region Flappy UI
    public void StartFlappyGame()
    {
        ChangeState(UIState.FlappyGame);
        GameManager.Instance.GameStart();
    }

    public void ChangeFlappyScore(int score)
    {
        flappyGameUI.UpdateScoreText(score);
    }

    public void ChangeMainScene()
    {
        GameManager.Instance.LoadMainGame();
    }
    #endregion

    #region Dialogue UI
    public void SetNpcDialogue(NpcController npc)
    {
        dialogueUI.SetNpcDialogue(npc);
    }

    public void SetDialogueNextButtonListener(UnityEngine.Events.UnityAction callback)
    {
        dialogueUI.SetNextButtonListener(callback);
    }

    public void ClearDialogueText()
    {
        dialogueUI.ClearText();
    }

    public void ShowDialogueLine(string line)
    {
        dialogueUI.ShowLine(line);
    }
    #endregion

    #region MainGameUI
    public void UpdateMainGameUI()
    {
        mainGameUI?.SetPoint(GameManager.Instance.Point);
    }
    #endregion

    public void SetPressSpaceActiveFalse()
    {
        if (pressSpaceUI != null)
            pressSpaceUI?.gameObject.SetActive(false);
    }
}
