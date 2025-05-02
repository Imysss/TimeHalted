using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    private readonly Dictionary<string, List<string[]>> DialogueMap = new Dictionary<string, List<string[]>>
    {
        {
            "npc_lument", new List<string[]>
            {
                new string[] 
                {
                    "오... 그래. 너구나....",
                    "마침내... 이곳에 또 하나의 '움직이는 시간'이 찾아왔구먼."
                },
                new string[] 
                {
                    "그래, 다시 돌아왔군.",
                    "시간의 조각은 잘 모으고 있나?"
                },
                new string[] 
                {
                    "이제 말할 건 다 했지... 자네 앞길에 행운이 있기를."
                }
            }
        }
    };

    public string[] GetDialogue(string npcId, int dialogueIndex)
    {
        if (DialogueMap.TryGetValue(npcId, out var dialogueStages))
        {
            //대화 인덱스가 범위 초과되면 마지막 대사 반복
            dialogueIndex = Mathf.Clamp(dialogueIndex, 0, dialogueStages.Count - 1);
            return dialogueStages[dialogueIndex];
        }

        return null;
    }

    public int GetDialogueCount(string npcId)
    {
        if (DialogueMap.TryGetValue(npcId, out var dialogueStages))
            return dialogueStages.Count;
        return 0;
    }
}
