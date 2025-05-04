using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCType
{
    Shop,
    Talk,
}
public class NpcController : MonoBehaviour
{
    [SerializeField] private string npcName = "[루멘트]";     //npc 이름
    public string NpcName {  get { return npcName; } }
    [SerializeField] private string npcId = "npc_lument";   //npc id
    public string NpcId { get { return npcId; } }
    [SerializeField] private Sprite npcsprite;              //npc sprite
    public Sprite NpcSprite { get { return npcsprite; } }
    [SerializeField] private int dialogueIndex = 0;         //대화 index
    public int DialogueIndex { get { return dialogueIndex; } }

    [SerializeField] private NPCType npcType;
    public NPCType NPCType { get { return npcType; } }

    public void AdvanceDialogue()
    {
        int totalStages=GameManager.Instance.DialogueManager.dialogueDatabase.GetDialogueCount(npcId);
        if (dialogueIndex < totalStages - 1)
            dialogueIndex++;
    }
}
