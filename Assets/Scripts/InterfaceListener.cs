using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceListener : RhythmListener
{

    public GameObject[] logo;

    Vector3[] logoScale;

    private void Start()
    {
        logoScale = new Vector3[logo.Length];
        for (int i = 0; i < logo.Length; i++)
        {
            logoScale[i] = logo[i].transform.localScale;
        }
    }

    private void Update()
    {
        for (int i = 0; i < logo.Length; i++)
        {
            Vector3 logoScl = logo[i].transform.localScale;
            logoScl += (logoScale[i] - logoScl) / 10f * Time.deltaTime * 60f;
            logo[i].transform.localScale = logoScl;
        }
    }

    public override void BPMEvent(RhythmEventData data)
    {
        for (int i = 0; i < logo.Length; i++)
        {
            logo[i].transform.localScale = logoScale[i] * 1.1f;
        }
    }

    public override void RhythmEvent(RhythmEventData data)
    {
        
    }
}
