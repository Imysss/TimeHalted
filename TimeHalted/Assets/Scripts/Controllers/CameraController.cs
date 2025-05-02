using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private float offsetX;
    private float offsetY;

    private bool isFlappyGame = false;

    private void Start()
    {
        if (target == null)
            return;

        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;

        if (GameObject.Find("FlappyGameManager") == null)
        {
            isFlappyGame = false;
        }
        else
        {
            isFlappyGame = true;
        }
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        //FlappyGame을 시작했을 경우에는 y축은 움직이지 않도록 설정
        if (!isFlappyGame)
        {
            pos.y = target.position.y + offsetY;
        }
        transform.position = pos;
    }
}
