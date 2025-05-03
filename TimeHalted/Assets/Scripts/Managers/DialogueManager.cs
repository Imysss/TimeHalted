using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private string[] dialogueLines; //대사 내용
    [SerializeField] private int currentLine = 0;    //현재 대사 인덱스

    [SerializeField] public DialogueDatabase dialogueDatabase;

    private UIManager uiManager;

    private void Start()
    {
        dialogueDatabase = GetComponent<DialogueDatabase>();
        uiManager = GameManager.Instance.UIManager;
    }

    public void ShowDialogueUI(NpcController npc)
    {
        uiManager.ChangeState(UIState.Dialogue);
        uiManager.SetNpcDialogue(npc);
        StartDialogue(dialogueDatabase.GetDialogue(npc.NpcId, npc.DialogueIndex));
        npc.AdvanceDialogue();
    }

    public void StartDialogue(string[] lines)
    {
        dialogueLines = lines;
        currentLine = 0;

        uiManager.SetDialogueNextButtonListener(NextLine);
        uiManager.ClearDialogueText();
        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        if (currentLine < dialogueLines.Length) 
        {
            uiManager.ShowDialogueLine(dialogueLines[currentLine]);
        }
    }

    private void NextLine()
    {
        currentLine++;
        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();
        }
    }

    private void EndDialogue()
    {
        uiManager.ClearDialogueText();
        uiManager.ChangeState(UIState.None);
    }
}
