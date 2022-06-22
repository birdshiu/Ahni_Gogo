using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private GameManager gameManager;
    private float speed = 20.0f;
    private float xBoundary = -20f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsGameOver())
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < xBoundary && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
