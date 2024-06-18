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
        logo[0].transform.localScale = logoScale[0] * 1.05f;
        logo[1].transform.localScale = logoScale[1] * 1.1f;
        logo[2].transform.localScale = logoScale[2] * 1.01f;
    }

    public override void RhythmEvent(RhythmEventData data)
    {
        throw new System.NotImplementedException();
    }
} 
