using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Numerics;
using TMPro;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds;
    [SerializeField] public double marginOfError; // in seconds
    [SerializeField] PlayerMovement player; 

    public int inputDelayInMilliseconds;


    public string fileLocation;
    public float noteTime;
    public float noteSpawnX;
    public float noteTapX;
    public bool startPlaying;

    public float totalNotes;

    public GameObject resultsScreen;
    public TextMeshProUGUI oksText, goodsText, perfectsText, missesText, rankText, finalScoreText, percantageText;
    public float noteDespawnX
    {
        get
        {
            return noteTapX - (noteSpawnX - noteTapX);
        }
    }

    public static MidiFile midiFile;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }
    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        totalNotes = notes.Count;
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes) lane.SetTimeStamps(array);

        Invoke(nameof(StartSong), songDelayInSeconds);
    }
    public void StartSong()
    {
        audioSource.Play();
        player.goLeft();
    }
    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    void Update()
    {
        if(!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                ReadFromFile();
            }
        }
        else
        {
            if(!audioSource.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);

                oksText.text = ScoreManager.okHits.ToString();
                goodsText.text = ScoreManager.goodHits.ToString();
                perfectsText.text = ScoreManager.perfectHits.ToString();
                missesText.text = ScoreManager.missedHits.ToString();
                finalScoreText.text = ScoreManager.currentScore.ToString();
                float percantage = (ScoreManager.totalHits / totalNotes) * 100f;

                string rankVakue = "F";

                if (percantage > 40f)
                {
                    rankVakue = "D";
                    if (percantage > 55f)
                    {
                        rankVakue = "C";
                        if (percantage > 70f)
                        {
                            rankVakue = "B";
                            if (percantage > 85f)
                            {
                                rankVakue = "A";
                                if (percantage > 95f)
                                {
                                    rankVakue = "S";
                                }
                            }
                        }
                    }
                }
                percantageText.text = percantage.ToString("F1") + "%";
                rankText.text = rankVakue;
            }
        }
    }
}