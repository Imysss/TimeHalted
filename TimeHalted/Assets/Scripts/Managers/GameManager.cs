using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private DialogueManager dialogue;
    public DialogueManager DialogueManager { get { return dialogue; } }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dialogue = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }
}
