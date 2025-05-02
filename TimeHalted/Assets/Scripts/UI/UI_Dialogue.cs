using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dialogue : MonoBehaviour
{
    [SerializeField] private Image npcImage;
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button nextButton;
    [SerializeField] private float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;

    public void Init(NpcController npc)
    {
        npcImage = transform.Find("NPCImage").GetComponent<Image>();
        npcNameText = transform.Find("DialogueBox/NPCName").GetComponent<TextMeshProUGUI>();
        dialogueText = transform.Find("DialogueBox/DialogueText").GetComponent<TextMeshProUGUI>();
        nextButton = transform.Find("DialogueBox/NextButton").GetComponent<Button>();

        npcImage.sprite = npc.NpcSprite;
        npcNameText.text = npc.NpcName;
    }

    public void SetNextButtonActive(bool isActive)
    {
        nextButton.gameObject.SetActive(isActive);
    }

    public void SetNextButtonListener(UnityEngine.Events.UnityAction callback)
    {
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(callback);
    }

    public void ClearText()
    {
        dialogueText.text = "";
    }

    public void ShowLine(string line)
    {
        if (typingCoroutine != null) 
        { 
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    private IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        SetNextButtonActive(false);

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        SetNextButtonActive(true);
    }
}
