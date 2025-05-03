using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private float offsetX;
    private float offsetY;

    [SerializeField] private bool isFlappyGame = false;

    public void Init()
    {
        if (target == null)
            return;

        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;

        if (GameManager.Instance.GameMode == GameMode.FlappyBird)
        {
            isFlappyGame = true;
        }
        else
        {
            isFlappyGame = false;
        }
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        //FlappyGame�� �������� ��쿡�� y���� �������� �ʵ��� ����
        if (!isFlappyGame)
        {
            pos.y = target.position.y + offsetY;
        }
        transform.position = pos;
    }
}
