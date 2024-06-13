using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMPro.TextMeshPro scoreText;
    public TMPro.TextMeshPro multiplierText;
    public static int currentScore { get; set; }
    static int scorePerNote = 100;
    static int scorePerGoodNote = 120;
    static int scorePerPerfectNote = 150;

    static int currentMultiplier;
    static int multiplierTracker;
    static int[] multiplierThresholds = { 4, 8, 16 };

    public static int okHits { get; set; } = 0;
    public static int goodHits { get; set; } = 0;
    public static int perfectHits { get; set; } = 0;
    public static int missedHits { get; set; } = 0;
    public static int totalHits { get; set; } = 0;

    void Start()
    {
        Instance = this;
        currentScore = 0;
        currentMultiplier = 1;
        multiplierTracker = 0;
    }
    public static void Hit()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        { 
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        Instance.hitSFX.Play();
    }

    public static void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        Hit();
        okHits++;
    }
    public static void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        Hit();
        goodHits++;
    }
    public static void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        Hit();
        perfectHits++;
    }

    public static void Miss()
    {
        missedHits++;
        currentMultiplier = 1;
        multiplierTracker = 0;
        Instance.missSFX.Play();
    }
    private void Update()
    {
        scoreText.text = currentScore.ToString();
        multiplierText.text = "x" + currentMultiplier.ToString();
        totalHits = okHits + goodHits + perfectHits;
    }
}