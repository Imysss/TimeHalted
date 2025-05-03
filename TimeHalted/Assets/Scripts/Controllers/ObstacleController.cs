using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float highPosY = 1f;
    [SerializeField] private float lowPosY = -1f;

    [SerializeField] private float holeSizeMin = 1f;
    [SerializeField] private float holeSizeMax = 3f;

    [SerializeField] private float widthPadding = 4f;

    public Transform topObject;
    public Transform bottomObject;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;
        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y=Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlaneController plane = collision.GetComponent<PlaneController>();
        if (plane != null && !plane.IsDead)
        {
            gameManager.AddFlappyScore(1);
            gameManager.AddPoint(10);
        }
    }
}
