using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab, aim, okEffect, goodEffect, perfectEffect, missEffect;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();
    public Animator flash;

    int spawnIndex = 0;
    int inputIndex = 0;

    private IEnumerator delay(float delay, int inputIndex)
    {
        yield return new WaitForSeconds(delay);
        Destroy(notes[inputIndex].transform.GetChild(1).gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(input))
            {
                aim.GetComponent<Animator>().SetTrigger("pressed");

                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {

                    if (Math.Abs(audioTime - timeStamp) < 0.04)
                    {
                        ScoreManager.GoodHit();
                        GameObject effect = Instantiate(goodEffect, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z - 1), goodEffect.transform.rotation);
                        effect.GetComponent<FollowPlayer>().Player = transform;
                        effect.GetComponent<Animator>().SetTrigger("destruct");
                    }
                    else if (Math.Abs(audioTime - timeStamp) < 0.07)
                    {
                        ScoreManager.PerfectHit();
                        GameObject effect = Instantiate(perfectEffect, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z - 1), goodEffect.transform.rotation);
                        effect.GetComponent<FollowPlayer>().Player = transform;
                        effect.GetComponent<Animator>().SetTrigger("destruct");
                    }
                    else
                    {
                        ScoreManager.NormalHit();
                        GameObject effect = Instantiate(okEffect, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z - 1), goodEffect.transform.rotation);
                        effect.GetComponent<FollowPlayer>().Player = transform;
                        effect.GetComponent<Animator>().SetTrigger("destruct");
                    }
                    notes[inputIndex].transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("destruct");
                    print($"Hit on {inputIndex} note");
                    StartCoroutine(delay(0.3f, inputIndex));
                    inputIndex++;
                    flash.SetTrigger("flash");
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                GameObject effect = Instantiate(missEffect, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z - 1), goodEffect.transform.rotation);
                effect.GetComponent<FollowPlayer>().Player = transform;
                effect.GetComponent<Animator>().SetTrigger("destruct");
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
        }

    }

    private void Miss()
    {
        ScoreManager.Miss();
    }
}