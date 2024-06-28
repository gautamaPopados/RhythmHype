using UnityEngine;
using System.Collections;
using System;

public class DuckController : MonoBehaviour
{
    private GameDuckController gameController;
    public GameObject thisDuck;
    private float moveSpeed = 3.5f;
    public bool isMovingUp = true;
    private Vector2 positionOnStart;


    public void Initialize(GameDuckController controller)
    {
        gameController = controller;
        positionOnStart = thisDuck.transform.position;
    }

    void Update()
    {
        if (isMovingUp)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            if (transform.position.y - positionOnStart.y > 2f)
            {
                isMovingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            if (transform.position.y < positionOnStart.y)
            {
                thisDuck.SetActive(false);
                isMovingUp = true;
            }
        }
    }

    void HideDuck()
    {
        transform.position = positionOnStart;
        thisDuck.SetActive(false);
    }

    void OnMouseDown()
    {
        gameController.AddScore(10);
        thisDuck.SetActive(false);
        isMovingUp = true;
        HideDuck();
    }
}
