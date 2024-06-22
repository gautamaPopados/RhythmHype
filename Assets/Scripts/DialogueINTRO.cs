using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueINTRO : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public bool isFinal;
    private int index;
    public LevelLoader levelLoader;
    // Start is called before the first frame update



    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);

        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine (TypeLine());
        }
        else
        {
            if (isFinal)
                levelLoader.LoadNextLevel();
            gameObject.SetActive(false);
        }
    }
}
