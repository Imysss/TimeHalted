using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private string[] dialogueLines; //대사 내용
    [SerializeField] private int currentLine = 0;    //현재 대사 인덱스

    [SerializeField] public UI_Dialogue dialogueUI;
    [SerializeField] public DialogueDatabase dialogueDatabase;

    private void Start()
    {
        dialogueDatabase = GetComponent<DialogueDatabase>();
        dialogueUI.gameObject.SetActive(false);
    }

    public void ShowDialogueUI(NpcController npc)
    {
        dialogueUI.gameObject.SetActive(true);
        dialogueUI.Init(npc);
        StartDialogue(dialogueDatabase.GetDialogue(npc.NpcId, npc.DialogueIndex));
        npc.AdvanceDialogue();
    }

    public void StartDialogue(string[] lines)
    {
        dialogueLines = lines;
        currentLine = 0;

        dialogueUI.SetNextButtonListener(NextLine);
        dialogueUI.ClearText();
        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        if (currentLine < dialogueLines.Length) 
        {
            dialogueUI.ShowLine(dialogueLines[currentLine]);
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
        dialogueUI.ClearText();
        //dialogueUI.SetNextButtonActive(false);
        dialogueUI.gameObject.SetActive(false);
    }
}
