using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooperController : MonoBehaviour
{
    private int obstacleCount = 0;
    private int backgroundCount = 5;

    private Vector3 obstacleLastPosition = Vector3.zero;

    private void Start()
    {
        ObstacleController[] obstacles = GameObject.FindObjectsOfType<ObstacleController>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Background와 부딪히면 해당 배경을 제일 뒷쪽으로 보내기
        if (collision.CompareTag("Background"))
        {
            float widthOfBackgroundObject = ((BoxCollider2D)collision).size.x;

            Vector3 pos = collision.transform.position;
            pos.x += widthOfBackgroundObject * backgroundCount;

            collision.transform.position = pos;
            return;
        }

        //Obstacle과 충돌하면 해당 Obstacle을 제일 뒷쪽으로 보내기
        ObstacleController obstacle = collision.GetComponent<ObstacleController>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}
