using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameDuckController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    private int score;
    public GameObject duckPrefab;
    private List<GameObject> ducks = new List<GameObject>();
    private Vector3[] positions = new Vector3[6];

    public float gameTime = 30f;
    private float timeRemaining;
    private bool gameActive = true;

    void Start()
    {
        score = 0;
        timeRemaining = gameTime;
        UpdateScore();
        UpdateTimerText();
        InitializeDucks();
        InvokeRepeating("ActivateRandomDuck", 2f, 2f);
    }

    void Update()
    {
        if (gameActive)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            if (timeRemaining <= 0)
            {
                EndGame();
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        if (gameActive)
        {
            score += newScoreValue;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        scoreText.text = "" + score;
    }

    void UpdateTimerText()
    {
        int timeToShow = Mathf.FloorToInt(Mathf.Max(timeRemaining, 0));
        timerText.text = "Time: " + timeToShow.ToString();
    }

    void InitializeDucks()
    {
        // позиции уток
        positions[0] = new Vector3(-4f, -3f, 0);
        positions[1] = new Vector3(4f, -3f, 0);
        positions[2] = new Vector3(0f, -3f, 0);
        positions[3] = new Vector3(0f, 0.7f, 0);
        positions[4] = new Vector3(4f, 0.7f, 0);
        positions[5] = new Vector3(-4f, 0.7f, 0);

        for (int i = 0; i < 6; i++)
        {
            GameObject duck = Instantiate(duckPrefab, positions[i], Quaternion.identity);
            duck.SetActive(false);
            DuckController duckController = duck.GetComponent<DuckController>();
            if (duckController != null)
            {
                duckController.Initialize(this);
            }
            ducks.Add(duck);
        }
    }

    void ActivateRandomDuck()
    {
        if (gameActive)
        {
            int randomIndex = UnityEngine.Random.Range(0, ducks.Count);
            ducks[randomIndex].SetActive(true);
        }
    }

    void EndGame()
    {
        gameActive = false;
        timerText.text = "Time: 0";
        CancelInvoke("ActivateRandomDuck");
        Debug.Log("Game Over! Final Score: " + score);
        // Здесь можно добавить логику для окончания игры, например, показать экран результатов
    }
}
