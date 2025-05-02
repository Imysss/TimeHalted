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
                    "��... �׷�. �ʱ���....",
                    "��ħ��... �̰��� �� �ϳ��� '�����̴� �ð�'�� ã�ƿԱ���."
                },
                new string[] 
                {
                    "�׷�, �ٽ� ���ƿԱ�.",
                    "�ð��� ������ �� ������ �ֳ�?"
                },
                new string[] 
                {
                    "���� ���� �� �� ����... �ڳ� �ձ濡 ����� �ֱ⸦."
                }
            }
        }
    };

    public string[] GetDialogue(string npcId, int dialogueIndex)
    {
        if (DialogueMap.TryGetValue(npcId, out var dialogueStages))
        {
            //��ȭ �ε����� ���� �ʰ��Ǹ� ������ ��� �ݺ�
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
