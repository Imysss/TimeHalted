using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : CreatureController
{
    private Camera camera;
    private float detectionRadius = 1.5f;
    [SerializeField] private LayerMask npcLayer;
    [SerializeField] public GameObject UI_PressSpace;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
        npcLayer = LayerMask.GetMask("NPC");

        UI_PressSpace.SetActive(false);
    }

    public void Interact()
    {
        Collider2D npc = Physics2D.OverlapCircle(transform.position, detectionRadius, npcLayer);
        if (npc != null)
        {
            NpcController npcController = npc.GetComponent<NpcController>();
            if (npcController.NPCType == NPCType.Talk)
            {
                GameManager.Instance.DialogueManager.ShowDialogueUI(npc.GetComponent<NpcController>());
            }
            else if (npcController.NPCType == NPCType.Shop)
            {
                npcController.OpenShop();
            }
            UI_PressSpace.SetActive(false);
        }
    }

    void OnMove(InputValue inputValue)
    {
        moveDirection = inputValue.Get<Vector2>();
        moveDirection = moveDirection.normalized;
    }

    void OnLook(InputValue inputValue)
    {
        Vector2 mousePosition = inputValue.Get<Vector2>();
        Vector2 worldPosition = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPosition - (Vector2)transform.position);

        if (lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    void OnInteract(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FlappyGame"))
        {
            SceneManager.LoadScene("FlappyBirdScene");
        }
        else if(collision.gameObject.CompareTag("StackGame"))
        {
            Debug.Log("Load Stack Game");
        }
        else if(collision.gameObject.CompareTag("ShootingGame"))
        {
            Debug.Log("Load Shooting Game");
        }
        else if(collision.gameObject.CompareTag("NPC"))
        {
            UI_PressSpace.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (UI_PressSpace != null)
            UI_PressSpace.SetActive(false);
    }
}
