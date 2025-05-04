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
                    "호오, 자네... 뭔가 특별한 기운이 느껴지는구먼.",
                    "이 땅엔 숨겨진 비밀이 잔뜩 있지.",
                    "맵 곳곳을 잘 살펴보게나, 숨겨진 퍼즐과 미니 게임들이 자네를 기다리고 있을 걸세."
                },
                new string[] 
                {
                    "미니 게임을 잘 마치면 포인트를 얻게 될 걸세.",
                    "그 포인트로 말이지... 흐흐, 내 옆에 있는 저 오우거 친구에게 가면 특별한 비행기를 살 수 있지!",
                    "미니 게임에 딱 맞는 물건이라네."
                },
                new string[]
                {
                    "왼쪽에 있는 엘프 녀석에게 가면 캐릭터를 커스터마이징도 할 수 있다네.",
                    "이걸로 게임의 재미를 더해 줄 수 있겠지...."
                },
                new string[] 
                {
                    "자, 망설일 시간이 없지 않나?",
                    "비밀을 풀고 게임을 진행하시게나."
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
