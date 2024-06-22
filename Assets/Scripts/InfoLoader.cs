using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoLoader : MonoBehaviour
{
    public TextMeshProUGUI[] rankTexts;
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.HasKey("level" + (i+1).ToString()))
            {
                rankTexts[i].text = PlayerPrefs.GetString("level" + (i + 1).ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
