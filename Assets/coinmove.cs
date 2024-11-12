using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float moveSpeed = 1f;
    public float moveHeight = 0.5f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        this.transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f), Space.World);

        float newY = Mathf.Sin(Time.time * moveSpeed) * moveHeight;
        transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z);
    }
}
