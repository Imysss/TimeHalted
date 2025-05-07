using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Score : UI_Base
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        currentScoreText = transform.Find("Panel/CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreText = transform.Find("Panel/BestScoreText").GetComponent<TextMeshProUGUI>();
        exitButton = transform.Find("Panel/ExitButton").GetComponent<Button>();

        exitButton.onClick.AddListener(OnClickExitButton);
    }

    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public void SetUI(int score, int bestScore)
    {
        currentScoreText.text = score.ToString();
        bestScoreText.text = bestScore.ToString();
    }

    public void OnClickExitButton()
    {
        gameObject.SetActive(false);
        //강제로 포커스 해제
        EventSystem.current.SetSelectedGameObject(null);
    }
}
